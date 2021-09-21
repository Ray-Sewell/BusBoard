using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class BusStop
    {
        public string id;
        public float distance;
        public BusStop(string id, float distance)
        {
            this.id = id;
            this.distance = distance;
        }
        public String Show()
        {
            var info = "Bus Stop ID: " + id;
            return info;
        }
    }
}
