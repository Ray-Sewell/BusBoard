using System.Collections.Generic;

namespace BusBoard.Web.Models
{
    public class BusStopWeb
    {
        public string id;
        public float distance;
        public List<BusWeb> buses;
        public BusStopWeb(string id, float distance, List<BusWeb> buses)
        {
            this.id = id;
            this.distance = distance;
            this.buses = buses;
        }
        public string Show()
        {
            return "Busstop id: " + id + " distance: " + distance;
        }
    }
}