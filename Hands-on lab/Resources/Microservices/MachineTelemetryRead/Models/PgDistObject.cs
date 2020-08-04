using System;
using System.Collections.Generic;

namespace MachineTelemetryRead.Models
{
    public partial class PgDistObject
    {
        public uint Classid { get; set; }
        public uint Objid { get; set; }
        public int Objsubid { get; set; }
        public string Type { get; set; }
        public string[] ObjectNames { get; set; }
        public string[] ObjectArgs { get; set; }
        public int? DistributionArgumentIndex { get; set; }
        public int? Colocationid { get; set; }
    }
}
