using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard
{
  class Program
  {
    static void Main(string[] args)
    {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var rest_client = new RestClient("https://api.tfl.gov.uk/");
            var request = new RestRequest("StopPoint/490008660N/Arrivals", DataFormat.Json);
            var response = rest_client.Get(request);

            List<Bus> bus_list = JsonConvert.DeserializeObject<List<Bus>>(response.Content);

            List<Bus> bus_list_sorted =  bus_list.OrderBy(o => o.timeToStation).ToList();
            foreach (var bus in bus_list_sorted)
            {
                Console.WriteLine(bus.vehicleId+ " " + bus.timeToStation);
            }
            Console.WriteLine("TEST");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(bus_list_sorted[i].Show());
            }
            Console.ReadLine();
        }
  }
  class Bus
    {
        public string id;
        public string vehicleId;
        public string stationName;
        public string destinationName;
        public int timeToStation;
        public Bus(string id, string vehicleId, string stationName, string destinationName, int timeToStation)
        {
            this.id = id;
            this.vehicleId = vehicleId;
            this.stationName = stationName;
            this.destinationName = destinationName;
            this.timeToStation = timeToStation;
        }
        public String Show()
        {
            var info = "Bus ID: " + vehicleId + " from " + stationName + " to " + destinationName + " arriving in " + timeToStation + " seconds";
            return info;
        }
    }
}
