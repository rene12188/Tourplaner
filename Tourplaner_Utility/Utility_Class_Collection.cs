

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
        public int Distance { get; set; }
        ObservableCollection<Tourlog> Tourlogs = new ObservableCollection<Tourlog>();


         public Tour(string name,string description ,string source, string dest,int distance )
         { 

            this.Name = name;
            this.Description = description;
            this.Source = source;
            this.Destination = dest;
            this.Distance = distance;

         }
    }
}

    public class Tourlog
    {
        int TLID;
        int TID;
        private DateTime Timestamp;
        private string Report;
        private int Distance;
        private int Rating;

        public Tourlog(int tlid, int tid, DateTime timestamp, string report, int distance, int rating)
        {
            this.TLID = tlid;
            this.TID = tid;
            this.Timestamp = timestamp;
            this.Distance = distance;
            this.Rating = rating;
            this.Report = report;


    }
}
