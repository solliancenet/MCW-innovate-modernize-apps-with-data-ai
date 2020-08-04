using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;

namespace SynapseInnovateMcwWebapp.Interfaces
{
    public interface IWriteService
    {
        Task CreateFactoryAsync(MetadataFactory metadataFactory);
        Task CreateFactoriesAsync(ICollection<MetadataFactory> metadataFactories);
        Task CreateMachineAsync(MetadataMachine metadataMachine);
        Task CreateMachinesAsync(ICollection<MetadataMachine> metadataMachines);
        Task CreateMaintenanceLookupAsync(MetadataMaintenanceLookup metadataMaintenanceLookup);
        Task CreateMaintenanceLookupsAsync(ICollection<MetadataMaintenanceLookup> metadataMaintenanceLookups);
    }
}