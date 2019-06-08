using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TABASWebServices.Models;

namespace TABASWebServices.Controllers
{
    public class BrandController : ApiController
    {
        /// <summary>
        /// Returns all the airports from the database
        /// </summary>
        /// <returns></returns>
        [Route("Brands")]
        [HttpGet]
        public List<Brand> GetBrands()
        {
            return QueryManager.GetInstance().GetBrands();
        }

        /// <summary>
        /// returns an airport by its iata code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Brands/{id}")]
        [HttpGet]
        public HttpResponseMessage GetBrandByid(int id)
        {
            var ent = QueryManager.GetInstance().GetBrandbyid(id);
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
        [Route("Brands/AddBrand")]
        [HttpPost]
        public HttpResponseMessage PostFlight([FromBody] Brand brand)
        {
            try
            {
                QueryManager.GetInstance().Insert_Brand(brand);
                var message = Request.CreateResponse(HttpStatusCode.Created, brand);
                message.Headers.Location = new Uri(Request.RequestUri + brand.BRAND_ID.ToString());
                return message;
            
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
