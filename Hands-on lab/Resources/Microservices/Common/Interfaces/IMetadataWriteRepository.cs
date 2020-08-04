using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;

namespace Common.Interfaces
{
    public interface IMetadataWriteRepository
    {
        Task CreateFactoryAsync(MetadataFactory metadataFactory);
        Task CreateMachineAsync(MetadataMachine metadataMachine);
        Task CreateMaintenanceLookupAsync(MetadataMaintenanceLookup metadataMaintenanceLookup);
        Task CreateFactoriesAsync(ICollection<MetadataFactory> metadataFactories);
        Task CreateMachinesAsync(ICollection<MetadataMachine> metadataMachines);
        Task CreateMaintenanceLookupsAsync(ICollection<MetadataMaintenanceLookup> metadataMaintenanceLookups);
    }
}