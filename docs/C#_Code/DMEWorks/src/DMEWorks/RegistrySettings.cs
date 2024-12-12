namespace DMEWorks
{
    using DMEWorks.Core;
    using Microsoft.VisualBasic.CompilerServices;
    using Microsoft.Win32;
    using System;

    public class RegistrySettings
    {
        private const string Key_Root = @"Software\DME\DMEWorks!";

        public static bool GetMachineString(string Name, ref string Value)
        {
            Value = "";
            try
            {
                bool flag;
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\DME\DMEWorks!", false))
                {
                    if (key == null)
                    {
                        flag = false;
                    }
                    else
                    {
                        object obj2 = key.GetValue(Name);
                        if (obj2 == null)
                        {
                            flag = false;
                        }
                        else
                        {
                            Value = Convert.ToString(obj2);
                            flag = true;
                        }
                    }
                }
                return flag;
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                TraceHelper.TraceException(ex);
                ProjectData.ClearProjectError();
            }
            return false;
        }

        public static bool? GetUserBool(string Name)
        {
            bool? nullable;
            int? userInt = GetUserInt(Name);
            if (userInt == null)
            {
                nullable = null;
            }
            else
            {
                bool? nullable1;
                int? nullable3 = userInt;
                if (nullable3 != null)
                {
                    nullable1 = new bool?(nullable3.GetValueOrDefault() != 0);
                }
                else
                {
                    nullable1 = null;
                }
                nullable = nullable1;
            }
            return nullable;
        }

        public static int? GetUserInt(string Name)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\DME\DMEWorks!", false))
                {
                    if (key != null)
                    {
                        return NullableConvert.ToInt32(key.GetValue(Name));
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
            return null;
        }

        public static bool GetUserString(string Name, ref string Value)
        {
            Value = "";
            try
            {
                bool flag;
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\DME\DMEWorks!", false))
                {
                    if (key == null)
                    {
                        flag = false;
                    }
                    else
                    {
                        object obj2 = key.GetValue(Name);
                        if (obj2 == null)
                        {
                            flag = false;
                        }
                        else
                        {
                            Value = Convert.ToString(obj2);
                            flag = true;
                        }
                    }
                }
                return flag;
            }
            catch (Exception exception1)
            {
                Exception ex = exception1;
                ProjectData.SetProjectError(ex);
                TraceHelper.TraceException(ex);
                ProjectData.ClearProjectError();
            }
            return false;
        }

        public static void SetUserBool(string Name, bool Value)
        {
            SetUserInt(Name, Value ? 1 : 0);
        }

        public static void SetUserInt(string Name, int Value)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\DME\DMEWorks!"))
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

