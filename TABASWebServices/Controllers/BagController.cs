using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TABASWebServices.Models;

namespace TABASWebServices.Controllers
{
    public class BagController : ApiController
    {
        /// <summary>
        /// Returns all the bags from the database
        /// </summary>
        /// <returns></returns>
        [Route("Bags")]
        [HttpGet]
        public List<Bag> GetBags()
        {
            return QueryManager.GetInstance().GetBags();
        }

        /// <summary>
        /// returns an airport by its iata code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Bags/{id}")]
        [HttpGet]
        public HttpResponseMessage GetBrandByid(int id)
        {
            var ent = QueryManager.GetInstance().GetBagbyid(id);
            if (ent != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ent);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "La maleta con identificación " + id + "no existe.");
            }
        }


        /// <summary>
        /// inserts a new Brand
        /// </summary>
        /// <param name="bag"></param>
        /// <returns></returns>
        [Route("Bags/AddBag")]
        [HttpPost]
        public HttpResponseMessage PostFlight([FromBody] Bag bag)
        {
            try
            {
                QueryManager.GetInstance().Insert_Bag(bag);
                var message = Request.CreateResponse(HttpStatusCode.Created, bag);
                message.Headers.Location = new Uri(Request.RequestUri + bag.BAG_ID.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
