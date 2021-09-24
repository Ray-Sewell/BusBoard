using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusBoard.Web.Models
{
    public class BusWeb
    {
        public string vehicleId;
        public string stationName;
        public string destinationName;
        public int timeToStation;
        public string timeFormatted;

        public BusWeb(string vehicleId, string stationName, string destinationName, int timeToStation)
        {
            this.vehicleId = vehicleId;
            this.stationName = stationName;
            this.destinationName = destinationName;
            this.timeToStation = timeToStation;
            this.timeFormatted = timeToStation / 60 + " minutes";
        }
        public string Show()
        {
            return "Bus id: " + vehicleId + " calling from " + stationName + " to " + destinationName + " arriving in " + timeFormatted;
        }
    }
}