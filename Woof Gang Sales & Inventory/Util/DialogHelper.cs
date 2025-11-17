using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Woof_Gang_Sales___Inventory.Util
{
    public static class DialogHelper
    {
        public static void ShowCustomDialog(string title, string message, string type)
        {
            Color headerColor;
            Image iconImage;
            SystemSound sound = SystemSounds.Asterisk;

            switch (type.ToLower())
            {
                case "success":
                    headerColor = Color.FromArgb(47, 74, 145);
                    iconImage = Properties.Resources.checkgreen;
                    sound = SystemSounds.Asterisk;
                    break;
                case "warning":
                    headerColor = Color.FromArgb(255, 193, 7);
                    iconImage = Properties.Resources.warning_yellow;
                    sound = SystemSounds.Exclamation;
                    break;
                case "error":
                    headerColor = Color.FromArgb(220, 53, 69);
                    iconImage = Properties.Resources.error;
                    sound = SystemSounds.Hand;
                    break;
                case "info":
                    headerColor = Color.FromArgb(47, 74, 145);
                    iconImage = Properties.Resources.information;
                    sound = SystemSounds.Asterisk;
                    break;
                default:
                    headerColor = Color.Gray;
                    iconImage = Properties.Resources.information;
                    break;
            }

            sound.Play();

            Form dialog = new Form()
            {
                Width = 420,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                ShowInTaskbar = false,
                TopMost = true,
                Padding = new Padding(1) // Add a small padding for the border effect
            };

            var shadow = new Guna2ShadowForm { TargetForm = dialog, ShadowColor = Color.Black };

            Panel header = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = headerColor
            };

            Label lblTitle = new Label()
            {
                Text = title,
                ForeColor = Color.White,
                Font = new Font("Inter", 12F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            header.Controls.Add(lblTitle);

            Panel bottomPanel = new Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 65, // 15px padding + 35px button + 15px padding
                BackColor = Color.White,
                Padding = new Padding(0, 15, 0, 15) // Center the button vertically
            };

            Guna2Button okButton = new Guna2Button()
            {
                Text = "OK",
                FillColor = headerColor,
                Size = new Size(100, 35),
                BorderRadius = 6,
                Font = new Font("Inter", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand,

                // ✅ --- FIX 1: Center button based on the dialog's width ---
                Location = new Point((dialog.Width - 100) / 2, 15)
            };
            okButton.Click += (s, e) => dialog.Close();
            bottomPanel.Controls.Add(okButton);

            Panel mainPanel = new Panel()
            {
                Dock = DockStyle.Fill, // It will fill the space between header and footer
                BackColor = Color.White,
                Padding = new Padding(20, 20, 20, 10) // Add padding
            };

            PictureBox iconBox = new PictureBox()
            {
                Image = iconImage,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(64, 64),
                Dock = DockStyle.Left, // Dock icon to the left
                Padding = new Padding(10, 0, 10, 0)
            };

            Label lblMessage = new Label()
            {
                Text = message,
                Font = new Font("Inter", 10F),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill, // Let the label fill the remaining space

                // ✅ --- FIX 2: Center the message text ---
                TextAlign = ContentAlignment.MiddleCenter,

                Padding = new Padding(10, 0, 0, 0)
            };

            mainPanel.Controls.Add(lblMessage); // Add label first (so it can fill)
            mainPanel.Controls.Add(iconBox);   // Add icon second

            dialog.Controls.Add(mainPanel);   // This is in the middle
            dialog.Controls.Add(bottomPanel); // This is at the bottom
            dialog.Controls.Add(header);      // This is at the top

            dialog.MinimumSize = new Size(420, 220);
            dialog.AutoSize = true;
            dialog.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            dialog.ShowDialog();
        }

        // Shortcuts for readability
        public static void ShowSuccess(string message)
            => ShowCustomDialog("Success", message, "success");

        public static void ShowError(string message)
            => ShowCustomDialog("Error", message, "error");

        public static void ShowInfo(string message)
            => ShowCustomDialog("Information", message, "info");

        public static void ShowWarning(string message)
            => ShowCustomDialog("Warning", message, "warning");


        public static DialogResult ShowConfirmDialog(string title, string message, string type)
        {
            Color headerColor;
            Image iconImage;
            SystemSound sound = SystemSounds.Asterisk;

            switch (type.ToLower())
            {
                case "warning":
                    headerColor = Color.FromArgb(255, 193, 7);
                    iconImage = Properties.Resources.warning_yellow;
                    sound = SystemSounds.Exclamation;
                    break;
                case "error":
                    headerColor = Color.FromArgb(220, 53, 69);
                    iconImage = Properties.Resources.error;
                    sound = SystemSounds.Hand;
                    break;
                case "info":
                    headerColor = Color.FromArgb(47, 74, 145);
                    iconImage = Properties.Resources.information;
                    sound = SystemSounds.Asterisk;
                    break;
                default:
                    headerColor = Color.FromArgb(47, 74, 145);
                    iconImage = Properties.Resources.information;
                    break;
            }

            sound.Play();

            Form dialog = new Form()
            {
                Width = 420,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                ShowInTaskbar = false,
                TopMost = true,
                Padding = new Padding(1)
            };

            new Guna2ShadowForm { TargetForm = dialog, ShadowColor = Color.Black };

            Panel header = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = headerColor
            };

            Label lblTitle = new Label()
            {
                Text = title,
                ForeColor = Color.White,
                Font = new Font("Inter", 12F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            header.Controls.Add(lblTitle);

            Panel bottomPanel = new Panel()
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.White,
                Padding = new Padding(0, 15, 0, 15)
            };

            // 🔘 Yes button
            Guna2Button btnYes = new Guna2Button()
            {
                Text = "Yes",
                FillColor = headerColor,
                Size = new Size(100, 35),
                BorderRadius = 6,
                Font = new Font("Inter", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };

            // 🔘 No button
            Guna2Button btnNo = new Guna2Button()
            {
                Text = "No",
                FillColor = Color.LightGray,
                Size = new Size(100, 35),
                BorderRadius = 6,
                Font = new Font("Inter", 10F, FontStyle.Bold),
                ForeColor = Color.Black,
                Cursor = Cursors.Hand
            };

            // ✅ --- FIX 3: Center buttons based on the dialog's width ---
            int totalButtonWidth = btnYes.Width + btnNo.Width + 10; // 10px spacing
            int startX = (dialog.Width - totalButtonWidth) / 2;

            btnYes.Location = new Point(startX, 15);
            btnNo.Location = new Point(btnYes.Right + 10, 15);
            // --- END OF FIX ---

            bottomPanel.Controls.Add(btnYes);
            bottomPanel.Controls.Add(btnNo);

            Panel mainPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20, 20, 20, 10)
            };

            PictureBox iconBox = new PictureBox()
            {
                Image = iconImage,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(64, 64),
                Dock = DockStyle.Left,
                Padding = new Padding(10, 0, 10, 0)
            };

            Label lblMessage = new Label()
            {
                Text = message,
                Font = new Font("Inter", 10F),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,

                // ✅ --- FIX 2: Center the message text ---
                TextAlign = ContentAlignment.MiddleCenter,

                Padding = new Padding(10, 0, 0, 0)
            };

            mainPanel.Controls.Add(lblMessage);
            mainPanel.Controls.Add(iconBox);


            DialogResult result = DialogResult.No;
            btnYes.Click += (s, e) => { result = DialogResult.Yes; dialog.Close(); };
            btnNo.Click += (s, e) => { result = DialogResult.No; dialog.Close(); };

            dialog.Controls.Add(mainPanel);
            dialog.Controls.Add(bottomPanel);
            dialog.Controls.Add(header);

            dialog.MinimumSize = new Size(420, 220);
            dialog.AutoSize = true;
            dialog.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            dialog.ShowDialog();
            return result;
        }
    }
}