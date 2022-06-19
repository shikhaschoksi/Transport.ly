using System.Collections.Generic;

namespace Transportly.Class
{
    public class FlightDetails
    {
        public int Flight { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public int Assigned { get; set; } = 0;
        public IEnumerable<string> Orders { get; set; }
        public bool isFull { get; set; } = false;
        public int Capacity { get; set; } = 20;
    }
}
