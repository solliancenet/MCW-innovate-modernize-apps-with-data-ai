using System;
using System.Collections.Generic;

namespace MachineTelemetryRead.Models
{
    public partial class PartConfig
    {
        public string ParentTable { get; set; }
        public string Control { get; set; }
        public string PartitionType { get; set; }
        public string PartitionInterval { get; set; }
        public string[] ConstraintCols { get; set; }
        public int Premake { get; set; }
        public int OptimizeTrigger { get; set; }
        public int OptimizeConstraint { get; set; }
        public string Epoch { get; set; }
        public bool? InheritFk { get; set; }
        public string Retention { get; set; }
        public string RetentionSchema { get; set; }
        public bool? RetentionKeepTable { get; set; }
        public bool? RetentionKeepIndex { get; set; }
        public bool InfiniteTimePartitions { get; set; }
        public string DatetimeString { get; set; }
        public string AutomaticMaintenance { get; set; }
        public bool? Jobmon { get; set; }
        public bool SubPartitionSetFull { get; set; }
        public bool UndoInProgress { get; set; }
        public bool? TriggerExceptionHandling { get; set; }
        public string Upsert { get; set; }
        public bool? TriggerReturnNull { get; set; }
        public string TemplateTable { get; set; }
        public string[] Publications { get; set; }
        public bool? InheritPrivileges { get; set; }

        public virtual PartConfigSub PartConfigSub { get; set; }
    }
}
