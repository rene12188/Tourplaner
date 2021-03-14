using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Shapes;



namespace TourPlaner
{


    public class Database
    {
        private class Connectionhander
        {
            private static NpgsqlConnection conn = new NpgsqlConnection(LoadCFG());

            public static NpgsqlConnection returnConnection()
            {
                return conn;
            }

            private static string LoadCFG()
            {

                string[] lines = System.IO.File.ReadAllLines(@"E:\Programming\C#\SWE2\TourPlaner\config.txt");

                /*"Host = localhost;Username=postgres;Password=a;Database=tourplaner";*/
                string connectionstring =
                    string.Format("Host = localhost;Username= {0};Password={1};Database=tourplaner;", lines[0],
                        lines[1]);
                Console.WriteLine(connectionstring);
                return connectionstring;
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

                if (!ndr.HasRows)
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

       
    }

}

