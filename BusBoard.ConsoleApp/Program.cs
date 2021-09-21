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
            Console.WriteLine("Please enter postcode");
            var input = Console.ReadLine();
            Console.WriteLine(FindPostCode(input).Show());
            //List<Bus> bus_list = JsonConvert.DeserializeObject<List<Bus>>(response.Content);

            //List<Bus> bus_list_sorted =  bus_list.OrderBy(o => o.timeToStation).ToList();
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
    }
}
