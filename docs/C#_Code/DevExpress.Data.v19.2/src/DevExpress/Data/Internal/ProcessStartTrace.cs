namespace DevExpress.Data.Internal
{
    using DevExpress.Data.Diagnostics;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Security;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ProcessStartTrace
    {
        internal static ProcessStartDiagnosticsLevel? diagnosticsLevel;
        internal static ProcessStartTraceLevelResolver diagnosticsLevelResolver;
        private static TraceSource traceSource;

        internal static void Assert(ProcessStartInfo startInfo);
        internal static void Assert(ProcessStartInfo startInfo, ProcessStartDiagnosticsLevel actualTraceLevel);
        internal static IDisposable Enter(ProcessStartDiagnosticsLevel level, Action<ProcessStartTrustedContext> setup);
        internal static ProcessStartDiagnosticsLevel ResolveTraceLevel(ProcessStartInfo startInfo);
        internal static void SetLevel(ProcessStartDiagnosticsLevel level, ProcessStartTraceLevelResolver resolver);
        private static void Trace(ProcessStartInfo startInfo, TraceEventType eventType);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Trusted(Action trustedAction, Action<ProcessStartTrustedContext> setup);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Trusted(Action trustedAction, string fileName);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Trusted(Action trustedAction, string fileName, string arguments);

        private sealed class AlreadyInDiagnosticsContextException : ProcessStartTrace.ProcessStartPolicyViolationException
        {
            private const string messageFormat = "You cannot enter the {0} security context within another security context.";

            private AlreadyInDiagnosticsContextException(string level);
            public static void Throw(string levelName);
        }

        private sealed class Context : IDisposable
        {
            [ThreadStatic]
            private static ProcessStartTrace.Context Instance;
            private readonly ProcessStartDiagnosticsLevel level;
            private ProcessStartTraceLevelResolver resolver;
            private readonly ProcessStartTrustedContext trustedContext;
            public static readonly ProcessStartTraceLevelResolver DefaultResolver;

            static Context();
            public Context(ProcessStartDiagnosticsLevel level, ProcessStartTrustedContext trustedContext);
            private static void EnsureAlreadyInDiagnosticsContext(ProcessStartTrace.Context @this);
            private ProcessStartDiagnosticsLevel Resolve(ProcessStartInfo startInfo);
            private ProcessStartDiagnosticsLevel Resolve(ProcessStartInfo startInfo, ProcessStartDiagnosticsLevel? level);
            public static ProcessStartDiagnosticsLevel Resolve(ProcessStartTraceLevelResolver resolver, ProcessStartInfo startInfo, ProcessStartDiagnosticsLevel? level);
            private static ProcessStartDiagnosticsLevel ResolveCore(ProcessStartInfo startInfo, ProcessStartDiagnosticsLevel? level);
            void IDisposable.Dispose();

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly ProcessStartTrace.Context.<>c <>9;
                public static ProcessStartTraceLevelResolver <>9__6_0;

                static <>c();
                internal ProcessStartDiagnosticsLevel <.cctor>b__12_0(ProcessStartInfo startInfo, ProcessStartDiagnosticsLevel _);
                internal ProcessStartDiagnosticsLevel <Resolve>b__6_0(ProcessStartInfo psi, ProcessStartDiagnosticsLevel _);
            }
        }

        private sealed class NonTrustedProcessStartException : ProcessStartTrace.ProcessStartPolicyViolationException
        {
            private const string messageFormat = "The {0} process is not allowed to start due to security reasons.";

            private NonTrustedProcessStartException(string typeName);
            public static void Throw(ProcessStartInfo startInfo);
        }

        internal sealed class ProcessStartConfirmationNeededException : ProcessStartTrace.ProcessStartPolicyViolationException
        {
            private const string messageFormat = "The {0} process is not allowed to start without confirmation due to security reasons.";

            private ProcessStartConfirmationNeededException(string typeName, bool confirmationPlatformAvailable);
            public static void Throw(ProcessStartInfo startInfo, bool confirmationAvailable);

            public bool ConfirmationPlatformAvailable { get; private set; }
        }

        internal abstract class ProcessStartPolicyViolationException : SecurityException
        {
            private const string articleMessage = "See the https://go.devexpress.com/.NET_SafeProcess_Start.aspx article for additional information.";

            protected ProcessStartPolicyViolationException(string message);
        }
    }
}

