using System;
using System.Net;
using BusBoard.Api;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Menu();
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
                            var response_list = API.APICall(Console.ReadLine());
                            foreach (string response in response_list)
                            {
                                Console.WriteLine(response);
                            }
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
    }
}
