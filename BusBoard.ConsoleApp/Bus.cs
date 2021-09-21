using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
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
