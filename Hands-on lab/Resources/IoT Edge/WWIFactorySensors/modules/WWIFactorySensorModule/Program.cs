namespace WWIFactorySensorModule
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Loader;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.Devices.Client;
    using Microsoft.Azure.Devices.Client.Transport.Mqtt;

    using System.Collections.Generic;
    using Microsoft.Azure.Devices.Shared;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class Program
    {
        class MessageBody
        {
            public int factoryId {get; set;}
            public Machine machine {get;set;}
            public Ambient ambient {get; set;}
            public string timeCreated {get; set;}
        }
        class Machine
        {
            public int machineId {get; set;}
            public double temperature {get; set;}
            public double pressure {get; set;}
            public double electricityUtilization {get; set;}
        }
        class Ambient
        {
            public double temperature {get; set;}
            public int humidity {get; set;}
        }

        public int Interval { get; private set; }
        public string OutputChannelName { get; private set; }

        static async Task Main(string[] args)
        {
            var module = new Program();
            await module.Run();
        }

        public async Task Run()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            // Unloading assembly or Ctrl+C will trigger cancellation token
            AssemblyLoadContext.Default.Unloading += (ctx) => cancellationTokenSource.Cancel();
            Console.CancelKeyPress += (sender, cpe) => cancellationTokenSource.Cancel();

            try
            {
                // use environment variable or defaults
                Interval = 5000; // Generate a new message every 5 seconds.
                int interval;
                if (Int32.TryParse(Environment.GetEnvironmentVariable("Interval"), out interval))
                    Interval = interval;
                OutputChannelName = Environment.GetEnvironmentVariable("OutputChannelName") ?? "output1";

                MqttTransportSettings mqttSetting = new MqttTransportSettings(TransportType.Mqtt_Tcp_Only);
                ITransportSettings[] settings = { mqttSetting };

                // Open a connection to the Edge runtime
                using (var moduleClient = await ModuleClient.CreateFromEnvironmentAsync(settings))
                {
                    await moduleClient.OpenAsync(cancellationTokenSource.Token);
                    Console.WriteLine("IoT Hub module client initialized.");

                    await moduleClient.SetDesiredPropertyUpdateCallbackAsync(DesiredPropertyUpdateHandler, moduleClient, cancellationTokenSource.Token);

                    // Fire the DesiredPropertyUpdateHandler manually to read initial values
                    var twin = await moduleClient.GetTwinAsync();
                    await DesiredPropertyUpdateHandler(twin.Properties.Desired, moduleClient);

                    Random rand = new Random();
                    while (!cancellationTokenSource.IsCancellationRequested)
                    {
                        // Generate new data to send as a message
                        var msg = GenerateNewMessage(rand);
                        var messageString = JsonConvert.SerializeObject(msg);
                        var messageBytes = Encoding.UTF8.GetBytes(messageString);

                        await moduleClient.SendEventAsync(OutputChannelName, new Message(messageBytes));
                        await Task.Delay(Interval, cancellationTokenSource.Token);
                    }
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Asynchronous operation cancelled.");
            }

            Console.WriteLine("IoT Hub module client exiting.");
        }

        private MessageBody GenerateNewMessage(Random rand)
        {
            var machine = new Machine { machineId = 12345 };
            machine.temperature = GenerateDoubleValue(rand, 55.0, 2.4);
            machine.pressure = GenerateDoubleValue(rand, 7539, 14);
            machine.electricityUtilization = GenerateDoubleValue(rand, 29.36, 1.1);

            var ambient = new Ambient();
            ambient.temperature = GenerateDoubleValue(rand, 22.6, 1.1);
            ambient.humidity = Convert.ToInt32(GenerateDoubleValue(rand, 20.0, 3.5));

            return new MessageBody
            {
                factoryId = 1,
                machine = machine,
                ambient = ambient,
                timeCreated = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ")
            };
        }

        private double GenerateDoubleValue(Random rand, double mean, double stdDev, double likelihoodOfAnomaly = 0.06)
        {
            double u1 = rand.NextDouble();

            double res = BoxMullerTransformation(rand, mean, stdDev);

            if (u1 <= likelihoodOfAnomaly / 2.0)
            {
                // Generate a negative anomaly
                res = res * 0.6;
            }
            else if (u1 > likelihoodOfAnomaly / 2.0 && u1 <= likelihoodOfAnomaly)
            {
                // Generate a positive anomaly
                res = res * 1.8;
            }

            return res;
        }

        // Reference: https://stackoverflow.com/questions/218060/random-gaussian-variables
        private double BoxMullerTransformation(Random rand, double mean, double stdDev)
        {
            double u1 = 1.0 - rand.NextDouble();
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return mean + stdDev * randStdNormal;
        }

        private async Task DesiredPropertyUpdateHandler(TwinCollection desiredProperties, object userContext)
        {
            var moduleClient = userContext as ModuleClient;

            if (desiredProperties.Contains("OutputChannel"))
            {
                OutputChannelName = desiredProperties["OutputChannel"];
            }
            if (desiredProperties.Contains("Interval"))
            {
                Interval = desiredProperties["Interval"];
            }
            var reportedProperties = new TwinCollection(new JObject(), null);
            reportedProperties["OutputChannel"] = OutputChannelName;
            reportedProperties["Interval"] = Interval;
            await moduleClient.UpdateReportedPropertiesAsync(reportedProperties);

        }

        public async Task<byte[]> ReadAllBytesAsync(string filePath, CancellationToken cancellationToken)
        {
            const int bufferSize = 4096;
            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: bufferSize, useAsync: true))
            {
                byte[] data = new byte[sourceStream.Length];
                int numRead = 0;
                int totalRead = 0;
              
                while ((numRead = await sourceStream.ReadAsync(data, totalRead, Math.Min(bufferSize,(int)sourceStream.Length-totalRead), cancellationToken)) != 0)
                {
                   totalRead += numRead;
                }
                return data;
            }
        }
    }
}
