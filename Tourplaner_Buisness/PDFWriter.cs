using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Drawing;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Grid;
using Tourplaner_Utility;

namespace Tourplaner_Buisness
{

    /// <summary>This Class has the Job of Creating the PDF Summary and The PDF Report</summary>
    public static class PdfWriter
    {


        /// <summary>Creates the PDF report.
        /// Needs A valid Path to a Folder as a String</summary>
        /// <param name="path">The path.</param>
        /// <param name="selectedTour">The selected tour.</param>
        /// <returns>
        ///   <para>No Returnvalue, but a Directory not Found Exception may be thrown</para>
        /// </returns>
        public static int  CreatePdfReport(string path, Tour selectedTour)
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
                foreach (Tourlog tl in selectedTour.Tourlogs)
                {

                    dataTable.Rows.Add(tl.PrintToPDF());
                }
                //Assign data source
                pdfGrid.DataSource = dataTable;

                //Load the image from the disk
                FileStream imageFileStream = File.Open(selectedTour.Image, FileMode.Open);
                PdfBitmap image = new PdfBitmap(imageFileStream);
                graphics.DrawImage(image, 0, 0);
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


        /// <summary>Creates the PDF summary.</summary>
        /// <param name="path">The path.</param>
        /// <param name="Tourlist">The tourlist.</param>
        /// <returns>
        ///   <para>No Returnvalue, but may throw a Directory not found Exception</para>
        /// </returns>
        public static int CreatePdfSummary(string path, ObservableCollection<Tour> Tourlist)
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

                graphics.DrawString("Tour Summary", font, PdfBrushes.Black, new PointF(10, 10));

                PdfGrid pdfGrid = new PdfGrid();
                //Create a DataTable
                DataTable dataTable = new DataTable();
                //Add columns to the DataTable
                dataTable.Columns.Add("Name");
                dataTable.Columns.Add("Description");
                dataTable.Columns.Add("Source");
                dataTable.Columns.Add("Destination");
                dataTable.Columns.Add("Distance");
                dataTable.Columns.Add("Number of Logs");
                //Add rows to the DataTable
                foreach (Tour tl in Tourlist)
                {

                    dataTable.Rows.Add(tl.PrintToPDF());
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
    }
}
