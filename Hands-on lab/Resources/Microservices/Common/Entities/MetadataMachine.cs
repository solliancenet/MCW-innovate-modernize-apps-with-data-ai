using Common.Enums;

namespace Common.Entities
{
    public class MetadataMachine : MetadataBase
    {
        public MetadataMachine()
        {
            EntityType = MetadataEntityType.Machine;
        }
        
        public string SerialNumber { get; set; }
        public string DateInService { get; set; }
        public string LastMaintenanceDate { get; set; }
    }
}