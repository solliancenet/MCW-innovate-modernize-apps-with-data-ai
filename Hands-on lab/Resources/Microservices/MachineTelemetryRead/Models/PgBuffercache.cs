using System;
using System.Collections.Generic;

namespace MachineTelemetryRead.Models
{
    public partial class PgBuffercache
    {
        public int? Bufferid { get; set; }
        public uint? Relfilenode { get; set; }
        public uint? Reltablespace { get; set; }
        public uint? Reldatabase { get; set; }
        public short? Relforknumber { get; set; }
        public long? Relblocknumber { get; set; }
        public bool? Isdirty { get; set; }
        public short? Usagecount { get; set; }
        public int? PinningBackends { get; set; }
    }
}
