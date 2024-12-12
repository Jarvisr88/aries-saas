namespace DevExpress.Utils
{
    using DevExpress.Data.Diagnostics;
    using DevExpress.Data.Internal;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class DeserializationSettings
    {
        private static readonly Lazy<XtraSerializationTrustedContext> registrations = new Lazy<XtraSerializationTrustedContext>(() => CreateRegistrations());

        private static XtraSerializationTrustedContext CreateRegistrations()
        {
            XtraSerializationTrustedContext context = new XtraSerializationTrustedContext(null);
            XtraSerializationSecurityTrace.diagnosticsLevelResolver = new XtraSerializationSecurityTraceLevelResolver(context.Resolve);
            return context;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void EnableSafeDeserialization()
        {
            XtraSerializationSecurityTrace.SetLevel(XtraSerializationSecurityDiagnosticsLevel.Throw, XtraSerializationSecurityTrace.diagnosticsLevelResolver);
        }

        public static void InvokeTrusted(Action trustedAction)
        {
            using (XtraSerializationSecurityTrace.Enter(XtraSerializationSecurityDiagnosticsLevel.Disable, (XtraSerializationSecurityTraceLevelResolver) null))
            {
                if (trustedAction != null)
                {
                    trustedAction();
                }
            }
        }

        public static void RegisterTrustedAssembly(Assembly assembly)
        {
            Registrations.Trusted(assembly);
        }

        public static void RegisterTrustedAssembly(string assemblyName)
        {
            Registrations.TrustedAssembly(assemblyName);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void RegisterTrustedClass(string typeName)
        {
            Registrations.TrustedType(typeName);
        }

        public static void RegisterTrustedClass(Type type)
        {
            Registrations.Trusted(type);
        }

        public static void RegisterTrustedClass(string assemblyName, string typeName)
        {
            Registrations.TrustedType(assemblyName, typeName);
        }

        private static XtraSerializationTrustedContext Registrations =>
            registrations.Value;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DeserializationSettings.<>c <>9 = new DeserializationSettings.<>c();

            internal XtraSerializationTrustedContext <.cctor>b__11_0() => 
                DeserializationSettings.CreateRegistrations();
        }
    }
}

