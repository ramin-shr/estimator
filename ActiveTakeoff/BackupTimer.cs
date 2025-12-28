using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlan
{
	public class BackupTimer
	{
		public int Interval
		{
			[CompilerGenerated]
			get
			{
				return this.<Interval>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Interval>k__BackingField = value;
			}
		}

		public bool Suspend
		{
			[CompilerGenerated]
			get
			{
				return this.<Suspend>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Suspend>k__BackingField = value;
			}
		}

		public event EventHandler OnTimer
		{
			add
			{
				EventHandler eventHandler = this.OnTimer;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnTimer, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.OnTimer;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.OnTimer, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public BackupTimer()
		{
			this.Interval = 5;
			this.Suspend = false;
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
			this.timer.Tick += this.timer_Tick;
			this.timer.Interval = this.Interval * 60000 / 10;
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

		public bool InProgress()
		{
			return this.inProgress;
		}

		private EventHandler OnTimer;

		private bool inProgress;

		private Timer timer;

		[CompilerGenerated]
		private int <Interval>k__BackingField;

		[CompilerGenerated]
		private bool <Suspend>k__BackingField;
	}
}
