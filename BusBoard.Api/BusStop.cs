using System;

namespace BusBoard.Api
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
