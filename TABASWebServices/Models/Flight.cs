using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TABASWebServices.Models
{
    public class Flight
    {
        public int FLIGHT_ID { get; set; }
        public string ORIGIN_AIRPORT { get; set; }
        public string DESTINATION_AIRPORT { get; set; }
        public string DEPARTURE_DATE { get; set; }
        public string DEPARTURE_HOUR { get; set; }
        public int PRICE { get; set; }
        public int PLANE_ID { get; set; }
        public int BAG_CAPACITY { get; set; }
    }
}