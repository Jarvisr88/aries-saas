namespace DevExpress.Utils.About
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Windows.Forms;

    internal class UAlgoExceptionReportGenerator
    {
        private static string GenerateExceptionReport(Exception[] list)
        {
            StringBuilder builder = new StringBuilder();
            string str = "";
            for (int i = 0; i < list.Length; i++)
            {
                Exception e = list[i];
                builder.AppendFormat("{0}{2} {1}", str, GetExceptionType(e), (i < (list.Length - 1)) ? "**** Inner Exception" : "----");
                builder.AppendLine();
                builder.AppendFormat("{0}{1}", str, e.Message);
                builder.AppendLine();
                string stackTrace = e.StackTrace;
                if (!string.IsNullOrWhiteSpace(stackTrace))
                {
                    builder.AppendFormat("{0}Stack:{1}", str, stackTrace);
                }
                builder.AppendLine();
                if (e.Data != null)
                {
                    foreach (object obj2 in e.Data.Keys)
                    {
                        object obj3 = e.Data[obj2];
                        builder.AppendFormat("{0}  #{1} == {2}", str, obj2, (obj3 == null) ? "#null" : obj3);
                        builder.AppendLine();
                    }
                }
                str = str + new string(' ', 4);
            }
            return builder.ToString();
        }

        private string GenerateExceptionReportCore(Exception e)
        {
            if (e == null)
            {
                return "**** Exception is null *** ";
            }
            try
            {
                return GenerateExceptionReportEx(e);
            }
            catch
            {
                return "*** XR ***";
            }
        }

        private static string GenerateExceptionReportEx(Exception e) => 
            GenerateExceptionReport(GenerateList(e));

        private static Exception[] GenerateList(Exception e)
        {
            if (e.InnerException == null)
            {
                return new Exception[] { e };
            }
            Stack<Exception> stack = new Stack<Exception>();
            int num = 0;
            while ((e != null) && (++num <= 4))
            {
                stack.Push(e);
                e = e.InnerException;
            }
            return stack.ToArray();
        }

        private static string GetExceptionType(Exception e)
        {
            System.Type type = e.GetType();
            return (((type.FullName == null) || !type.FullName.StartsWith("System.")) ? type.FullName : type.Name);
        }

        private string GetInfo()
        {
            string str5;
            try
            {
                string processName = Process.GetCurrentProcess().ProcessName;
                string str2 = $"OS {Environment.OSVersion.ToString()}, x64:{Environment.Is64BitOperatingSystem},{Environment.Is64BitProcess}";
                string str3 = (Process.GetCurrentProcess().WorkingSet64 / 0x100000L).ToString() + "mb";
                string str4 = $"{SystemInformation.WorkingArea.Size.Width}x{SystemInformation.WorkingArea.Size.Height}";
                string[] textArray1 = new string[] { processName, "\r\n", str2, "\r\n", str3, "\r\n", str4, "\r\n" };
                str5 = string.Concat(textArray1);
            }
            catch
            {
                return "";
            }
            return str5;
        }

        public string GetText(Exception eo) => 
            this.GetInfo() + this.GenerateExceptionReportCore(eo);
    }
}

