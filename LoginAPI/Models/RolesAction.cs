using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class RolesAction
    {
        //Metodo para insertar un rol a la base de datos
        public void insertRole(string Rname)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("INSERT INTO ROLES(RNAME,ROLEID) VALUES ('" + Rname.ToString() + "',(SELECT COUNT(ROLEID) FROM ROLES))", conn);
            read = command.ExecuteReader();

            read.Close();
            conn.Close();
        }

        //Metodo para obtener todos los roles
        public List<Role> getAllRoles()
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("SELECT * FROM ROLES", conn);
            read = command.ExecuteReader();

            List<Role> roles = new List<Role>();

            while (read.Read())
            {
                Role r = new Role();

                r.RNAME = read["RNAME"].ToString();
                r.ROLEID = Convert.ToInt32(read["ROLEID"]);

                roles.Add(r);
            }

            read.Close();
            conn.Close();

            return roles;
        }
    }
}