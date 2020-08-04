using Common.Enums;

namespace Common.Entities
{
    public class MetadataMaintenanceLookup : MetadataBase
    {
        public MetadataMaintenanceLookup()
        {
            EntityType = MetadataEntityType.MaintenanceLookup;
        }
        
        public string Pressure { get; set; }
        public string MachineTemperature { get; set; }
        public string MaintenanceAdjustmentRequired { get; set; }
    }
}