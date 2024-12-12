namespace DevExpress.Data.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class ProcessStartTrustedContext : IDisposable
    {
        private readonly ProcessStartDiagnosticsLevel? initialDiagnosticsLevel;
        private readonly ProcessStartTraceLevelResolver initialResolver;
        private readonly HashSet<string> fileNames;
        private readonly HashSet<string> fileNamesAndArguments;

        internal ProcessStartTrustedContext(Action<ProcessStartTrustedContext> setup);
        internal ProcessStartDiagnosticsLevel Resolve(ProcessStartInfo startInfo, ProcessStartDiagnosticsLevel traceLevel);
        void IDisposable.Dispose();
        public ProcessStartTrustedContext Trusted(string fileName);
        public ProcessStartTrustedContext Trusted(string fileName, string arguments);
    }
}

