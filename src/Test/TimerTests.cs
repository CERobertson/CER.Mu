namespace CER.Test
{
    using CER.Roslyn;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    [TestClass]
    public class TimerTests : Tests<System.Timers.Timer, Timer>
    {
        [TestMethod]
        public void Main()
        {
            try
            {
                var timer = new Timer { CallbackCount = 0, SmallThresholdViolationCount = 0, };
                timer.TimerToTest = new System.Timers.Timer();
                timer.TimerToTest.Interval = 5000;
                foreach (var assertion in this.Assertions)
                {
                    timer.TimerToTest.Elapsed += (object o, System.Timers.ElapsedEventArgs a) => assertion(timer.TimerToTest, timer);
                }
                timer.TimerToTest.Enabled = true;
                timer.TimerToTest.Start();
                while (timer.LargeThresholdViolationCount == 0) { }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.DetailedMessage());
            }
        }

        new Assertion[] Assertions = new Assertion[]
        {
			(obj, dc) => {dc.CallbackCount++;},
			(obj, dc) => 
            {
                DateTime currentTime = DateTime.Now;
                if ((currentTime - dc.CurrentTime).Seconds > 5)
                {
                    dc.SmallThresholdViolationCount++;
                }
                dc.CurrentTime = currentTime;
            },
			(obj, dc) => 
            {
                if((DateTime.Now - dc.StartTime).Seconds > 10)
                {
                    dc.LargeThresholdViolationCount++;
                }
            },
			(obj, dc) => 
            {
                Console.WriteLine("{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                    TimerTests.Seperator,
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    dc.CurrentTime.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    dc.CallbackCount,
                    dc.SmallThresholdViolationCount,
                    dc.LargeThresholdViolationCount);
            }
        };
    }

    public class Timer
    {
        public System.Timers.Timer TimerToTest { get; set; }
        public DateTime StartTime { get; private set; }

        public Timer()
        {
            DateTime currentTime = DateTime.Now;
            this.StartTime = currentTime;
            this.CurrentTime = currentTime;
        }

        private readonly object callbackcount_lock = new object();
        private int callbackcount;
        public int CallbackCount 
        { 
            get { return callbackcount; } 
            set { lock (callbackcount_lock) callbackcount = value; } 
        }

        private readonly object smallthresholdviolationcount_lock = new object();
        private int smallthresholdviolationcount;
        public int SmallThresholdViolationCount 
        { 
            get { return smallthresholdviolationcount; } 
            set { lock (smallthresholdviolationcount_lock) smallthresholdviolationcount = value; }
        }

        private readonly object largethresholdviolationcount_lock = new object();
        private int lagethresholdviolationcount;
        public int LargeThresholdViolationCount
        {
            get { return lagethresholdviolationcount; }
            set { lock (largethresholdviolationcount_lock) lagethresholdviolationcount = value; }
        }

        private readonly object currenttime_lock = new object();
        private DateTime currenttime;
        public DateTime CurrentTime
        {
            get { return currenttime; }
            set { lock (currenttime_lock) currenttime = value; }
        }
    }
}
