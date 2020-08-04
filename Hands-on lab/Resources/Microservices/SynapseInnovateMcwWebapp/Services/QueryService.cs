using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Common.DTOs;
using Common.Utils;
using Microsoft.AspNetCore.WebUtilities;
using SynapseInnovateMcwWebapp.Interfaces;

namespace SynapseInnovateMcwWebapp.Services
{
    public class QueryService : IQueryService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        private string _queryServiceUrl;

        public QueryService()
        {
            _client = new HttpClient();
            _queryServiceUrl = EnvUtils.Get("QUERY_SERVICE_URL");
        }
        
        public async Task<MetadataDto> GetMetadataAsync(int limit, int skip = 0)
        {
            var url = $"{_queryServiceUrl}/metadata?limit={limit}";

            if (skip != 0)
            {
                url = QueryHelpers.AddQueryString(url, "skip", skip.ToString());
            }

            var metadataDtoStream = await _client.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<MetadataDto>(metadataDtoStream, _jsonSerializerOptions);
        }

        public async Task<List<MachineTelemetryDto>> GetMachineTelemetryAsync(int limit, int skip = 0)
        {
            var url = $"{_queryServiceUrl}/machineTelemetry?limit={limit}";

            if (skip != 0)
            {
                url = QueryHelpers.AddQueryString(url, "skip", skip.ToString());
            }

            var machineTelemetryDtoStream = await _client.GetStreamAsync(url);
            return await JsonSerializer.DeserializeAsync<List<MachineTelemetryDto>>(machineTelemetryDtoStream,
                _jsonSerializerOptions);
        }
    }
}