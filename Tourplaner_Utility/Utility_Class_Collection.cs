

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Tourplaner_Utility
{
    public class Tour
    {


         private string Name;
         private string Description;
         private string Source;
         private string Destination;
         private int Distance;
         ObservableCollection<Tourlog> Tourlogs = new ObservableCollection<Tourlog>();


         public Tour(string name,string description ,string source, string dest,int distance )
         { 

            this.Name = name;
            this.Description = description;
            this.Source = source;
            this.Destination = dest;
            this.Distance = distance;

         }

         public string getName()
         {
             return this.Name;
         }

         public string getSource()
         {
             return this.Source;
         }


         public string getDestination()
         {
             return this.Destination;
         }
        public override string ToString()
         {
             return this.Name;
         }

        public string getDescription()
        {
            return this.Description;
        }

        public int getDistance()
        {
            return this.Distance;
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
