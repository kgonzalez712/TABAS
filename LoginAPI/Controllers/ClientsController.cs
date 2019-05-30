using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoginAPI.Models;

namespace LoginAPI.Controllers
{
    public class ClientsController : ApiController
    {
        //Metodo para insertar un usuario
        [HttpPost]
        [Route("Clients/AddClient")]
        public void insertClient([FromBody] Client client)
        {
            ClientAction con = new ClientAction();
            con.insertClient(client.ID, client.FNAME, client.LNAME, client.UID, client.PHONENO, client.CARDNO, client.MILES, client.EMAIL, client.PASSWRD, client.UNIVERSITY_ID);
        }

        //Metodo para obtener todos los usuarios
        [HttpGet]
        [Route("Clients")]
        public IHttpActionResult getAllEmployees()
        {
            ClientAction con = new ClientAction();
            return Ok(con.getAllClient());
        }

        //Metodo para obtener un usuario dado su id
        [HttpGet]
        [Route("Clients/{id}")]
        public IHttpActionResult getClientByID(string id)
        {
            ClientAction con = new ClientAction();
            return Ok(con.getClientByID(id));
        }
    }
}
