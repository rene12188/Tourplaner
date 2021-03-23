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
        public void Database_GetTours_SerachTerm()
        {
            List<Tour> tmp = new List<Tour>();
            tmp = Database.SearchTours("Kurze Runde");

            Assert.AreEqual(tmp[0].Name, "Kurze Runde");
        }

        [Test]
        public void Database_GetTours_NOSearchterm()
        {
            List<Tour> tmp = new List<Tour>();
            tmp = Database.SearchTours();

            Assert.AreEqual(2, tmp.Count);
        }
    }
}