using NUnit.Framework;
using Tourplaner_Data;
using System.Collections.Generic;
using Tourplaner_Utility;

namespace Tourplaner_Tests
{
    public class Tests
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
        public void Database_GetTours()
        {
            List<Tour> tmp = new List<Tour>();
            tmp = Database.SearchTours();

            Assert.AreEqual(tmp[0].Name, "Kurze Runde");
        }
    }
}