using System;
using System.Collections.Generic;
using Tourplaner_Data;
using Tourplaner_Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Tourplaner_Buisness
{
    static public class Mainlogic
    {

        public static int  CreatePDF(string path, Tour selectedTour)
        {
            try
            {
                FileStream fs = File.Open(Path.Combine(path, "Report.pdf"), FileMode.Create);
                if (!fs.CanWrite)
                {
                    return -1;
                }
                PdfDocument document = new PdfDocument();

                PdfPage page = document.Pages.Add();

                PdfGraphics graphics = page.Graphics;

                PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

                graphics.DrawString(selectedTour.Name, font, PdfBrushes.Black, new PointF(10, 10));

                PdfGrid pdfGrid = new PdfGrid();
                //Create a DataTable
                DataTable dataTable = new DataTable();
                //Add columns to the DataTable
                dataTable.Columns.Add("TLID");
                dataTable.Columns.Add("Timestamp");
                dataTable.Columns.Add("Report");
                dataTable.Columns.Add("Distance");
                dataTable.Columns.Add("Totaltime");
                dataTable.Columns.Add("Rating");
                dataTable.Columns.Add("AvgSpeed");
                dataTable.Columns.Add("Difficulty");
                dataTable.Columns.Add("EnergyBurn");
                dataTable.Columns.Add("Temperature");
                dataTable.Columns.Add("Water");
                //Add rows to the DataTable
                foreach (Tourlog TL in selectedTour.Tourlogs)
                {

                    dataTable.Rows.Add(TL.PrintToPDF());
                }
                //Assign data source
                pdfGrid.DataSource = dataTable;
                //Draw grid to the page of PDF document
                pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 40));

                document.Save(fs);

                document.Close(true);
                fs.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return 0;

        }
        public static async Task<int> SaveTour(Tour tour)
        {
            try
            {
                Task<float> tmp = WebRequester.ReturnDistance(tour.Source, tour.Destination);
                await tmp;
                tour.Distance = tmp.Result;

                int rCode = Database.InsertTour(tour);
                if (rCode == 0)
                {
                    FetchImage(tour.Name, tour.Source, tour.Destination);
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Encountered:{0}", e.Message);
            }

            return 1;
        }

        public static void DeleteTourLog(Tourlog tmp)
        {
            Database.DeleteTourlog(tmp.TLID);
        }

        public static void Inserttourlog(Tourlog tmp, string selectedname)
        {
            Database.InsertTourlogs(tmp, selectedname);
        }

        public static ObservableCollection<Tour> UpdateTours(string term = "")
        {
            return Database.SearchTours(term);
        }

        public static async  Task<int>  DeleteTour(Tour tour)
        {
            DeleteTourimage(tour.Image);
            Database.DeleteTour(tour.Name);
            return 1;

        }

        public static void DeleteTourimage(string imagepath)
        {
            File.Delete(imagepath);
        }

        public static int CopyTour(string tmp)
        {

            return Database.CopyTour(tmp);
        }

        public static async Task<int> FetchImage(string tourname, string from, string to)
        {
            byte[] image = null;
            try
            {
                byte [] tmp = await WebRequester.GetPicture(from, to);
                image = tmp;
                SaveImage(image, tourname);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Encountered: {0}", e.Message);
            }

            return 0;
        }


        public static void SaveImage(byte[] image, string tourname)
        {
            try
            {
                string path = @"E:\Programming\C#\SWE2\Tourplaner_Buisness\Images\" + tourname+".jpg";
                using (var sw = new FileStream(path,FileMode.Create, FileAccess.Write) )
                {
                    sw.Write(image);
                    sw.Close();
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Encountered:{0}", e.Message);
            }

        }

        public static void Export(ObservableCollection<Tour> tourlist, string filepath)
        {
            string JSON = SerializeTours(tourlist);


            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filepath, "Tours.json")))
            {
                outputFile.WriteLine(JSON);
            }


        }

        public static string SerializeTours(ObservableCollection<Tour> tourlist)
        {
            string returnstring = String.Empty;
            returnstring += JsonSerializer.Serialize<ObservableCollection<Tour>>(tourlist);
            
          /*  foreach (var obj in tourlist)
            {
                
            }*/

          return returnstring;
        }

        public static void Import(string filepath)
        {
            ObservableCollection<Tour> tmp = DeserializeTours(filepath);

            Database.NukeDatabase();

            foreach (var tmpTour in tmp)
            {
                Database.InsertTour(tmpTour);
                foreach (var tmpLog in tmpTour.Tourlogs)
                {
                    Database.InsertTourlogs(tmpLog, tmpTour.Name);
                }

            }
        }

        public static ObservableCollection<Tour> DeserializeTours(string filepath)
        {
            Tour tmp = null;
            ObservableCollection<Tour> retunrval = new ObservableCollection<Tour>();
            string s = File.ReadAllText(filepath);
            retunrval = JsonSerializer.Deserialize<ObservableCollection<Tour>>(s);


            return retunrval;
        }
    }
}


