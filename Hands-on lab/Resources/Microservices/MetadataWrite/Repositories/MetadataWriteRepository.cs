using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;
using Common.Interfaces;
using Common.Utils;
using Microsoft.Azure.Cosmos;

namespace MetadataWrite.Repositories
{
    public class MetadataWriteRepository : IMetadataWriteRepository
    {
        private readonly Container _container;

        public MetadataWriteRepository()
        {
            var accountEndpoint = EnvUtils.Get("COSMOS_DB_ACCOUNT_ENDPOINT");
            var authKey = EnvUtils.Get("COSMOS_DB_AUTH_KEY");
            var client = new CosmosClient(accountEndpoint, authKey, new CosmosClientOptions
            {
                SerializerOptions = new CosmosSerializationOptions
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase,
                }
            });
            _container = client.GetContainer("sensors", "metadata");
        }

        public async Task CreateFactoryAsync(MetadataFactory metadataFactory)
        {
            metadataFactory.Id ??= Guid.NewGuid();

            try
            {
                await _container.CreateItemAsync(metadataFactory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task CreateMachineAsync(MetadataMachine metadataMachine)
        {
            metadataMachine.Id ??= Guid.NewGuid();

            try
            {
                await _container.CreateItemAsync(metadataMachine);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task CreateMaintenanceLookupAsync(MetadataMaintenanceLookup metadataMaintenanceLookup)
        {
            metadataMaintenanceLookup.Id ??= Guid.NewGuid();

            try
            {
                await _container.CreateItemAsync(metadataMaintenanceLookup);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task CreateFactoriesAsync(ICollection<MetadataFactory> metadataFactories)
        {
            foreach (var metadataFactory in metadataFactories)
            {
                await CreateFactoryAsync(metadataFactory);
            }
        }

        public async Task CreateMachinesAsync(ICollection<MetadataMachine> metadataMachines)
        {
            foreach (var metadataMachine in metadataMachines)
            {
                await CreateMachineAsync(metadataMachine);
            }
        }

        public async Task CreateMaintenanceLookupsAsync(ICollection<MetadataMaintenanceLookup> metadataMaintenanceLookups)
        {
            foreach (var metadataMaintenanceLookup in metadataMaintenanceLookups)
            {
                await CreateMaintenanceLookupAsync(metadataMaintenanceLookup);
            }
        }
    }
}