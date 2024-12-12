namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    public abstract class LogBase
    {
        protected LogBase();
        [Conditional("DEBUGTEST")]
        public static void Add(object sender, object value, string message = "");
    }
}

