using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tourplaner_Data;
using Tourplaner_Utility;

namespace Tourplaner_Buisness
{
    public static class JsonOperator
    {
        public static void Export(ObservableCollection<Tour> tourlist, string filepath)
        {
            string JSON = SerializeTours(tourlist);
            try
            {

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(filepath, "Tours.json")))
                {
                    outputFile.WriteLine(JSON);
                }

            }
            catch (Exception e)
            {
                throw e;
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


            return retunrval;
        }
    }
}
