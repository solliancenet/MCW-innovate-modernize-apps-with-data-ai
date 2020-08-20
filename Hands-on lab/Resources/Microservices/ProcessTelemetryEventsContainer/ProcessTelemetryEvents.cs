using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;
using Npgsql;


namespace ProcessTelemetryEventsContainer
{
    public class PredictionInput
    {
        [JsonProperty("Pressure")]
        public double Pressure;
        [JsonProperty("MachineTemperature")]
        public double MachineTemperature;
    }
    // The data structure expected by Azure ML
    public class InputData
    {
        [JsonProperty("data")]
        public List<PredictionInput> data;
    }
    public class MessageBody
    {
        public int factoryId {get; set;}
        public Machine machine {get;set;}
        public Ambient ambient {get; set;}
        public string timeCreated {get; set;}
    }
    public class Machine
    {
        public int machineId {get; set;}
        public double temperature {get; set;}
        public double pressure {get; set;}
        public double electricityUtilization {get; set;}
    }
    public class Ambient
    {
        public double temperature {get; set;}
        public int humidity {get; set;}
    }

    public class TelemetryOutput
    {
        public int machineid { get; set; }
        public string id {get; set;}
        public string event_type {get; set;}
        public string entity_type {get; set;}
        public string entity_id {get; set;}
        public string event_data {get; set;}
    }

    public class MaintenancePredictionMessageBody
    {
        public int factoryId {get; set;}
        public Machine machine {get;set;}
        public Ambient ambient {get; set;}
        public string timeCreated {get; set;}
        public int maintenanceRecommendation {get; set;}
    }

    public static class ProcessTelemetryEvents
    {
        private static readonly string azureMLEndpointUrl = System.Environment.GetEnvironmentVariable("azureMLEndpointUrl");
        private static readonly string eventHubConnection = System.Environment.GetEnvironmentVariable("EventHubConnection");
        private static readonly string eventHubName = "telemetry_to_score";
        
        
        public static async Task Run(IReadOnlyCollection<Document> input, CancellationToken token)
        {
            foreach (Document doc in input) {
                // Deserialize the incoming document and event data
                TelemetryOutput to = LoadTelemetryRecord(doc);
                MessageBody msg = JsonConvert.DeserializeObject<MessageBody>(to.event_data);

                // Write telemetry event to Postgres
                WriteTelemetryEventToPostgres(to);

                // Perform Azure ML call and build a new output object
                InputData payload = new InputData();
                var machinetemp = msg.machine.temperature;
                var pressure = msg.machine.pressure;
                payload.data = new List<PredictionInput> {
                    new PredictionInput { MachineTemperature = machinetemp, Pressure = pressure }
                };
                int maintenanceRecommendation = GetMaintenancePrediction(payload);
                TelemetryOutput newTo = CreateNewTelemetryOutput(to, msg, maintenanceRecommendation);

                // Write maintenance recommendation events for downstream processing
                WriteTelemetryEventToPostgres(newTo);
                await WriteTelemetryEventToEventHub(newTo);
            }
        }

        public static TelemetryOutput LoadTelemetryRecord(Document doc)
        {
            TelemetryOutput to = new TelemetryOutput();
            to.entity_id = doc.GetPropertyValue<string>("entity_id");
            to.entity_type = doc.GetPropertyValue<string>("entity_type");
            to.event_data = doc.GetPropertyValue<string>("event_data");
            to.event_type = doc.GetPropertyValue<string>("event_type");
            to.machineid = doc.GetPropertyValue<int>("machineid");
            to.id = doc.GetPropertyValue<string>("id");

            return to;
        }

        public static int GetMaintenancePrediction(InputData payload)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(azureMLEndpointUrl));
            request.Content = new StringContent(JsonConvert.SerializeObject(payload));

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.SendAsync(request).Result;

            // Azure ML returns an array of integer results, one per input item.
            var predictions = JsonConvert.DeserializeObject<List<int>>(response.Content.ReadAsStringAsync().Result);

            return predictions.ElementAt(0);
        }

        public static TelemetryOutput CreateNewTelemetryOutput(TelemetryOutput to, MessageBody msg, int maintenanceRecommendation)
        {
            var newMsgBody = new MaintenancePredictionMessageBody
            {
                factoryId = msg.factoryId,
                machine = msg.machine,
                ambient = msg.ambient,
                timeCreated = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                maintenanceRecommendation = maintenanceRecommendation
            };
            string newMsg = JsonConvert.SerializeObject(newMsgBody);

            TelemetryOutput newTo = new TelemetryOutput
            {
                id = System.Guid.NewGuid().ToString(),
                machineid = to.machineid,
                event_type = "Maintenance Prediction",
                entity_type = "MachineTelemetry",
                entity_id = to.entity_id,
                event_data = newMsg
            };

            return newTo;
        }

        public static void WriteTelemetryEventToPostgres(TelemetryOutput to)
        {
            string insertCommand = @"INSERT INTO event(
                machine_id,
                event_id,
                event_type,
                entity_type,
                entity_id,
                event_data
            )
            VALUES
            (
                @machine_id,
                @event_id,
                @event_type,
                @entity_type,
                @entity_id,
                CAST(@event_data AS json)
            );";

            // Connect to Postgres
            var connStr = Environment.GetEnvironmentVariable("pg_connection");
            using (var conn = new NpgsqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand(insertCommand, conn))
                {
                    cmd.Parameters.AddWithValue("machine_id", to.machineid);
                    cmd.Parameters.AddWithValue("event_id", to.id);
                    cmd.Parameters.AddWithValue("event_type", to.event_type);
                    cmd.Parameters.AddWithValue("entity_type", to.entity_type);
                    cmd.Parameters.AddWithValue("entity_id", to.entity_id);
                    cmd.Parameters.AddWithValue("event_data", to.event_data);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static async Task WriteTelemetryEventToEventHub(TelemetryOutput to)
        {
            await using (var producerClient = new EventHubProducerClient(eventHubConnection, eventHubName))
            {
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();
                string msg = JsonConvert.SerializeObject(to);
                eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(msg)));

                await producerClient.SendAsync(eventBatch);
            }
        }
    }
}