using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Npgsql;

namespace ModernizeApps
{
    public class SensorEvent
    {
        public DateTime event_time {get; set;}
        public int factory_id {get; set;}
        public int machine_id {get; set;}
        public float machine_temperature {get; set;}
        public float machine_pressure {get; set;}
        public float ambient_temperature {get; set;}
        public float ambient_humidity {get; set;}
        public float electricity_utilization {get; set;}
    }
    public static class WriteSensorDataToPostgres
    {
        [FunctionName("WriteSensorDataToPostgres")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic array = JsonConvert.DeserializeObject(requestBody);

            string insertCommand = @"INSERT INTO sensordata
            (
                event_time,
                factory_id,
                machine_id,
                machine_temperature,
                machine_pressure,
                ambient_temperature,
                ambient_humidity,
                electricity_utilization
            )
            VALUES
            (
                @event_time,
                @factory_id,
                @machine_id,
                @machine_temperature,
                @machine_pressure,
                @ambient_temperature,
                @ambient_humidity,
                @electricity_utilization
            );";

            // Connect to Postgres
            var connStr = Environment.GetEnvironmentVariable("pg_connection");
            using (var conn = new NpgsqlConnection(connStr))
            {
                conn.Open();

                foreach(var data in array)
                {
                    SensorEvent se = new SensorEvent();
                    se.event_time = data?.event_time;
                    se.factory_id = data?.factory_id;
                    se.machine_id = data?.machine_id;
                    se.machine_temperature = data?.machine_temperature;
                    se.machine_pressure = data?.machine_pressure;
                    se.ambient_temperature = data?.ambient_temperature;
                    se.ambient_humidity = data?.ambient_humidity;
                    se.electricity_utilization = data?.electricity_utilization;

                    using (var cmd = new NpgsqlCommand(insertCommand, conn))
                    {
                        cmd.Parameters.AddWithValue("event_time", se.event_time);
                        cmd.Parameters.AddWithValue("factory_id", se.factory_id);
                        cmd.Parameters.AddWithValue("machine_id", se.machine_id);
                        cmd.Parameters.AddWithValue("machine_temperature", se.machine_temperature);
                        cmd.Parameters.AddWithValue("machine_pressure", se.machine_pressure);
                        cmd.Parameters.AddWithValue("ambient_temperature", se.ambient_temperature);
                        cmd.Parameters.AddWithValue("ambient_humidity", se.ambient_humidity);
                        cmd.Parameters.AddWithValue("electricity_utilization", se.electricity_utilization);

                        cmd.ExecuteNonQuery();
                    }
                }
            }            

            string responseMessage = "This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
