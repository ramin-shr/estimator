using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace QuoterPlanControls
{
    public sealed class IdleMonitor : IDisposable
    {
        private readonly int idleThresholdMilliseconds;
        private int lastUpdateTicks;
        private bool isIdle;

        private LastInputInfo lastInputInfo = new LastInputInfo
        {
            cbSize = LastInputInfo.Size
        };

        private readonly Timer timer;

        public event EventHandler IdleStateChanged;

        public bool Enabled
        {
            get => timer.Enabled;
            set => timer.Enabled = value;
        }

        public bool IsIdle
        {
            get => isIdle;
            private set
            {
                if (isIdle == value)
                    return;

                isIdle = value;
                IdleStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public IdleMonitor(int idleThreshold, int refreshInterval)
        {
            idleThresholdMilliseconds = idleThreshold;
            lastUpdateTicks = Environment.TickCount;

            timer = new Timer
            {
                Interval = refreshInterval,
                Enabled = true
            };

            timer.Tick += OnTimerTick;
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            int tickCount = Environment.TickCount;

            GetLastInputInfo(ref lastInputInfo);

            uint elapsedMs = unchecked((uint)tickCount - lastInputInfo.dwTime);
            IsIdle = elapsedMs > (uint)idleThresholdMilliseconds;

            lastUpdateTicks = tickCount;
        }

        public void Dispose()
        {
            timer.Dispose();
        }

        [DllImport("user32.dll", ExactSpelling = false, CharSet = CharSet.Auto)]
        private static extern bool GetLastInputInfo(ref LastInputInfo plii);

        private struct LastInputInfo
        {
            public static readonly uint Size;

            public uint cbSize;
            public uint dwTime;

            static LastInputInfo()
            {
                Size = (uint)Marshal.SizeOf(typeof(LastInputInfo));
            }
        }
    }
}
