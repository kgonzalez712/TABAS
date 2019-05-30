using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class Client
    {
        public string ID { get; set; }
        public string FNAME { get; set; }
        public string LNAME { get; set; }
        public string UID { get; set; }
        public string PHONENO { get; set; }
        public string CARDNO { get; set; }
        public string EMAIL { get; set; }
        public string PASSWRD { get; set; }
        public int MILES { get; set; }
        public int UNIVERSITY_ID { get; set; }
    }
}