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
        /// Returns all the bags from the database
        /// </summary>
        /// <returns></returns>
        [Route("Bags/Client/{id}")]
        [HttpGet]
        public List<Bag> GetCBags(string id)
        {
            return QueryManager.GetInstance().GetClientsBags(id);
        }

        /// <summary>
        /// Returns all the bags from the database
        /// </summary>
        /// <returns></returns>
        [Route("Bags/Bagcart/{id}")]
        [HttpGet]
        public List<Bag> GetBCBags(int id)
        {
            return QueryManager.GetInstance().GetBagsBC(id);
        }

        /// <summary>
        /// Returns all the bags from the database
        /// </summary>
        /// <returns></returns>
        [Route("Bags/Acepted")]
        [HttpGet]
        public List<Bag> GetABags()
        {
            return QueryManager.GetInstance().GetAceptedBags();
        }

        /// <summary>
        /// Returns all the bags from the database
        /// </summary>
        /// <returns></returns>
        [Route("Bags/Bagcart/{id}/Acepted")]
        [HttpGet]
        public List<Bag> GetABCBags(int id)
        {
            return QueryManager.GetInstance().GetAceptedBagsBC(id);
        }

        /// <summary>
        /// Returns all the bags from the database
        /// </summary>
        /// <returns></returns>
        [Route("Bags/Denied")]
        [HttpGet]
        public List<Bag> GetDBags()
        {
            return QueryManager.GetInstance().GetDenniedBags();
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
