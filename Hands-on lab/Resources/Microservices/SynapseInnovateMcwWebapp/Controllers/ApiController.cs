using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTOs;
using Common.Entities;
using Common.Utils;
using Microsoft.AspNetCore.Mvc;
using SynapseInnovateMcwWebapp.Interfaces;

namespace SynapseInnovateMcwWebapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController
    {
        private IQueryService _queryService;
        private IWriteService _writeService;

        public ApiController(IQueryService queryService, IWriteService writeService)
        {
            _queryService = queryService;
            _writeService = writeService;
        }
        
        [HttpGet]
        [Route("metadata")]
        public async Task<MetadataDto> GetMetadataAsync()
        {
            return await _queryService.GetMetadataAsync(100);
        }

        [HttpPost]
        [Route("metadata/factory")]
        public async Task PostMetadataFactoryAsync([FromBody] MetadataFactory metadataFactory)
        {
            await _writeService.CreateFactoryAsync(metadataFactory);
        }

        [HttpPost]
        [Route("metadata/machine")]
        public async Task PostMetadataMachineAsync([FromBody] MetadataMachine metadataMachine)
        {
            await _writeService.CreateMachineAsync(metadataMachine);
        }

        [HttpPost]
        [Route("metadata/maintenanceLookup")]
        public async Task PostMaintenanceLookupAsync([FromBody] MetadataMaintenanceLookup metadataMaintenanceLookup)
        {
            await _writeService.CreateMaintenanceLookupAsync(metadataMaintenanceLookup);
        }

        [HttpGet]
        [Route("machineTelemetry")]
        public async Task<List<MachineTelemetryDto>> GetMachineTelemetryAsync()
        {
            return await _queryService.GetMachineTelemetryAsync(100);
        }

        [HttpGet]
        [Route("powerBiDashboard")]
        public ActionResult<string> GetPowerBiDashboardUrl()
        {
            var url = Environment.GetEnvironmentVariable("POWER_BI_DASHBOARD_URL");
            if (url == null)
            {
                return new NotFoundResult();
            }

            return url;
        }
    }
}