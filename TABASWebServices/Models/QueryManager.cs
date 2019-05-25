using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using NpgsqlTypes;

namespace TABASWebServices.Models
{

    /// <summary>
    /// This class is in charge of all the querys that are going to be executed on the PostgreSQL Databse
    /// </summary>
    public class QueryManager
    {
        static QueryManager queryManager = null;


        /// <summary>
        /// This method verifies that only one instance of QueryManager is running
        /// </summary>
        /// <returns></returns>
        public static QueryManager GetInstance()
        {
            if (queryManager == null)
            {
                queryManager = new QueryManager();
                return queryManager;
            }
            else
            {
                return queryManager;
            }
        }

        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE AIRPORT ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// / This method inserts a new row in the Airport Table on the PostgreSQL Database
        /// </summary>
        /// <param name="airport"></param>
        public void Insert_Airport(Airport airport)
        {
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;
            int res;


            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT INSERT_AIRPORT(@ANAME,@ICODE,@SCODE,@CCODE,@CNAME)";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    comando.Parameters.Add("@ANAME", NpgsqlDbType.Varchar, 50).Value = airport.AIRPORT_NAME;
                    comando.Parameters.Add("@ICODE", NpgsqlDbType.Char, 3).Value = airport.IATA_CODE;
                    comando.Parameters.Add("@SCODE", NpgsqlDbType.Char, 2).Value = airport.STATE_CODE;
                    comando.Parameters.Add("@CCODE", NpgsqlDbType.Char, 2).Value = airport.COUNTRY_CODE;
                    comando.Parameters.Add("@CNAME", NpgsqlDbType.Varchar, 50).Value = airport.COUNTRY_NAME;
                    res = comando.ExecuteNonQuery();
                    if (res > 0)
                    {
                        Console.WriteLine("Insert completed");
                    }
                    else
                    {
                        Console.WriteLine("Insert operation could not be completed");
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

        }

        /// <summary>
        /// This methods returns all the rows on the Airport Table  from te PostgreSQL Database
        /// </summary>
        /// <returns></returns>
        public List<Airport> GetAirports()
        {
            var rlist = new List<Airport>();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM AIRPORT";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        var result = new Airport();
                        result.AIRPORT_NAME = (string)lector["AIRPORT_NAME"];
                        result.IATA_CODE = (string)lector["IATA_CODE"];
                        result.STATE_CODE = (string)lector["STATE_CODE"];
                        result.COUNTRY_CODE = (string)lector["COUNTRY_CODE"];
                        result.COUNTRY_NAME = (string)lector["COUNTRY_NAME"];
                        rlist.Add(result);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return rlist;
        }


        /// <summary>
        /// This method returns an Airport from the PostgreSQL Database by its IATA Code
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Airport GetAirportsbyid(string a)
        {
            var result = new Airport();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM AIRPORT WHERE IATA_CODE =" + "'" + a + "'";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.AIRPORT_NAME = (string)lector["AIRPORT_NAME"];
                        result.IATA_CODE = (string)lector["IATA_CODE"];
                        result.STATE_CODE = (string)lector["STATE_CODE"];
                        result.COUNTRY_CODE = (string)lector["COUNTRY_CODE"];
                        result.COUNTRY_NAME = (string)lector["COUNTRY_NAME"];


                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return result;
        }



        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE AIRPLANE ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////



    }
}