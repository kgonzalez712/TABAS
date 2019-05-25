using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TABASWebServices.Models
{
    public class Airplane
    {
        public int AIRPLANE_ID { get; set; }
        public string PILOT_FULLNAME { get; set; }
        public string MODEL { get; set; }
        public int STORAGE_BINS { get; set; }
        public int CAPACITY { get; set; }
    }
}