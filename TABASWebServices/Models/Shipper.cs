using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TABASWebServices.Models
{
    public class Shipper
    {
        public int SHIPPING_ID { get; set; }
        public string SHIPPING_DATE { get; set; }
        public string SHIPPING_HOUR { get; set; }
        public string STATUS { set; get; }
        public int BAGCART_ID { set; get; }
        public int BAG_ID { set; get; }
        public string SUPERVISOR { get; set; }
    }
}