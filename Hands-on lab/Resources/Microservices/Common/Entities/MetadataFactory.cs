using Common.Enums;

namespace Common.Entities
{
    public class MetadataFactory : MetadataBase
    {
        public MetadataFactory()
        {
            EntityType = MetadataEntityType.Factory;
        }
        
        public string Name { get; set; }
        public Location Location { get; set; }
        public string DateInService { get; set; }
    }
}