namespace DevExpress.Data.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public delegate ProcessStartDiagnosticsLevel ProcessStartTraceLevelResolver(ProcessStartInfo startInfo, ProcessStartDiagnosticsLevel traceLevel);
}

