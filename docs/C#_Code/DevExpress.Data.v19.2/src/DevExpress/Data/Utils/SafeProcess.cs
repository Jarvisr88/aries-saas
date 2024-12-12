namespace DevExpress.Data.Utils
{
    using DevExpress.Utils;
    using DevExpress.Utils.IoC;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class SafeProcess
    {
        [ThreadStatic]
        private static IntegrityContainer serviceContainer;

        private static bool? ConfirmCore(ProcessStartInfo processStartInfo);
        private static ProcessStartInfo CreateProcessStartInfoCore(string fileName, string arguments);
        public static Process Open(string fileName, string arguments = null, Action<ProcessStartInfo> setup = null);
        [SecuritySafeCritical]
        private static SafeProcess.ProcessStartResult OpenCore(string fileName, string arguments, Action<ProcessStartInfo> setup);
        public static void RegisterConfirmationService(SafeProcess.IConfirmationService service);
        public static Process Start(string fileName, string arguments = null, Action<ProcessStartInfo> setup = null);
        public static Process StartConfirmed(string fileName, string arguments = null, Action<ProcessStartInfo> setup = null);
        [SecuritySafeCritical]
        private static SafeProcess.ProcessStartResult StartConfirmedCore(ProcessStartInfo processStartInfo, Action<ProcessStartInfo> setup);
        [SecuritySafeCritical]
        private static SafeProcess.ProcessStartResult StartCore(ProcessStartInfo processStartInfo);
        [SecuritySafeCritical]
        private static SafeProcess.ProcessStartResult StartCore(ProcessStartInfo processStartInfo, Action<ProcessStartInfo> setup);
        public static bool TryOpen(string fileName, string arguments = null, Action<ProcessStartInfo> setup = null);
        public static bool TryStart(string fileName, string arguments = null, Action<ProcessStartInfo> setup = null);
        public static void Wait(string fileName, string arguments = null, Action<ProcessStartInfo> setup = null);

        private sealed class ConfirmationInfo : SafeProcess.IConfirmationInfo
        {
            private readonly ProcessStartInfo startInfo;

            public ConfirmationInfo(ProcessStartInfo startInfo);

            public string FileName { get; }

            public string Title { get; }

            public string Header { get; }

            public string Location { get; }

            public string Message { get; }
        }

        internal sealed class FailedWeakEventHandler : WeakEventHandler<ProcessStartPolicy.ProcessStartFailedExceptionEventArgs, EventHandler<ProcessStartPolicy.ProcessStartFailedExceptionEventArgs>>
        {
            [ThreadStatic]
            private static SafeProcess.FailedWeakEventHandler instanceCore;

            private FailedWeakEventHandler();
            internal static void AddHandler(EventHandler<ProcessStartPolicy.ProcessStartFailedExceptionEventArgs> value);
            internal static bool RaiseFailed(ProcessStartInfo startInfo, Exception exception);
            internal static void RemoveHandler(EventHandler<ProcessStartPolicy.ProcessStartFailedExceptionEventArgs> value);

            private static SafeProcess.FailedWeakEventHandler Instance { get; }
        }

        public interface IConfirmationInfo
        {
            string Title { get; }

            string Header { get; }

            string Location { get; }

            string FileName { get; }

            string Message { get; }
        }

        public interface IConfirmationService
        {
            bool? Confirm(SafeProcess.IConfirmationInfo info);
        }

        private sealed class ProcessStartResult
        {
            public readonly System.Diagnostics.Process Process;
            public readonly System.Exception Exception;
            public static SafeProcess.ProcessStartResult NotStarted;

            static ProcessStartResult();
            private ProcessStartResult(System.Diagnostics.Process process, System.Exception exception);
            public static SafeProcess.ProcessStartResult FromException(System.Exception exception);
            public static SafeProcess.ProcessStartResult FromProcess(System.Diagnostics.Process process);

            public bool Failed { get; }
        }

        internal sealed class StartedWeakEventHandler : WeakEventHandler<EventArgs, EventHandler>
        {
            [ThreadStatic]
            private static SafeProcess.StartedWeakEventHandler instanceCore;

            private StartedWeakEventHandler();
            internal static void AddHandler(EventHandler value);
            internal static void RaiseStarted(Process process);
            internal static void RemoveHandler(EventHandler value);

            private static SafeProcess.StartedWeakEventHandler Instance { get; }
        }

        internal sealed class StartingWeakEventHandler : WeakEventHandler<CancelEventArgs, CancelEventHandler>
        {
            [ThreadStatic]
            private static SafeProcess.StartingWeakEventHandler instanceCore;

            private StartingWeakEventHandler();
            internal static void AddHandler(CancelEventHandler value);
            internal static bool RaiseStarting(ProcessStartInfo startInfo);
            internal static void RemoveHandler(CancelEventHandler value);

            private static SafeProcess.StartingWeakEventHandler Instance { get; }
        }
    }
}

