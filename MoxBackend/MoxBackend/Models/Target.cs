using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoxBackend.Models
{
    public class Target
    {
        public DateTime Date { get; set; }
        public long ActiveMinutes { get; set; }
        public long Steps { get; set; }
        public String DeviceId { get; set; }
    }
}