using System.Threading.Tasks;
using Common.DTOs;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MetadataRead.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetadataController
    {
        private IMetadataReadRepository _metadataRepository;

        public MetadataController(IMetadataReadRepository metadataRepository)
        {
            _metadataRepository = metadataRepository;
        }

        public async Task<MetadataDto> GetAsync([FromQuery] int limit = 100, [FromQuery] int skip = 0)
        {
            return await _metadataRepository.GetAllMetadataDtosAsync(limit, skip);
        }
    }
}