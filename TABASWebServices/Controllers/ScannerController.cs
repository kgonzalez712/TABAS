using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TABASWebServices.Models;

namespace TABASWebServices.Controllers
{
    public class ScannerController : ApiController
    {
        /// <summary>
        /// Returns all the bags from the database
        /// </summary>
        /// <returns></returns>
        [Route("Scans")]
        [HttpGet]
        public List<Scanner> GetScans()
        {
            return QueryManager.GetInstance().GetScannings();
        }

        /// <summary>
        /// returns an Scan by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("Scans/{id}")]
        [HttpGet]
        public HttpResponseMessage GetScanByid(int id)
        {
            var ent = QueryManager.GetInstance().GetScanbyid(id);
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
        [Route("Scans/Supervisor/{id}")]
        [HttpGet]
        public HttpResponseMessage GetScanBySuper(string id)
        {
            var ent = QueryManager.GetInstance().GetScanbySuper(id);
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
        [Route("Scans/AddScan")]
        [HttpPost]
        public HttpResponseMessage PostFlight([FromBody] Scanner scan)
        {
            try
            {
                QueryManager.GetInstance().Insert_Scanning(scan);
                var message = Request.CreateResponse(HttpStatusCode.Created, scan);
                message.Headers.Location = new Uri(Request.RequestUri + scan.SCAN_ID.ToString());
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
        [Route("Scans/Bag/{id}/A")]
        [HttpPut]
        public void UpdateBagStatus(int id)
        {
            try
            {
                QueryManager.GetInstance().UpdateScAStatus(id);
                var message = Request.CreateResponse(HttpStatusCode.Accepted);

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Modifies a status
        /// </summary>
        /// <param name="shp"></param>
        /// <returns></returns>
        [Route("Scans/Bag/{id}/D")]
        [HttpPut]
        public void UpdateBagStatusD(int id)
        {
            try
            {
                QueryManager.GetInstance().UpdateScDStatus(id);
                var message = Request.CreateResponse(HttpStatusCode.Accepted);

            }
            catch (Exception ex)
            {
            }
        }
    }
}
