namespace DevExpress.Data.Internal
{
    using DevExpress.Data.Diagnostics;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    [EditorBrowsable(EditorBrowsableState.Never), SecuritySafeCritical]
    public static class XtraSerializationSecurityTrace
    {
        internal static XtraSerializationSecurityDiagnosticsLevel? diagnosticsLevel;
        internal static XtraSerializationSecurityTraceLevelResolver diagnosticsLevelResolver;
        private static TraceSource traceSource;

        private static void Assert(XtraSerializationSecurityTrace.ApiLevel apiLevel, string assembly, string type);
        private static TraceSource CreateTraceSource();
        private static TraceSource EnsureTraceSource();
        internal static IDisposable Enter(XtraSerializationSecurityDiagnosticsLevel level, XtraSerializationSecurityTraceLevelResolver resolver);
        internal static IDisposable Enter(XtraSerializationSecurityDiagnosticsLevel level, Action<XtraSerializationTrustedContext> setup);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void NonTrustedType(string assembly, string type);
        internal static XtraSerializationSecurityDiagnosticsLevel ResolveTraceLevel(string assembly, string type);
        internal static void SetLevel(XtraSerializationSecurityDiagnosticsLevel level, XtraSerializationSecurityTraceLevelResolver resolver);
        private static void Trace(XtraSerializationSecurityTrace.ApiLevel level, string assembly, string type, TraceEventType eventType);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Trusted(Action trustedAction, XtraSerializationSecurityTraceLevelResolver trustedResolver = null);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Trusted(Action trustedAction, Action<XtraSerializationTrustedContext> setup);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static T Trusted<T>(Func<T> trustedFunction, XtraSerializationSecurityTraceLevelResolver trustedResolver = null);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void TrustedType(string assembly, string type);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void UnsafeType(string assembly, string type);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly XtraSerializationSecurityTrace.<>c <>9;
            public static XtraSerializationSecurityTraceLevelResolver <>9__9_0;

            static <>c();
            internal XtraSerializationSecurityDiagnosticsLevel <Trusted>b__9_0(string a, string t, XtraSerializationSecurityDiagnosticsLevel l);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__11<T>
        {
            public static readonly XtraSerializationSecurityTrace.<>c__11<T> <>9;
            public static XtraSerializationSecurityTraceLevelResolver <>9__11_0;

            static <>c__11();
            internal XtraSerializationSecurityDiagnosticsLevel <Trusted>b__11_0(string a, string t, XtraSerializationSecurityDiagnosticsLevel l);
        }

        private sealed class AlreadyInDiagnosticsContextException : XtraSerializationSecurityTrace.XtraSerializationSecurityException
        {
            private const string messageFormat = "You cannot enter the {0} security context within another security context.";

            private AlreadyInDiagnosticsContextException(string level);
            public static void Throw(string levelName);
        }

        private enum ApiLevel
        {
            public const XtraSerializationSecurityTrace.ApiLevel TrustedType = XtraSerializationSecurityTrace.ApiLevel.TrustedType;,
            public const XtraSerializationSecurityTrace.ApiLevel NonTrustedType = XtraSerializationSecurityTrace.ApiLevel.NonTrustedType;,
            public const XtraSerializationSecurityTrace.ApiLevel UnsafeType = XtraSerializationSecurityTrace.ApiLevel.UnsafeType;
        }

        private sealed class Context : IDisposable
        {
            [ThreadStatic]
            private static XtraSerializationSecurityTrace.Context Instance;
            private readonly XtraSerializationSecurityDiagnosticsLevel level;
            private XtraSerializationSecurityTraceLevelResolver resolver;
            private readonly XtraSerializationTrustedContext trustedContext;
            public static readonly XtraSerializationSecurityTraceLevelResolver DefaultResolver;

            static Context();
            public Context(XtraSerializationSecurityDiagnosticsLevel level, XtraSerializationSecurityTraceLevelResolver resolver);
            public Context(XtraSerializationSecurityDiagnosticsLevel level, XtraSerializationTrustedContext trustedContext);
            private static void EnsureAlreadyInDiagnosticsContext(XtraSerializationSecurityTrace.Context @this);
            private XtraSerializationSecurityDiagnosticsLevel Resolve(string assembly, string type);
            private XtraSerializationSecurityDiagnosticsLevel Resolve(string assembly, string type, XtraSerializationSecurityDiagnosticsLevel? level);
            public static XtraSerializationSecurityDiagnosticsLevel Resolve(XtraSerializationSecurityTraceLevelResolver resolver, string assembly, string type, XtraSerializationSecurityDiagnosticsLevel? level);
            private static XtraSerializationSecurityDiagnosticsLevel ResolveCore(string assembly, string type, XtraSerializationSecurityDiagnosticsLevel? level);
            void IDisposable.Dispose();

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly XtraSerializationSecurityTrace.Context.<>c <>9;
                public static XtraSerializationSecurityTraceLevelResolver <>9__7_0;

                static <>c();
                internal XtraSerializationSecurityDiagnosticsLevel <.cctor>b__13_0(string a, string t, XtraSerializationSecurityDiagnosticsLevel l);
                internal XtraSerializationSecurityDiagnosticsLevel <Resolve>b__7_0(string a, string t, XtraSerializationSecurityDiagnosticsLevel l);
            }
        }

        internal sealed class NonPrimitiveValueForPropertyException : XtraSerializationSecurityTrace.XtraSerializationSecurityException
        {
            private const string BC4915 = "https://go.devexpress.com/Jan2019_Deserialization_Issue_Tag_Property.aspx";
            private const string messageFormat = "The {0} property value cannot be safely serialized. It is skipped for security reasons.";

            private NonPrimitiveValueForPropertyException(string propertyName);
            public static void Mute(string propertyName);
        }

        private sealed class NonTrustedTypeDeserializationException : XtraSerializationSecurityTrace.XtraSerializationSecurityException
        {
            private const string messageFormat = "The {0} type is not in the list of trusted types and therefore is not deserialized due to security reasons.";

            private NonTrustedTypeDeserializationException(string typeName);
            public static void Throw(string typeName);
        }

        private sealed class UnsafeTypeDeserializationException : XtraSerializationSecurityTrace.XtraSerializationSecurityException
        {
            private const string messageFormat = "The {0} type is considered unsafe and therefore is not deserialized due to security reasons.";

            private UnsafeTypeDeserializationException(string typeName);
            public static void Throw(string typeName);
        }

        [SecuritySafeCritical]
        internal abstract class XtraSerializationSecurityException : SecurityException
        {
            private const string articleMessage = "See the {0} article for additional information.";

            protected XtraSerializationSecurityException(string message, string link = null);
            internal static Exception Unwrap(Exception exception);
        }
    }
}

