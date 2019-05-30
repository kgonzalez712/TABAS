using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TABASWebServices.Models
{
    public class Bagcart
    {
        public int BAGCART_ID { get; set; }
        public int CAPACITY { get; set; }
        public string QRCODE { get; set; }
        public string STATUS { get; set; }
        public string BRAND_NAME { get; set; }
        public string BRAND_MODEL { get; set; }
        public int FLIGHT_ID { get; set; }

    }
}