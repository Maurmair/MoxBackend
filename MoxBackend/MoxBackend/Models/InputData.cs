using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoxBackend.Models
{
    public class InputData
    {
        public long RecordId { get; set; }
        public DateTime Date { get; set; }
        public long ActiveMinutesReached { get; set; }
        public long StepsReached { get; set; }
    }
}