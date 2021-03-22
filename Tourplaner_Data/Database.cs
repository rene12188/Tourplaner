using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using Tourplaner_Utility;

namespace Tourplaner_Data
{


    public class Database
    {
        private class Connectionhander //Singelton
        {
            private static NpgsqlConnection conn = new NpgsqlConnection(LoadCFG());

            public static NpgsqlConnection returnConnection()
            {
                try
                {
                    return conn;
                }
                catch (Exception e)
                {
                    Console.WriteLine("SQ :Query Error: " + e.Message);
                    throw e;
                }
                
            }

            private static string LoadCFG()
            {

              //  string[] lines = System.IO.File.ReadAllLines(@"E:\Programming\C#\SWE2\config.txt");

                /*"Host = localhost;Username=postgres;Password=a;Database=tourplaner";*/
               /* string connectionstring =
                    Console.WriteLine(connectionstring);*/
                return "Host=localhost;Username=postgres; Password=a;Database=tourplaner";
            }
        }
     

        public static async void SimpleQuery(string queryToRun, string payload)
        {

            using NpgsqlConnection conn = Connectionhander.returnConnection();
            conn.Open();

            try
            {

                var cmd = new NpgsqlCommand(queryToRun, conn);
                //databaseConnection.Open();
                // MySqlDataReader myReader = commandDatabase.ExecuteReader();


                cmd.Parameters.Add(new NpgsqlParameter("payload", payload));

                NpgsqlDataReader ndr = await cmd.ExecuteReaderAsync();

                if (ndr.HasRows)
                {
                    Console.WriteLine("Query Successfully executed!");
                }
                else
                {
                    Console.WriteLine("OOPS");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("SQ :Query Error: " + e.Message);
                throw e;

            }
        }

        public static List<Tour> SearchTours(string Searchterm ="")
        {
            
            List<Tour> returnval = new List<Tour>();
            using NpgsqlConnection conn = Connectionhander.returnConnection();


            try
            {
                conn.Open();
                var cmd = new NpgsqlCommand($"Select * from Tour", conn);
                //databaseConnection.Open();
                // MySqlDataReader myReader = commandDatabase.ExecuteReader();


               // cmd.Parameters.Add(new NpgsqlParameter("payload", Searchterm)
               NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    if (myReader.Read())
                    {
                        returnval.Add(new Tour(myReader.GetInt16(0), myReader.GetString(1), myReader.GetDouble(2), myReader.GetDouble(3), myReader.GetDouble(4), myReader.GetDouble(5)));
                    }
                    conn.Close();
                    return returnval;
                }
                conn.Close();
                return null;

            }
            catch (Exception e)
            {
                Console.WriteLine("SQ :Query Error: " + e.Message);
                throw;

            }
        }


    }

}

