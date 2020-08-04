using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetadataWrite.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetadataController
    {
        private readonly IMetadataWriteRepository _metadataRepository;

        public MetadataController(IMetadataWriteRepository metadataRepository)
        {
            _metadataRepository = metadataRepository;
        }

        [HttpPost]
        [Route("factory")]
        public async Task PostFactoryAsync([FromBody] MetadataFactory metadataFactory)
        {
            await _metadataRepository.CreateFactoryAsync(metadataFactory);
        }
        
        [HttpPost]
        [Route("factory/bulk")]
        public async Task PostFactoriesAsync([FromBody] ICollection<MetadataFactory> metadataFactories)
        {
            await _metadataRepository.CreateFactoriesAsync(metadataFactories);
        }

        [HttpPost]
        [Route("machine")]
        public async Task PostMachineAsync([FromBody] MetadataMachine metadataMachine)
        {
            await _metadataRepository.CreateMachineAsync(metadataMachine);
        }
        
        [HttpPost]
        [Route("machine/bulk")]
        public async Task PostMachinesAsync([FromBody] ICollection<MetadataMachine> metadataMachines)
        {
            await _metadataRepository.CreateMachinesAsync(metadataMachines);
        }

        [HttpPost]
        [Route("maintenanceLookup")]
        public async Task PostMaintenanceLookupAsync([FromBody] MetadataMaintenanceLookup metadataMaintenanceLookup)
        {
            await _metadataRepository.CreateMaintenanceLookupAsync(metadataMaintenanceLookup);
        }
        
        [HttpPost]
        [Route("maintenanceLookup/bulk")]
        public async Task PostMaintenanceLookupsAsync([FromBody] ICollection<MetadataMaintenanceLookup> metadataMaintenanceLookups)
        {
            await _metadataRepository.CreateMaintenanceLookupsAsync(metadataMaintenanceLookups);
        }
    }
}