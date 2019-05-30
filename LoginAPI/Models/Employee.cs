using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class Employee
    {
        public string FNAME { get; set; }
        public int ID { get; set; }
        public string LNAME { get; set; }
        public string PASSWRD { get; set; }
        public string ROLENAME { get; set; }
        public int ROLEID { get; set; }
    }
}