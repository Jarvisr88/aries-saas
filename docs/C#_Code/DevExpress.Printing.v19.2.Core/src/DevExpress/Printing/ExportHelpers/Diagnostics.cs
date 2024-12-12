namespace DevExpress.Printing.ExportHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class Diagnostics
    {
        private static List<string> tracesCore = new List<string>();

        public static void ClearTracing()
        {
            tracesCore.Clear();
        }

        [Conditional("DEBUGTEST")]
        public static void Trace()
        {
            StackFrame frame = new StackTrace(false).GetFrame(1);
            Traces.Add(frame.GetMethod().ReflectedType.FullName + "_" + frame.GetMethod().Name);
        }

        public static List<string> Traces =>
            tracesCore;
    }
}

