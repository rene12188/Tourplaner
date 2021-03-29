using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplaner_Data;
using Tourplaner_Utility;
using System.Collections.ObjectModel;

namespace Tourplaner_Buisness
{
    static public class Mainlogic
    {
        public static int SaveTour(Tour tmp)
        {
            Database.InsertTour(tmp);

            return 0;
        }

        public static ObservableCollection<Tour> UpdateTours(string term = "")
        {
            return Database.SearchTours(term);


        }

        public static int DeleteTour(string name)
        {
            return Database.DeleteTour(name);
        }


    }
}
