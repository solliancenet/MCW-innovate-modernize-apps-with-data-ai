using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTOs;
using Common.Entities;
using Common.Enums;
using Common.Interfaces;
using Common.Utils;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace MetadataRead.Repositories
{
    public class MetadataReadRepository : IMetadataReadRepository
    {
        private readonly Container _container;

        public MetadataReadRepository()
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
        
        public async Task<List<MetadataBase>> GetAllMetadataAsync(int limit, int skip = 0)
        {
            using var feedIterator = _container.GetItemLinqQueryable<MetadataBase>()
                .Skip(skip)
                .Take(limit)
                .ToFeedIterator();

            return await GetAllItemsFromIterator(feedIterator);
        }

        public async Task<List<MetadataFactory>> GetFactoryMetadata(int limit, int skip = 0)
        {
            using var feedIterator = _container.GetItemLinqQueryable<MetadataFactory>()
                .Where(mf => mf.EntityType == MetadataEntityType.Factory)
                .Skip(skip)
                .Take(limit)
                .ToFeedIterator();
            
            return await GetAllItemsFromIterator(feedIterator);
        }

        public async Task<List<MetadataMachine>> GetMachineMetadata(int limit, int skip = 0)
        {
            using var feedIterator = _container.GetItemLinqQueryable<MetadataMachine>()
                .Where(mm => mm.EntityType == MetadataEntityType.Machine)
                .Skip(skip)
                .Take(limit)
                .ToFeedIterator();

            return await GetAllItemsFromIterator(feedIterator);
        }

        public async Task<List<MetadataMaintenanceLookup>> GetMaintenanceLookupMetadata(int limit, int skip = 0)
        {
            using var feedIterator = _container.GetItemLinqQueryable<MetadataMaintenanceLookup>()
                .Where(mm => mm.EntityType == MetadataEntityType.MaintenanceLookup)
                .Skip(skip)
                .Take(limit)
                .ToFeedIterator();

            return await GetAllItemsFromIterator(feedIterator);
        }

        public async Task<MetadataDto> GetAllMetadataDtosAsync(int limit, int skip = 0)
        {
            return new MetadataDto
            {
                MetadataFactories = await GetFactoryMetadata(limit / 3 + (limit % 3 > 0 ? 1 : 0), skip / 3 + (skip % 3 > 0 ? 1 : 0)),
                MetadataMachines = await GetMachineMetadata(limit/3 + (limit % 3 > 1 ? 1 : 0), skip / 3 + (skip % 3 > 1 ? 1 : 0)),
                MetadataMaintenanceLookups = await GetMaintenanceLookupMetadata(limit/3, skip/3),
            };
        }
        
        private async Task<List<T>> GetAllItemsFromIterator<T>(FeedIterator<T> feedIterator)
        {
            var metadata = new List<T>();

            while (feedIterator.HasMoreResults)
            {
                metadata.AddRange(await feedIterator.ReadNextAsync());
            }

            return metadata;
        }
    }
}