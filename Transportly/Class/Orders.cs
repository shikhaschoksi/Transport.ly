using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportly.Class
{
    public class Orders
    {
        public string ID { get; set; }
        public string Destination { get; set; }
        public bool isProcessed { get; set; } = false;
    }
}
