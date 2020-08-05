using System;
using System.Collections.Generic;

namespace MachineTelemetryRead.Models
{
    public partial class Event1
    {
        public int Id { get; set; }
        public int? MachineId { get; set; }
        public string EventId { get; set; }
        public string EventType { get; set; }
        public string EntityType { get; set; }
        public string EntityId { get; set; }
        public string EventData { get; set; }
    }
}
