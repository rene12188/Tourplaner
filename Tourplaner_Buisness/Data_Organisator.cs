using System;
using Tourplaner_Data;
using Tourplaner_Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tourplaner_Buisness
{
    static public class Mainlogic
    {
        public static async Task<int> SaveTour(Tour tour)
        {
            try
            {
                //string tourJson = WebRequester.GetJson(tour.Source, tour.Destination).Result;
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

            return 1;
        }

        public static void DeleteTourLog(Tourlog tmp)
        {
            Database.DeleteTourlog(tmp.TLID);
        }

        public static void Inserttourlog(Tourlog tmp, string selectedname)
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

        public static int CopyTour(string tmp)
        {

            return Database.CopyTour(tmp);
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

        public static void Export(ObservableCollection<Tour> tourlist, string filepath)
        {
            string JSON = SerializeTours(tourlist);


            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filepath, "Tours.json")))
            {
                outputFile.WriteLine(JSON);
            }


        }

        public static string SerializeTours(ObservableCollection<Tour> tourlist)
        {
            string returnstring = String.Empty;
            returnstring += JsonSerializer.Serialize<ObservableCollection<Tour>>(tourlist);
            
          /*  foreach (var obj in tourlist)
            {
                
            }*/

          return returnstring;
        }

        public static void Import(string filepath)
        {
            ObservableCollection<Tour> tmp = DeserializeTours(filepath);

            Database.NukeDatabase();

            foreach (var tmpTour in tmp)
            {
                Database.InsertTour(tmpTour);
                foreach (var tmpLog in tmpTour.Tourlogs)
                {
                    Database.InsertTourlogs(tmpLog, tmpTour.Name);
                }

            }
        }

        public static ObservableCollection<Tour> DeserializeTours(string filepath)
        {
            Tour tmp = null;
            ObservableCollection<Tour> retunrval = new ObservableCollection<Tour>();
            string s = File.ReadAllText(filepath);
            retunrval = JsonSerializer.Deserialize<ObservableCollection<Tour>>(s);
            /*using (StreamReader sr = File.OpenText(Path.Combine(filepath, "Tours.txt")))
            {
                while ((s = sr.ReadLine()) != null)
                {
                    tmp = JsonSerializer.Deserialize<Tour>(s);
                    retunrval.Add(tmp);
                }
            }*/


            return retunrval;
        }
    }
}


