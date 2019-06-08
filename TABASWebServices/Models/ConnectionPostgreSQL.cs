using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;

namespace TABASWebServices.Models
{
    /// <summary>
    /// This class is in charge of managing the connection between the API and the TABAS PostgreSQL Database
    /// </summary>
    public class ConnectionPostgreSQL
    {
        /// <summary>
        /// this method opens the connection to the database
        /// </summary>
        /// <returns> A connection to the PostgreSQL Database </returns>
        public NpgsqlConnection OpenConnection()
        {
            NpgsqlConnection con = new NpgsqlConnection();
            var constr ="Server = tabasdb.postgres.database.azure.com; Database =TABAS; Port = 5432; User Id = Kevin@tabasdb; Password =CE07121995tec; Ssl Mode = Require";
            // var constr = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=12345;Database=TABAS";

            if (!string.IsNullOrWhiteSpace(constr))
            {
                try
                {
                    con = new NpgsqlConnection(constr);
                    con.Open();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error en la conexion");
                    con.Close();
                    throw;
                }
            }

            return con;
        }
    }
}