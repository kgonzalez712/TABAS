using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class UniversityActions
    {
        //metodo para obtener todas las universidades de la base de datos
        public List<University> AllUniversity()
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Server=tcp:tabaslog.database.windows.net,1433;Initial Catalog=tabaslgin;Persist Security Info=False;User ID=Kevin;Password=CE071295tec;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            conn.Open();
            command = new SqlCommand("SELECT * FROM UNIVERSITY WHERE UNIVERSITY_ID>0", conn);
            read = command.ExecuteReader();
            List<University> UList = new List<University>();
            while (read.Read())
            {
                University U = new University();

                U.UNIVERSITY_ID = Convert.ToInt32(read["UNIVERSITY_ID"]);
                U.UNIVERSITY_NAME = read["UNIVERSITY_NAME"].ToString();

                UList.Add(U);
            }
            read.Close();
            conn.Close();
            return UList;
        }

        //Metodo para obtener una universidad dado su id de la base de datos
        public University UniversityByID(int id)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("SELECT * FROM UNIVERSITY WHERE UNIVERSITY_ID=" + id.ToString(), conn);
            read = command.ExecuteReader();

            University U = new University();
            while (read.Read())
            {
                U.UNIVERSITY_ID = Convert.ToInt32(read["UNIVERSITY_ID"]);
                U.UNIVERSITY_NAME = read["UNIVERSITY_NAME"].ToString();
            }

            read.Close();
            conn.Close();
            return U;
        }

        //metodo para insertar una nueva universidad de la base de datos
        public void insertUniversity(string name)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=DESKTOP-5SKOEVC;Initial Catalog=LOGINDB;Integrated Security=True; User ID=sa;Password=12345");
            conn.Open();
            command = new SqlCommand("INSERT INTO UNIVERSITY(UNIVERSITY_NAME) VALUES('" + name.ToString() + "')", conn);
            read = command.ExecuteReader();

            read.Close();
            conn.Close();
        }
    }
}
