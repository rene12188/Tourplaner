using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tourplaner_Utility
{

    /// <summary>Data Class  representing a Tourlog</summary>
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


        /// <summary>
        ///   <para>
        /// Creates a String Array that can be Print onto a Pdf</para>
        /// </summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public string[] PrintToPDF()
        {
            string[] expected;
            return  expected = new string[] { this.TLID.ToString(), this.Timestamp.ToString(), this.Report, this.Distance.ToString(),this.Totaltime.ToString(),this.Rating.ToString(),this.AvgSpeed.ToString(), this.Difficulty.ToString(), this.EnergyBurn.ToString(),this.Temperature.ToString(), this.Water.ToString() };

        }
    }
}
