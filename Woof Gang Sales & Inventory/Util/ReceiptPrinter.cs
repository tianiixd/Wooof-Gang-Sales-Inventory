using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Models;

namespace Woof_Gang_Sales___Inventory.Util
{
    public static class ReceiptPrinter
    {
        // This will hold "drawing actions"
        private static Queue<Action<Graphics, float, float>> printActions;
        private static Font fontBold, fontNormal, fontLarge;
        private static float currentY;
        private static float printableWidth;

        // ✅ NEW: This bool will track our "two-page" trick
        private static bool isSecondPage;

        public static void Print(Sale sale, List<CartItem> cartItems, string cashierName, decimal amountTendered, decimal changeDue, string printerName)
        {
            try
            {
                // --- 1. SETUP FONTS AND QUEUE ---
                fontBold = new Font("Helvetica", 7f, FontStyle.Bold);
                fontNormal = new Font("Helvetica", 7f, FontStyle.Regular);
                fontLarge = new Font("Helvetica", 9f, FontStyle.Regular);

                printActions = new Queue<Action<Graphics, float, float>>();

                // --- 2. BUILD THE DRAWING ACTIONS (mimicking your PDF) ---

                // Helper to add a centered line
                Action<string, Font> AddCenteredLine = (text, font) =>
                {
                    printActions.Enqueue((g, x, y) => {
                        float textWidth = g.MeasureString(text, font).Width;
                        // ✅ FIX 1 (CENTERING): Center based on the *full* printableWidth
                        float centeredX = (printableWidth - textWidth) / 2;
                        if (centeredX < 0) centeredX = 0;
                        g.DrawString(text, font, Brushes.Black, centeredX, y);
                    });
                };

                // Helper to add a left-aligned line (with word wrap)
                Action<string, Font> AddLeftLine = (text, font) =>
                {
                    printActions.Enqueue((g, x, y) => {
                        RectangleF rect = new RectangleF(x, y, printableWidth, font.GetHeight(g) * 2);
                        g.DrawString(text, font, Brushes.Black, rect);
                    });
                };

                // Helper to add a spacer
                Action AddSpacer = () => {
                    printActions.Enqueue((g, x, y) => { /* Just advances Y */ });
                };

                // Helper to add a dashed line
                Action AddDashedLine = () => {
                    printActions.Enqueue((g, x, y) => {
                        // This is your dash string, I will not change it.
                        string dashes = "-----------------------------------------------------------------";
                        float textWidth = g.MeasureString(dashes, fontNormal).Width;
                        float centeredX = (printableWidth - textWidth) / 2;
                        if (centeredX < 0) centeredX = 0;
                        g.DrawString(dashes, fontNormal, Brushes.Black, centeredX, y);
                    });
                };

                // Helper for left-right (e.g., "Total: ... P100.00")
                Action<string, string, Font> AddLeftRightLine = (left, right, font) =>
                {
                    printActions.Enqueue((g, x, y) => {
                        g.DrawString(left, font, Brushes.Black, x, y);
                        float rightWidth = g.MeasureString(right, font).Width;
                        // ✅ FIX 1 (ALIGNMENT): Align to the *full* printableWidth
                        float rightX = printableWidth - rightWidth;
                        g.DrawString(right, font, Brushes.Black, rightX, y);
                    });
                };

                // --- 3. REBUILD YOUR PDF LAYOUT ---
                // (This part of your code is perfect)

                // --- Header ---
                AddCenteredLine("WooofGang Pet Store", fontBold);
                AddCenteredLine("#40 Yakal Street, Neopolitan VI", fontNormal);
                AddCenteredLine("Sitio Seville, Fairview, Quezon City", fontNormal);
                DateTime dt = sale.SaleDate.Date + sale.SaleTime;
                AddCenteredLine(dt.ToString("M/d/yyyy hh:mm tt"), fontNormal);
                AddSpacer();

                // --- Sale Info ---
                AddLeftLine($"Sales Invoice No.: {sale.SaleID.ToString("D6")}", fontNormal);
                AddLeftLine($"Cashier: {cashierName}", fontNormal);
                AddDashedLine();

                // --- Items (The 2-Line Fix) ---
                foreach (var item in cartItems)
                {
                    AddLeftLine(item.ProductBrand + " " + item.ProductName, fontNormal);
                    string details = $"  ({item.Quantity} @ {item.UnitPrice.ToString("N2")})";
                    AddLeftRightLine(details, item.Subtotal.ToString("N2"), fontNormal);
                }
                AddDashedLine();

                // --- Totals ---
                decimal subtotal = cartItems.Sum(i => i.Subtotal);
                decimal discount = subtotal - sale.TotalAmount;
                AddLeftRightLine("Subtotal:", subtotal.ToString("N2"), fontNormal);
                AddLeftRightLine("Discount:", discount.ToString("N2"), fontNormal);
                AddLeftRightLine("TOTAL:", $"P{sale.TotalAmount.ToString("N2")}", fontBold);
                AddSpacer();
                AddLeftRightLine("Payment Method:", sale.PaymentMethod, fontNormal);
                AddLeftRightLine("Cash:", $"P{amountTendered.ToString("N2")}", fontNormal);
                AddLeftRightLine("Change:", $"P{changeDue.ToString("N2")}", fontNormal);
                AddSpacer();

                // --- Footer ---
                AddCenteredLine("Thank you for shopping!", fontLarge);
                AddDashedLine();
                AddCenteredLine("VAT Reg TIN: 000-000-000-000", fontNormal);
                AddCenteredLine("DTI Permit No: 12345678", fontNormal);
                AddSpacer();
                AddSpacer();
                AddCenteredLine("Follow us on Facebook!", fontBold);
                AddCenteredLine("fb.com/wooofgang.pets", fontNormal);

                // ✅ FIX 2 (CUT-OFF): Removed all 15 spacers from here.
                // We will use the "two-page" trick instead.

                // --- 4. SEND TO PRINTER ---
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = printerName;

                // This is your 48mm "roll" setting, which is correct
                PaperSize customPaperSize = new PaperSize("48mmRoll", 189, 12900);
                pd.DefaultPageSettings.PaperSize = customPaperSize;

                // ✅ FIX 1 (CENTERING): Set all margins to 0.
                pd.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

                pd.PrintPage += new PrintPageEventHandler(Pd_PrintPage);

                // ✅ FIX 2 (CUT-OFF): Reset our "two-page" tracker
                isSecondPage = false;

                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not print the receipt. Check printer.\n" + ex.Message, "Print Error");
            }
        }

        // This event handler is called by the PrintDocument
        private static void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;

            // ✅ FIX 2 (CUT-OFF): Check if this is the "flush" page
            if (isSecondPage)
            {
                e.HasMorePages = false;
                return; // Stop. Do not print anything on the second page.
            }

            // ✅ FIX 1 (CENTERING): Use the *full* page width
            printableWidth = e.PageBounds.Width;
            float leftMargin = e.PageBounds.Left; // Should be 0
            currentY = e.PageBounds.Top; // Should be 0

            // Draw each "action" from our queue
            while (printActions.Count > 0)
            {
                Action<Graphics, float, float> action = printActions.Dequeue();
                action(g, leftMargin, currentY);

                // Advance Y position
                currentY += fontNormal.GetHeight(g) + 1; // +1 for line spacing
            }

            // ✅ FIX 2 (CUT-OFF): The queue is empty.
            // Tell the printer "we have one more (blank) page".
            // This forces it to "flush" this page before cutting.
            e.HasMorePages = true;
            isSecondPage = true;
        }
    }
}