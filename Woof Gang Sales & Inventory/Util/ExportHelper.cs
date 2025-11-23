using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.StyledXmlParser.Jsoup.Nodes;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Util;
namespace Woof_Gang_Sales___Inventory.Util
{
    public static class ExportHelper
    {
        // ... (Keep ExportGridToCSV) ...

        /// <summary>
        /// Prints the DataGridView (User can select 'Microsoft Print to PDF')
        /// </summary>
        /// 

        public static void SalesExportGridToCSV(DataGridView grid, string filename)
        {
            // 1. Safety Check: Is there data?
            if (grid.Rows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Data", "There is no data to export.", "warning");
                return;
            }

            try
            {
                // 2. Open the Save File Dialog
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel CSV (*.csv)|*.csv";
                sfd.FileName = $"{filename}_{DateTime.Now:yyyy_MM_dd_HHmm}.csv"; // e.g. Sales_Report_20251123_1430.csv

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();

                    // --- STEP 3: WRITE HEADERS ---
                    for (int i = 0; i < grid.Columns.Count; i++)
                    {
                        if (grid.Columns[i].Visible) // Only export visible columns
                        {
                            csv.Append(grid.Columns[i].HeaderText + ",");
                        }
                    }
                    csv.AppendLine(); // End header row

                    // --- STEP 4: WRITE ROWS ---
                    foreach (DataGridViewRow row in grid.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int i = 0; i < grid.Columns.Count; i++)
                            {
                                if (grid.Columns[i].Visible)
                                {
                                    // Get value, handle nulls
                                    string cellValue = row.Cells[i].Value?.ToString() ?? "";

                                    // IMPORTANT: Replace commas with spaces to prevent breaking the CSV format
                                    // (Since CSV means "Comma Separated Values", a comma inside data breaks columns)
                                    cellValue = cellValue.Replace(",", " ");

                                    // Remove newlines to keep row integrity
                                    cellValue = cellValue.Replace("\n", " ").Replace("\r", "");

                                    csv.Append(cellValue + ",");
                                }
                            }
                            csv.AppendLine(); // End data row
                        }
                    }

                    // --- STEP 5: SAVE TO FILE ---
                    File.WriteAllText(sfd.FileName, csv.ToString());

                    DialogHelper.ShowCustomDialog("Success", "Report exported successfully!", "success");

                    // Optional: Auto-open the file after saving
                    // System.Diagnostics.Process.Start("explorer.exe", sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                DialogHelper.ShowCustomDialog("Export Error", $"Failed to export: {ex.Message}", "error");
            }
        }



        public static void SalesExportGridToPdf(DataGridView grid, string filename, string reportTitle, string generatedBy)
        {
            if (grid.Rows.Count == 0)
            {
                DialogHelper.ShowCustomDialog("No Data", "No data available to export.", "warning");
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF File|*.pdf";
            sfd.FileName = $"{filename}_{DateTime.Now:yyyy_MM_dd}.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (PdfWriter writer = new PdfWriter(sfd.FileName))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            // ✅ FIX: Use "iText.Layout.Document" explicitly to avoid conflict
                            using (iText.Layout.Document doc = new iText.Layout.Document(pdf, PageSize.A4.Rotate()))
                            {
                                doc.SetMargins(40, 40, 40, 40);

                                // Fonts
                                PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                                PdfFont fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                                // --- HEADER ---
                                doc.Add(new Paragraph("Wooof Gang Pet Hotel, Grooming & Supplies")
                                    .SetFont(fontBold).SetFontSize(16).SetTextAlignment(TextAlignment.CENTER));

                                doc.Add(new Paragraph("#40 Yakal Street, Neopolitan VI Sitio Seville, Fairview, Quezon City.\nContact: 09626947474")
                                    .SetFont(fontNormal).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)
                                    .SetFontColor(ColorConstants.DARK_GRAY));

                                // --- TITLE ---
                                doc.Add(new Paragraph(reportTitle.ToUpper())
                                    .SetFont(fontBold).SetFontSize(14).SetTextAlignment(TextAlignment.CENTER)
                                    .SetMarginTop(20).SetMarginBottom(20));

                                // --- TABLE ---
                                int visibleCols = 0;
                                foreach (DataGridViewColumn col in grid.Columns) if (col.Visible) visibleCols++;

                                Table table = new Table(visibleCols).UseAllAvailableWidth();

                                // Headers
                                foreach (DataGridViewColumn col in grid.Columns)
                                {
                                    if (col.Visible)
                                    {
                                        Cell header = new Cell().Add(new Paragraph(col.HeaderText));
                                        header.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                                        header.SetFont(fontBold).SetFontSize(10);
                                        header.SetTextAlignment(TextAlignment.CENTER);
                                        table.AddHeaderCell(header);
                                    }
                                }

                                // Rows & Total Calculation
                                decimal totalRevenue = 0;

                                foreach (DataGridViewRow row in grid.Rows)
                                {
                                    if (!row.IsNewRow)
                                    {
                                        // Calculate Total Logic
                                        if (grid.Columns.Contains("Revenue") && row.Cells["Revenue"].Value != null)
                                        {
                                            string rawVal = row.Cells["Revenue"].Value.ToString().Replace("₱", "").Replace(",", "");
                                            if (decimal.TryParse(rawVal, out decimal val)) totalRevenue += val;
                                        }

                                        foreach (DataGridViewCell gridCell in row.Cells)
                                        {
                                            if (gridCell.OwningColumn.Visible)
                                            {
                                                string val = gridCell.Value?.ToString() ?? "";
                                                Cell cell = new Cell().Add(new Paragraph(val));
                                                cell.SetFont(fontNormal).SetFontSize(9).SetPadding(5);

                                                if (val.StartsWith("₱") || decimal.TryParse(val, out _))
                                                    cell.SetTextAlignment(TextAlignment.RIGHT);
                                                else
                                                    cell.SetTextAlignment(TextAlignment.LEFT);

                                                table.AddCell(cell);
                                            }
                                        }
                                    }
                                }

                                doc.Add(table);

                                // --- FOOTER & TOTALS ---
                                Paragraph pTotal = new Paragraph($"Total Sales: {totalRevenue:N2}")
                                    .SetFont(fontBold).SetFontSize(12)
                                    .SetTextAlignment(TextAlignment.RIGHT).SetMarginTop(10);
                                doc.Add(pTotal);

                                Paragraph pFooter = new Paragraph()
                                    .SetFont(fontNormal).SetFontSize(9)
                                    .SetTextAlignment(TextAlignment.RIGHT).SetMarginTop(30)
                                    .SetFontColor(ColorConstants.DARK_GRAY);

                                pFooter.Add($"Generated by: {generatedBy}\n");
                                pFooter.Add($"Date Generated: {DateTime.Now:MM/dd/yyyy h:mm tt}");

                                doc.Add(pFooter);
                            }
                        }
                    }

                    DialogHelper.ShowCustomDialog("Success", "Report exported successfully!", "success");
                    try { Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true }); } catch { }
                }
                catch (Exception ex)
                {
                    DialogHelper.ShowCustomDialog("PDF Error", ex.Message, "error");
                }
            }
        }


        public static void ExportInventoryToPdf(DataGridView grid, string filename, string generatedBy, string titleOverride = "INVENTORY REPORT")
        {
            if (grid.Rows.Count == 0) return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF File|*.pdf";
            sfd.FileName = $"{filename}_{DateTime.Now:yyyyMMdd}.pdf";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (PdfWriter writer = new PdfWriter(sfd.FileName))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            // Use A4 Landscape for Inventory (Columns are wide)
                            using (iText.Layout.Document doc = new iText.Layout.Document(pdf, PageSize.A4.Rotate()))
                            {
                                doc.SetMargins(20, 20, 20, 20);

                                PdfFont fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                                PdfFont fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                                // --- HEADER (Matches your image) ---
                                doc.Add(new Paragraph("Wooof Gang Pet Hotel, Grooming & Supplies")
                                    .SetFont(fontBold).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));

                                doc.Add(new Paragraph("#40 Yakal Street, Neopolitan VI Sitio Seville, Fairview, Quezon City.\nContact: 0962 694 7474")
                                    .SetFont(fontNormal).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER)
                                    .SetFontColor(ColorConstants.DARK_GRAY)
                                    .SetMarginBottom(20)); // Add space before title

                                // --- TITLE ---
                                doc.Add(new Paragraph(titleOverride.ToUpper())
                                    .SetFont(fontBold).SetFontSize(14).SetTextAlignment(TextAlignment.CENTER)
                                    .SetMarginBottom(15));

                                // --- TABLE ---
                                int visibleCols = 0;
                                foreach (DataGridViewColumn col in grid.Columns) if (col.Visible) visibleCols++;
                                Table table = new Table(visibleCols).UseAllAvailableWidth();

                                // Headers
                                foreach (DataGridViewColumn col in grid.Columns)
                                {
                                    if (col.Visible)
                                    {
                                        Cell header = new Cell().Add(new Paragraph(col.HeaderText));
                                        header.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                                        header.SetFont(fontBold).SetFontSize(9);
                                        header.SetTextAlignment(TextAlignment.CENTER);
                                        table.AddHeaderCell(header);
                                    }
                                }

                                // Rows
                                foreach (DataGridViewRow row in grid.Rows)
                                {
                                    if (!row.IsNewRow)
                                    {
                                        foreach (DataGridViewCell gridCell in row.Cells)
                                        {
                                            if (gridCell.OwningColumn.Visible)
                                            {
                                                string val = gridCell.Value?.ToString() ?? "";
                                                Cell cell = new Cell().Add(new Paragraph(val));
                                                cell.SetFont(fontNormal).SetFontSize(9).SetPadding(4);

                                                // --- ALIGNMENT LOGIC ---
                                                cell.SetTextAlignment(TextAlignment.CENTER); // Default Center

                                                // Exception: Money is Right Aligned
                                                if (val.StartsWith("₱") || gridCell.OwningColumn.HeaderText.Contains("Value") || gridCell.OwningColumn.HeaderText.Contains("Cost"))
                                                {
                                                    cell.SetTextAlignment(TextAlignment.RIGHT);
                                                }
                                                // Exception: Long text like Item Name is Left Aligned
                                                else if (gridCell.OwningColumn.Name == "ItemName" || gridCell.OwningColumn.Name == "ProductName")
                                                {
                                                    cell.SetTextAlignment(TextAlignment.LEFT);
                                                }

                                                // --- COLOR LOGIC ---
                                                if (gridCell.OwningColumn.Name == "Status")
                                                {
                                                    cell.SetFontColor(ColorConstants.WHITE);
                                                    if (val == "Out of Stock")
                                                        cell.SetBackgroundColor(new DeviceRgb(50, 50, 50)); // Dark Gray
                                                    else if (val == "Low Stock")
                                                        cell.SetBackgroundColor(ColorConstants.ORANGE);
                                                    else
                                                        cell.SetBackgroundColor(new DeviceRgb(40, 167, 69)); // Green
                                                }

                                                table.AddCell(cell);
                                            }
                                        }
                                    }
                                }
                                doc.Add(table);

                                // --- FOOTER (Bottom Right) ---
                                Paragraph pFooter = new Paragraph()
                                    .SetFont(fontNormal)
                                    .SetFontSize(9)
                                    .SetTextAlignment(TextAlignment.RIGHT)
                                    .SetMarginTop(20)
                                    .SetFontColor(ColorConstants.DARK_GRAY);

                                pFooter.Add($"Generated by: {generatedBy}\n");
                                pFooter.Add($"Date Generated: {DateTime.Now:MMMM dd, yyyy h:mm tt}");

                                doc.Add(pFooter);
                            }
                        }
                    }
                    DialogHelper.ShowCustomDialog("Success", "Inventory Report exported!", "success");
                    try { Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true }); } catch { }
                }
                catch (Exception ex) { DialogHelper.ShowCustomDialog("PDF Error", ex.Message, "error"); }
            }
        }



    }
}