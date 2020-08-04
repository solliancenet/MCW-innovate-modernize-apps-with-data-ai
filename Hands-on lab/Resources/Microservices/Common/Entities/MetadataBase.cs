using System;
using Common.Enums;

namespace Common.Entities
{
    public class MetadataBase
    {
        public Guid? Id { get; set; }
        public MetadataEntityType EntityType { get; set; }
    }
}