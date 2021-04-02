using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tourplaner_Data;
using Tourplaner_Utility;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tourplaner_Buisness
{
    static public class Mainlogic
    {
        public static async void SaveTour(Tour tour)
        {
            try
            {
                string tourJson = WebRequester.GetJson(tour.Source, tour.Destination).Result;
                int rCode = Database.InsertTour(tour);
                if (rCode == 0)
                {

                    FetchImage(tour.Name, tour.Source, tour.Destination);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Encountered:{0}", e.Message);
            }

        }


        public static ObservableCollection<Tour> UpdateTours(string term = "")
        {
            return Database.SearchTours(term);


        }

        public static int DeleteTour(Tour tour)
        {
           
            DeleteTourimage(tour.Image);
            return Database.DeleteTour(tour.Name);
        }

        public static void DeleteTourimage(string imagepath)
        {
            File.Delete(imagepath);
        }

        public static int CopyTour(string tmp)
        {

            return Database.CopyTour(tmp);
        }

        public static void DeserializeJson(string content)
        {

        }

        public static async void FetchImage(string tourname, string from, string to)
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

        }


        public static void SaveImage(byte[] image, string tourname)
        {
            try
            {
                string path = @"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\" + tourname+".jpg";
                using (var sw = new FileStream(path,FileMode.Create, FileAccess.Write) )
                {
                    sw.Write(image);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Encountered:{0}", e.Message);
            }


        }
    }
}


