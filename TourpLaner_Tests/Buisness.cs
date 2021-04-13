using System;
using NUnit.Framework;
using Tourplaner_Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
         public void Buisnesslogic_SaveImage()
         { 
             Byte[] result = Encoding.ASCII.GetBytes("Hello World");
           Mainlogic.SaveImage(result, "test");
           Assert.IsTrue(File.Exists(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\test.jpg"));
         }


         [Test]
         public  void Buisnesslogic_Fetchimage()
         {
              Mainlogic.FetchImage("ASD", "Illmitz", "Apetlon");
             
             Assert.IsTrue(File.Exists(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\ASD.jpg"));
         }
         [Test]
        public void Buisnesslogic_Deleteimage()
        {
            Mainlogic.DeleteTourimage(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\ASD.jpg");
            Assert.IsFalse(File.Exists(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\ASD.jpg"));
        }

        [Test]
        public void Buisnesslogic_SerializeTourJson()
        {
            Tour Tour1 = new Tour(null,"Weite Runde", "abc", "Illmitz", "Podersdorf", 20);
            Tourlog TL1 = new Tourlog(-1, DateTime.Now, "Very Nice", 10, 120, 3, 3.34, 4, 200, 40, 3.5);
            
            ObservableCollection<Tourlog> Tourloglist = new ObservableCollection<Tourlog>();
            ObservableCollection<Tour> Tourlog = new ObservableCollection<Tour>();


            Tourloglist.Add(TL1);
            Tour1.Tourlogs = Tourloglist;
            Tourlog.Add(Tour1);


            string returnval = Mainlogic.SerializeTours(Tourlog);
            string expt = JsonSerializer.Serialize(Tourlog) ;
            Assert.AreEqual(expt, returnval);
        }

        [Test]
        public void Buisnesslogic_SerializeExport()
        {
            Tour Tour1 = new Tour(null ,"Weite Runde", "abc", "Illmitz", "Podersdorf", 20);
            Tourlog TL1 = new Tourlog(-1, DateTime.Now, "Very Nice", 10, 120, 3, 3.34, 4, 200, 40, 3.5);
            Tour Tour2 = new Tour(null, "Weite Runde2", "abc", "Illmitz", "Podersdorf", 20);
            ObservableCollection<Tourlog> Tourloglist = new ObservableCollection<Tourlog>();
            ObservableCollection<Tour> Tourlog = new ObservableCollection<Tour>();

            Tourloglist.Add(TL1);
            Tour1.Tourlogs = Tourloglist;
            Tourlog.Add(Tour1);
            Tourlog.Add(Tour2);
            Mainlogic.Export(Tourlog, @"E:\tmp");

           
            Assert.IsTrue(File.Exists(@"E:\tmp\Tours.txt"));
        }

        [Test]
        public void Buisnesslogic_Deserialize()
        {
            Tour Tour1 = new Tour(null,"Weite Runde", "abc", "Illmitz", "Podersdorf", 20);
            Tourlog TL1 = new Tourlog(-1, DateTime.Now, "Very Nice", 10, 120, 3, 3.34, 4, 200, 40, 3.5);
          

            ObservableCollection<Tourlog> Tourloglist = new ObservableCollection<Tourlog>();
            ObservableCollection<Tour> Tourlog = new ObservableCollection<Tour>();
            Tourloglist.Add(TL1);
            Tour1.Tourlogs = Tourloglist;
            Tourlog.Add(Tour1);

            ObservableCollection<Tour> returnval = Mainlogic.DeserializeTours(@"E:\tmp");

            Assert.AreEqual(returnval[0].Name, Tourlog[0].Name);
        }

        [TearDown]
         public void Teardown()
         {
            File.Delete(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\test.jpg"); 
            File.Delete(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\ASD.jpg");
        }
    }
}