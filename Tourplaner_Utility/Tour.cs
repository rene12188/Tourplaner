using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
