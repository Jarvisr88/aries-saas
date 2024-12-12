namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Utils;
    using System;
    using System.Security;

    public static class ProcessLauncher
    {
        [SecuritySafeCritical]
        public static void Launch(string applicationName)
        {
            if (!string.IsNullOrEmpty(applicationName))
            {
                SafeProcess.Start(applicationName, null, null);
            }
        }
    }
}

