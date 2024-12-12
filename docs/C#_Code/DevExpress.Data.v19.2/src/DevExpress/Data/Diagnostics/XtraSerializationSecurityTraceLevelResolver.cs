namespace DevExpress.Data.Diagnostics
{
    using System;
    using System.Runtime.CompilerServices;

    public delegate XtraSerializationSecurityDiagnosticsLevel XtraSerializationSecurityTraceLevelResolver(string assembly, string type, XtraSerializationSecurityDiagnosticsLevel traceLevel);
}

