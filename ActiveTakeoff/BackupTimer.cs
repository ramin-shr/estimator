using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
    public class BackupTimer
    {
        private bool inProgress;

        private Timer timer;

        public int Interval
        {
            get;
            set;
        }

        public bool Suspend
        {
            get;
            set;
        }

        public BackupTimer()
        {
            this.Interval = 5;
            this.Suspend = false;
        }

        public bool InProgress()
        {
            return this.inProgress;
        }

        public void Start()
        {
            if (this.Interval == 0)
            {
                return;
            }
            if (this.timer != null)
            {
                return;
            }
            this.timer = new Timer();
            this.timer.Tick += new EventHandler(this.timer_Tick);
            this.timer.Interval = this.Interval * 0xea60 / 10;
            this.timer.Start();
        }

        public void Stop()
        {
            if (this.timer == null)
            {
                return;
            }
            this.timer.Stop();
            this.timer.Enabled = false;
            this.timer = null;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.Suspend)
            {
                return;
            }
            this.inProgress = true;
            if (this.OnTimer != null)
            {
                this.OnTimer(this, new EventArgs());
            }
            this.inProgress = false;
        }

        public event EventHandler OnTimer;
    }
}