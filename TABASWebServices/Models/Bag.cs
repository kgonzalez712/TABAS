using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TABASWebServices.Models
{
    public class Bag
    {
        public int BAG_ID { get; set; }
        public string COLOUR { get; set; }
        public int WEIGHT { get; set; }
        public int PRICE { get; set; }
        public string SCAN_STATUS { get; set; }
        public string SHIP_STATUS { get; set; }
        public int BAGCART_ID { get; set; }
        public int PLANE_ID { get; set; }
        public string PLANE_BIN { get; set; }
        public string CLIENT_ID { get; set; }

    }
}