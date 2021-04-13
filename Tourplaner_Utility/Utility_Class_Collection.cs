using System;
using System.Collections.ObjectModel;

namespace Tourplaner_Utility
{
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