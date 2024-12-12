namespace DevExpress.Data.Utils
{
    using DevExpress.Data.Diagnostics;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public static class ProcessStartPolicy
    {
        [ThreadStatic]
        private static int suppressionCounter;
        [ThreadStatic]
        private static int throwOnErrorsCounter;
        private static readonly Lazy<ProcessStartTrustedContext> registrations;

        public static event EventHandler<ProcessStartPolicy.ProcessStartFailedExceptionEventArgs> Failed;

        public static event EventHandler Started;

        public static event CancelEventHandler Starting;

        static ProcessStartPolicy();
        private static ProcessStartTrustedContext CreateRegistrations();
        public static void RegisterTrustedProcess(string fileName);
        public static void RegisterTrustedProcess(string fileName, string arguments);
        public static void RequireConfirmation();
        public static void SuppressAll();
        public static void ThrowAlways();
        public static void ThrowOnErrors();

        internal static bool IsProcessStartSuppressed { get; }

        private static ProcessStartTrustedContext Registrations { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ProcessStartPolicy.<>c <>9;

            static <>c();
            internal ProcessStartTrustedContext <.cctor>b__24_0();
        }

        public class ProcessStartFailedExceptionEventArgs : EventArgs
        {
            public ProcessStartFailedExceptionEventArgs(System.Exception exception);

            public System.Exception Exception { get; private set; }

            public bool Throw { get; set; }
        }
    }
}

