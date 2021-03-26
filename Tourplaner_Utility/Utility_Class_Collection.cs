

using System;

namespace Tourplaner_Utility
{
    public class Tour
    {
         int ID;
         public string Name;
         private string source;
         private string dest;


         public Tour(int id ,string name ,string source, string dest )
         { 
            this.ID = id;
            this.Name = name;
            this.source = source;
            this.dest = dest;

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
}
