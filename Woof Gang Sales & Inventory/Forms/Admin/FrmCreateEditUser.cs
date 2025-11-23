using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Data;
using Woof_Gang_Sales___Inventory.Models;
using Woof_Gang_Sales___Inventory.Util;

namespace Woof_Gang_Sales___Inventory.Forms.Admin
{
    public partial class FrmCreateEditUser : Sample
    {
        public event Action<User>? OnUserUpdated;
        private string[] roles = { "Admin", "StoreClerk" };
        private string selectedImagePath = string.Empty;
        private string previousImagePath = string.Empty;
        private bool imageChanged = false;
        private int userID = 0;
        public bool IsEditMode { get; set; } = false;

        public FrmCreateEditUser()
        {
            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;

            cmbRole.Items.Clear();
            foreach (var role in roles)
            {
                cmbRole.Items.Add(role);
            }
            cmbRole.SelectedIndex = 0;
           
        }

        private void FrmCreateEdit_Load(object sender, EventArgs e)
        {
            if (!IsEditMode)
            {
                toggleUserStatus.Checked = false;
                toggleUserStatus.Enabled = false;
                lblStatus.Text = "";
            }
            else
            {
                toggleUserStatus.Enabled = true;
            }
        }

        // ✅ HELPER METHOD: Load image without locking the file
        private Image LoadImageWithoutLock(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
                return null;

            using (var tempImage = Image.FromFile(path))
            {
                // Create a copy to release the file lock immediately
                return new Bitmap(tempImage);
            }
        }

        // ✅ HELPER METHOD: Safely dispose current image
        private void DisposeCurrentImage()
        {
            if (picProfileImage.Image != null && picProfileImage.Image != Properties.Resources.user)
            {
                var img = picProfileImage.Image;
                picProfileImage.Image = null;
                img.Dispose();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 🧩 Step 1: Validate inputs based on mode
            if (IsEditMode)
            {
                if (!verifyInputForEdit(txtFName.Text, txtLName.Text, txtUsername.Text, txtPassword.Text, txtConfirmPassword.Text))
                    return;
            }
            else
            {
                if (!verifyInput(txtFName.Text, txtLName.Text, txtUsername.Text, txtPassword.Text, txtConfirmPassword.Text))
                    return;
            }

            // 🖼️ Step 2: Handle image saving logic
            string imagePath = previousImagePath; // Default: keep existing image

            if (!IsEditMode || imageChanged)
            {
                if (picProfileImage.Image != null && !string.IsNullOrEmpty(selectedImagePath))
                {
                    try
                    {
                        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string profileImagesDir = Path.Combine(desktopPath, "ProfileImages");

                        if (!Directory.Exists(profileImagesDir))
                            Directory.CreateDirectory(profileImagesDir);

                        // Build filename: FirstName_LastName.jpg
                        string userName = $"{txtFName.Text.Trim()}_{txtLName.Text.Trim()}";
                        string imageExtension = Path.GetExtension(selectedImagePath) ?? ".jpg";
                        string imageFileName = $"{userName}{imageExtension}";
                        imagePath = Path.Combine(profileImagesDir, imageFileName);

                        // Avoid overwriting existing files
                        int count = 1;
                        while (File.Exists(imagePath))
                        {
                            imageFileName = $"{userName}_{count}{imageExtension}";
                            imagePath = Path.Combine(profileImagesDir, imageFileName);
                            count++;
                        }

                        // ✅ Create a copy to save (avoid locking the original)
                        using (var imageCopy = new Bitmap(picProfileImage.Image))
                        {
                            imageCopy.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }

                        // 🧹 Delete old image if different
                        if (IsEditMode && !string.IsNullOrEmpty(previousImagePath)
                            && File.Exists(previousImagePath)
                            && previousImagePath != imagePath)
                        {
                            try
                            {
                                // ✅ Dispose current image to release lock
                                DisposeCurrentImage();

                                // Small delay to ensure handle is released
                                System.Threading.Thread.Sleep(50);

                                File.Delete(previousImagePath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Could not delete old image: {ex.Message}", "Warning",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            // 🧩 Step 3: Build user object
            User user = new User
            {
                UserID = userID,
                FirstName = txtFName.Text.Trim(),
                MiddleName = txtMName.Text.Trim(),
                LastName = txtLName.Text.Trim(),
                Username = txtUsername.Text.Trim(),
                PasswordHash = txtPassword.Text,
                Role = cmbRole.Text,
                IsActive = toggleUserStatus.Checked,
                ProfileImagePath = imagePath
            };

            var userRepo = new UserRepository();
            bool success = false;

            // 🧩 Step 4: Create or update user
            if (user.UserID == 0)
            {
                success = userRepo.CreateUser(user);
                if (success)
                {
                    ResetInput();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                string newPassword = txtPassword.Text.Trim();
                success = !string.IsNullOrWhiteSpace(newPassword)
                    ? userRepo.UpdateUser(user, newPassword)
                    : userRepo.UpdateUser(user, null);

                if (success)
                {
                    // ✅ Small delay to ensure event processes
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        public void EditUser(User user)
        {
            this.lblTitle.Text = "Edit User";
            this.lblID.Text = user.UserID.ToString();
            this.txtFName.Text = user.FirstName ?? "";
            this.txtMName.Text = user.MiddleName ?? "";
            this.txtLName.Text = user.LastName ?? "";
            this.txtUsername.Text = user.Username ?? "";

            this.txtPassword.Text = "";
            this.txtPassword.PlaceholderText = "Leave empty to keep current password";

            this.txtConfirmPassword.Text = "";
            this.txtConfirmPassword.PlaceholderText = "Confirm new password (if changing)";

            this.btnSave.Text = "Save";
            this.btnSave.Image = Properties.Resources.edit3;
            this.btnSave.TextOffset = new Point(-3, 0);

            // ✅ Load image without file lock
            if (!string.IsNullOrEmpty(user.ProfileImagePath) && File.Exists(user.ProfileImagePath))
            {
                DisposeCurrentImage(); // Clean up old image first

                picProfileImage.Image = LoadImageWithoutLock(user.ProfileImagePath);
                picProfileImage.SizeMode = PictureBoxSizeMode.Zoom;

                selectedImagePath = user.ProfileImagePath;
                previousImagePath = user.ProfileImagePath;
                imageChanged = false;
            }
            else
            {
                picProfileImage.Image = Properties.Resources.user;
                selectedImagePath = string.Empty;
                previousImagePath = string.Empty;
                imageChanged = false;
            }

            // Role handling
            if (cmbRole.Items.Count == 0)
            {
                foreach (var role in roles)
                {
                    cmbRole.Items.Add(role);
                }
            }

            if (!string.IsNullOrEmpty(user.Role) && cmbRole.Items.Contains(user.Role))
                cmbRole.SelectedItem = user.Role;
            else
                cmbRole.SelectedIndex = 0;

            toggleUserStatus.Checked = user.IsActive;
            lblStatus.Text = user.IsActive ? "Activated" : "Deactivated";
            lblStatus.ForeColor = user.IsActive ? Color.Green : Color.Red;

            this.userID = user.UserID;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public bool verifyInput(string firstName, string lastName, string username, string password, string confirmPassword)
        {
            // 1. Check for empty required fields
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                DialogHelper.ShowCustomDialog("Missing Information",
                    "Please enter all the required fields.", "warning");
                return false;
            }

            // 2. Regex for First Name (Allows "Jr.", "3rd", etc.)
            // Improved pattern: Letters, Numbers, Spaces, Dots, Hyphens, Apostrophes
            if (!Regex.IsMatch(firstName.Trim(), @"^[\p{L}0-9\s.'-]+$"))
            {
                DialogHelper.ShowCustomDialog("Invalid First Name",
                    "First name contains invalid characters. Allowed: Letters, Numbers, Spaces, Dots, Hyphens, and Apostrophes.", "warning");
                return false;
            }

            // 3. Regex for Last Name (Allows "Jr.", "3rd", etc.)
            if (!Regex.IsMatch(lastName.Trim(), @"^[\p{L}0-9\s.'-]+$"))
            {
                DialogHelper.ShowCustomDialog("Invalid Last Name",
                    "Last name contains invalid characters. Allowed: Letters, Numbers, Spaces, Dots, Hyphens, and Apostrophes.", "warning");
                return false;
            }

            // 4. Regex for Username
            // Added {4,20} to limit max length to 20 (prevents DB errors)
            if (!Regex.IsMatch(username.Trim(), @"^[a-zA-Z0-9_-]{4,20}$"))
            {
                DialogHelper.ShowCustomDialog("Invalid Username",
                    "Username must be 4-20 characters long and can only contain letters, numbers, underscores, and hyphens.", "warning");
                return false;
            }

            // 5. Regex for Password
            // FIXED: Changed [A-Za-z\d] to . (dot) to allow special characters like @, #, !
            if (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d).{8,}$"))
            {
                DialogHelper.ShowCustomDialog("Invalid Password",
                    "Password must be at least 8 characters long and contain at least one letter and one number.", "warning");
                return false;
            }

            // 6. Check if passwords match
            if (password != confirmPassword)
            {
                DialogHelper.ShowCustomDialog("Password Mismatch",
                    "The passwords you entered do not match.", "warning");
                return false;
            }

            return true;
        }

        public bool verifyInputForEdit(string firstName, string lastName, string username, string newPassword, string confirmPassword)
        {
            // 1. Check for empty required fields (passwords are optional)
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(username))
            {
                DialogHelper.ShowCustomDialog("Missing Information",
                    "Please enter all the required fields (First Name, Last Name, Username).", "warning");
                return false;
            }

            // 2. Regex for First Name
            if (!Regex.IsMatch(firstName.Trim(), @"^[\p{L}\s'-]+$"))
            {
                DialogHelper.ShowCustomDialog("Invalid Input",
                    "First name can only contain letters, spaces, hyphens, or apostrophes.", "warning");
                return false;
            }

            // 3. Regex for Last Name
            if (!Regex.IsMatch(lastName.Trim(), @"^[\p{L}\s'-]+$"))
            {
                DialogHelper.ShowCustomDialog("Invalid Input",
                    "Last name can only contain letters, spaces, hyphens, or apostrophes.", "warning");
                return false;
            }

            // 4. Regex for Username
            if (!Regex.IsMatch(username.Trim(), @"^[a-zA-Z0-9_-]{4,}$"))
            {
                DialogHelper.ShowCustomDialog("Invalid Username",
                    "Username must be at least 4 characters long and can only contain letters, numbers, underscores, and hyphens.", "warning");
                return false;
            }

            // 5. NEW: Check password ONLY if it's not empty
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                // A. Check password strength
                if (!Regex.IsMatch(newPassword, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
                {
                    DialogHelper.ShowCustomDialog("Invalid Password",
                        "Password must be at least 8 characters long and contain at least one letter and one number.", "warning");
                    return false;
                }

                // B. Check if passwords match
                if (newPassword != confirmPassword)
                {
                    DialogHelper.ShowCustomDialog("Password Mismatch",
                        "The passwords you entered do not match. Please confirm your new password.", "warning");
                    return false;
                }
            }
            // NEW: Check if user typed in confirm but not new password
            else if (string.IsNullOrWhiteSpace(newPassword) && !string.IsNullOrWhiteSpace(confirmPassword))
            {
                DialogHelper.ShowCustomDialog("Password Mismatch",
                       "Please enter your new password in both password fields.", "warning");
                return false;
            }

            return true;
        }

        private void ResetInput()
        {
            txtFName.Text = string.Empty;
            txtMName.Text = string.Empty;
            txtLName.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty; 

            DisposeCurrentImage();
            picProfileImage.Image = Properties.Resources.user;
            selectedImagePath = string.Empty;
            previousImagePath = string.Empty;
            imageChanged = false;
        }

        private void toggleUserStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (toggleUserStatus.Checked)
            {
                lblStatus.Text = "Activated";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.Text = "Deactivated";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Choose Profile Image";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DisposeCurrentImage(); // Clean up old image

                        // ✅ Load without locking the file
                        picProfileImage.Image = LoadImageWithoutLock(ofd.FileName);
                        picProfileImage.SizeMode = PictureBoxSizeMode.Zoom;

                        selectedImagePath = ofd.FileName;
                        imageChanged = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error selecting image: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // ✅ Clean up when form closes
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DisposeCurrentImage();
            base.OnFormClosing(e);
        }
    }
}