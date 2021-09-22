using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.Api
{
    public class API
    {
        public static List<string> APICall(string input)
        {
            PostCode postcode = FindPostCode(input);
            List<string> return_list = new List<string>();

            foreach (BusStop bus_stop in FindBusStops(postcode).OrderBy(o => o.distance).ToList().Take(2))
            {
                return_list.Add("");
                return_list.Add(bus_stop.Show());
                var bus_list = FindBus(bus_stop);

                if (bus_list.Count <= 0)
                {
                    return_list.Add("No buses are available at this time");
                }
                else
                {
                    foreach (Bus bus in bus_list.OrderBy(o => o.timeToStation).ToList().Take(5))
                    {
                        return_list.Add(bus.Show());
                    }
                }
            }
            return return_list;
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
