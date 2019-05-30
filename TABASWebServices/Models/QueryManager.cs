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
                connstr = "SELECT * FROM PLANE_TYPE";
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
                connstr = "SELECT * FROM PLANE_TYPE WHERE TYPE_D =" + a;
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
                connstr = "SELECT * FROM AIRPLANE_DETAILS WHERE AIRPLANE_ID =" + a;
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
                connstr = "SELECT * FROM FLIGHT_INFO WHERE FLIGHT_ID =" +  a;
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


        /// <summary>
        /// This method returns an Flight from the PostgreSQL Database by its ID 
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int GetFlightPlanebyid(int a)
        {
            var result = new Flight();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT PLANE_ID FROM FLIGHT_INFO WHERE FLIGHT_ID =" + a;
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.PLANE_ID = (int)lector["PLANE_ID"];
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            return result.PLANE_ID;
        }

        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE BRAND ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// / This method inserts a new row in the BRAND Table on the PostgreSQL Database
        /// </summary>
        /// <param name="brand"></param>
        public void Insert_Brand(Brand brand)
        {
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;
            int res;


            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT INSERT_BRAND(@BNAME,@BMODEL)";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    comando.Parameters.Add("@BNAME", NpgsqlDbType.Varchar, 25).Value = brand.BRAND_NAME;
                    comando.Parameters.Add("@BMODEL", NpgsqlDbType.Char, 20).Value = brand.BRAND_MODEL;
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
        /// This methods returns all the rows on the Brand Table  from te PostgreSQL Database
        /// </summary>
        /// <returns></returns>
        public List<Brand> GetBrands()
        {
            var rlist = new List<Brand>();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM BRAND";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        var result = new Brand();
                        result.BRAND_ID = (int)lector["BRAND_ID"];
                        result.BRAND_NAME = (string)lector["BRAND_NAME"];
                        result.BRAND_MODEL = (string)lector["BRAND_MODEL"];
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
        /// Gets a Brand by id from the database
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Brand GetBrandbyid(int a)
        {
            var result = new Brand();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM BRAND WHERE BRAND_ID =" + a;
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.BRAND_ID = (int)lector["BRAND_ID"];
                        result.BRAND_NAME = (string)lector["BRAND_NAME"];
                        result.BRAND_MODEL = (string)lector["BRAND_MODEL"];
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




        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE BAGCART ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// / This method inserts a new row in the BRAND Table on the PostgreSQL Database
        /// </summary>
        /// <param name="brand"></param>
        public void Insert_Bagcart(Bagcart bc)
        {
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;
            int res;


            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT INSERT_BAGCART(@BCID,@C,@QR,@S,@BNAME,@BMODEL,@FID)";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    comando.Parameters.Add("@BCID", NpgsqlDbType.Integer).Value = bc.BAGCART_ID;
                    comando.Parameters.Add("@C", NpgsqlDbType.Integer).Value = bc.CAPACITY;
                    comando.Parameters.Add("@QR", NpgsqlDbType.Varchar, 10).Value = bc.QRCODE;
                    comando.Parameters.Add("@S", NpgsqlDbType.Varchar, 10).Value = bc.STATUS;
                    comando.Parameters.Add("@BNAME", NpgsqlDbType.Varchar, 25).Value = bc.BRAND_NAME;
                    comando.Parameters.Add("@BMODEL", NpgsqlDbType.Varchar, 20).Value = bc.BRAND_MODEL;
                    comando.Parameters.Add("@FID", NpgsqlDbType.Integer).Value = bc.FLIGHT_ID;
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
        /// This methods returns all the rows on the Brand Table  from te PostgreSQL Database
        /// </summary>
        /// <returns></returns>
        public List<Bagcart> GetBagCarts()
        {
            var rlist = new List<Bagcart>();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM BAGCART_DETAILS";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        var result = new Bagcart();
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.CAPACITY = (int) lector["CAPACITY"];
                        result.QRCODE = (string)lector["QRCODE"];
                        result.STATUS = (string)lector["STATUS"];
                        result.BRAND_NAME = (string)lector["BRAND_NAME"];
                        result.BRAND_MODEL = (string)lector["BRAND_MODEL"];
                        result.FLIGHT_ID = (int)lector["FLIGHT_ID"];
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
        /// Gets a Bagcart by its id from the database
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Bagcart GetBagcartbyid(int a)
        {
            var result = new Bagcart();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM BAGCART_DETAILS WHERE BAGCART_ID =" + a;
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.CAPACITY = (int)lector["CAPACITY"];
                        result.QRCODE = (string)lector["QRCODE"];
                        result.STATUS = (string)lector["STATUS"];
                        result.BRAND_NAME = (string)lector["BRAND_NAME"];
                        result.BRAND_MODEL = (string)lector["BRAND_MODEL"];
                        result.FLIGHT_ID = (int)lector["FLIGHT_ID"];
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


        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE BAG ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// / This method inserts a new row in the BRAND Table on the PostgreSQL Database
        /// </summary>
        /// <param name="brand"></param>
        public void Insert_Bag(Bag bag)
        {
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;
            int res;


            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT INSERT_BAG(@COLOUR,@WEIGHT,@PRICE,@BAGCART_ID,@CLIENT_ID)";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    comando.Parameters.Add("@COLOUR", NpgsqlDbType.Varchar,10).Value = bag.COLOUR;
                    comando.Parameters.Add("@WEIGHT", NpgsqlDbType.Double).Value = bag.WEIGHT;
                    comando.Parameters.Add("@PRICE", NpgsqlDbType.Double).Value = bag.PRICE;
                    comando.Parameters.Add("@BAGCART_ID", NpgsqlDbType.Integer).Value = bag.BAGCART_ID;
                    comando.Parameters.Add("@CLIENT_ID", NpgsqlDbType.Integer).Value = bag.CLIENT_ID;
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
        /// This methods returns all the rows on the Brand Table  from te PostgreSQL Database
        /// </summary>
        /// <returns></returns>
        public List<Bag> GetBags()
        {
            var rlist = new List<Bag>();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM BAG";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        var result = new Bag();
                        result.BAG_ID = (int)lector["BAG_ID"];
                        result.COLOUR = (string)lector["COLOUR"];
                        result.WEIGHT = (float)lector["WEIGHT"];
                        result.PRICE = (float)lector["PRICE"];
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.CAPACITY = (int)lector["CAPACITY"];
                        result.CLIENT_ID = (string)lector["CLIENT_ID"];
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
        /// Gets a Bagcart by its id from the database
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Bag GetBagbyid(int a)
        {
            var result = new Bag();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM BAG WHERE BAG_ID =" + a;
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.BAG_ID = (int)lector["BAG_ID"];
                        result.COLOUR = (string)lector["COLOUR"];
                        result.WEIGHT = (float)lector["WEIGHT"];
                        result.PRICE = (float)lector["PRICE"];
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.CAPACITY = (int)lector["CAPACITY"];
                        result.CLIENT_ID = (string)lector["CLIENT_ID"];
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

        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE SHIPPER ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////



        /// <summary>
        /// / This method inserts a new row in the SHIPPING Table on the PostgreSQL Database
        /// </summary>
        /// <param name="shp"></param>
        public void Insert_Shipping(Shipper shp)
        {
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;
            int res;


            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT INSERT_SHIPPING(@D,@H,@ST,@BC ,@B,@SUPER)";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    comando.Parameters.Add("@D", NpgsqlDbType.Date).Value = shp.SHIPPING_ID;
                    comando.Parameters.Add("@H", NpgsqlDbType.Time).Value = shp.SHIPPING_HOUR;
                    comando.Parameters.Add("@ST", NpgsqlDbType.Varchar, 10).Value = shp.STATUS;
                    comando.Parameters.Add("@B", NpgsqlDbType.Integer).Value = shp.BAG_ID;
                    comando.Parameters.Add("@BC", NpgsqlDbType.Integer).Value = shp.BAGCART_ID;
                    comando.Parameters.Add("@SUPER", NpgsqlDbType.Integer).Value = shp.SUPERVISOR;
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
        /// This methods returns all the rows on the Shipper Table  from te PostgreSQL Database
        /// </summary>
        /// <returns></returns>
        public List<Shipper> GetShippings()
        {
            var rlist = new List<Shipper>();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM SHIPPER";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        var result = new Shipper();
                        result.SHIPPING_ID = (int)lector["SHIPPING_ID"];
                        result.SHIPPING_DATE = lector["SHIPPING_DATE"].ToString();
                        result.SHIPPING_HOUR = lector["SHIPPING_HOUR"].ToString();
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.BAG_ID = (int)lector["BAG_ID"];
                        result.SUPERVISOR = (string)lector["SUPERVISOR"];
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
        /// Gets a Shipping by its id from the database
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Shipper GetShippingbyid(int a)
        {
            var result = new Shipper();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM SHIPPER WHERE SHIPPING_ID =" + a;
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.SHIPPING_ID = (int)lector["SHIPPING_ID"];
                        result.SHIPPING_DATE = lector["SHIPPING_DATE"].ToString();
                        result.SHIPPING_HOUR = lector["SHIPPING_HOUR"].ToString();
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.BAG_ID = (int)lector["BAG_ID"];
                        result.SUPERVISOR = (string)lector["SUPERVISOR"];
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
        /// Gets a Shipping by its id from the database
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Shipper GetShippingbySuper(string a)
        {
            var result = new Shipper();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM SHIPPER WHERE SUPERVISOR =" + "'" + a + "'";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.SHIPPING_ID = (int)lector["SHIPPING_ID"];
                        result.SHIPPING_DATE = lector["SHIPPING_DATE"].ToString();
                        result.SHIPPING_HOUR = lector["SHIPPING_HOUR"].ToString();
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.BAG_ID = (int)lector["BAG_ID"];
                        result.SUPERVISOR = (string)lector["SUPERVISOR"];
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




        /////////////////////////////////////////////////////////////// CRUD METHODS RELATED TO THE SCANNER ENTITY //////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// / This method inserts a new row in the SCANNER Table on the PostgreSQL Database
        /// </summary>
        /// <param name="shp"></param>
        public void Insert_Scanning(Scanner sc)
        {
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;
            int res;


            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT INSERT_SCANNER(@D,@H,@ST,@BC ,@B,@SUPER)";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    comando.Parameters.Add("@D", NpgsqlDbType.Date).Value = sc.SCAN_DATE;
                    comando.Parameters.Add("@H", NpgsqlDbType.Time).Value = sc.SCAN_HOUR;
                    comando.Parameters.Add("@ST", NpgsqlDbType.Varchar, 10).Value = sc.STATUS;
                    comando.Parameters.Add("@B", NpgsqlDbType.Integer).Value = sc.BAG_ID;
                    comando.Parameters.Add("@BC", NpgsqlDbType.Integer).Value = sc.BAGCART_ID;
                    comando.Parameters.Add("@SUPER", NpgsqlDbType.Integer).Value = sc.SUPERVISOR;
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
        /// This methods returns all the rows on the Shipper Table  from te PostgreSQL Database
        /// </summary>
        /// <returns></returns>
        public List<Scanner> GetScannings()
        {
            var rlist = new List<Scanner>();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM SCANNER";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    int i = 0;
                    while (lector.Read())
                    {
                        var result = new Scanner();
                        result.SCAN_ID = (int)lector["SCAN_ID"];
                        result.SCAN_DATE = lector["SCAN_DATE"].ToString();
                        result.SCAN_HOUR = lector["SCAN_HOUR"].ToString();
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.BAG_ID = (int)lector["BAG_ID"];
                        result.SUPERVISOR = (string)lector["SUPERVISOR"];
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
        /// Gets a Shipping by its id from the database
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Scanner GetScanbyid(int a)
        {
            var result = new Scanner();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM SCANNER WHERE SCAN_ID =" + a;
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.SCAN_ID = (int)lector["SCAN_ID"];
                        result.SCAN_DATE = lector["SCAN_DATE"].ToString();
                        result.SCAN_HOUR = lector["SCAN_HOUR"].ToString();
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.BAG_ID = (int)lector["BAG_ID"];
                        result.SUPERVISOR = (string)lector["SUPERVISOR"];
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
        /// Gets a Shipping by its supervisor from the database
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Scanner GetScanbySuper(string a)
        {
            var result = new Scanner();
            var conn = new ConnectionPostgreSQL();
            var connstr = string.Empty;

            using (var db = conn.OpenConnection())
            {
                connstr = "SELECT * FROM SCANNER WHERE SUPERVISOR =" + "'"+ a + "'";
                try
                {
                    var comando = new NpgsqlCommand(connstr, db);
                    var lector = comando.ExecuteReader();
                    while (lector.Read())
                    {
                        result.SCAN_ID = (int)lector["SCAN_ID"];
                        result.SCAN_DATE = lector["SCAN_DATE"].ToString();
                        result.SCAN_HOUR = lector["SCAN_HOUR"].ToString();
                        result.BAGCART_ID = (int)lector["BAGCART_ID"];
                        result.BAG_ID = (int)lector["BAG_ID"];
                        result.SUPERVISOR = (string)lector["SUPERVISOR"];
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

    }
}