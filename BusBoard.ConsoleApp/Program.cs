using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Console.WriteLine("Please enter postcode\n");

            var input = Console.ReadLine();
            var postcode = FindPostCode(input);

            foreach (BusStop bus_stop in FindBusStops(postcode).OrderBy(o => o.distance).ToList().Take(2))
            {
                Console.WriteLine();
                Console.WriteLine(bus_stop.Show());
       
                var bus_list = FindBus(bus_stop);

                if (bus_list.Count <= 0)
                {
                    Console.WriteLine("No buses are available at this time");
                } 
                else
                {
                    foreach (Bus bus in bus_list.OrderBy(o => o.timeToStation).ToList().Take(5))
                    {
                        Console.WriteLine(bus.Show());
                    }
                }
            }

            Console.ReadLine();
        }
        static PostCode FindPostCode(String input)
        {
            var client = new RestClient("https://api.postcodes.io/postcodes/");
            var request = new RestRequest(input, DataFormat.Json);
            var response = client.Get(request);
            var postcode_raw = JsonConvert.DeserializeObject<PostCodeResult>(response.Content);

            return postcode_raw.result;
        }
        static List<BusStop> FindBusStops(PostCode input)
        {
            var client = new RestClient("https://api.tfl.gov.uk/StopPoint/");
            var request = new RestRequest("/?lat="+input.latitude+"&lon="+input.longitude+"&stopTypes=NaptanPublicBusCoachTram", DataFormat.Json);
            var response = client.Get(request);
            var bus_stops_raw = JsonConvert.DeserializeObject<BusStopResult>(response.Content);

            return bus_stops_raw.stopPoints;
        }
        static List<Bus> FindBus(BusStop input)
        {
            var client = new RestClient("https://api.tfl.gov.uk/StopPoint/");
            var request = new RestRequest(input.id+"/Arrivals", DataFormat.Json);
            var response = client.Get(request);
            var bus_list = JsonConvert.DeserializeObject<List<Bus>>(response.Content);

            return bus_list;
        }
    }
}
