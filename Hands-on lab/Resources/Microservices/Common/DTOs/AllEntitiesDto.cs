using System.Collections.Generic;

namespace Common.DTOs
{
    public class AllEntitiesDto : MetadataDto
    {
        public List<MachineTelemetryDto> MachineTelemetries { get; set; }
    }
}