

using System;

namespace Tourplaner_Utility
{
    public class Tour
    {
        int ID;
         string Name;
         double Start_long;
         double Start_lat;
         double Finish_long;
         double Finish_lat;

        public Tour(int id ,string name ,double startLong, double startLat, double finishLong, double finishLat )
        {
            this.ID = id;
            this.Name = name;
            this.Start_long = startLong;
            this.Start_lat = startLat;
            this.Finish_long = finishLong;
            this.Finish_lat = finishLat;
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
