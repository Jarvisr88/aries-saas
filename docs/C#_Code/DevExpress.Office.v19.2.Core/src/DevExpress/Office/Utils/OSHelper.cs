namespace DevExpress.Office.Utils
{
    using Microsoft.Win32;
    using System;

    public static class OSHelper
    {
        public static bool IsWindows =>
            true;

        public static bool IsMacOS =>
            false;

        public static bool IsLinux =>
            false;

        public static bool IsWin8OrHigher
        {
            get
            {
                OperatingSystem oSVersion = Environment.OSVersion;
                return ((oSVersion.Platform == PlatformID.Win32NT) && (oSVersion.Version >= new Version(6, 2, 0x23f0)));
            }
        }

        public static bool IsWin10CreatorsOrHigher
        {
            get
            {
                if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                {
                    return false;
                }
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                return (new Version(key.GetValue("CurrentVersion") + "." + key.GetValue("CurrentBuild")) >= new Version(6, 3, 0x3ad7));
            }
        }

        public static bool IsWin10FallCreatorsOrHigher
        {
            get
            {
                if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                {
                    return false;
                }
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                return (new Version(key.GetValue("CurrentVersion") + "." + key.GetValue("CurrentBuild")) >= new Version(6, 3, 0x3fab));
            }
        }

        public static bool IsWin10May2020OrHigher
        {
            get
            {
                if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                {
                    return false;
                }
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
                return (new Version(key.GetValue("CurrentVersion") + "." + key.GetValue("CurrentBuild")) >= new Version(6, 3, 0x4a61));
            }
        }

        public static bool IsCLR3 =>
            false;

        public static bool IsCLR30 =>
            false;

        public static bool IsCLR31 =>
            false;
    }
}

