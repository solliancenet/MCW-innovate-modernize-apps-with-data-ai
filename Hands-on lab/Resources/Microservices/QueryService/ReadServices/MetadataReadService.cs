using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Common.DTOs;
using Microsoft.AspNetCore.WebUtilities;

namespace QueryService.ReadServices
{
    public class MetadataReadService : IMetadataReadService
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public MetadataReadService()
        {
            _client = new HttpClient();
        }
        
        public async Task<MetadataDto> GetAllMetadataAsync(int limit, int skip = 0)
        {
            var url = Environment.GetEnvironmentVariable("METADATA_READ_URL");
            var requestUri = $"{url}/metadata?limit={limit}";

            if (skip != 0)
            {
                requestUri = QueryHelpers.AddQueryString(url, "skip", skip.ToString());
            }
            
            var metadataDtoJson = await _client.GetStreamAsync(requestUri);
            return await JsonSerializer.DeserializeAsync<MetadataDto>(metadataDtoJson, _jsonSerializerOptions);
        }
    }

    public interface IMetadataReadService
    {
        public Task<MetadataDto> GetAllMetadataAsync(int limit, int skip = 0);
    }
}