using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent; // Use the new fluent namespace
using System.Threading.Tasks;
using System;
using System.Threading;
using Common.Utils;
using Microsoft.Azure.Documents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProcessTelemetryEventsContainer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureServices((context, collection) =>
                {
                    var accountEndpoint = EnvUtils.Get("COSMOS_DB_ACCOUNT_ENDPOINT");
                    var key = EnvUtils.Get("COSMOS_DB_AUTH_KEY");
                    CosmosClient cosmosClient = new CosmosClientBuilder(accountEndpoint, key)
                        .WithApplicationRegion(Regions.EastUS)
                        .WithApplicationName("Telemetry Events Processor Container")
                        .Build();

                    collection.AddSingleton(cosmosClient);
                    collection.AddHostedService<ChangeFeedProcessorHostedService>();
                });

            await hostBuilder.RunConsoleAsync();
        }
    }

    class ChangeFeedProcessorHostedService : IHostedService
    {
        private readonly CosmosClient _cosmosClient;

        public ChangeFeedProcessorHostedService(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
        }
        
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await StartChangeFeedProcessorAsync(_cosmosClient);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
        private static async Task<ChangeFeedProcessor> StartChangeFeedProcessorAsync(
            CosmosClient cosmosClient)
        {
            string databaseName = "sensors";
            string sourceContainerName = "telemetry";
            string leaseContainerName = "leases";

            Container leaseContainer = cosmosClient.GetContainer(databaseName, leaseContainerName);
            ChangeFeedProcessor changeFeedProcessor = cosmosClient.GetContainer(databaseName, sourceContainerName)
                .GetChangeFeedProcessorBuilder<Document>(processorName: "containerProcessor", ProcessTelemetryEvents.Run)
                .WithInstanceName("consoleHost")
                .WithLeaseContainer(leaseContainer)
                .Build();

            Console.WriteLine("Starting Change Feed Processor...");
            await changeFeedProcessor.StartAsync();
            Console.WriteLine("Change Feed Processor started.");
            return changeFeedProcessor;
        }
    }
}