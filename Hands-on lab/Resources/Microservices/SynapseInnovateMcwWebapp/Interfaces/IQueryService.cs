using System.Collections.Generic;
using System.Threading.Tasks;
using Common.DTOs;

namespace SynapseInnovateMcwWebapp.Interfaces
{
    public interface IQueryService
    {
        Task<MetadataDto> GetMetadataAsync(int limit, int skip = 0);
        Task<List<MachineTelemetryDto>> GetMachineTelemetryAsync(int limit, int skip = 0);
    }
}