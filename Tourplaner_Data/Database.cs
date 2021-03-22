using Npgsql;
using System;
using System.Collections.Generic;

namespace Tourplaner_Data
{


    public class Database
    {
        private class Connectionhander //Singelton
        {
            private static NpgsqlConnection conn = new NpgsqlConnection(LoadCFG());

            public static NpgsqlConnection returnConnection()
            {
                return conn;
            }

            private static string LoadCFG()
            {

                string[] lines = System.IO.File.ReadAllLines(@"E:\Programming\C#\SWE2\config.txt");

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

        public static void GetTours(string Searchterm)
        {

            using NpgsqlConnection conn = Connectionhander.returnConnection();
            conn.Open();

            try
            {

                var cmd = new NpgsqlCommand("Select * from Tour where Name = @payload;", conn);
                //databaseConnection.Open();
                // MySqlDataReader myReader = commandDatabase.ExecuteReader();


                cmd.Parameters.Add(new NpgsqlParameter("payload", Searchterm));

                NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    if (myReader.Read())
                    {
                       
                        card_tmp = new Tour(myReader.GetString(0), myReader.GetInt16(1), myReader.GetInt16(2), tmp_special, myReader.GetInt32(4));
                    }
                    return card_tmp;
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

