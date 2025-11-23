using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Admin; // For FrmAdminDashboard
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmPayment : Sample
    {
        //Repositories needed for the "Four Things"
        private readonly SalesRepository salesRepo = new SalesRepository();

        // Data passed from FrmPOS
        private decimal _totalAmount = 0m;
        private int? _discountID = null;
        private List<CartItem> _shoppingCart;
        private User _loggedInUser;

        // ✅ --- ADDED: A private variable to hold the tendered amount ---
        private decimal _tenderedAmount = 0m;


        public FrmPayment()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            
        }

        /// <summary>
        /// This is called by FrmPOS to pass the cart data to this popup.
        /// </summary>
        public void LoadPaymentDetails(decimal totalAmount, int? discountID, List<CartItem> shoppingCart, User loggedInUser)
        {
            _totalAmount = totalAmount;
            _discountID = discountID;
            _shoppingCart = shoppingCart;
            _loggedInUser = loggedInUser;

            // Display the total
            lblTotalDue.Text = _totalAmount.ToString("C"); // e.g., "₱1,650.00"
            lblChange.Text = (0m).ToString("C");

            // Setup payment methods
            cmbPaymentMethod.Items.Add("Cash");
            cmbPaymentMethod.Items.Add("GCash");
            cmbPaymentMethod.Items.Add("Maya");
            cmbPaymentMethod.SelectedIndex = 0; // Default to Cash

            // ✅ --- FIX: Hide ref# by default ---
            txtPaymentRef.Visible = true;
            txtPaymentRef.Enabled = false;
        }

        private void FrmPayment_Load(object sender, EventArgs e)
        {
            // Wire up events
            cmbPaymentMethod.SelectedIndexChanged += cmbPaymentMethod_SelectedIndexChanged;
            txtAmountTendered.TextChanged += txtAmountTendered_TextChanged;
            btnCancel.Click += (s, ev) => { this.Close(); };
        }

        /// <summary>
        /// Show/Hide the Reference# box based on payment method
        /// </summary>
        private void cmbPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if an item is actually selected
            if (cmbPaymentMethod.SelectedItem == null) return;

            string method = cmbPaymentMethod.SelectedItem.ToString();
            if (method == "Cash")
            {
                // ✅ --- FIX: Show/Enable Tendered, Hide/Disable Ref ---
                txtAmountTendered.Text = string.Empty;
                txtAmountTendered.Enabled = true;


                lblRefTitle.Visible = false;

                txtPaymentRef.Visible = false;
                txtPaymentRef.Enabled = false;

                txtPaymentRef.PlaceholderText = string.Empty;
                lblChange.Visible = true;
                lblTitleChange.Visible = true;

                this.Size = new System.Drawing.Size(744, 470);
                btnConfirmPayment.Location = new System.Drawing.Point(60, 362);
                btnCancel.Location = new System.Drawing.Point(441, 362);

            }
            else
            {
                // ✅ --- FIX: For digital, auto-fill tendered, show Ref ---
                txtAmountTendered.Text = _totalAmount.ToString("F2"); // Auto-fill exact amount
                txtAmountTendered.Enabled = false; // Disable tendered for digital

                lblRefTitle.Visible = true;
                txtPaymentRef.Visible = true;
                txtPaymentRef.Enabled = true;
                txtPaymentRef.PlaceholderText = $"{method} Reference #";
                txtPaymentRef.Focus();

                lblChange.Visible = false;
                lblTitleChange.Visible = false;

                this.Size = new System.Drawing.Size(744, 590);
                btnConfirmPayment.Location = new System.Drawing.Point(60, 482);
                btnCancel.Location = new System.Drawing.Point(441, 482);

            }
        }

        /// <summary>
        /// Auto-calculate the change
        /// </summary>
        private void txtAmountTendered_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmountTendered.Text, out decimal tendered))
            {
                _tenderedAmount = tendered; // ✅ Store the tendered amount
                decimal change = tendered - _totalAmount;
                if (change < 0)
                {
                    lblChange.Text = "Waiting...";
                }
                else
                {
                    lblChange.Text = change.ToString("C");
                }
            }
            else
            {
                _tenderedAmount = 0; // Reset if invalid
                lblChange.Text = (0m).ToString("C");
            }
        }

        /// <summary>
        /// This is it! The "Four Things" get executed here.
        /// </summary>
        private void btnConfirmPayment_Click(object sender, EventArgs e)
        {
            // Check if an item is actually selected
            if (cmbPaymentMethod.SelectedItem == null)
            {
                DialogHelper.ShowCustomDialog("Missing Information", "Please select a payment method.", "warning");
                return;
            }

            string paymentMethod = cmbPaymentMethod.SelectedItem.ToString();
            string refNumber = txtPaymentRef.Text.Trim();

            // 1. Validation
            // We already stored the tendered amount in the _tenderedAmount variable
            
            
                // Only enforce this if payment method is Cash
            if (paymentMethod == "Cash")
            {
                if (_tenderedAmount < _totalAmount)
                {
                    DialogHelper.ShowCustomDialog("Insufficient Funds", "Amount tendered is less than the total due.", "warning");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(refNumber))
                {
                    DialogHelper.ShowCustomDialog("Missing Reference", $"Please enter the {paymentMethod} Reference Number.", "warning");
                    return;
                }

                // ✅ REGEX VALIDATION FOR GCASH
                if (paymentMethod == "GCash")
                {
                    // GCash usually has 13 digits (e.g., 9000000000001)
                    if (!Regex.IsMatch(refNumber, @"^\d{13}$"))
                    {
                        DialogHelper.ShowCustomDialog("Invalid GCash Ref", "GCash Reference Number must be exactly 13 digits.", "warning");
                        return;
                    }
                }
                // ✅ REGEX VALIDATION FOR MAYA
                else if (paymentMethod == "Maya")
                {
                    // Maya usually has 12 digits
                    if (!Regex.IsMatch(refNumber, @"^\d{12}$"))
                    {
                        DialogHelper.ShowCustomDialog("Invalid Maya Ref", "Maya Reference Number must be exactly 12 digits.", "warning");
                        return;
                    }
                }
            }


            // ✅ --- FIX: For digital payments, tendered amount is always the total amount ---
            decimal finalTenderedAmount = (paymentMethod == "Cash") ? _tenderedAmount : _totalAmount;
            string finalRefNumber = (paymentMethod == "Cash") ? null : refNumber;

            string customerName = txtCustomerName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(customerName))
            {
                if (!Regex.IsMatch(customerName, @"^[\p{L}0-9\s.'-]+$"))
                {
                    DialogHelper.ShowCustomDialog("Invalid Name",
                        "Customer name contains invalid characters. Allowed: Letters, Numbers, Spaces, Dots, Hyphens, and Apostrophes.", "warning");
                    return;
                }
            }

            // --- Build the Sale Object ---
            Sale newSale = new Sale
            {
                SaleDate = DateTime.Now.Date,     // ✅ Use .Date
                SaleTime = DateTime.Now.TimeOfDay,  // ✅ Use .TimeOfDay
                CashierID = _loggedInUser.UserID,
                DiscountID = _discountID,
                CustomerName = customerName, // We can add a textbox for this later if needed
                TotalAmount = _totalAmount,
                PaymentMethod = paymentMethod,
                PaymentRef = finalRefNumber,
                Details = new List<SalesDetail>() // We will build this from the cart
            };

            // Convert CartItems to SalesDetails
            foreach (var item in _shoppingCart)
            {
                newSale.Details.Add(new SalesDetail
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    Subtotal = item.Subtotal
                });
            }

            // --- Execute the "Four Things" Transaction ---
            int newSaleID = salesRepo.CreateNewSale(newSale);

            // ✅ --- Check if SaleID is valid (0 means it failed) ---
            if (newSaleID > 0)
            {
                // The sale was successful. We now have the official SaleID.
                newSale.SaleID = newSaleID;

                string cashierName = $"{_loggedInUser.FirstName} {_loggedInUser.LastName}";

                // ✅ --- THIS IS THE FIX ---
                // Calculate change and pass all 5 arguments to the PDF generator
                decimal changeDue = (paymentMethod == "Cash") ? (finalTenderedAmount - _totalAmount) : 0;

                string generatedPdfPath = PdfGenerator.CreateReceipt(newSale, _shoppingCart, cashierName, finalTenderedAmount, changeDue);
                if (generatedPdfPath != null)
                {
                    // 3. Ask the user to print
                    DialogResult result = DialogHelper.ShowConfirmDialog("Print Receipt", "Sale complete! Print a receipt?", "info");

                    // "Sale complete! Print a receipt?",
                    //"Print Receipt",
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                           
                            string printerName = "ReceiptPrint";


                            ReceiptPrinter.Print(newSale, _shoppingCart, cashierName, finalTenderedAmount, changeDue, printerName);
                        }
                        catch (Exception ex)
                        {
                            DialogHelper.ShowCustomDialog("Print Error", "Could not print the receipt. Check printer.\n" + ex.Message, "error");
                        }
                    }


                    this.DialogResult = DialogResult.OK; // Tell FrmPOS it worked
                    this.Close();
                }
                // If it fails (newSaleID == 0), the salesRepo's DialogHelper
                // already showed the error, so we just stay on this form.
            }
        }
    }
}