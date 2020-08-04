using System;

namespace Common.DTOs
{
    public class MachineTelemetryDto
    {
        public int Id { get; set; }
        public int? MachineId { get; set; }
        public Guid EventId { get; set; }
        public string EventType { get; set; }
        public string EntityType { get; set; }
        public Guid EntityId { get; set; }
        public int FactoryId { get; set; }
        public MachineData Machine { get; set; }
        public MachineAmbientData Ambient { get; set; }
        public DateTime TimeCreated { get; set; }
    }

    public class MachineData
    {
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double ElectricityUtilization { get; set; }
    }

    public class MachineAmbientData
    {
        public double Temperature { get; set; }
        public int Humidity { get; set; }
    }
}