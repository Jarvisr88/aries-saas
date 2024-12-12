namespace Devart.Common
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    internal class ad
    {
        private string a;
        private string b;
        private string c;
        private PerformanceCounter d;
        private PerformanceCounterType e;
        private static string f;
        private const int g = 0x7f;

        static ad()
        {
            string friendlyName = AppDomain.CurrentDomain.FriendlyName;
            string str2 = string.Empty;
            try
            {
                str2 = Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture);
            }
            catch
            {
                str2 = "-" + Guid.NewGuid().ToString().Substring(0, 8);
            }
            string str3 = (((friendlyName.Length + str2.Length) + 2) > 0x7f) ? (friendlyName.Substring(0, Math.Min(friendlyName.Length, (0x7f - str2.Length) - 5)) + "...[" + str2 + "]") : (friendlyName + "[" + str2 + "]");
            f = str3.Replace('(', '[').Replace(')', ']').Replace('#', '_').Replace('\\', '_').Replace('/', '_');
        }

        public ad(string A_0, string A_1, string A_2, PerformanceCounterType A_3)
        {
            this.a = A_0;
            this.b = A_1;
            this.c = A_2;
            this.e = A_3;
        }

        public void a()
        {
            try
            {
                if (this.d == null)
                {
                    this.d = new PerformanceCounter();
                    this.d.CategoryName = this.a;
                    this.d.CounterName = this.b;
                    this.d.InstanceName = f;
                    this.d.InstanceLifetime = PerformanceCounterInstanceLifetime.Process;
                    this.d.ReadOnly = false;
                    this.d.RawValue = 0L;
                }
            }
            catch (InvalidOperationException)
            {
            }
        }

        public void b()
        {
            if (this.d != null)
            {
                this.d.RemoveInstance();
                this.d = null;
            }
        }

        public CounterCreationData c()
        {
            CounterCreationData data1 = new CounterCreationData();
            data1.CounterName = this.b;
            data1.CounterHelp = this.c;
            data1.CounterType = this.e;
            return data1;
        }

        public void d()
        {
            if (this.d != null)
            {
                this.d.Increment();
            }
        }

        public void e()
        {
            if (this.d != null)
            {
                this.d.Decrement();
            }
        }

        public bool f() => 
            this.d != null;
    }
}

