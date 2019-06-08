using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TABASWebServices.Models;

namespace TABASWebServices.Controllers
{
    public class ShipperController : ApiController
    {
        /// <summary>
        /// Returns all the bags from the database
        /// </summary>
        /// <returns></returns>
        [Route("Shipping")]
        [HttpGet]
        public List<Shipper> GetShipping()
        {
            return QueryManager.GetInstance().GetShippings();
        }

        /// <summary>
        /// returns an Scan by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Shipping/{id}")]
        [HttpGet]
        public HttpResponseMessage GetShippByid(int id)
        {
            var ent = QueryManager.GetInstance().GetShippingbyid(id);
            if (ent != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ent);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "El escaneo de maleta con identificación " + id + "no existe.");
            }
        }

        /// <summary>
        /// returns an Scan by its supervisor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Shipping/Supervisor/{id}")]
        [HttpGet]
        public HttpResponseMessage GetShipBySuper(string id)
        {
            var ent = QueryManager.GetInstance().GetShippingbySuper(id);
            if (ent != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ent);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    "El supervisor de maleta con identificación " + id + "no existe.");
            }
        }


        /// <summary>
        /// inserts a new Brand
        /// </summary>
        /// <param name="bag"></param>
        /// <returns></returns>
        [Route("Shipping/AddShipping")]
        [HttpPost]
        public HttpResponseMessage PostFlight([FromBody] Shipper shp)
        {
            try
            {
                QueryManager.GetInstance().Insert_Shipping(shp);
                var message = Request.CreateResponse(HttpStatusCode.Created, shp);
                message.Headers.Location = new Uri(Request.RequestUri + shp.SHIPPING_ID.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// Modifies a status
        /// </summary>
        /// <param name="shp"></param>
        /// <returns></returns>
        [Route("Shipping/Bag/{id}")]
        [HttpPut]
        public void UpdateBagStatus(int id )
        {
            try
            {
                QueryManager.GetInstance().UpdateShStatus(id);
                var message = Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
            }
        }

        [Route("setBin/{id}/{value}")]
        [HttpPut]
        public void UpdateBagBinStatus(int id, string value)
        {
            try
            {
                QueryManager.GetInstance().UpdatePbStatus(id,value);
                var message = Request.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
            }
        }

    }
}
