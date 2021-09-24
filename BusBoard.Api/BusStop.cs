using System;
using System.Collections.Generic;

namespace BusBoard.Api
{
    public class BusStop
    {
        public string id;
        public float distance;
        public List<Bus> buses;
        public BusStop(string id, float distance, List<Bus> buses)
        {
            this.id = id;
            this.distance = distance;
            this.buses = buses;
        }
    }
}
