using System;
using System.Collections.Generic;

namespace MachineTelemetryRead.Models
{
    public partial class PartConfigSub
    {
        public string SubParent { get; set; }
        public string SubPartitionType { get; set; }
        public string SubControl { get; set; }
        public string SubPartitionInterval { get; set; }
        public string[] SubConstraintCols { get; set; }
        public int SubPremake { get; set; }
        public int SubOptimizeTrigger { get; set; }
        public int SubOptimizeConstraint { get; set; }
        public string SubEpoch { get; set; }
        public bool? SubInheritFk { get; set; }
        public string SubRetention { get; set; }
        public string SubRetentionSchema { get; set; }
        public bool? SubRetentionKeepTable { get; set; }
        public bool? SubRetentionKeepIndex { get; set; }
        public bool SubInfiniteTimePartitions { get; set; }
        public string SubAutomaticMaintenance { get; set; }
        public bool? SubJobmon { get; set; }
        public bool? SubTriggerExceptionHandling { get; set; }
        public string SubUpsert { get; set; }
        public bool? SubTriggerReturnNull { get; set; }
        public string SubTemplateTable { get; set; }
        public bool? SubInheritPrivileges { get; set; }

        public virtual PartConfig SubParentNavigation { get; set; }
    }
}
