using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourplaner_Utility;

namespace Tourplaner_Tests
{
    [TestFixture]
    public class Util
    {
        [Test]
        public void TestPDFStringTourlog()
        {
             var TL = new Tourlog(-1, DateTime.Now, "Very Nice", 10, 120, 3, 3.34, 4, 200, 40, 3.5);
             string[] expected = new string[] { TL.TLID.ToString(), TL.Timestamp.ToString(), TL.Report, TL.Distance.ToString(), TL.Totaltime.ToString(), TL.Rating.ToString(), TL.AvgSpeed.ToString(), TL.Difficulty.ToString(), TL.EnergyBurn.ToString(), TL.Temperature.ToString(), TL.Water.ToString() };
                 Assert.AreEqual(TL.PrintToPDF(), expected, "Some useful error message");
        }

        [Test]
        public void Test_Tour_to_String()
        {
            Tour Tour1 = new Tour(null, "Weite Runde", "abc", "Illmitz", "Podersdorf", 20);
            Assert.AreEqual(Tour1.Name, Tour1.ToString() , "Some useful error message");
        }

    }
}
