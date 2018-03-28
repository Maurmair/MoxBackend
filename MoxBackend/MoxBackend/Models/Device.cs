using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoxBackend.Models
{
    public class Device
    {
        public long RecordId { get; set; }
        public String DeviceId { get; set; }
        public String CoupledDevice { get; set; }        
    }
}