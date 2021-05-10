using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace Tourplaner_Utility
{

    /// <summary>
    ///   <para>A Data Class which represents a Tour</para>
    /// </summary>
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
            Image = CFGManager.ReadSetting("ImageFolder") + "/" + name + ".jpg";

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


        /// <summary>
        ///   <para>
        /// Print a String Array for the PDFWriter.
        /// </para>
        /// </summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public string[] PrintToPDF()
        {
            string[] expected;
            return expected = new string[] { this.Name, this.Description, this.Source, this.Destination, this.Distance.ToString(), this.Tourlogs.Count.ToString() };
        }
    }
}
