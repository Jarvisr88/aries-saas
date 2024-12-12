namespace DevExpress.Data.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class XtraSerializationTrustedContext : IDisposable
    {
        private readonly XtraSerializationSecurityDiagnosticsLevel? initialDiagnosticsLevel;
        private readonly XtraSerializationSecurityTraceLevelResolver initialResolver;
        private readonly HashSet<string> assemblies;
        private readonly HashSet<string> types;
        private readonly HashSet<string> typesFromAssemblies;

        internal XtraSerializationTrustedContext(Action<XtraSerializationTrustedContext> setup);
        internal XtraSerializationSecurityDiagnosticsLevel Resolve(string assembly, string type, XtraSerializationSecurityDiagnosticsLevel traceLevel);
        void IDisposable.Dispose();
        public XtraSerializationTrustedContext Trusted(Assembly assembly);
        public XtraSerializationTrustedContext Trusted(Type type);
        public XtraSerializationTrustedContext TrustedAssembly(string assembly);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public XtraSerializationTrustedContext TrustedType(string type);
        public XtraSerializationTrustedContext TrustedType(string assembly, string type);
    }
}

