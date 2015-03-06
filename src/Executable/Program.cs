namespace CER.Executable
{
    using System;
    using System.Diagnostics;

    public class Program
    {
        protected static readonly char Separator = '|';

        public static readonly string TraceSourceToken = "Warnings";
        public static readonly TraceSource External = new TraceSource(Program.TraceSourceToken, SourceLevels.All);

        public static readonly string TraceSwitchToken = "Prolixity Level";
        public static readonly TraceSwitch TraceSwitch = new TraceSwitch(Program.TraceSwitchToken, string.Empty);

        static Program()
        {
            Program.External.Listeners.Remove("Default");
            Program.LogI(Program.StartDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        //Identity information.
        public static readonly Guid Process = Guid.NewGuid();
        public static readonly DateTime StartDateTime = DateTime.Now;
        public static readonly long Start = Program.StartDateTime.Ticks;

        private static void LogToExternal(string input)
        {
            var time_offset = DateTime.Now.Ticks - Program.Start;
            Program.External.TraceEvent(TraceEventType.Information, 0, string.Format("{1}{0}{2}{0}{3}",
                Program.Separator,
                Program.Process,
                time_offset.ToString(),
                input));
        }
        public static void LogE(string input)
        {
            Trace.WriteLineIf(Program.TraceSwitch.TraceError,input);
            Program.LogToExternal(input);
        }
        public static void LogW(string input)
        {
            Program.LogToExternal(input);
        }
        public static void LogI(string input)
        {
            Trace.WriteLineIf(Program.TraceSwitch.TraceInfo, input);
        }
        public static void LogV(string input)
        {
            Trace.WriteLineIf(Program.TraceSwitch.TraceVerbose, input);
        }
    }
}

