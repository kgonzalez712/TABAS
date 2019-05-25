using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TABASWebServices.Models;

namespace TABASWebServices.Controllers
{
    public class AirplanesController : ApiController
    {
        /// <summary>
        /// Returns all the airplanes from the database
        /// </summary>
        /// <returns></returns>
        [Route("Airplanes")]
        [HttpGet]
        public List<Airplane> GetAirplanes()
        {
            return QueryManager.GetInstance().GetAirplanes();
        }

        /// <summary>
        /// returns an airplane by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Airplanes/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAirplaneByid(int id)
        {
            var ent = QueryManager.GetInstance().GetAirplanebyid(id);
            if (ent != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ent);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "El avión con identificación " + id + "no existe.");
            }
        }
    }
}
