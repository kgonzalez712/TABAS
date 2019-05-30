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
        public float WEIGHT { get; set; }
        public float PRICE { get; set; }
        public int BAGCART_ID { get; set; }
        public int CAPACITY { get; set; }
        public string CLIENT_ID { get; set; }

    }
}