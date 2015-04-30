namespace CER.Test
{
    using CER.Roslyn;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    [TestClass]
    public class TimerTests : Tests<System.Timers.Timer, async_TimerContext>
    {
        [TestMethod]
        public void Main()
        {
            try
            {
                var interval = (int)Math.Pow(2, 12);
                var duration = 30;
                while ((interval /= 2) >= (int)Math.Pow(2, 7))
                {
                    TimerTests.CancellationTokenSource = new System.Threading.CancellationTokenSource();
                    var cancel_token = TimerTests.CancellationTokenSource.Token;
                    var timer = new async_TimerContext(interval, duration);
                    timer.TimerToTest = new System.Timers.Timer();
                    timer.TimerToTest.Interval = interval;
                    foreach (var assertion in this.Assertions)
                    {
                        timer.TimerToTest.Elapsed += (object o, System.Timers.ElapsedEventArgs a) => assertion(timer.TimerToTest, timer);
                    }
                    timer.TimerToTest.Enabled = true;
                    timer.TimerToTest.Start();
                    while (!cancel_token.IsCancellationRequested)
                    {
                    }
                    timer.TimerToTest.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.DetailedMessage());
            }
        }

        [TestMethod]
        public void ParallelMain()
        {
            TimerTests.CancellationTokenSource = new System.Threading.CancellationTokenSource();
            var interval = (int)Math.Pow(2, 12);
            var duration = 30;
            var tasks = new List<Task>();
            while ((interval /= 2) >= (int)Math.Pow(2, 7))
            {
                var timer = new async_TimerContext(interval, duration);
                timer.TimerToTest = new System.Timers.Timer();
                timer.TimerToTest.Interval = interval;
                tasks.Add(Task.Run(() =>
                {
                    var cancel_token = TimerTests.CancellationTokenSource.Token;
                    foreach (var assertion in this.Assertions)
                    {
                        timer.TimerToTest.Elapsed += (object o, System.Timers.ElapsedEventArgs a) => assertion(timer.TimerToTest, timer);
                    }
                    timer.TimerToTest.Enabled = true;
                    timer.TimerToTest.Start();
                    while (!cancel_token.IsCancellationRequested)
                    {
                    }
                    timer.TimerToTest.Dispose();
                }, TimerTests.CancellationTokenSource.Token));
            }
            Task.WaitAll(tasks.ToArray());
        }

        new Assertion[] Assertions = new Assertion[]
        {
            (obj, dc) => {dc.CallbackCount++;},
            (obj, dc) => 
            {
                DateTime currentTime = DateTime.Now;
                dc.SmallThresholdViolationCount = (currentTime - dc.CurrentTime).Milliseconds - dc.IntervalInMilliseconds;
                dc.CurrentTime = currentTime;
            },
            (obj, dc) => 
            {
                DateTime currentTime = DateTime.Now;
                if ((currentTime - dc.StartTime).Seconds > dc.DurationInSeconds)
                {
                    TimerTests.CancellationTokenSource.Cancel();
                }
            },
            (obj, dc) => 
            {
                Console.WriteLine(string.Join(
                    TimerTests.Separator.ToString(),
                    dc.IntervalInMilliseconds,
                    dc.CallbackCount,
                    dc.SmallThresholdViolationCount));
            }
        };
    }

    public class async_TimerContext
    {
        public System.Timers.Timer TimerToTest { get; set; }

        public DateTime StartTime { get; private set; }
        public int IntervalInMilliseconds { get; private set; }
        public int DurationInSeconds { get; private set; }

        public async_TimerContext(int interval_in_milliseconds, int duration_in_seconds)
        {
            DateTime currentTime = DateTime.Now;
            this.StartTime = currentTime;
            this.CurrentTime = currentTime;
            this.IntervalInMilliseconds = interval_in_milliseconds;
            this.DurationInSeconds = duration_in_seconds;
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
