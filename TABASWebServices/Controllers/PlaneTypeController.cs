using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TABASWebServices.Models;

namespace TABASWebServices.Controllers
{
    public class PlaneTypeController : ApiController
    {
        /// <summary>
        /// Returns all the airports from the database
        /// </summary>
        /// <returns></returns>
        [Route("PlaneTypes")]
        [HttpGet]
        public List<Plane_Type> GetAirports()
        {
            return QueryManager.GetInstance().GetPlaneTypes();
        }

        /// <summary>
        /// returns an airport by its iata code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("PlaneTypes/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAirportByid(int id)
        {
            var ent = QueryManager.GetInstance().GetPlaneTypebyid(id);
            if (ent != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ent);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "El tipo de avion con identificación " + id + "no existe.");
            }
        }
    }
}
