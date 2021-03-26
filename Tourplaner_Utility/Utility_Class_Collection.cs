

using System;
using System.Threading.Tasks;

namespace Tourplaner_Utility
{
    public class Tour
    {


         public string Name;
         private string source;
         private string dest;


         public Tour(string name ,string source, string dest )
         { 

            this.Name = name;
            this.source = source;
            this.dest = dest;

         }

         public string getName()
         {
             return this.Name;
         }

         public string getSource()
         {
             return source;
         }


         public string getDestination()
         {
             return dest;
         }
        public override string ToString()
         {
             return this.Name;
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
