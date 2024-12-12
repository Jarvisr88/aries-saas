namespace DevExpress.Internal
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Text;

    public static class WinApiRegistryHelper
    {
        [SecuritySafeCritical]
        public static void CloseRegistryKey(IntPtr key)
        {
            Import.RegCloseKey(key);
        }

        private static IntPtr HkeyToPtr(RegistryHive hkey) => 
            (hkey == RegistryHive.CurrentUser) ? Import.HKEY_CURRENT_USER : Import.HKEY_LOCAL_MACHINE;

        [SecuritySafeCritical]
        public static IntPtr OpenRegistryKey(RegistryHive hkey, string subkey, ResigtryAccess access)
        {
            IntPtr zero;
            if (Import.RegOpenKeyEx(HkeyToPtr(hkey), subkey, 0, (int) access, out zero) != 0)
            {
                zero = IntPtr.Zero;
            }
            return zero;
        }

        [SecuritySafeCritical]
        public static string[] ReadRegistryKeyMultiSzValue(IntPtr key, string name)
        {
            Import.RType regMultiSz = Import.RType.RegMultiSz;
            uint pcbData = 0;
            if (Import.RegQueryValueEx(key, name, 0, ref regMultiSz, null, ref pcbData) != 0)
            {
                return null;
            }
            byte[] pvData = new byte[pcbData];
            if (Import.RegQueryValueEx(key, name, 0, ref regMultiSz, pvData, ref pcbData) != 0)
            {
                return null;
            }
            List<string> list = new List<string>();
            string str = Encoding.Unicode.GetString(pvData, 0, (int) pcbData);
            int index = -1;
            while (true)
            {
                int startIndex = index + 1;
                index = str.IndexOf('\0', startIndex);
                if (index <= startIndex)
                {
                    return list.ToArray();
                }
                list.Add(str.Substring(startIndex, index - startIndex));
            }
        }

        [SecuritySafeCritical]
        public static string ReadRegistryKeySzValue(IntPtr key, string name)
        {
            Import.RType regSz = Import.RType.RegSz;
            uint pcbData = 0;
            if (Import.RegQueryValueEx(key, name, 0, ref regSz, null, ref pcbData) != 0)
            {
                return null;
            }
            byte[] pvData = new byte[pcbData];
            return ((Import.RegQueryValueEx(key, name, 0, ref regSz, pvData, ref pcbData) == 0) ? Encoding.Unicode.GetString(pvData, 0, (int) pcbData) : null);
        }

        private static class Import
        {
            public static IntPtr HKEY_LOCAL_MACHINE = new IntPtr(-2147483646);
            public static IntPtr HKEY_CURRENT_USER = new IntPtr(-2147483647);
            public const int KEY_QUERY_VALUE = 1;
            public const int KEY_SET_VALUE = 2;
            public const int KEY_CREATE_SUB_KEY = 4;
            public const int KEY_ENUMERATE_SUB_KEYS = 8;
            public const int KEY_NOTIFY = 0x10;
            public const int KEY_CREATE_LINK = 0x20;
            public const int KEY_WOW64_32KEY = 0x200;
            public const int KEY_WOW64_64KEY = 0x100;
            public const int KEY_WOW64_RES = 0x300;
            public const int KEY_READ = 0x20019;

            [DllImport("advapi32.dll", SetLastError=true)]
            public static extern int RegCloseKey(IntPtr hKey);
            [DllImport("advapi32.dll", CharSet=CharSet.Auto)]
            public static extern int RegOpenKeyEx(IntPtr hKey, string subKey, int ulOptions, int samDesired, out IntPtr hkResult);
            [DllImport("advapi32.dll", EntryPoint="RegQueryValueExW", CharSet=CharSet.Unicode, SetLastError=true)]
            public static extern uint RegQueryValueEx(IntPtr hKey, string lpValueName, int lpReserved, ref RType lpType, byte[] pvData, ref uint pcbData);

            public enum RFlags
            {
                Any = 0xffff,
                RegNone = 1,
                Noexpand = 0x10000000,
                RegBinary = 8,
                Dword = 0x18,
                RegDword = 0x10,
                Qword = 0x48,
                RegQword = 0x40,
                RegSz = 2,
                RegMultiSz = 0x20,
                RegExpandSz = 4,
                RrfZeroonfailure = 0x20000000
            }

            public enum RType
            {
                RegNone = 0,
                RegSz = 1,
                RegExpandSz = 2,
                RegMultiSz = 7,
                RegBinary = 3,
                RegDword = 4,
                RegQword = 11,
                RegQwordLittleEndian = 11,
                RegDwordLittleEndian = 4,
                RegDwordBigEndian = 5,
                RegLink = 6,
                RegResourceList = 8,
                RegFullResourceDescriptor = 9,
                RegResourceRequirementsList = 10
            }
        }

        [Flags]
        public enum ResigtryAccess
        {
            QueryValue = 1,
            SetValue = 2,
            CreateSubKey = 4,
            EnumerateSubKeys = 8,
            Notify = 0x10,
            CreateLink = 0x20,
            Read = 0x20019,
            WOW64_32Key = 0x200,
            WOW64_64Key = 0x100,
            WOW64_Res = 0x300
        }
    }
}

