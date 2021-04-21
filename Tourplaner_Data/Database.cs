using Npgsql;
using System;
using System.Buffers;
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
            Tour tmp = null;
            ObservableCollection<Tour> returnval = new ObservableCollection<Tour>();
            using NpgsqlConnection conn = Connectionhander.returnConnection();
            Searchterm = '%' + Searchterm + '%';
            
            try
            {
                conn.Open();
                var cmd = new NpgsqlCommand($"Select Name, Description  ,Source, Destination,Distance from Tour  WHERE Name Like @payload;", conn);
                cmd.Parameters.Add(new NpgsqlParameter("payload", Searchterm));

                NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    while (myReader.Read())
                    {
                        tmp = new Tour(null,myReader.GetString(0), myReader.GetString(1), myReader.GetString(2), myReader.GetString(3), myReader.GetDouble(4));
                        tmp.Tourlogs = SearchTourlogs(tmp.Name);
                        returnval.Add(tmp);
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

        public static ObservableCollection<Tourlog> SearchTourlogs(string nameoftour)
        {
            ObservableCollection<Tourlog> returnval = new ObservableCollection<Tourlog>();
            using NpgsqlConnection conn = Connectionhander.returnConnection();
            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"Select tlid ,DateTime,Report,Distance,Totaltime,Rating,AvgSpeed,Difficulty,EnergyBurn,Temperature,WaterRecomendation from Tour_Log WHERE TID = (Select TID from Tour WHERE Name = @payload); ", conn);

                cmd.Parameters.Add(new NpgsqlParameter("payload", nameoftour));

                NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    while (myReader.Read())
                    {
                        returnval.Add(new Tourlog(
                            myReader.GetInt16(0),
                            myReader.GetDateTime(1), 
                            myReader.GetString(2),
                            myReader.GetDouble(3),
                            myReader.GetInt16(4),
                            myReader.GetInt16(5),
                            myReader.GetDouble(6),
                            myReader.GetInt16(7), 
                            myReader.GetInt16(8),
                            myReader.GetInt16(9),
                            myReader.GetDouble(10)));;
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

        //Select insert_tourlog(2, 'THis is a report' , current_date, 1.0,60,3,3,20);

       
        public static int InsertTour(Tour tmo)
        {
            int returnval = -3;
            using NpgsqlConnection conn = Connectionhander.returnConnection();

            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"SELECT insert_tours(@Name, @DESC ,@SRC, @DSC, @DIST);", conn);
                cmd.Parameters.Add(new NpgsqlParameter("Name", tmo.Name));
                cmd.Parameters.Add(new NpgsqlParameter("SRC", tmo.Source));
                cmd.Parameters.Add(new NpgsqlParameter("DSC", tmo.Destination));
                cmd.Parameters.Add(new NpgsqlParameter("DESC", tmo.Description));
                cmd.Parameters.Add(new NpgsqlParameter("DIST", tmo.Distance));

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
                DeleteTourlog(name);
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

        public static int InsertTourlogs(Tourlog TL, string name)
        {
            int returnval = -3;
            using NpgsqlConnection conn = Connectionhander.returnConnection();
            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"Select insert_tourlog((Select TID from Tour WHERE Name = @payload), @report , @datetime,@dist,@tottime,@rateing,@diff,@temp); ", conn);
                cmd.Parameters.Add(new NpgsqlParameter("payload", name));
                cmd.Parameters.Add(new NpgsqlParameter("report", TL.Report));
                cmd.Parameters.Add(new NpgsqlParameter("datetime", TL.Timestamp));

                cmd.Parameters.Add(new NpgsqlParameter("dist", TL.Distance));
                cmd.Parameters.Add(new NpgsqlParameter("tottime", TL.Totaltime));
                cmd.Parameters.Add(new NpgsqlParameter("rateing", TL.Rating));
                cmd.Parameters.Add(new NpgsqlParameter("diff", TL.Difficulty));
                cmd.Parameters.Add(new NpgsqlParameter("temp", TL.Temperature));

                NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    while (myReader.Read())
                    {
                        returnval = myReader.GetInt16(0);
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
        public static void DeleteTourlog(int Logid)
        {
            int returnval = -3;
            using NpgsqlConnection conn = Connectionhander.returnConnection();

            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"Delete FROM Tour_Log Where TLID = @LID; ", conn);
                cmd.Parameters.Add(new NpgsqlParameter("LID", Logid));


                NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    while (myReader.Read())
                    {
                        returnval = myReader.GetInt16(0);
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
        }

        public static void DeleteTourlog(string Tourname)
        {
            int returnval = -3;
            using NpgsqlConnection conn = Connectionhander.returnConnection();

            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"Delete FROM Tour_Log Where TID = (Select TID from Tour WHERE Name = @payload); ", conn);
                cmd.Parameters.Add(new NpgsqlParameter("payload", Tourname));


                NpgsqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.HasRows)
                {
                    Console.WriteLine("Query Generated result:");

                    while (myReader.Read())
                    {
                        returnval = myReader.GetInt16(0);
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
        }
        public static void NukeDatabase()
        {
            int returnval = -3;
            using NpgsqlConnection conn = Connectionhander.returnConnection();

            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"Select Nuke(); ", conn);


                NpgsqlDataReader myReader = cmd.ExecuteReader();

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
        public static int CopyTour(string name)
        {
            int returnval = -3;
            using NpgsqlConnection conn = Connectionhander.returnConnection();

            conn.Open();
            try
            {
                var cmd = new NpgsqlCommand($"Select copy_tour(@name); ", conn);
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

