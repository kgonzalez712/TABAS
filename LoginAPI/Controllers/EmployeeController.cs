using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoginAPI.Models;

namespace LoginAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        //Metodo para obtener todos los empleados
        [HttpGet]
        [Route("Employees")]
        public IHttpActionResult GetAllEmployees()
        {
            EmployeeAction emp = new EmployeeAction();
            return Ok(emp.getAllEmployee());
        }


        //Metodo para obtener todos los empleados de un rol
        [HttpGet]
        [Route("Employees/Role/{idrole}")]
        public IHttpActionResult getEmployeeByRole(int idrole)
        {
            EmployeeAction con = new EmployeeAction();
            return Ok(con.getEmployeeByROLE(idrole));
        }


        //Metodo para obtener un empleado dado su id
        [HttpGet]
        [Route("Employees/{id}")]
        public IHttpActionResult getEmployeeByID(int id)
        {
            EmployeeAction con = new EmployeeAction();
            return Ok(con.getEmployeeByID(id));
        }

        //Metodo para el login 
        [HttpGet]
        [Route("Login/{id}/{password}")]
        public IHttpActionResult Login(int id, string password)
        {
            EmployeeAction con = new EmployeeAction();
            LoginInfo login = con.getEmployeeInfo(id);

            if (login.PASSWRD == password)
            {
                return Ok(id.ToString());
            }
            else
            {
                return Ok("Error");
            }
        }

        //Metodo para insertar un empleado
        [HttpPost]
        [Route("Employees/AddEmployee")]
        public void insertEmployee([FromBody] Employee emp)
        {
            EmployeeAction con = new EmployeeAction();
            con.insertEmployee(emp.ID, emp.PASSWRD, emp.FNAME, emp.LNAME, emp.ROLEID);
        }
    }
}
