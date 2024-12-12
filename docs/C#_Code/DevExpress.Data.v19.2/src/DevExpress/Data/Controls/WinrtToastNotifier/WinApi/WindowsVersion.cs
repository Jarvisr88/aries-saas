namespace DevExpress.Data.Controls.WinrtToastNotifier.WinApi
{
    using System;
    using System.Security;

    internal static class WindowsVersion
    {
        public static int Major;
        public static int Minor;
        public static int Build;

        static WindowsVersion();
        [SecuritySafeCritical]
        private static string GetOSVersionString();

        public static bool IsWin10AnniversaryOrNewer { get; }
    }
}

