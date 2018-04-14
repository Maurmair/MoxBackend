using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoxBackend.Models
{
    public class InputData
    {
        public DateTime Date { get; set; }
        public long ActiveMinutesReached { get; set; }
        public long StepsReached { get; set; }
        public string DeviceId { get; set; }
    }
}