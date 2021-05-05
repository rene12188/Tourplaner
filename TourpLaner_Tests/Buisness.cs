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
         public async Task Buisnesslogic_Fetchimage()
         {
              Task<int> tmp =  Mainlogic.FetchImage("ASD", "Illmitz", "Apetlon");
              await tmp;

             Assert.IsTrue(File.Exists(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\ASD.jpg"));
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


            string returnval = JsonOperator.SerializeTours(Tourlog);
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
            JsonOperator.Export(Tourlog, @"E:\tmp");

           
            Assert.IsTrue(File.Exists(@"E:\tmp\Tours.json"));
        }


        [Test]
        public void Buisnesslogic_SerializeExportFailed()
        {
            Exception ex_returnval = null;
            Tour Tour1 = new Tour(null, "Weite Runde", "abc", "Illmitz", "Podersdorf", 20);
            Tourlog TL1 = new Tourlog(-1, DateTime.Now, "Very Nice", 10, 120, 3, 3.34, 4, 200, 40, 3.5);
            Tour Tour2 = new Tour(null, "Weite Runde2", "abc", "Illmitz", "Podersdorf", 20);
            ObservableCollection<Tourlog> Tourloglist = new ObservableCollection<Tourlog>();
            ObservableCollection<Tour> Tourlog = new ObservableCollection<Tour>();

            Tourloglist.Add(TL1);
            Tour1.Tourlogs = Tourloglist;
            Tourlog.Add(Tour1);
            Tourlog.Add(Tour2);
            try
            {

                JsonOperator.Export(Tourlog, @"Z:\tmp");

            }
            catch (Exception e)
            {
                ex_returnval = e;
            }



            Assert.AreEqual(typeof(DirectoryNotFoundException), ex_returnval.GetType());
            Assert.Pass();
        }

        [Test]
        public void Buisnesslogic_DeserializeTourJson()
        {
            Tour Tour1 = new Tour(null,"Weite Runde", "abc", "Illmitz", "Podersdorf", 20);
            Tourlog TL1 = new Tourlog(-1, DateTime.Now, "Very Nice", 10, 120, 3, 3.34, 4, 200, 40, 3.5);
          

            ObservableCollection<Tourlog> Tourloglist = new ObservableCollection<Tourlog>();
            ObservableCollection<Tour> Tourlog = new ObservableCollection<Tour>();
            Tourloglist.Add(TL1);
            Tour1.Tourlogs = Tourloglist;
            Tourlog.Add(Tour1);

            ObservableCollection<Tour> returnval = JsonOperator.DeserializeTours(@"E:\tmp\Tours.json");

            Assert.AreEqual(returnval[0].Name, Tourlog[0].Name);
        }

        [Test]
        public void Buisnesslogic_CreatePDF()
        {
            Tour Tour1 = new Tour(null, "Weite Runde", "abc", "Illmitz", "Podersdorf", 20);
            Tourlog TL1 = new Tourlog(-1, DateTime.Now, "Very Nice", 10, 120, 3, 3.34, 4, 200, 40, 3.5);


            ObservableCollection<Tourlog> Tourloglist = new ObservableCollection<Tourlog>();
            ObservableCollection<Tour> Tourlog = new ObservableCollection<Tour>();
            Tourloglist.Add(TL1);
            Tour1.Tourlogs = Tourloglist;
            

           PdfWriter.CreatePdfReport(@"C:\tmp", Tour1);

            Assert.AreEqual(true, File.Exists(@"C:\tmp\Report.pdf") );
        }

        [Test]
        public void Buisnesslogic_CreatePDFFailed()
        {
            Exception ex_returnval = null;
            Tour Tour1 = new Tour(null, "Weite Runde", "abc", "Illmitz", "Podersdorf", 20);
            Tourlog TL1 = new Tourlog(-1, DateTime.Now, "Very Nice", 10, 120, 3, 3.34, 4, 200, 40, 3.5);


            ObservableCollection<Tourlog> Tourloglist = new ObservableCollection<Tourlog>();
            ObservableCollection<Tour> Tourlog = new ObservableCollection<Tour>();
            Tourloglist.Add(TL1);
            Tour1.Tourlogs = Tourloglist;

            try
            {
                int returnval = PdfWriter.CreatePdfReport(@"U:\tmp", Tour1);
            }
            catch (Exception e)
            {
                  ex_returnval = e;
            }

            

            Assert.AreEqual(typeof(DirectoryNotFoundException), ex_returnval.GetType());
        }

        [TearDown]
         public void Teardown()
         {
            File.Delete(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\test.jpg"); 
            File.Delete(@"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\ASD.jpg");
            File.Delete(@"C:\tmp\Report.pdf");
        }
    }
}