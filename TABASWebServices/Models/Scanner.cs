using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TABASWebServices.Models
{
    public class Scanner
    {
        public int SCAN_ID { get; set; }
        public string SUPERVISOR { get; set; }
        public string SCAN_DATE_HOUR { get; set; }
        public int BAGCART_ID { set; get; }
        public int BAG_ID { set; get; }
    }
}