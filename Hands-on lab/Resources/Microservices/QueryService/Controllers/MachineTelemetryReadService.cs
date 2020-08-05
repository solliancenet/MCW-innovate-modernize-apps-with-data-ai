using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Common.DTOs;
using Microsoft.AspNetCore.WebUtilities;

namespace QueryService.Controllers
{
    public class MachineTelemetryReadService : IMachineTelemetryReadService
    {
        private readonly HttpClient _client;
        
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public MachineTelemetryReadService()
        {
            _client = new HttpClient();
        }

        public async Task<List<MachineTelemetryDto>> GetAllTelemetryDataAsync(int limit, int skip = 0)
        {
            var url = Environment.GetEnvironmentVariable("MACHINE_TELEMETRY_READ_URL");
            var requestUri = $"{url}/machineTelemetry";
            
            if (skip != 0)
            {
                requestUri = QueryHelpers.AddQueryString(url, "skip", skip.ToString());
            }
            
            var machineTelemetryDtoJson = await _client.GetStreamAsync(requestUri);
            return await JsonSerializer.DeserializeAsync<List<MachineTelemetryDto>>(machineTelemetryDtoJson,
                _jsonSerializerOptions);
        }
    }

    public interface IMachineTelemetryReadService
    {
        Task<List<MachineTelemetryDto>> GetAllTelemetryDataAsync(int limit, int skip = 0);
    }
}