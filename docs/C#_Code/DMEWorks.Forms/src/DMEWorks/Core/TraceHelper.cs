namespace DMEWorks.Core
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public static class TraceHelper
    {
        public static string FormatException(Exception ex)
        {
            string str = (string) typeof(Exception).GetMethod("GetClassName", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(ex, new object[0]);
            if (!string.IsNullOrEmpty(ex.Message))
            {
                str = str + ": " + ex.Message;
            }
            if (ex.InnerException != null)
            {
                string[] textArray1 = new string[] { str, " ---> ", FormatException(ex.InnerException), Environment.NewLine, "   --- End of inner exception stack trace ---" };
                str = string.Concat(textArray1);
            }
            if (ex.StackTrace != null)
            {
                str = str + Environment.NewLine + ex.StackTrace;
            }
            return str;
        }

        [Conditional("TRACE")]
        public static void TraceError(string message)
        {
            Trace.TraceError(message);
            Trace.Flush();
        }

        [Conditional("TRACE")]
        public static void TraceError(string format, params object[] args)
        {
            Trace.TraceError(format, args);
            Trace.Flush();
        }

        [Conditional("TRACE")]
        public static void TraceException(Exception ex)
        {
            Trace.TraceError((ex != null) ? FormatException(ex) : "");
            Trace.Flush();
        }

        [Conditional("TRACE")]
        public static void TraceInfo(string message)
        {
            Trace.TraceInformation(message);
            Trace.Flush();
        }

        [Conditional("TRACE")]
        public static void TraceInfo(string format, params object[] args)
        {
            Trace.TraceInformation(format, args);
            Trace.Flush();
        }

        [Conditional("TRACE")]
        public static void TraceWarning(string message)
        {
            Trace.TraceWarning(message);
            Trace.Flush();
        }

        [Conditional("TRACE")]
        public static void TraceWarning(string format, params object[] args)
        {
            Trace.TraceWarning(format, args);
            Trace.Flush();
        }
    }
}

