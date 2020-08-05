using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.DTOs;
using Common.Entities;
using Common.Interfaces;

namespace MetadataRead.Repositories
{
    public class MockMetadataRepository : IMetadataReadRepository
    {
        private readonly List<MetadataFactory> _factoryMetadata = new List<MetadataFactory>
        {
            new MetadataFactory
            {
                Id = Guid.NewGuid(),
                Name = "Factory 1",
                Location = new Location
                {
                    Latitude = -34,
                    Longitude = 151,
                    Address1 = "4122 Broad Bay Way",
                    Address2 = "Suite 400",
                    City = "Aurora",
                    State = "IL",
                    Country = "US",
                    PostalCode = "60502",
                },
                DateInService = "4/12/2011",
            }
        };
        private readonly List<MetadataMachine> _machineMetadata = new List<MetadataMachine>
        {
            new MetadataMachine
            {
                Id = Guid.NewGuid(),
                SerialNumber = "212X4821BYG",
                DateInService = "7/23/2017",
                LastMaintenanceDate = "7/27/2019 13:42:01Z",
            }
        };
        private readonly List<MetadataMaintenanceLookup> _maintenanceLookupMetadata = new List<MetadataMaintenanceLookup>
        {
            new MetadataMaintenanceLookup
            {
                Id = Guid.NewGuid(),
                Pressure = "<7475",
                MachineTemperature = "<70",
                MaintenanceAdjustmentRequired = "Tighten Adjustment Harness",
            }
        };
        
        private List<MetadataBase> AllMetadata => new List<MetadataBase>()
            .Concat(_factoryMetadata)
            .Concat(_machineMetadata)
            .Concat(_maintenanceLookupMetadata)
            .ToList();
        
        public async Task<List<MetadataBase>> GetAllMetadataAsync(int limit, int skip = 0)
        {
            var metadata = new List<MetadataBase>();
            for (int i = 0; i < limit; i++)
            {
                metadata.Add(AllMetadata[i % AllMetadata.Count + skip]);
            }
            return metadata;
        }

        public async Task<List<MetadataFactory>> GetFactoryMetadata(int limit, int skip = 0)
        {
            var metadata = new List<MetadataFactory>();
            for (int i = 0; i < limit; i++)
            {
                metadata.Add(_factoryMetadata[i % _factoryMetadata.Count + skip]);
            }
            return metadata;
        }

        public async Task<List<MetadataMachine>> GetMachineMetadata(int limit, int skip = 0)
        {
            var metadata = new List<MetadataMachine>();
            for (int i = 0; i < limit; i++)
            {
                metadata.Add(_machineMetadata[i % _machineMetadata.Count + skip]);
            }

            return metadata;
        }

        public async Task<List<MetadataMaintenanceLookup>> GetMaintenanceLookupMetadata(int limit, int skip = 0)
        {
            var metadata = new List<MetadataMaintenanceLookup>();
            for (int i = 0; i < limit; i++)
            {
                metadata.Add(_maintenanceLookupMetadata[i % _maintenanceLookupMetadata.Count + skip]);
            }

            return metadata;
        }

        public async Task<MetadataDto> GetAllMetadataDtosAsync(int limit, int skip = 0)
        {
            var metadataList = await GetAllMetadataAsync(limit, skip);
            return new MetadataDto
            {
                MetadataFactories = metadataList.OfType<MetadataFactory>().ToList(),
                MetadataMachines = metadataList.OfType<MetadataMachine>().ToList(),
                MetadataMaintenanceLookups = metadataList.OfType<MetadataMaintenanceLookup>().ToList(),
            };
        }
    }
}