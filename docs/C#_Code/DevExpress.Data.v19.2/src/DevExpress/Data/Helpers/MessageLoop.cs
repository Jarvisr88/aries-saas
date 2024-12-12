namespace DevExpress.Data.Helpers
{
    using System;
    using System.Reflection;
    using System.Security;

    public static class MessageLoop
    {
        public static bool IsAvailable { get; }

        [SecuritySafeCritical]
        private static class PresentationFramework
        {
            private static System.Reflection.Assembly assemblyCore;

            internal static bool IsLoaded();
            private static bool IsPresentationFramework(System.Reflection.Assembly assembly);

            internal static System.Reflection.Assembly Assembly { get; }
        }

        private static class WPFApplication
        {
            private static PropertyInfo p_Application_Current;

            internal static bool IsRunning(Assembly presentationFrameworkAssembly);
        }
    }
}

