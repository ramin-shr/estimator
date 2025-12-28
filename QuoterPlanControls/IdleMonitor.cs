using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace QuoterPlanControls
{
	public class IdleMonitor : IDisposable
	{
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetLastInputInfo(ref IdleMonitor.LastInputInfo plii);

		public event EventHandler IdleStateChanged
		{
			add
			{
				EventHandler eventHandler = this.IdleStateChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.IdleStateChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			remove
			{
				EventHandler eventHandler = this.IdleStateChanged;
				EventHandler eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.IdleStateChanged, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		public IdleMonitor(int idleThreshold, int refreshInterval)
		{
			this._IdleThreshold = idleThreshold;
			this._LastUpdateTicks = Environment.TickCount;
			this._Timer = new Timer
			{
				Interval = refreshInterval
			};
			this._Timer.Tick += delegate(object A_1, EventArgs A_2)
			{
				int tickCount = Environment.TickCount;
				IdleMonitor.GetLastInputInfo(ref this._LastInputInfo);
				this.IsIdle = ((long)tickCount - (long)((ulong)this._LastInputInfo.dwTime) > (long)this._IdleThreshold);
				this._LastUpdateTicks = tickCount;
			};
			this._Timer.Enabled = true;
		}

		public bool Enabled
		{
			get
			{
				return this._Timer.Enabled;
			}
			set
			{
				this._Timer.Enabled = value;
			}
		}

		public bool IsIdle
		{
			get
			{
				return this._IsIdle;
			}
			set
			{
				if (this._IsIdle != value)
				{
					this._IsIdle = value;
					if (this.IdleStateChanged != null)
					{
						this.IdleStateChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		public void Dispose()
		{
			this._Timer.Dispose();
		}

		[CompilerGenerated]
		private void <.ctor>b__2(object A_1, EventArgs A_2)
		{
			int tickCount = Environment.TickCount;
			IdleMonitor.GetLastInputInfo(ref this._LastInputInfo);
			this.IsIdle = ((long)tickCount - (long)((ulong)this._LastInputInfo.dwTime) > (long)this._IdleThreshold);
			this._LastUpdateTicks = tickCount;
		}

		private int _IdleThreshold;

		private int _LastUpdateTicks;

		private bool _IsIdle;

		private IdleMonitor.LastInputInfo _LastInputInfo = new IdleMonitor.LastInputInfo
		{
			cbSize = IdleMonitor.LastInputInfo.Size
		};

		private Timer _Timer;

		private EventHandler IdleStateChanged;

		private struct LastInputInfo
		{
			// Note: this type is marked as 'beforefieldinit'.
			static LastInputInfo()
			{
			}

			public static readonly uint Size = (uint)Marshal.SizeOf(typeof(IdleMonitor.LastInputInfo));

			public uint cbSize;

			public uint dwTime;
		}
	}
}
