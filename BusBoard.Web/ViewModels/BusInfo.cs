using BusBoard.Api;
using System.Collections.Generic;
using BusBoard.Web.Models;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public string PostCode { get; set; }
        public List<BusStopWeb> BusStop { get; set; }
        public BusInfo(string postCode)
        {
            PostCode = postCode;
            BusStop = GetBusStops(postCode);
        }
        public List<BusStopWeb> GetBusStops(string postCode)
        {
            PostCode = postCode;
            List<BusStop> buses_raw = API.APICall(postCode);
            List<BusStopWeb> buses = new List<BusStopWeb>();
            foreach (BusStop bus_stop in buses_raw)
            {
                List<BusWeb> bus_list = new List<BusWeb>();
                foreach (Bus bus in bus_stop.buses)
                {
                    bus_list.Add(new BusWeb(bus.vehicleId, bus.stationName, bus.destinationName, bus.timeToStation));
                }
                buses.Add(new BusStopWeb(bus_stop.id, bus_stop.distance, bus_list));
            }
            return buses;
        }
    }
}