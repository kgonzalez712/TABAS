using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LoginAPI.Models
{
    public class ClientAction
    {
        //Metodo para insertar un usuario a la base de datos
        public void insertClient(string id, string fn, string ln, string uid, string phone, string cardno, int miles, string email, string password, int universityid)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;
            if (uid == "")
            {
                //conn = new SqlConnection("Data Source=DESKTOP-5SKOEVC;Initial Catalog=LOGINDB;Integrated Security=True; User ID=sa;Password=12345");

                conn = new SqlConnection("Server=tcp:tabaslog.database.windows.net,1433;Initial Catalog=tabaslgin;Persist Security Info=False;User ID=Kevin;Password=CE071295tec;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();
                command = new SqlCommand("INSERT INTO CLIENT(CLIENT_ID,CLIENT_FNAME,CLIENT_LNAME,CLIENT_PHONENO,CLIENT_CARDNO,CLIENT_MILES,CLIENT_EMAIL,CLIENT_PASSWRD) VALUES('" + id.ToString() + "','" + fn.ToString() + "','" + ln.ToString() + "','" +
                    phone.ToString() + "','" + cardno.ToString() + "','" + miles.ToString() + "','" + email.ToString() + "','" + password.ToString() + "')", conn);
                read = command.ExecuteReader();

                read.Close();
                conn.Close();
            }
            else
            {
                conn = new SqlConnection("Server=tcp:tabaslog.database.windows.net,1433;Initial Catalog=tabaslgin;Persist Security Info=False;User ID=Kevin;Password=CE071295tec;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                conn.Open();
                command = new SqlCommand("INSERT INTO CLIENT(CLIENT_ID,CLIENT_FNAME,CLIENT_LNAME,CLIENT_PHONENO,CLIENT_CARDNO,CLIENT_MILES,CLIENT_EMAIL,CLIENT_PASSWRD) VALUES('" + id.ToString() + "','" + fn.ToString() + "','" + ln.ToString() + "','" +
                    phone.ToString() + "','" + cardno.ToString() + "','" + miles.ToString() + "','" + email.ToString() + "','" + password.ToString() + "') INSERT INTO ClientXUniversity(CLIENT_ID,UNIVERSITY_ID,CLIENT_UID) VALUES ('" + id.ToString() + "','" + universityid.ToString() + "','" + uid.ToString() + "')", conn);
                read = command.ExecuteReader();

                read.Close();
                conn.Close();
            }
        }


        //Metodo para obtener todos los empleados de la base de datos
        public List<Client> getAllClient()
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Server=tcp:tabaslog.database.windows.net,1433;Initial Catalog=tabaslgin;Persist Security Info=False;User ID=Kevin;Password=CE071295tec;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            conn.Open();
            command = new SqlCommand("SELECT * FROM CLIENT C LEFT JOIN ClientXUniversity CU ON C.CLIENT_ID=CU.CLIENT_ID LEFT JOIN UNIVERSITY U ON U.UNIVERSITY_ID=CU.UNIVERSITY_ID", conn);
            read = command.ExecuteReader();

            List<Client> clients = new List<Client>();
            while (read.Read())
            {
                if (read["CLIENT_UID"] == System.DBNull.Value)
                {
                    Client c = new Client();

                    c.ID = read["CLIENT_ID"].ToString();
                    c.FNAME = read["CLIENT_FNAME"].ToString();
                    c.LNAME = read["CLIENT_LNAME"].ToString();
                    c.PHONENO = read["CLIENT_PHONENO"].ToString();
                    c.CARDNO = read["CLIENT_CARDNO"].ToString();
                    c.MILES = Convert.ToInt32(read["CLIENT_MILES"]);
                    c.EMAIL = read["CLIENT_EMAIL"].ToString();
                    c.PASSWRD = read["CLIENT_PASSWRD"].ToString();

                    clients.Add(c);
                }
                else
                {
                    Client c = new Client();

                    c.ID = read["CLIENT_ID"].ToString();
                    c.FNAME = read["CLIENT_FNAME"].ToString();
                    c.LNAME = read["CLIENT_LNAME"].ToString();
                    c.PHONENO = read["CLIENT_PHONENO"].ToString();
                    c.CARDNO = read["CLIENT_CARDNO"].ToString();
                    c.MILES = Convert.ToInt32(read["CLIENT_MILES"]);
                    c.EMAIL = read["CLIENT_EMAIL"].ToString();
                    c.PASSWRD = read["CLIENT_PASSWRD"].ToString();
                    c.UID = read["CLIENT_UID"].ToString();
                    c.UNIVERSITY_ID = Convert.ToInt32(read["UNIVERSITY_ID"]);

                    clients.Add(c);
                }
            }
            read.Close();
            conn.Close();
            return clients;
        }


        //metodo para obtener todos los clientes no estudiantes de la base de datos
        public List<Client> getNotStudent()
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Server=tcp:tabaslog.database.windows.net,1433;Initial Catalog=tabaslgin;Persist Security Info=False;User ID=Kevin;Password=CE071295tec;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            conn.Open();
            command = new SqlCommand("SELECT C.CLIENT_CARDNO, C.CLIENT_EMAIL,C.CLIENT_FNAME,C.CLIENT_ID,C.CLIENT_LNAME,C.CLIENT_MILES,C.CLIENT_PASSWRD,C.CLIENT_PHONENO FROM CLIENT C LEFT JOIN ClientXUniversity CxU ON C.CLIENT_ID = CxU.CLIENT_ID WHERE CxU.CLIENT_UID IS NULL", conn);
            read = command.ExecuteReader();

            List<Client> clients = new List<Client>();
            while (read.Read())
            {

                Client c = new Client();

                c.ID = read["CLIENT_ID"].ToString();
                c.FNAME = read["CLIENT_FNAME"].ToString();
                c.LNAME = read["CLIENT_LNAME"].ToString();
                c.PHONENO = read["CLIENT_PHONENO"].ToString();
                c.CARDNO = read["CLIENT_CARDNO"].ToString();
                c.MILES = Convert.ToInt32(read["CLIENT_MILES"]);
                c.EMAIL = read["CLIENT_EMAIL"].ToString();
                c.PASSWRD = read["CLIENT_PASSWRD"].ToString();

                clients.Add(c);
            }
            read.Close();
            conn.Close();
            return clients;
        }

        //metodo para obtener todos los clientes estudiantes en la base de datos
        public List<Client> getStudent()
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("SELECT * FROM CLIENT C JOIN ClientXUniversity CxU ON C.CLIENT_ID = CxU.CLIENT_ID", conn);
            read = command.ExecuteReader();

            List<Client> clients = new List<Client>();
            while (read.Read())
            {

                Client c = new Client();

                c.ID = read["CLIENT_ID"].ToString();
                c.FNAME = read["CLIENT_FNAME"].ToString();
                c.LNAME = read["CLIENT_LNAME"].ToString();
                c.PHONENO = read["CLIENT_PHONENO"].ToString();
                c.CARDNO = read["CLIENT_CARDNO"].ToString();
                c.MILES = Convert.ToInt32(read["CLIENT_MILES"]);
                c.EMAIL = read["CLIENT_EMAIL"].ToString();
                c.PASSWRD = read["CLIENT_PASSWRD"].ToString();

                clients.Add(c);
            }
            read.Close();
            conn.Close();
            return clients;
        }

        //Metodo para obtener un empleado dado su id de la base de datos
        public Client getClientByID(string id)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("SELECT * FROM CLIENT C WHERE C.CLIENT_ID='" + id.ToString() + "'", conn);
            read = command.ExecuteReader();


            Client c = new Client();
            while (read.Read())
            {

                c.ID = read["CLIENT_ID"].ToString();
                c.FNAME = read["CLIENT_FNAME"].ToString();
                c.LNAME = read["CLIENT_LNAME"].ToString();
                c.PHONENO = read["CLIENT_PHONENO"].ToString();
                c.CARDNO = read["CLIENT_CARDNO"].ToString();
                c.MILES = Convert.ToInt32(read["CLIENT_MILES"]);
                c.EMAIL = read["CLIENT_EMAIL"].ToString();
                c.PASSWRD = read["CLIENT_PASSWRD"].ToString();

            }
            read.Close();
            conn.Close();
            return c;
        }

        //Metodo para agregar millas a un cliente en la base de datos
        public void addMiles(string id, int miles)
        {
            SqlConnection conn;
            SqlCommand command;
            SqlDataReader read;

            conn = new SqlConnection("Data Source=(local);Initial Catalog=LOGINDB;Integrated Security=True");
            conn.Open();
            command = new SqlCommand("UPDATE CLIENT SET CLIENT_MILES=CLIENT_MILES+" + miles.ToString() + " WHERE CLIENT_ID='" + id.ToString() + "'", conn);
            read = command.ExecuteReader();

            read.Close();
            conn.Close();
        }
    }
}