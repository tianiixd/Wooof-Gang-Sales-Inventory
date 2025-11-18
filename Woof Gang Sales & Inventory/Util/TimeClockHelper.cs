using System;
using System.Windows.Forms;

namespace Woof_Gang_Sales___Inventory.Util
{
    public class TimeClockHelper
    {
        private Timer _timer;
        private Control _timeControl;
        private Control _dateControl;

        
        public void StartClock(Control timeControl, Control dateControl = null)
        {
            _timeControl = timeControl;
            _dateControl = dateControl;

           
            _timer = new Timer();
            _timer.Interval = 1000; // 1 second eto same sa javascript
            _timer.Tick += Timer_Tick;

            
            UpdateDisplay();

            
            _timer.Start();
        }

        public void StopClock()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            DateTime now = DateTime.Now;

            if (_timeControl != null && !_timeControl.IsDisposed)
            {
                
                _timeControl.Text = now.ToString("h:mm:ss tt");
            }
            else
            {
                StopClock();
                return;
            }

            
            if (_dateControl != null && !_dateControl.IsDisposed)
            {
                
                _dateControl.Text = now.ToString("dddd, MMMM d, yyyy");
            }
        }
    }
}