using System;
using NUnit.Framework;
using Tourplaner_Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Tourplaner_Buisness;
using Tourplaner_Utility;

namespace Tourplaner_Tests
{
    public class Backend_Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void Database_Connection_is_valid()
        {
           Database.SimpleQuery("Select * from Tour;", "");
        }



        [Test]
        public void Database_GetTours_NOSearchterm()
        {
            ObservableCollection<Tour> tmp = new ObservableCollection<Tour>();
            tmp = Database.SearchTours();

            Assert.AreEqual(2, tmp.Count);
        }

        [Test]
        public void Database_Inserttours()
        {
            Tour tmp = new Tour("Weite Runde","abc" ,"Illmitz", "Podersdorf", 20);
            int returncode = Database.InsertTour(tmp);

            Assert.AreEqual(0, returncode);
        }
        [Test]
        public void Database_GetTours_Inserttourlog()
        {
            Tourlog tmp = new Tourlog(-1,DateTime.Now, "Very Nice",10,120,3,3.34,4,200,40,3.5 );
            int ret = Database.InsertTourlogs(tmp, "Weite Runde");

            Assert.AreEqual(0, ret);
        }

        [Test]
        public void Database_GetTours_SerachTerm()
        {
            ObservableCollection<Tour> tmp = new ObservableCollection<Tour>();
            tmp = Database.SearchTours("Weite Runde");

            Assert.AreEqual(tmp[0].Name, "Weite Runde");
        }
        [Test]
        public void Database_Deletetours()
        {
            int returncode = Database.DeleteTour("Weite Runde");

            Assert.AreEqual(-3, returncode);
        }

         [Test]
         public void Database_GetImage()
         {
            Byte[] result = null;
            Task<Byte[]> tmp = WebRequester.GetPicture("Illmitz", "Neusiedl am See");
            tmp.Wait();
            result = tmp.Result;
            Mainlogic.SaveImage(result,"test");
            Assert.IsNotNull(result);
         }
    }
}