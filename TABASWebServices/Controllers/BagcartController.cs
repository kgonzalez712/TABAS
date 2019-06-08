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
        public List<Bagcart> GetBagcarts()
        {
            return QueryManager.GetInstance().GetBagCarts();
        }

        /// <summary>
        /// Returns all the airports from the database
        /// </summary>
        /// <returns></returns>
        [Route("Bagcarts/NF/{id}")]
        [HttpGet]
        public List<Bagcart> GetBagcartsD(int id)
        {
            return QueryManager.GetInstance().GetBagCartsNF(id);
        }

        /// <summary>
        /// returns an airport by its iata code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Bagcarts/{id}")]
        [HttpGet]
        public HttpResponseMessage GetBagcartByid(int id)
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
        public HttpResponseMessage PostBC([FromBody] BC bc)
        {
            try
            {
                QueryManager.GetInstance().Insert_Bagcart(bc);
                var message = Request.CreateResponse(HttpStatusCode.Created, bc);
                message.Headers.Location = new Uri(Request.RequestUri + bc.BAGCART_ID.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
