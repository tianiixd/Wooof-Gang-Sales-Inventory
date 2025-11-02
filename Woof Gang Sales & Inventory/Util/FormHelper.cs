using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Woof_Gang_Sales___Inventory.Admin;

namespace Woof_Gang_Sales___Inventory.Util
{
    public class FormHelper
    {

        public static void BlurBackground(Form modal, Form owner)
        {
            Form background = new Form();
            try
            {
                // Configure the blur form
                background.StartPosition = FormStartPosition.Manual;
                background.FormBorderStyle = FormBorderStyle.None;
                background.Opacity = 0.5d;
                background.BackColor = Color.Black;
                background.Size = owner.Size;
                background.Location = owner.Location;
                background.ShowInTaskbar = false;

                // Show the blur form behind the modal
                background.Show();

                // Set modal's owner and show it
                modal.Owner = background;
                modal.ShowDialog(background);
            }
            finally
            {
                background.Dispose();
            }
        }
    }
}
