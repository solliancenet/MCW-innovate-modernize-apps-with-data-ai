using System.Collections.Generic;
using Common.Entities;

namespace Common.DTOs
{
    public class MetadataDto
    {
        public List<MetadataFactory> MetadataFactories { get; set; }
        public List<MetadataMachine> MetadataMachines { get; set; }
        public List<MetadataMaintenanceLookup> MetadataMaintenanceLookups { get; set; }
    }
}