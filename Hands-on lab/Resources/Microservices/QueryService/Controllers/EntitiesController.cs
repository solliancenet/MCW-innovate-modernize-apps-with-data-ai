using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using QueryService.ReadServices;

namespace QueryService.Controllers
{
    [ApiController]
    [Route("/")]
    public class EntitiesController
    {
        private readonly IMetadataReadService _metadataReadService;
        private readonly IMachineTelemetryReadService _machineTelemetryReadService;

        public EntitiesController(IMetadataReadService metadataReadService, 
            IMachineTelemetryReadService machineTelemetryReadService)
        {
            _metadataReadService = metadataReadService;
            _machineTelemetryReadService = machineTelemetryReadService;
        }
        
        [HttpGet]
        public async Task<AllEntitiesDto> GetAsync()
        {
            var metadata = await _metadataReadService.GetAllMetadataAsync(100);
            var machineTelemetries = await _machineTelemetryReadService.GetAllTelemetryDataAsync(100);
            var dto = new AllEntitiesDto
            {
                MetadataFactories = metadata.MetadataFactories,
                MetadataMachines = metadata.MetadataMachines,
                MetadataMaintenanceLookups = metadata.MetadataMaintenanceLookups,
                MachineTelemetries = machineTelemetries,
            };
            return dto;
        }

        [HttpGet("/metadata")]
        public async Task<MetadataDto> GetMetadataAsync()
        {
            return await _metadataReadService.GetAllMetadataAsync(100);
        }

        [HttpGet("/machineTelemetry")]
        public async Task<List<MachineTelemetryDto>> GetMachineTelemetryAsync()
        {
            return await _machineTelemetryReadService.GetAllTelemetryDataAsync(100);
        }
    }
}