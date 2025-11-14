using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq; // Added for .Sum()
using System.Windows.Forms; // Required for MessageBox
using Woof_Gang_Sales___Inventory.Models;

namespace Woof_Gang_Sales___Inventory.Util
{
    public static class PdfGenerator
    {

        public static string CreateReceipt(Sale sale, List<CartItem> cartItems, string cashierName, decimal amountTendered, decimal changeDue)
        {
    string fileName = string.Empty;
    string filePath = string.Empty;

    try
    {
        // --- 1. Define Fonts and Font Sizes ---
        // ✅ FIX: Switched back to COURIER for the professional "receipt" look
        PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
        PdfFont fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

        float stdSize = 8f;   // Standard size for almost everything
        float largeSize = 10f; // A little bigger for the "Thank you"

        // --- 2. Define File Path ---
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string receiptsDir = System.IO.Path.Combine(desktopPath, "WooofGang_Receipts");
        Directory.CreateDirectory(receiptsDir);
        fileName = $"Sale_{sale.SaleID.ToString("D6")}.pdf";
        filePath = System.IO.Path.Combine(receiptsDir, fileName);

        using (PdfWriter writer = new PdfWriter(filePath))
        {
            using (PdfDocument pdf = new PdfDocument(writer))
            {
                // --- 3. Create the custom tall page ---
                
                // ✅ *** THE CRITICAL CHANGE FOR YOUR PRINTER ***
                // 1mm = 2.8346 points. So 57mm is ~162 points.
                float receiptWidth = 162f; // This matches your 57mm paper
                
                // We still use a tall height so it never splits to a 2nd page
                float receiptHeight = PageSize.A5.GetHeight(); 
                
                PageSize customReceiptSize = new PageSize(receiptWidth, receiptHeight);

                using (Document doc = new Document(pdf, customReceiptSize))
                {
                    // Set tiny margins to use all the 57mm width
                    doc.SetMargins(10, 5, 10, 5); // Top, Right, Bottom, Left

                    // --- 4. Add Store Header ---
                    doc.Add(new Paragraph("WooofGang Pet Store")
                        .SetFont(fontBold) 
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(stdSize)); 

                    doc.Add(new Paragraph("#40 Yakal Street, Neopolitan VI")
                        .SetFont(fontNormal)
                        .SetFontSize(stdSize)
                        .SetTextAlignment(TextAlignment.CENTER).SetMarginTop(-5));

                    doc.Add(new Paragraph("Sitio Seville, Fairview, Quezon City")
                        .SetFont(fontNormal)
                        .SetFontSize(stdSize)
                        .SetTextAlignment(TextAlignment.CENTER).SetMarginTop(-5));

                    DateTime fullSaleDateTime = sale.SaleDate.Date + sale.SaleTime;
                    doc.Add(new Paragraph($"Date: {fullSaleDateTime.ToString("M/d/yyyy hh:mm tt")}")
                        .SetFont(fontNormal)
                        .SetFontSize(stdSize)
                        .SetTextAlignment(TextAlignment.CENTER).SetMarginTop(-5));
                    
                    doc.Add(new Paragraph("").SetMarginBottom(5)); 

                    // --- 5. Add Sale Info ---
                    doc.Add(new Paragraph($"Sales Invoice No.: {sale.SaleID.ToString("D6")}")
                        .SetFont(fontNormal)
                        .SetFontSize(stdSize));

                    doc.Add(new Paragraph($"Cashier: {cashierName}")
                        .SetFont(fontNormal)
                        .SetFontSize(stdSize)
                        .SetMarginTop(-5));

                    var line = new iText.Layout.Element.LineSeparator(new DashedLine(1f));
                    doc.Add(line.SetMarginTop(5).SetMarginBottom(5));

                    // --- 6. Add Items Table ---
                    // ✅ We use 4 columns, width is fine
                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 1f, 3f, 2f, 2f }))
                        .UseAllAvailableWidth();

                    table.AddCell(new Cell().Add(new Paragraph("Qty")).SetFont(fontNormal).SetFontSize(stdSize).SetBorder(Border.NO_BORDER));
                    table.AddCell(new Cell().Add(new Paragraph("Item")).SetFont(fontNormal).SetFontSize(stdSize).SetBorder(Border.NO_BORDER));
                    table.AddCell(new Cell().Add(new Paragraph("Price")).SetFont(fontNormal).SetFontSize(stdSize).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Cell().Add(new Paragraph("Cost")).SetFont(fontNormal).SetFontSize(stdSize).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));

                    foreach (var item in cartItems)
                    {
                        table.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToString())).SetFont(fontNormal).SetFontSize(stdSize).SetBorder(Border.NO_BORDER));
                        table.AddCell(new Cell().Add(new Paragraph(item.ProductName)).SetFont(fontNormal).SetFontSize(stdSize).SetBorder(Border.NO_BORDER));
                        table.AddCell(new Cell().Add(new Paragraph(item.UnitPrice.ToString("N2"))).SetFont(fontNormal).SetFontSize(stdSize).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
                        table.AddCell(new Cell().Add(new Paragraph(item.Subtotal.ToString("N2"))).SetFont(fontNormal).SetFontSize(stdSize).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
                    }
                    doc.Add(table);
                    doc.Add(line.SetMarginTop(5)); 

                    // --- 7. Add Totals ---
                    // This 2-column table is perfect
                    decimal subtotal = cartItems.Sum(item => item.Subtotal);
                    decimal discountAmount = subtotal - sale.TotalAmount;
                    
                    Table totalsTable = new Table(UnitValue.CreatePercentArray(new float[] { 1f, 1f }))
                        .UseAllAvailableWidth()
                        .SetFontSize(stdSize)
                        .SetFont(fontNormal);

                    // Helper to create cells
                    Action<Table, string, string, PdfFont> addRow = 
                        (tbl, label, value, font) =>
                        {
                            tbl.AddCell(new Cell().Add(new Paragraph(label)).SetFont(font).SetBorder(Border.NO_BORDER));
                            tbl.AddCell(new Cell().Add(new Paragraph(value)).SetFont(font).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER));
                        };

                    addRow(totalsTable, "Subtotal:", subtotal.ToString("N2"), fontNormal);
                    addRow(totalsTable, "Discount:", discountAmount.ToString("N2"), fontNormal);
                    addRow(totalsTable, "TOTAL:", $"P{sale.TotalAmount.ToString("N2")}", fontNormal); 
                    
                    totalsTable.AddCell(new Cell().Add(new Paragraph(" ")).SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph(" ")).SetBorder(Border.NO_BORDER));

                    addRow(totalsTable, "Payment Method:", sale.PaymentMethod, fontNormal);
                    addRow(totalsTable, "Cash:", $"P{amountTendered.ToString("N2")}", fontNormal);
                    addRow(totalsTable, "Change:", $"P{changeDue.ToString("N2")}", fontNormal);

                    doc.Add(totalsTable);

                    // --- 8. Add Footer ---
                    
                    doc.Add(new Paragraph("Thank you for shopping!")
                        .SetFont(fontNormal)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetMarginTop(15)
                        .SetFontSize(largeSize)); 

                    Div footerDiv = new Div()
                        .SetTextAlignment(TextAlignment.CENTER) 
                        .SetFont(fontNormal)                      
                        .SetFontSize(stdSize) 
                        .SetMarginTop(10);                      

                    var solidLine = new iText.Layout.Element.LineSeparator(new SolidLine(1f));
                    footerDiv.Add(solidLine.SetMarginBottom(5));

                    // ✅ FIX: Disclaimers are removed from here
                    
                    footerDiv.Add(new Paragraph("VAT Reg TIN: 000-000-000-000")
                        .SetMarginTop(10));
                    footerDiv.Add(new Paragraph("DTI Permit No: 12345678")
                        .SetMarginTop(-5));

                    footerDiv.Add(new Paragraph("Return & Exchange Policy")
                        .SetFont(fontBold).SetMarginTop(10)); 
                    footerDiv.Add(new Paragraph("Items may be returned within 7 days of purchase " +
                                              "with a valid receipt. Items must be in original, " +
                                              "unused condition. Pet food and treats are non-refundable.")
                        .SetMarginTop(-5));

                    footerDiv.Add(new Paragraph("Follow us on Facebook!")
                         .SetFont(fontBold).SetMarginTop(10));
                    footerDiv.Add(new Paragraph("fb.com/wooofgang.pets") 
                         .SetMarginTop(-5));

                    doc.Add(footerDiv);

                    doc.Add(new Paragraph("THIS SERVES AS YOUR SALES INVOICE")
                        .SetFont(fontNormal)
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(stdSize)
                        .SetMarginTop(0)); 
                }
            }
        }
        
        // --- 10. Open the PDF ---
        new Process
        {
            StartInfo = new ProcessStartInfo(filePath)
            {
                UseShellExecute = true
            }
        }.Start();

        // --- 11. Return the path for printing ---
        return filePath;
    }
    catch (IOException ioEx)
    {
        if (ioEx.Message.Contains("The process cannot access the file"))
        {
            MessageBox.Show($"Could not save the PDF. Is it already open in another program?\n\nFile: {fileName}", "PDF Error");
        }
        else
        {
            MessageBox.Show($"An error occurred while saving the PDF:\n\nMessage: {ioEx.Message}\n\nStack Trace: {ioEx.StackTrace}", "PDF IO Error");
        }
        return null; // Return null on failure
    }
    catch (Exception ex)
    {
        MessageBox.Show($"An error occurred while generating the PDF:\n\nMessage: {ex.Message}\n\nStack Trace: {ex.StackTrace}", "PDF Generator Error");
        return null; // Return null on failure
    }
}

        
    }
}