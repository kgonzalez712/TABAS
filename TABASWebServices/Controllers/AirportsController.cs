using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using  TABASWebServices.Models;

namespace TABASWebServices.Controllers
{

    public class AirportsController : ApiController
    {
        /// <summary>
        /// Returns all the airports from the database
        /// </summary>
        /// <returns></returns>
        [Route("Airports")]
        [HttpGet]
        public List<Airport> GetAirports()
        {
           return QueryManager.GetInstance().GetAirports();
        }

        /// <summary>
        /// returns an airport by its iata code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Airports/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAirportByid(string id)
        {
            var ent = QueryManager.GetInstance().GetAirportsbyid(id);
            if (ent != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ent);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "El aeropuerto con identificación " + id + "no existe.");
            }
        }
    }
}
