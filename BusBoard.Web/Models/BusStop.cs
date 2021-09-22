using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusBoard.Web.Models
{
    public class BusStop
    {
        public string id;
        public int distance;
        public List<Bus> buses { get; set; }
        public BusStop(string id, int distance, List<Bus> buses)
        {
            this.id = id;
            this.distance = distance;
            this.buses = buses;
        }
    }
}