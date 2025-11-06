using System;
using System.Drawing;
using System.Windows.Forms;

namespace Woof_Gang_Sales___Inventory.Forms.Controls
{
    public partial class ProductCard : UserControl
    {
        // This event will fire when the card is clicked
        public event EventHandler CardClicked;

        // Store the ID
        public int ProductID { get; set; }

        // ✅ --- ADD THIS NEW PROPERTY ---
        /// <summary>
        /// A flag to tell FrmPOS if this item is allowed to be added to the cart.
        /// </summary>
        public bool IsSellable { get; private set; } = true; // Default to true

        // --- Private fields for state management ---
        private decimal _unitPrice;
        private int _productStock;
        private DateTime? _expirationDate;
        private Image _originalImage;
        private Color _originalTitleColor;

        // --- Public Properties ---
        public decimal UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                if (lblPrice != null)
                {
                    lblPrice.Text = _unitPrice.ToString("C"); // "C" for currency
                }
            }
        }

        public int ProductStock
        {
            get { return _productStock; }
            set
            {
                _productStock = value;
                UpdateStatus(); // Call the master update method
            }
        }

        public DateTime? ExpirationDate
        {
            get { return _expirationDate; }
            set
            {
                _expirationDate = value;
                UpdateStatus(); // Call the master update method
            }
        }

        public string ProductTitle
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public Image ProductImage
        {
            get { return picImage.Image; }
            set
            {
                _originalImage = value; // Save the original image
                picImage.Image = value;
            }
        }

        // --- Constructor ---
        public ProductCard()
        {
            InitializeComponent();

            // Save the original title color
            _originalTitleColor = lblTitle.ForeColor;

            // Wire up the click event for all controls
            this.Click += OnCardClicked;
            foreach (Control control in this.Controls)
            {
                control.Click += OnCardClicked;
                if (control is Panel p)
                {
                    foreach (Control child in p.Controls)
                    {
                        child.Click += OnCardClicked;
                    }
                }
            }
        }

        /// <summary>
        /// A single, master method to update the card's UI based on priority.
        /// </summary>
        private void UpdateStatus()
        {
            if (lblStock == null || picImage == null || lblTitle == null)
                return;

            // --- Priority 1: Check for Expiration ---
            // (Products expiring today or earlier are unsellable)
            if (_expirationDate.HasValue && _expirationDate.Value.Date <= DateTime.Today)
            {
                IsSellable = false;
                picImage.Image = Properties.Resources.expired2; // "Expired" overlay
                lblTitle.ForeColor = Color.Red;
                lblStock.Text = $"Expired Stock: {_productStock}";
                lblStock.ForeColor = Color.Red;
                this.Cursor = Cursors.No;
            }
            // --- Priority 2: Check for Out of Stock ---
            else if (_productStock <= 0)
            {
                IsSellable = false;
                picImage.Image = Properties.Resources.outofstock; // "Out of Stock" overlay
                lblTitle.ForeColor = _originalTitleColor;
                lblStock.Text = "Out of Stock";
                lblStock.ForeColor = Color.Red;
                this.Cursor = Cursors.No;
            }
            // --- Priority 3: It's Sellable (Normal or Low Stock) ---
            else
            {
                IsSellable = true;
                picImage.Image = _originalImage; // Restore original image
                lblTitle.ForeColor = _originalTitleColor;
                this.Cursor = Cursors.Hand;

                // Set stock text and color
                lblStock.Text = $"Stock: {_productStock}";
                if (_productStock <= 5) // Low stock warning
                {
                    lblStock.ForeColor = Color.FromArgb(255, 128, 0); // Orange
                }
                else // Sufficient stock
                {
                    lblStock.ForeColor = Color.FromArgb(28, 44, 73); // Dark blue
                }
            }
        }

        // --- Event Handler ---
        private void OnCardClicked(object sender, EventArgs e)
        {
            CardClicked?.Invoke(this, e);
        }
    }
}