namespace DMEWorks
{
    using DMEWorks.Core;
    using Microsoft.VisualBasic.CompilerServices;
    using Microsoft.Win32;
    using System;

    public class UserSettings
    {
        private const string Key_Root = @"Software\DME";

        public static bool GetBool(string Name, bool Default)
        {
            int num = !Default ? 0 : 1;
            return (GetInt(Name, num) != 0);
        }

        public static int GetInt(string Name, int Default)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\DME", false))
                {
                    if (key != null)
                    {
                        return NullableConvert.ToInt32(key.GetValue(Name), Default);
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                TraceHelper.TraceException(ex);
                ProjectData.ClearProjectError();
            }
            return Default;
        }

        public static void SetBool(string Name, bool Value)
        {
            int num = !Value ? 0 : 1;
            SetInt(Name, num);
        }

        public static void SetInt(string Name, int Value)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\DME"))
                {
                    if (key != null)
                    {
                        key.SetValue(Name, Value);
                    }
                }
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                TraceHelper.TraceException(ex);
                ProjectData.ClearProjectError();
            }
        }
    }
}

