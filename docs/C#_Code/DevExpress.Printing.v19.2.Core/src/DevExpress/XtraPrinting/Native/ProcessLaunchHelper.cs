namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public static class ProcessLaunchHelper
    {
        public static Process StartProcess(string path);
        public static Process StartProcess(string path, bool waitForInputIdle);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ProcessLaunchHelper.<>c <>9;
            public static Action<ProcessStartInfo> <>9__1_0;

            static <>c();
            internal void <StartProcess>b__1_0(ProcessStartInfo x);
        }
    }
}

