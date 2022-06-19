using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportly.Class
{
    public class Schedule
    {
        public int Day { get; set; }
        public List<FlightDetails> FlightDetail { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
