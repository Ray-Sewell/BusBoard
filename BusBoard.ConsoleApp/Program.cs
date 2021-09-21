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
            Menu();
        }
        static void Post(PostCode postcode)
        {
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
        }
        static void Menu()
        {
            bool on = true;
            while(on)
            {
                Console.Clear();
                Console.WriteLine("Please enter [1] to enter Postcode \nPlease enter [2] to Exit\n");
                switch(Console.ReadLine())
                {
                    case "1": 
                        Console.WriteLine("\nPlease enter a postcode to search\n");
                        try 
                        {
                            var postcode = FindPostCode(Console.ReadLine());
                            Post(postcode);
                        }
                        catch
                        {
                            Console.WriteLine("\nPostcode not found\n");
                        }
                        break;
                    case "2": 
                        Console.WriteLine("\nGoodbye <3\n");
                        on = false;
                        break;
                    default: 
                        Console.WriteLine("\nInvalid selection\n");
                        break;
                }
                Console.ReadLine();
            }
        }
        static PostCode FindPostCode(String input)
        {
            var client = new RestClient("https://api.postcodes.io/postcodes/");
            var request = new RestRequest(input, DataFormat.Json);
            var response = client.Get(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception("Postcode not found");
                //throw new NullReferenceException("Postcode not found");
            } 
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
