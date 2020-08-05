using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Common.Entities;
using Common.Utils;
using SynapseInnovateMcwWebapp.Interfaces;

namespace SynapseInnovateMcwWebapp.Services
{
    public class WriteService : IWriteService
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
            {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

        private string _writeServiceUrl;

        public WriteService()
        {
            _client = new HttpClient();
            _writeServiceUrl = EnvUtils.Get("METADATA_WRITE_SERVICE_URL");
        }

        public async Task CreateFactoryAsync(MetadataFactory metadataFactory)
        {
            var url = $"{_writeServiceUrl}/metadata/factory";

            var content = new StringContent(JsonSerializer.Serialize(metadataFactory, _jsonSerializerOptions),
                Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Factory Metadata Creation failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task CreateFactoriesAsync(ICollection<MetadataFactory> metadataFactories)
        {
            var url = $"{_writeServiceUrl}/metadata/factory/bulk";
            
            var content = new StringContent(JsonSerializer.Serialize(metadataFactories, _jsonSerializerOptions),
                Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Factory Metadata Creation failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task CreateMachineAsync(MetadataMachine metadataMachine)
        {
            var url = $"{_writeServiceUrl}/metadata/machine";

            var content = new StringContent(JsonSerializer.Serialize(metadataMachine, _jsonSerializerOptions),
                Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Machine Metadata Creation failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task CreateMachinesAsync(ICollection<MetadataMachine> metadataMachines)
        {
            var url = $"{_writeServiceUrl}/metadata/machine/bulk";

            var content = new StringContent(JsonSerializer.Serialize(metadataMachines, _jsonSerializerOptions),
                Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Machine Metadata Creation failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task CreateMaintenanceLookupAsync(MetadataMaintenanceLookup metadataMaintenanceLookup)
        {
            var url = $"{_writeServiceUrl}/metadata/maintenanceLookup";
            
            var content = new StringContent(JsonSerializer.Serialize(metadataMaintenanceLookup, _jsonSerializerOptions),
                Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Maintenance Lookup Metadata Creation failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }
        }

        public async Task CreateMaintenanceLookupsAsync(ICollection<MetadataMaintenanceLookup> metadataMaintenanceLookups)
        {
            var url = $"{_writeServiceUrl}/metadata/maintenanceLookup/bulk";
            
            var content = new StringContent(JsonSerializer.Serialize(metadataMaintenanceLookups, _jsonSerializerOptions),
                Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Maintenance Lookup Metadata Creation failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }
        }
    }
}