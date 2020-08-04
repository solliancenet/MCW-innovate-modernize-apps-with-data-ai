using System;
using NpgsqlTypes;

namespace MachineTelemetryRead.Models
{
    public partial class CustomTimePartitions
    {
        public string ParentTable { get; set; }
        public string ChildTable { get; set; }
        public NpgsqlRange<DateTime> PartitionRange { get; set; }
    }
}
