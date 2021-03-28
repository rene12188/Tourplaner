using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using Tourplaner_Utility;

namespace Tourplaner_Data
{


    public class Database
    {
        private abstract class Connectionhander //Singelton
        {
            private static string CFGstring = LoadCFG();

            public static NpgsqlConnection returnConnection()
            {
                try
                {
                   return  new NpgsqlConnection(CFGstring);
                }
                catch (Exception e)
                {
                    Console.WriteLine("SQ :Query Error: " + e.Message);
                    throw e;
                }
                
            }

            private static string LoadCFG()
            {
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

        public static ObservableCollection<Tour> SearchTours(string Searchterm = "" )
        {
            ObservableCollection<Tour> returnval = new ObservableCollection<Tour>();
            using NpgsqlConnection conn = Connectionhander.returnConnection();
            Searchterm = '%' + Searchterm + '%';
            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"Select Name, Source, destination from Tour  WHERE Name Like @payload;", conn);
                cmd.Parameters.Add(new NpgsqlParameter("payload", Searchterm));

                NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    while (myReader.Read())
                    {
                        returnval.Add(new Tour(myReader.GetString(0), myReader.GetString(1), myReader.GetString(2)));
                    }

                   
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("SQ :Query Error: " + e.Message);
                throw;

            }
            finally
            {
               conn.Close();
            }
            return returnval;
        }

        public static int InsertTour(Tour tmo)
        {
            int returnval = -3;
            using NpgsqlConnection conn = Connectionhander.returnConnection();

            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"SELECT insert_tours(@Name, @SRC, @DSC);", conn);
                cmd.Parameters.Add(new NpgsqlParameter("Name", tmo.getName()));
                cmd.Parameters.Add(new NpgsqlParameter("SRC", tmo.getSource()));
                cmd.Parameters.Add(new NpgsqlParameter("DSC", tmo.getDestination()));

                NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    while (myReader.Read())
                    {
                        returnval = myReader.GetInt16(0);
                    }


                }

                return returnval;

            }
            catch (Exception e)
            {
                Console.WriteLine("SQ :Query Error: " + e.Message);
                throw;

            }
            finally
            {
                conn.Close();
            }
        }
        public static int DeleteTour(string name)
        {
            int returnval = -3;
            using NpgsqlConnection conn = Connectionhander.returnConnection();

            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"Delete FROM Tour Where Name = @name; ", conn);
                cmd.Parameters.Add(new NpgsqlParameter("name", name));


                NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    while (myReader.Read())
                    {
                        returnval = myReader.GetInt16(0);
                    }


                }

                return returnval;

            }
            catch (Exception e)
            {
                Console.WriteLine("SQ :Query Error: " + e.Message);
                throw;

            }
            finally
            {
                conn.Close();
            }
        }



    }

}

