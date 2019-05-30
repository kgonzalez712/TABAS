using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class EmployeeAction
    {
        //Metodo para obtener todos los empleados de la base de datos
        public List<Employee> getAllEmployee()
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("SELECT * FROM Employee E JOIN EmployeeXRole ExR ON E.EmployeeID=ExR.EmployeeID JOIN ROLES R ON R.ROLEID=ExR.ROLEID", conn);
            read = command.ExecuteReader();

            List<Employee> employees = new List<Employee>();
            while (read.Read())
            {

                Employee c = new Employee();

                c.ID = Convert.ToInt32(read["EmployeeID"]);
                c.FNAME = read["FName"].ToString();
                c.LNAME = read["LName"].ToString();
                c.PASSWRD = read["Passwrd"].ToString();
                c.ROLEID = Convert.ToInt32(read["ROLEID"]);
                c.ROLENAME = read["RNAME"].ToString();

                employees.Add(c);
            }
            read.Close();
            conn.Close();
            return employees;
        }

        //Metodo que obtine id y password de un empleado de la base de datos para realizar el login
        public LoginInfo getEmployeeInfo(int id)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("SELECT EmployeeID,Passwrd FROM Employee WHERE EmployeeID=" + id.ToString(), conn);
            read = command.ExecuteReader();


            LoginInfo c = new LoginInfo();

            while (read.Read())
            {

                c.ID = Convert.ToInt32(read["EmployeeID"]);
                c.PASSWRD = read["Passwrd"].ToString();

            }
            read.Close();
            conn.Close();
            return c;
        }


        //Metodo para insertar un empleado a la base de datos
        public void insertEmployee(int id, string passwrd, string fname, string lname, int roleid)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("INSERT INTO Employee(EmployeeID,Passwrd,FName,LName) VALUES(" + id.ToString() + ",'" + passwrd.ToString() + "','" +
                fname.ToString() + "','" + lname.ToString() + "') INSERT INTO EmployeeXRole(EmployeeID,ROLEID) VALUES (" + id.ToString() + "," + roleid.ToString() + ")", conn);
            read = command.ExecuteReader();

            read.Close();
            conn.Close();

        }


        //Metodo para obtener todos los empleados de un rol de la base de datos
        public List<Employee> getEmployeeByROLE(int roleid)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("SELECT * FROM Employee E JOIN EmployeeXRole ExR ON E.EmployeeID=ExR.EmployeeID JOIN ROLES R ON R.ROLEID=ExR.ROLEID WHERE ExR.ROLEID=" + roleid.ToString(), conn);
            read = command.ExecuteReader();

            List<Employee> employees = new List<Employee>();
            while (read.Read())
            {

                Employee e = new Employee();

                e.ID = Convert.ToInt32(read["EmployeeID"]);
                e.FNAME = read["FName"].ToString();
                e.LNAME = read["LName"].ToString();
                e.ROLENAME = read["RNAME"].ToString();
                e.ROLEID = Convert.ToInt32(read["ROLEID"]);

                employees.Add(e);
            }

            read.Close();
            conn.Close();
            return employees;
        }


        //Metodo para obtener un empleado dado su id de la base de datos
        public Employee getEmployeeByID(int id)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("SELECT * FROM Employee E JOIN EmployeeXRole ExR ON E.EmployeeID=ExR.EmployeeID JOIN ROLES R ON R.ROLEID=ExR.ROLEID  WHERE EmployeeID=" + id.ToString(), conn);
            read = command.ExecuteReader();


            Employee c = new Employee();

            while (read.Read())
            {

                c.ID = Convert.ToInt32(read["EmployeeID"]);
                c.FNAME = read["FName"].ToString();
                c.LNAME = read["LName"].ToString();
                c.ROLENAME = read["RNAME"].ToString();
                c.ROLEID = Convert.ToInt32(read["ROLEID"]);

            }
            read.Close();
            conn.Close();
            return c;
        }
    }
}