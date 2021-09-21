using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class PostCode
    {
        public string postcode;
        public decimal longitude;
        public decimal latitude;
        public PostCode(string postcode, decimal longitude, decimal latitude)
        {
            this.postcode = postcode;
            this.longitude = longitude;
            this.latitude = latitude;
        }
        public String Show()
        {
            var info = "Postcode: " + postcode + " long " + longitude + " lat " + latitude;
            return info;
        }
    }
}
