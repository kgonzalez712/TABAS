using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoginAPI.Models;

namespace LoginAPI.Controllers
{
    public class UniversityController : ApiController
    {
        //metodo para insertar una nueva universidad
        [HttpPost]
        [Route("Univerities/AddUniversity")]
        public void isertUniversity([FromBody] University u)
        {
            UniversityActions con = new UniversityActions();
            con.insertUniversity(u.UNIVERSITY_NAME);
        }

        //metodo para obtener todas las universidades
        [HttpGet]
        [Route("Universities")]
        public IHttpActionResult getAllUniversity()
        {
            UniversityActions con = new UniversityActions();
            return Ok(con.AllUniversity());
        }

        //Metodo para obtener una universidad dado su id
        [HttpGet]
        [Route("Universities/{id}")]
        public IHttpActionResult getUniversityByID(int id)
        {
            UniversityActions con = new UniversityActions();
            return Ok(con.UniversityByID(id));
        }
    }
}
