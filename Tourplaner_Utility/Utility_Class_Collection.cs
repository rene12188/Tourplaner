using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Collections.Specialized;


namespace Tourplaner_Utility
{
    public class CFGManager
    {
        CFGManager singeltonConfigurationManager = new CFGManager();
        protected CFGManager()
        {
       /*     string sAttr;
            // Read a particular key from the config file 
            sAttr = ConfigurationManager.AppSettings.Get("Key0");
            Console.WriteLine("The value of Key0: " + sAttr);

            // Read all the keys from the config file
            NameValueCollection sAll;
            sAll = ConfigurationManager.AppSettings;

            foreach (string s in sAll.AllKeys)
                Console.WriteLine("Key: " + s + " Value: " + sAll.Get(s));
            Console.ReadLine();*/
        }


    }

    //One Class per File
    public class Tour
    {


        public Tour(ObservableCollection<Tourlog> tourlogs, string name, string description, string source, string dest, double distance)
        {
            Tourlogs = tourlogs;
            Name = name;
            Description = description;
            Source = source;
            Destination = dest;
            Distance = distance;
            Image = @"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\" + name + ".jpg";

        }

        public Tour()
        {

        }

        public ObservableCollection<Tourlog> Tourlogs { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Distance { get; set; }

        public string Image { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
    public class Tourlog
    {
        public Tourlog(int tlid, DateTime timestamp, string report, double distance, int totaltime, int rating, double avgapseed,
            int difficultty, int energyburn, int temperature, double water)
        {
            this.TLID = tlid;
            this.Timestamp = timestamp;
            this.Report = report;
            this.Distance = distance;
            this.Totaltime = totaltime;
            this.Rating = rating;
            this.AvgSpeed = avgapseed;
            this.Difficulty = difficultty;
            this.EnergyBurn = energyburn;
            this.Temperature = temperature;
            this.Water = water;
        }

        public Tourlog(DateTime timestamp, string report, double distance, int totaltime, int rating,
            int difficultty, int temperature)
        {
            this.TLID = 0;
            this.Timestamp = timestamp;
            this.Report = report;
            this.Distance = distance;
            this.Totaltime = totaltime;
            this.Rating = rating;
            this.AvgSpeed = 0;
            this.Difficulty = difficultty;
            this.EnergyBurn = 0;
            this.Temperature = temperature;
            this.Water = 0;
        }

        public Tourlog()
        {

        }

        public int TLID { get; set; }
        public DateTime Timestamp { get; set; }
        public string Report { get; set; }
        public double Distance { get; set; }
        public int Totaltime { get; set; }
        public int Rating { get; set; }

        public double AvgSpeed { get; set; }

        public int Difficulty { get; set; }
        public int EnergyBurn { get; set; }
        public int Temperature { get; set; }
        public double Water { get; set; }
    }
}

