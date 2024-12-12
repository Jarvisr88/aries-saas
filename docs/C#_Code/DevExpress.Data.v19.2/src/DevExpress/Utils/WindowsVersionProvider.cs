namespace DevExpress.Utils
{
    using Microsoft.Win32;
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;

    public static class WindowsVersionProvider
    {
        private static string RegistryVersionKey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        private static WindowsVersionInfo info;

        private static int ConvertToInt32FromHex(object value) => 
            (value == null) ? -2147483648 : int.Parse(value.ToString(), NumberStyles.HexNumber);

        private static int ConvertToInt32FromString(object value) => 
            (value != null) ? int.Parse(value.ToString()) : -2147483648;

        private static string ConvertToString(object value) => 
            (value != null) ? value.ToString() : string.Empty;

        private static Version ConvertToVersion(object value)
        {
            Version result = new Version();
            if (value != null)
            {
                Version.TryParse(value.ToString(), out result);
            }
            return result;
        }

        private static T GetRegistryKeyValue<T>(RegistryKey key, string subKeyName, Func<object, T> convert, T defaultVal)
        {
            if (key != null)
            {
                object arg = key.GetValue(subKeyName);
                if (arg != null)
                {
                    return convert(arg);
                }
            }
            return defaultVal;
        }

        private static bool TryGetReleaseAndBuildVersion(out WindowsVersionInfo vi)
        {
            bool flag = false;
            vi = null;
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(RegistryVersionKey, false))
                {
                    if (key != null)
                    {
                        WindowsVersionInfo info = new WindowsVersionInfo {
                            CurrentBuild = GetRegistryKeyValue<int>(key, "CurrentBuild", new Func<object, int>(WindowsVersionProvider.ConvertToInt32FromString), -2147483648),
                            CurrentMajorVersionNumber = GetRegistryKeyValue<int>(key, "CurrentMajorVersionNumber", new Func<object, int>(WindowsVersionProvider.ConvertToInt32FromHex), -2147483648),
                            CurrentMinorVersionNumber = GetRegistryKeyValue<int>(key, "CurrentMinorVersionNumber", new Func<object, int>(WindowsVersionProvider.ConvertToInt32FromHex), -2147483648),
                            CurrentVersion = GetRegistryKeyValue<Version>(key, "CurrentVersion", new Func<object, Version>(WindowsVersionProvider.ConvertToVersion), null),
                            ProductName = GetRegistryKeyValue<string>(key, "ProductName", new Func<object, string>(WindowsVersionProvider.ConvertToString), string.Empty),
                            ReleaseID = GetRegistryKeyValue<int>(key, "ReleaseId", new Func<object, int>(WindowsVersionProvider.ConvertToInt32FromString), -2147483648)
                        };
                        vi = info;
                        flag = true;
                    }
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private static WindowsVersionInfo WinVersionInfo =>
            ((info != null) || TryGetReleaseAndBuildVersion(out info)) ? info : new WindowsVersionInfo();

        public static bool IsWindows10 =>
            WinVersionInfo.ProductName.ToLower().Contains("windows 10");

        public static bool IsWin10AnniversaryUpdateOrHigher =>
            (WinVersionInfo.CurrentMajorVersionNumber > 10) || ((WinVersionInfo.CurrentMajorVersionNumber == 10) && (WinVersionInfo.CurrentBuild >= 0x3839));

        public static bool IsWin10FallCreatorsUpdateOrHigher =>
            WinVersionInfo.ReleaseID >= 0x6ad;

        public static bool IsWin10SpringCreatorsUpdateOrHigher =>
            WinVersionInfo.ReleaseID >= 0x70b;

        public static bool IsWinSupportsAcrylicEffect =>
            (WinVersionInfo.ReleaseID >= 0x70b) && (WinVersionInfo.CurrentBuild >= 0x42a8);

        public static bool IsWindows10Build1903OrHigher =>
            WinVersionInfo.ReleaseID >= 0x76f;

        public static bool IsWindows10Build1809OrHigher =>
            WinVersionInfo.ReleaseID >= 0x711;
    }
}

