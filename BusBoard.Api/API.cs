using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.Api
{
    public class API
    {
        public static List<BusStop> APICall(string input)
        {
            List<BusStop> response_list = new List<BusStop>();
            foreach (BusStop bus_stop in FindBusStops(FindPostCode(input)))
            {
                BusStop temp_bus = bus_stop;
                temp_bus.buses = FindBus(bus_stop);
                response_list.Add(temp_bus);
            }
            return response_list;
        }
        static PostCode FindPostCode(String input)
        {
            var client = new RestClient("https://api.postcodes.io/postcodes/");
            var request = new RestRequest(input, DataFormat.Json);
            var response = client.Get(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Postcode not found");
            }
            var postcode_raw = JsonConvert.DeserializeObject<PostCodeResult>(response.Content);
            return postcode_raw.result;
        }
        static List<BusStop> FindBusStops(PostCode input)
        {
            var client = new RestClient("https://api.tfl.gov.uk/StopPoint/");
            var request = new RestRequest("/?lat=" + input.latitude + "&lon=" + input.longitude + "&stopTypes=NaptanPublicBusCoachTram", DataFormat.Json);
            var response = client.Get(request);
            var bus_stops_raw = JsonConvert.DeserializeObject<BusStopResult>(response.Content);

            return bus_stops_raw.stopPoints;
        }
        static List<Bus> FindBus(BusStop input)
        {
            var client = new RestClient("https://api.tfl.gov.uk/StopPoint/");
            var request = new RestRequest(input.id + "/Arrivals", DataFormat.Json);
            var response = client.Get(request);
            var bus_list = JsonConvert.DeserializeObject<List<Bus>>(response.Content);

            return bus_list;
        }
    }
}
