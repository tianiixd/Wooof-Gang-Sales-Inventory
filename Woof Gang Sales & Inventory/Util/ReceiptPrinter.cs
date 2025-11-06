//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Printing;
//using System.Linq;
//using System.Windows.Forms;
//using Woof_Gang_Sales___Inventory.Models;

//namespace Woof_Gang_Sales___Inventory.Util
//{
//    /// <summary>
//    /// This class uses the built-in .NET printing libraries
//    /// to generate and preview a receipt.
//    /// </summary>
//    public class ReceiptPrinter
//    {
//        // --- Data to print ---
//        private Sale _sale;
//        private List<CartItem> _cartItems;
//        private string _cashierName;
//        private decimal _amountTendered;
//        private decimal _changeDue;

//        // --- Drawing Tools ---
//        private Font fontBold = new Font("Courier New", 10, FontStyle.Bold);
//        private Font fontNormal = new Font("Courier New", 9, FontStyle.Regular);
//        private Font fontSmall = new Font("Courier New", 8, FontStyle.Regular);
//        private SolidBrush brush = new SolidBrush(Color.Black);
//        private float yPos = 0; // Tracks our vertical position on the page
//        private float leftMargin = 10;
//        private float rightMargin = 260; // Approx width for an 80mm receipt

//        /// <summary>
//        /// Constructor: Receives all data needed for the receipt.
//        /// </summary>
//        public ReceiptPrinter(Sale sale, List<CartItem> cartItems, string cashierName, decimal amountTendered, decimal changeDue)
//        {
//            _sale = sale;
//            _cartItems = cartItems;
//            _cashierName = cashierName;
//            _amountTendered = amountTendered;
//            _changeDue = changeDue;
//        }

//        /// <summary>
//        /// This is the main method that opens the Print Preview.
//        /// </summary>
//        public void Print()
//        {
//            // 1. Create the document to be printed
//            PrintDocument pd = new PrintDocument();

//            // Set the paper size (80mm is a common receipt width)
//            // 80mm = ~3.15 inches. 3.15 * 100 = 315
//            // Length is arbitrary, 11 inches (1100) is fine for preview.
//            pd.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 315, 1100);

//            // 2. Add an event handler for the "PrintPage" event
//            // This is where all our drawing logic happens
//            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

//            // 3. Create a Print Preview Dialog
//            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
//            previewDialog.Document = pd;

//            // Set preview settings
//            previewDialog.Width = 600;
//            previewDialog.Height = 800;
//            previewDialog.StartPosition = FormStartPosition.CenterScreen;
//            previewDialog.ShowIcon = false;

//            // 4. Show the dialog
//            previewDialog.ShowDialog();
//        }

//        /// <summary>
//        /// This event is fired when the Print() method is called.
//        /// It does all the actual drawing onto the 'e.Graphics' surface.
//        /// </summary>
//        private void pd_PrintPage(object sender, PrintPageEventArgs e)
//        {
//            Graphics g = e.Graphics;
//            yPos = 10; // Reset Y position for each page

//            StringFormat formatCenter = new StringFormat { Alignment = StringAlignment.Center };
//            StringFormat formatRight = new StringFormat { Alignment = StringAlignment.Far };
//            string line = "------------------------------------------";

//            // --- 1. Store Header ---
//            DrawText("WooofGang Pet Store", fontBold, formatCenter);
//            DrawText("#40 Yakal Street, Neopolitan VI", fontSmall, formatCenter);
//            DrawText("Sitio Seville, Fairview, Quezon City", fontSmall, formatCenter);
//            DrawText($"Date: {_sale.SaleDate.ToShortDateString()} {_sale.SaleTime.ToString(@"hh\:mm\:ss tt")}", fontSmall, formatCenter);
//            yPos += 10; // Add extra space

//            // --- 2. Sale Info ---
//            DrawText($"Receipt No.: {_sale.SaleID.ToString("D6")}");
//            DrawText($"Cashier: {_cashierName}");
//            DrawText(line);

//            // --- 3. Items Header ---
//            DrawText("Qty  Item", fontBold);
//            DrawText("Price", fontBold, new StringFormat { Alignment = StringAlignment.Far }); // Aligned right
//            yPos += 5;

//            // --- 4. Items List ---
//            foreach (var item in _cartItems)
//            {
//                // Draw Qty and Name
//                DrawText($"{item.Quantity}   {item.ProductName}", fontNormal);

//                // Draw Subtotal (aligned right)
//                DrawText(item.Subtotal.ToString("N2"), fontNormal, formatRight);

//                // Draw UnitPrice (on the line below, indented)
//                string unitPriceLine = $"    (@ {item.UnitPrice.ToString("N2")} ea)";
//                DrawText(unitPriceLine, fontSmall);
//                yPos += 5; // Add space between items
//            }
//            DrawText(line);

//            // --- 5. Totals ---
//            decimal subtotal = _cartItems.Sum(item => item.Subtotal);
//            decimal discountAmount = subtotal - _sale.TotalAmount;

//            DrawTextPair("SubTotal:", subtotal.ToString("N2"));
//            DrawTextPair("Discount:", discountAmount.ToString("N2"));
//            DrawTextPair("Grand Total:", _sale.TotalAmount.ToString("N2"), fontBold);
//            yPos += 10;

//            // --- 6. Payment Details ---
//            DrawTextPair("Payment Type:", _sale.PaymentMethod);
//            DrawTextPair("Tendered:", _amountTendered.ToString("N2"));
//            DrawTextPair("Change:", _changeDue.ToString("N2"), fontBold);
//            yPos += 15;

//            // --- 7. Footer ---
//            DrawText("Thank you for shopping!", fontNormal, formatCenter);
//            DrawText("- Please Come Again -", fontSmall, formatCenter);

//            // Tell the printer this is the last page
//            e.HasMorePages = false;
//        }

//        // Helper method to draw a line of text and advance the Y position
//        private void DrawText(string text, Font font, StringFormat format = null)
//        {
//            if (format == null) format = new StringFormat { Alignment = StringAlignment.Near };

//            // Define the drawing area for this line
//            RectangleF rect = new RectangleF(leftMargin, yPos, rightMargin - leftMargin, font.Height);

//            g.DrawString(text, font, brush, rect, format);

//            // Move Y position down for the next line
//            yPos += font.Height;
//        }

//        // Helper method to draw a "Name: Value" pair
//        private void DrawTextPair(string leftText, string rightText, Font font = null)
//        {
//            if (font == null) font = fontNormal;

//            // Draw left part
//            RectangleF rectLeft = new RectangleF(leftMargin, yPos, (rightMargin - leftMargin) / 2, font.Height);
//            g.DrawString(leftText, font, brush, rectLeft, new StringFormat { Alignment = StringAlignment.Near });

//            // Draw right part
//            RectangleF rectRight = new RectangleF(leftMargin, yPos, rightMargin - leftMargin, font.Height);
//            g.DrawString(rightText, font, brush, rectRight, new StringFormat { Alignment = StringAlignment.Far });

//            yPos += font.Height;
//        }
//    }
//}