using BusBoard.Api;
using System.Collections.Generic;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public string PostCode { get; set; }
        public List<string> BusStop { get; set; }
        public BusInfo(string postCode)
        {
            PostCode = postCode;
            BusStop = API.APICall(postCode);
        }
    }
}