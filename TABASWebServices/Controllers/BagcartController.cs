using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TABASWebServices.Models;

namespace TABASWebServices.Controllers
{
    public class BagcartController : ApiController
    {
        /// <summary>
        /// Returns all the airports from the database
        /// </summary>
        /// <returns></returns>
        [Route("Bagcarts")]
        [HttpGet]
        public List<Bagcart> GetBrands()
        {
            return QueryManager.GetInstance().GetBagCarts();
        }

        /// <summary>
        /// returns an airport by its iata code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Bagcarts/{id}")]
        [HttpGet]
        public HttpResponseMessage GetBrandByid(int id)
        {
            var ent = QueryManager.GetInstance().GetBagcartbyid(id);
            if (ent != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ent);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "La marca con identificación " + id + "no existe.");
            }
        }


        /// <summary>
        /// inserts a new Brand
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        [Route("Bagcarts/AddBagcart")]
        [HttpPost]
        public HttpResponseMessage PostFlight([FromBody] Bagcart bagcart)
        {
            try
            {
                QueryManager.GetInstance().Insert_Bagcart(bagcart);
                var message = Request.CreateResponse(HttpStatusCode.Created, bagcart);
                message.Headers.Location = new Uri(Request.RequestUri + bagcart.BAGCART_ID.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
