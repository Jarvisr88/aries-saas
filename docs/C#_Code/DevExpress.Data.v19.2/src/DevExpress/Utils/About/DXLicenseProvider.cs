namespace DevExpress.Utils.About
{
    using DevExpress.Data.Helpers;
    using DevExpress.Internal;
    using Microsoft.Win32;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Security;

    public abstract class DXLicenseProvider : LicenseProvider
    {
        private static Hashtable keys = new Hashtable();

        protected DXLicenseProvider()
        {
        }

        protected virtual string EncodeKey(DXLicenseType licType, UserData user) => 
            (licType == DXLicenseType.Full) ? "F" : ((licType == DXLicenseType.Trial) ? ("T" + ((user == null) ? "0" : user.GetExp())) : null);

        public override License GetLicense(LicenseContext context, Type type, object instance, bool allowExceptions)
        {
            bool flag = (context.UsageMode == LicenseUsageMode.Designtime) || this.IsMvcWizard;
            if (flag)
            {
                Utility.IsDesignMode = true;
            }
            ProductKind licProductKind = this.Kind;
            DXLicenseType none = DXLicenseType.None;
            string savedLicenseKey = this.GetSavedLicenseKey(context, type);
            DateTime minValue = DateTime.MinValue;
            if (!string.IsNullOrEmpty(savedLicenseKey))
            {
                none = this.ParseKey(savedLicenseKey, out minValue);
            }
            if ((none == DXLicenseType.None) | flag)
            {
                UserData info = null;
                if (!flag || (this.Kind == ProductKind.Default))
                {
                    none = DXLicenseType.Full;
                }
                else
                {
                    info = Utility.GetInfoEx();
                    if (info == null)
                    {
                        none = DXLicenseType.Trial;
                    }
                    else
                    {
                        none = DXLicenseType.Trial;
                        if (this.Kinds.Any<ProductKind>(delegate (ProductKind q) {
                            if (!info.IsLicensed(q))
                            {
                                return false;
                            }
                            licProductKind = q;
                            return true;
                        }))
                        {
                            none = DXLicenseType.Full;
                        }
                    }
                    try
                    {
                        RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\DevExpress\Components\v19.2");
                        if (key != null)
                        {
                            flag = false;
                            key.Close();
                        }
                    }
                    catch
                    {
                        flag = false;
                    }
                }
                if (!flag)
                {
                    savedLicenseKey = this.EncodeKey(none, info);
                }
            }
            if (savedLicenseKey != null)
            {
                this.SetSavedLicenseKey(context, type, savedLicenseKey);
            }
            if ((context.UsageMode == LicenseUsageMode.Runtime) && (none == DXLicenseType.Trial))
            {
                Utility.exp = new bool?(DateTime.Now > minValue);
            }
            return ((none == DXLicenseType.None) ? null : new DXLicense(none, DateTime.Now > minValue, licProductKind));
        }

        private string GetSavedLicenseKey(LicenseContext context, Type type)
        {
            object obj2 = keys[type.AssemblyQualifiedName];
            if (obj2 == null)
            {
                try
                {
                    obj2 = this.GetSavedLicenseKeyCaseInvensitive(context, type, null);
                }
                catch (SecurityException)
                {
                    try
                    {
                        Assembly asm = Assembly.Load("App_Licenses");
                        obj2 = this.GetSavedLicenseKeyCaseInvensitive(context, type, asm);
                    }
                    catch
                    {
                        return null;
                    }
                }
                catch (UriFormatException)
                {
                    try
                    {
                        Assembly asm = Assembly.Load("App_Licenses");
                        obj2 = this.GetSavedLicenseKeyCaseInvensitive(context, type, asm);
                    }
                    catch
                    {
                        return null;
                    }
                }
                catch (SerializationException)
                {
                    return "F";
                }
            }
            return obj2?.ToString();
        }

        private string GetSavedLicenseKeyCaseInvensitive(LicenseContext context, Type type, Assembly asm)
        {
            string savedLicenseKey = context.GetSavedLicenseKey(type, asm);
            if ((savedLicenseKey == null) && !SecurityHelper.IsPartialTrust)
            {
                FieldInfo field = context.GetType().GetField("savedLicenseKeys", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    Hashtable d = field.GetValue(context) as Hashtable;
                    if (d != null)
                    {
                        return (new Hashtable(d, StringComparer.InvariantCultureIgnoreCase)[type.AssemblyQualifiedName] as string);
                    }
                }
            }
            return savedLicenseKey;
        }

        protected virtual DXLicenseType ParseKey(string key, out DateTime date)
        {
            date = DateTime.MinValue;
            if (key == "F")
            {
                return DXLicenseType.Full;
            }
            if (key != "T")
            {
                if ((key == null) || !key.StartsWith("T"))
                {
                    return DXLicenseType.None;
                }
                try
                {
                    long fileTime = Convert.ToInt64(key.Substring(1));
                    date = DateTime.FromFileTime(fileTime);
                }
                catch
                {
                }
            }
            return DXLicenseType.Trial;
        }

        protected void SetSavedLicenseKey(LicenseContext context, Type type, string key)
        {
            if (key != null)
            {
                keys[type.AssemblyQualifiedName] = key;
                context.SetSavedLicenseKey(type, key);
            }
        }

        protected abstract ProductKind Kind { get; }

        protected virtual bool IsMvcWizard =>
            false;

        protected virtual ProductKind[] Kinds =>
            new ProductKind[] { this.Kind };
    }
}

