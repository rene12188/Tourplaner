

using System;
using System.Threading.Tasks;

namespace Tourplaner_Utility
{
    public class Tour
    {
<<<<<<< HEAD
        int ID;
         string Name;
         double Start_long;
         double Start_lat;
         double Finish_long;
         double Finish_lat;

        public Tour(string name ,double startLong, double startLat, double finishLong, double finishLat )
        {
            this.Name = name;
            this.Start_long = startLong;
            this.Start_lat = startLat;
            this.Finish_long = finishLong;
            this.Finish_lat = finishLat;
        }


        public string getName()
        {
            return this.Name;
        }
        public override string ToString()
        {
            return this.Name;
        }
=======
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
>>>>>>> DB
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
