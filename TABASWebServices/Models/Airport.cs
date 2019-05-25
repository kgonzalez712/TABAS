using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TABASWebServices.Models
{
    /// <summary>
    /// This Class represents the Airport entity on the database
    /// </summary>
    public class Airport
    {
        public string IATA_CODE { get; set; }
        public string AIRPORT_NAME { get; set; }
        public string STATE_CODE { get; set; }
        public string COUNTRY_CODE { get; set; }
        public string COUNTRY_NAME { get; set; }
    }
}