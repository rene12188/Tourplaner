

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Tourplaner_Utility
{
    public class Tour
    {


        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Distance { get; set; }

        public string Image { get; private set; }
        ObservableCollection<Tourlog> Tourlogs = new ObservableCollection<Tourlog>();


         public Tour(string name,string description ,string source, string dest, double distance )
         { 

            this.Name = name;
            this.Description = description;
            this.Source = source;
            this.Destination = dest;
            this.Distance = distance;
            this.Image = @"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\" + name + ".jpg";

         }

         public override string ToString()
         {
             return this.Name.ToString();
         }
    }
}

    public class Tourlog
    {
        private DateTime Timestamp { get; set; }
        private string Report { get; set; }
        private int Distance { get; set; }
        private int Totaltime { get; set; }
        private int Rating { get; set; }

        private float  AvgSpeed { get; set; }

        private int Difficulty { get; set; }
        private int EnergyBurn { get; set; }
        private int Temperature { get; set; }
        private float Water { get; set; }
    public Tourlog(DateTime timestamp, string report, int distance, int rating)
        {

            this.Timestamp = timestamp;
            this.Report = report;
            this.Distance = distance;
            this.Rating = rating;



    }
}
