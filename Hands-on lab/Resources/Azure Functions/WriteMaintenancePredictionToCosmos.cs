using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;

namespace ModernApp
{
    
    internal class PredictionInput
    {
        [JsonProperty("Pressure")]
        internal double Pressure;
        [JsonProperty("MachineTemperature")]
        internal double MachineTemperature;
    }
    // The data structure expected by Azure ML
    internal class InputData
    {
        [JsonProperty("data")]
        internal List<PredictionInput> data;
    }

    public static class WriteMaintenancePredictionToCosmos
    {
        private static readonly string cosmosEndpointUrl = System.Environment.GetEnvironmentVariable("cosmosEndpointUrl");
        private static readonly string cosmosPrimaryKey = System.Environment.GetEnvironmentVariable("cosmosPrimaryKey");
        private static readonly string azureMLEndpointUrl = System.Environment.GetEnvironmentVariable("azureMLEndpointUrl");
        private static readonly string databaseId = "sensors";
        private static readonly string outputContainerId = "maintenancepredictions";
        private static CosmosClient cosmosClient = new CosmosClient(cosmosEndpointUrl, cosmosPrimaryKey);


        [FunctionName("WriteMaintenancePredictionToCosmos")]
        public static async Task Run([CosmosDBTrigger(
            databaseName: "sensors",
            collectionName: "pressure",
            ConnectionStringSetting = "modernizeapp_DOCUMENTDB",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input, ILogger log)
        {
            var maintenancepredictions = cosmosClient.GetContainer(databaseId, outputContainerId);

            foreach(Document doc in input) {
                InputData payload = new InputData();
                var machinetemp = doc.GetPropertyValue<double>("MachineTemperature");
                var pressure = doc.GetPropertyValue<double>("Pressure");

                payload.data = new List<PredictionInput> {
                    new PredictionInput { MachineTemperature = machinetemp, Pressure = pressure }
                };

                try
                {
                    // Call Azure ML.  Get back response.
                    HttpClient client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, new Uri(azureMLEndpointUrl));
                    request.Content = new StringContent(JsonConvert.SerializeObject(payload));
                    
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = client.SendAsync(request).Result;

                    // Azure ML returns an array of integer results, one per input item.
                    var predictions = JsonConvert.DeserializeObject<List<int>>(response.Content.ReadAsStringAsync().Result);
                    
                    // Add the recommendation to our document.
                    // We process one message at a time, so expect one result.
                    doc.SetPropertyValue("Maintenance", predictions.ElementAt(0));

                    // Write the updated document back to Cosmos DB into a new container.
                    await maintenancepredictions.CreateItemAsync<Document>(doc);
                }
                catch (Exception e) {
                    log.LogError("Exception pushing prediction into the maintenance predictions container: " + e);
                }
            }
        }
    }
}
