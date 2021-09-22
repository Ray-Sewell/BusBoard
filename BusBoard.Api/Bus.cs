using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.Api
{
    class Bus
    {
        public string vehicleId;
        public string stationName;
        public string destinationName;
        public int timeToStation;
        public string timeFormatted;

        public Bus(string vehicleId, string stationName, string destinationName, int timeToStation)
        {
            this.vehicleId = vehicleId;
            this.stationName = stationName;
            this.destinationName = destinationName;
            this.timeToStation = timeToStation;
            this.timeFormatted = timeToStation/60 + " minutes";
        }
        public String Show()
        {
            var info = "Bus ID: " + vehicleId + " from " + stationName + " to " + destinationName + " arriving in approximately " + timeFormatted;
            return info;
        }
    }
}
