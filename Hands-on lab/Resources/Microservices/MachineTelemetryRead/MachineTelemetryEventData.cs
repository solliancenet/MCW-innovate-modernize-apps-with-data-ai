using System;

namespace MachineTelemetryRead
{
    public class MachineTelemetryEventData
    {
        public int FactoryId { get; set; }
        public MachineTelemetryEventDataMachine Machine { get; set; }
        public MachineTelemetryEventDataAmbient Ambient { get; set; }
        public string TimeCreated { get; set; }
    }

    public class MachineTelemetryEventDataMachine
    {
        public int MachineId { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double ElectricityUtilization { get; set; }
    }

    public class MachineTelemetryEventDataAmbient
    {
        public double Temperature { get; set; }
        public int Humidity { get; set; }
    }
}