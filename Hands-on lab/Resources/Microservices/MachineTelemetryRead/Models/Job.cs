using System;
using System.Collections.Generic;

namespace MachineTelemetryRead.Models
{
    public partial class Job
    {
        public long Jobid { get; set; }
        public string Schedule { get; set; }
        public string Command { get; set; }
        public string Nodename { get; set; }
        public int Nodeport { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public bool? Active { get; set; }
    }
}
