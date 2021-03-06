﻿using System;
using Tourplaner_Data;
using Tourplaner_Utility;
using System.Collections.ObjectModel;
using System.IO;

using System.Threading.Tasks;

using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Tourplaner_Buisness
{
    static public class Mainlogic
    {
        public static async Task<int> SaveTour(Tour tour)
        {
            try
            {
                Task<float> tmp = WebRequester.ReturnDistance(tour.Source, tour.Destination);
                await tmp;
                tour.Distance = tmp.Result;

                int rCode = Database.InsertTour(tour);
                if (rCode == 0)
                {
                    FetchImage(tour.Name, tour.Source, tour.Destination);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Encountered:{0}", e.Message);
            }

            return 1;
        }

        public static void DeleteTourLog(Tourlog tmp)
        {
            Database.DeleteTourlog(tmp.TLID);
        }

        public static async Task Inserttourlog(Tourlog tmp, string selectedname)
        {
            Database.InsertTourlogs(tmp, selectedname);
        }

        public static ObservableCollection<Tour> UpdateTours(string term = "")
        {
            return Database.SearchTours(term);
        }

        public static async  Task<int>  DeleteTour(Tour tour)
        {
            DeleteTourimage(tour.Image);
            Database.DeleteTour(tour.Name);
            return 1;

        }

        public static void DeleteTourimage(string imagepath)
        {
            File.Delete(imagepath);
        }

        public static async Task<int> CopyTour(string tmp)
        {

            return Database.CopyTour(tmp);
        }

        public static async Task<int> FetchImage(string tourname, string from, string to)
        {
            byte[] image = null;
            try
            {
                byte [] tmp = await WebRequester.GetPicture(from, to);
                image = tmp;
                SaveImage(image, tourname);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Encountered: {0}", e.Message);
            }

            return 0;
        }


        public static void SaveImage(byte[] image, string tourname)
        {
            try
            {
                string path = @"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\" + tourname+".jpg";
                using (var sw = new FileStream(path,FileMode.Create, FileAccess.Write) )
                {
                    sw.Write(image);
                    sw.Close();
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Encountered:{0}", e.Message);
            }

        }
    }
}


