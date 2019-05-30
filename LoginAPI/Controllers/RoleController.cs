using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoginAPI.Models;

namespace LoginAPI.Controllers
{
    public class RoleController : ApiController
    {

        //Metodo para insertar un rol
        [HttpPost]
        [Route("Roles/AddRole")]
        public void insertRole([FromBody] Role role)
        {
            RolesAction con = new RolesAction();
            con.insertRole(role.RNAME);
        }

        //Metodo para obtener todos los roles
        [HttpGet]
        [Route("Roles")]
        public IHttpActionResult getAllRole()
        {
            RolesAction con = new RolesAction();
            return Ok(con.getAllRoles());
        }
    }

}
