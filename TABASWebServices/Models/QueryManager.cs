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



        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE PLANE_TYPE ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// This methods returns all the rows on the Plane Type Table  from te PostgreSQL Database
        /// </summary>
        /// <returns></returns>
        public List<Plane_Type> GetPlaneTypes()
        {
            var rlist = new List<Plane_Type>();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM PLANE_TYPES";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        var result = new Plane_Type();
                        result.TYPE_ID = (int)lector["TYPE_ID"];
                        result.MODEL = (string)lector["MODEL"];
                        result.STORAGE_BINS = (int)lector["STORAGE_BINS"];
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
        /// This method returns an Plane Type from the PostgreSQL Database by its id
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Plane_Type GetPlaneTypebyid(int a)
        {
            var result = new Plane_Type();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM AIRPORT WHERE TYPE_D =" + "'" + a + "'";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.TYPE_ID = (int)lector["TYPE_ID"];
                        result.MODEL = (string)lector["MODEL"];
                        result.STORAGE_BINS = (int)lector["STATE_CODE"];
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


        /// <summary>
        /// This methods returns all the rows on the Airplane Detail View  from te PostgreSQL Database
        /// </summary>
        /// <returns></returns>
        public List<Airplane> GetAirplanes()
        {
            var rlist = new List<Airplane>();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM AIRPLANE_DETAILS";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        var result = new Airplane();
                        result.AIRPLANE_ID = (int)lector["PLANE_ID"];
                        result.PILOT_FULLNAME = (string) lector["PILOT_FULLNAME"];
                        result.MODEL = (string)lector["MODEL"];
                        result.STORAGE_BINS = (int)lector["STORAGE_BINS"];
                        result.CAPACITY = (int)lector["CAPACITY"];
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
        /// This method returns an Airplane from the PostgreSQL Database by its ID 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Airplane GetAirplanebyid(int a)
        {
            var result = new Airplane();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM AIRPLANE_DETAILS WHERE AIRPLANE_ID =" + "'" + a + "'";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.AIRPLANE_ID = (int)lector["PLANE_ID"];
                        result.PILOT_FULLNAME = (string)lector["PILOT_FULLNAME"];
                        result.MODEL = (string)lector["MODEL"];
                        result.STORAGE_BINS = (int)lector["STORAGE_BINS"];
                        result.CAPACITY = (int)lector["CAPACITY"];
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


        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE FLIGHT ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// This methods returns all the rows on the Flight Info View  from te PostgreSQL Database
        /// </summary>
        /// <returns></returns>
        public List<Flight> GetFlights()
        {
            var rlist = new List<Flight>();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM FLIGHT_INFO";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        var result = new Flight();
                        result.FLIGHT_ID = (int)lector["FLIGHT_ID"];
                        result.ORIGIN_AIRPORT = (string)lector["ORIGIN_AIRPORT"];
                        result.DESTINATION_AIRPORT = (string)lector["DESTINATION_AIRPORT"];
                        result.DEPARTURE_DATE = lector["DEPARTURE_DATE"].ToString();
                        result.DEPARTURE_HOUR = lector["DEPARTURE_HOUR"].ToString();
                        result.PRICE = (int)lector["PRICE"];
                        result.PLANE_ID = (int)lector["PLANE_ID"];
                        result.BAG_CAPACITY = (int)lector["BAG_CAPACITY"];
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
        /// This method returns an Flight from the PostgreSQL Database by its ID 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Flight GetFlightbyid(int a)
        {
            var result = new Flight();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM FLIGHT_INFO WHERE FLIGHT_ID =" + "'" + a + "'";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.FLIGHT_ID = (int)lector["FLIGHT_ID"];
                        result.ORIGIN_AIRPORT = (string)lector["ORIGIN_AIRPORT"];
                        result.DESTINATION_AIRPORT = (string)lector["DESTINATION_AIRPORT"];
                        result.DEPARTURE_DATE = lector["DEPARTURE_DATE"].ToString();
                        result.DEPARTURE_HOUR = lector["DEPARTURE_HOUR"].ToString();
                        result.PRICE = (int)lector["PRICE"];
                        result.PLANE_ID = (int)lector["PLANE_ID"];
                        result.BAG_CAPACITY = (int)lector["BAG_CAPACITY"];
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


        /// <summary>
        /// This method returns an Flight from the PostgreSQL Database by its origin and destiny 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Flight GetFlightbyod(string o, string d)
        {
            var result = new Flight();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM FLIGHT_INFO WHERE ORIGIN_AIRPORT =" + "'" + o + "'" + " AND DESTINATION_AIRPORT =" + "'" + d + "'";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.FLIGHT_ID = (int)lector["FLIGHT_ID"];
                        result.ORIGIN_AIRPORT = (string)lector["ORIGIN_AIRPORT"];
                        result.DESTINATION_AIRPORT = (string)lector["DESTINATION_AIRPORT"];
                        result.DEPARTURE_DATE = lector["DEPARTURE_DATE"].ToString();
                        result.DEPARTURE_HOUR = lector["DEPARTURE_HOUR"].ToString();
                        result.PRICE = (int)lector["PRICE"];
                        result.PLANE_ID = (int)lector["PLANE_ID"];
                        result.BAG_CAPACITY = (int)lector["BAG_CAPACITY"];
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

        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE BRAND ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}