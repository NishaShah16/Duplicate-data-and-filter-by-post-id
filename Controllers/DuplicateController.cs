using DiplicateReport.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Mvc;
using System.IO;

namespace DiplicateReport.Controllers
{
    public class DuplicateController : Controller
    {
        // GET: Duplicate
            DataClass _dataClass = new DataClass();

        public ActionResult Index(string searchPost)
        {
            List<Duplicate> duplicates;
            duplicates = _dataClass.GetAllCandidates();

            if (!string.IsNullOrEmpty(searchPost))
            {
                duplicates = _dataClass.GetCandidatesByPost(searchPost);

            }
            else
            {
                duplicates = _dataClass.GetAllCandidates();
            }
            if (!string.IsNullOrEmpty(searchPost))
            {
                if (duplicates.Count > 0)
                {
                    TempData["Message"] = $"Showing  records for Post: {searchPost}";
                }
                else
                {
                    TempData["Message"] = $"No records found for Post: {searchPost}";
                }
            }

            
            var groupedCandidates = duplicates.GroupBy(c => c.IndexNo).ToList();
            return View(groupedCandidates);
        }
        //public FileResult GeneratePdf(string searchPost)
        //{
        //    // Fetching data based on the searchPost parameter
        //    List<Duplicate> duplicates = string.IsNullOrEmpty(searchPost)
        //        ? _dataClass.GetAllCandidates()
        //        : _dataClass.GetCandidatesByPost(searchPost);

        //    // Grouping candidates by IndexNo (or PostId as needed)
        //    var groupedCandidates = duplicates.GroupBy(c => c.IndexNo).ToList();

        //    // Creating a memory stream to store the PDF document
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        Document document = new Document(PageSize.A4, 25, 25, 30, 30);
        //        PdfWriter writer = PdfWriter.GetInstance(document, ms);
        //        document.Open();

        //        // Defining fonts for headers and content
        //        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
        //        Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);

        //        // Adding some space before the title
        //        document.Add(new Paragraph(""));

        //        // Title (Centered)
        //        Paragraph title = new Paragraph("Constable GD 2025", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
        //        title.Alignment = Element.ALIGN_CENTER;
        //        document.Add(title);

        //        // Subtitle (Centered)
        //        Paragraph subtitle = new Paragraph("Multiple Registration (Different details)", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12));
        //        subtitle.Alignment = Element.ALIGN_CENTER;
        //        document.Add(subtitle);
        //        document.Add(new Paragraph("\n"));

        //        // Creating a table with 4 columns
        //        PdfPTable table = new PdfPTable(4); // 4 Columns
        //        table.WidthPercentage = 100;
        //        table.SetWidths(new float[] { 10, 30, 40, 20 }); // Adjust column widths

        //        // Add header row
        //        AddCellWithBorder(table, "ID", true, normalFont);
        //        AddCellWithBorder(table, "Registration No", true, normalFont);
        //        AddCellWithBorder(table, "Candidate Name", true, normalFont);
        //        AddCellWithBorder(table, "Photo", true, normalFont);

        //        // Loop through groups and add rows to the table
        //        foreach (var group in groupedCandidates)
        //        {
        //            // Loop through the candidates within each group
        //            foreach (var item in group)
        //            {
        //                // Add candidate data with light internal borders
        //                AddCellWithBorder(table, item.id.ToString(), false, normalFont);
        //                AddCellWithBorder(table, item.RegistrationNo, false, normalFont);
        //                AddCellWithBorder(table, item.CandidateName, false, normalFont);

        //                // Load Image from Server Path and add to table
        //                string imagePath = HttpContext.Server.MapPath($"~/Duplicate Registration/{item.PhotoPath}");
        //                if (System.IO.File.Exists(imagePath))
        //                {
        //                    Image candidateImage = Image.GetInstance(imagePath);
        //                    candidateImage.ScaleAbsolute(60f, 60f); // Resize Image
        //                    PdfPCell imageCell = new PdfPCell(candidateImage)
        //                    {
        //                        HorizontalAlignment = Element.ALIGN_CENTER,
        //                        VerticalAlignment = Element.ALIGN_MIDDLE,
        //                        BorderWidth = 1f, // Light border inside the image cell
        //                        BorderColor = BaseColor.LIGHT_GRAY // Light internal border
        //                    };
        //                    table.AddCell(imageCell);
        //                }
        //                else
        //                {
        //                    AddCellWithBorder(table, "No Image", false, normalFont);
        //                }
        //            }

        //            // Dark separator line after each group
        //            PdfPCell separatorCell = new PdfPCell(new Phrase(string.Empty))
        //            {
        //                Colspan = 4,
        //                BorderWidth = 2f,
        //                BorderColor = BaseColor.DARK_GRAY,
        //                Padding = 5f
        //            };
        //            table.AddCell(separatorCell);
        //        }

        //        // Add the table to the document
        //        document.Add(table);

        //        // Close the document
        //        document.Close();

        //        // Return the generated PDF as a file response
        //        return File(ms.ToArray(), "application/pdf", "GDConstable.pdf");
        //    }
        //}

        //// Helper method to add cells with light borders to the table
        //private void AddCellWithBorder(PdfPTable table, string text, bool isHeader, Font font)
        //{
        //    PdfPCell cell = new PdfPCell(new Phrase(text, font))
        //    {
        //        BorderWidth = 1f, // Light border
        //        BorderColor = BaseColor.LIGHT_GRAY, // Light border color
        //        Padding = 5f
        //    };

        //    if (isHeader)
        //    {
        //        cell.BackgroundColor = BaseColor.LIGHT_GRAY; // Light background for header
        //    }

        //    table.AddCell(cell);
        //}

    }
}
