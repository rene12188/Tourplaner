using System;
using NUnit.Framework;
using Tourplaner_Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tourplaner_Buisness;
using Tourplaner_Utility;

namespace Tourplaner_Tests
{
    public class Buisness_Tests
    {
        [SetUp]
        public void Setup()
        {
        }



         [Test]
         public void Database_SaveImage()
         { 
             Byte[] result = Encoding.ASCII.GetBytes("Hello World");
           Mainlogic.SaveImage(result, "test");
           Assert.IsTrue(File.Exists(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\test.jpg"));
         }


         [Test]
         public void Database_Fetchimage()
         {
             Mainlogic.FetchImage("ASD", "Illmitz", "Apetlon");
             Assert.IsTrue(File.Exists(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\ASD.jpg"));
         }

        [TearDown]
         public void Teardown()
         {
            File.Delete(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\test.jpg"); 
            File.Delete(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\ASD.jpg");
        }
    }
}