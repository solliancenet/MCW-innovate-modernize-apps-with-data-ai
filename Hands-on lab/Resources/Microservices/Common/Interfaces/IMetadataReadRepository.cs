using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTOs;
using Common.Entities;

namespace Common.Interfaces
{
    public interface IMetadataReadRepository
    {
        public Task<List<MetadataBase>> GetAllMetadataAsync(int limit, int skip = 0);
        public Task<List<MetadataFactory>> GetFactoryMetadata(int limit, int skip = 0);
        public Task<List<MetadataMachine>> GetMachineMetadata(int limit, int skip = 0);
        public Task<List<MetadataMaintenanceLookup>> GetMaintenanceLookupMetadata(int limit, int skip = 0);
        public Task<MetadataDto> GetAllMetadataDtosAsync(int limit, int skip = 0);
    }
}