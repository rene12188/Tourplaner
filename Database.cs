using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;



namespace Monster_Trading_Card_Game
{


    public class Database
    {
        private class Connectionhander
        {
            private static NpgsqlConnection conn = new NpgsqlConnection("Host = localhost;Username=postgres;Password=a;Database=tourplaner");

            public static NpgsqlConnection returnConnection()
            {
                return conn;
            }
        }
     
        private NpgsqlConnection ConnecttoDatabase()
        {

            string mySQLConnectionString = "Host=localhost;Username=postgres;Password=a;Database=tourplaner";
            NpgsqlConnection conn = new NpgsqlConnection(mySQLConnectionString);
            return conn;

            //MySqlConnection databaseConnection = new MySqlConnection(mySQLConnectionString);
            //return databaseConnection;
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

