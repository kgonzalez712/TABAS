using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TABASWebServices.Models;

namespace TABASWebServices.Controllers
{
    public class FlightsController : ApiController
    {
        /// <summary>
        /// Returns all the flight from the database
        /// </summary>
        /// <returns></returns>
        [Route("Flights")]
        [HttpGet]
        public List<Flight> GetFlights()
        {
            return QueryManager.GetInstance().GetFlights();
        }

        /// <summary>
        /// returns an flight by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Flights/{id}")]
        [HttpGet]
        public HttpResponseMessage GetFlightByid(int id)
        {
            var ent = QueryManager.GetInstance().GetFlightbyid(id);
            if (ent != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ent);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "El vuelo con identificación " + id + "no existe.");
            }
        }

        /// <summary>
        /// returns an flight by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Flights/{o}/{d}")]
        [HttpGet]
        public HttpResponseMessage GetFlightByOD(string o, string d)
        {
            var ent = QueryManager.GetInstance().GetFlightbyod(o,d);
            if (ent != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ent);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "El vuelo no existe.");
            }
        }
    }
    
}
