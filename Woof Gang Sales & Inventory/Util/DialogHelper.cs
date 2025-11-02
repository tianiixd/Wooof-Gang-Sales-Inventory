using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Woof_Gang_Sales___Inventory.Util
{
    public static class DialogHelper
    {
        /// <summary>
        /// Shows a custom modern dialog with your own icons (Success, Error, Info, Warning)
        /// </summary>
        public static void ShowCustomDialog(string title, string message, string type)
        {
            // 🎨 Colors for each type
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

            // 🔊 Play corresponding sound
            sound.Play();

            // 🧩 Create the dialog form
            Form dialog = new Form()
            {

                Width = 420,
                Height = 220,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                ShowInTaskbar = false,
                TopMost = true,
                Padding = new Padding(0, 0, 0, 10)
            };

            // 🟦 Rounded corners (aesthetic)
            var shadow = new Guna2ShadowForm
            {
                TargetForm = dialog,
                ShadowColor = Color.Black
            };

            // 🔹 Header bar
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

            // 🖼️ Icon
            PictureBox iconBox = new PictureBox()
            {
                Image = iconImage,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(64, 64),
                Location = new Point(35, 80)
            };

            // 📝 Message text
            Label lblMessage = new Label()
            {
                Text = message,
                Font = new Font("Inter", 10F),
                ForeColor = Color.Black,
                Location = new Point(110, 85),
                Size = new Size(280, 60),
                AutoSize = false
            };

            // 🟢 OK Button
            Guna2Button okButton = new Guna2Button()
            {
                Text = "OK",
                FillColor = headerColor,
                Size = new Size(100, 35),
                Location = new Point(dialog.Width / 2 - 50, 160),
                BorderRadius = 6,
                Font = new Font("Inter", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                Cursor = Cursors.Hand
            };

            okButton.Click += (s, e) => dialog.Close();

            // 🧩 Add controls
            dialog.Controls.Add(header);
            dialog.Controls.Add(iconBox);
            dialog.Controls.Add(lblMessage);
            dialog.Controls.Add(okButton);

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
                default:
                    headerColor = Color.FromArgb(47, 74, 145);
                    iconImage = Properties.Resources.information;
                    break;
            }

            sound.Play();

            Form dialog = new Form()
            {
                Width = 420,
                Height = 220,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.None,
                BackColor = Color.White,
                ShowInTaskbar = false,
                TopMost = true
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

            PictureBox iconBox = new PictureBox()
            {
                Image = iconImage,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(64, 64),
                Location = new Point(35, 80)
            };

            Label lblMessage = new Label()
            {
                Text = message,
                Font = new Font("Inter", 10F),
                ForeColor = Color.Black,
                Location = new Point(110, 85),
                Size = new Size(280, 60),
                AutoSize = false
            };

            // 🔘 Yes button
            Guna2Button btnYes = new Guna2Button()
            {
                Text = "Yes",
                FillColor = headerColor,
                Size = new Size(100, 35),
                Location = new Point(110, 160),
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
                Location = new Point(220, 160),
                BorderRadius = 6,
                Font = new Font("Inter", 10F, FontStyle.Bold),
                ForeColor = Color.Black,
                Cursor = Cursors.Hand
            };

            DialogResult result = DialogResult.No;
            btnYes.Click += (s, e) => { result = DialogResult.Yes; dialog.Close(); };
            btnNo.Click += (s, e) => { result = DialogResult.No; dialog.Close(); };

            dialog.Controls.Add(header);
            dialog.Controls.Add(iconBox);
            dialog.Controls.Add(lblMessage);
            dialog.Controls.Add(btnYes);
            dialog.Controls.Add(btnNo);

            dialog.ShowDialog();
            return result;
        }



    }
}
