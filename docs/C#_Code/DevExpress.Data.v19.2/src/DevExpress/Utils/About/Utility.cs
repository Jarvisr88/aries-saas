namespace DevExpress.Utils.About
{
    using DevExpress.Internal;
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;

    public class Utility
    {
        internal static ProductKind[] productList = new ProductKind[] { (ProductKind.Default | ProductKind.XtraReports), (ProductKind.Default | ProductKind.XPO) };
        internal static string[] productText = new string[] { "XtraReports", "eXpressPersistentObjects" };
        protected static TraceSwitch licensingSwitch = new TraceSwitch("DXAbout", "");
        private static bool isDesignMode;
        private static bool staticAboutShown = false;
        private static bool? isDebuggerAttached;
        internal static bool? exp = null;

        public static int DaysLeft()
        {
            if (IsExpired())
            {
                return 0;
            }
            UserData infoEx = GetInfoEx();
            if (infoEx == null)
            {
                return 0;
            }
            DateTime expiration = infoEx.expiration;
            return Math.Max(1, (int) expiration.Subtract(DateTime.Now).TotalDays);
        }

        public static bool GetAllowStaticAbout()
        {
            bool flag;
            TraceWithCallStack("GetAllowStaticAbout invoked");
            if (staticAboutShown)
            {
                return false;
            }
            if (!ShouldShowAbout())
            {
                return false;
            }
            staticAboutShown = true;
            try
            {
                AssemblyName name = Assembly.GetEntryAssembly()?.GetName();
                if ((name != null) && (name.Name.ToLower() == "lc"))
                {
                    flag = false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public static ProductKind GetDXperienceKind()
        {
            UserData info = GetInfo();
            return ((info != null) ? (!info.IsLicensed(ProductKind.Default | ProductKind.Docs | ProductKind.DXperienceASP | ProductKind.DXperiencePro | ProductKind.DXperienceSliverlight | ProductKind.DXperienceWPF | ProductKind.XPO) ? (!info.IsLicensed(ProductKind.Default | ProductKind.DXperiencePro) ? ProductKind.Default : (ProductKind.Default | ProductKind.DXperiencePro)) : (ProductKind.Default | ProductKind.Docs | ProductKind.DXperienceASP | ProductKind.DXperiencePro | ProductKind.DXperienceSliverlight | ProductKind.DXperienceWPF | ProductKind.XPO)) : ProductKind.Default);
        }

        public static string GetExpiredText(int width, int height)
        {
            string str = "Your trial period has expired.\r\nVisit www.devexpress.com to purchase a copy and activate your license.";
            if ((width < 100) || (height < 50))
            {
                str = "Expired.";
            }
            Type type = Type.GetType("DevExpress.Data.Properties.Resources");
            if (type != null)
            {
                string propertyText = GetPropertyText(type, ((width < 100) || (height < 50)) ? "ExpiredTextShort" : "ExpiredTextLong");
                if (!string.IsNullOrEmpty(propertyText))
                {
                    str = propertyText;
                }
            }
            return str;
        }

        public static UserData GetInfo()
        {
            Guid id = new Guid(0x378852d, 0xd597, 0x4a32, 0xb6, 0xd9, 0x68, 10, 0x16, 0xa3, 0xcd, 0xa6);
            string key = ReadVersion(id, 0xc0);
            if (key == null)
            {
                return null;
            }
            UserData data = UserInfoChecker.Default.Parse(key);
            if ((data == null) || !data.IsValid)
            {
                return null;
            }
            data.expiration = GetTicks();
            return data;
        }

        public static UserData GetInfoEx()
        {
            UserData info = GetInfo();
            return ((info == null) ? new UserData(GetTicks()) : info);
        }

        private static string GetPropertyText(Type type, string name)
        {
            PropertyInfo property = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Static);
            return ((property == null) ? string.Empty : $"{property.GetValue(null, new object[0])}");
        }

        public static string GetRegisteredText()
        {
            string str = string.Empty;
            UserData info = GetInfo();
            if (info == null)
            {
                return string.Empty;
            }
            for (int i = 0; i < productList.Length; i++)
            {
                ProductKind kind = productList[i];
                if (info.IsLicensed(kind))
                {
                    str = str + productText[i];
                    if (info.IsLicensedSource(kind))
                    {
                        str = str + " (with source)";
                    }
                    str = str + "\r\n";
                }
            }
            return str;
        }

        public static string GetSerial(ProductKind kind, ProductInfoStage stage)
        {
            if (stage != ProductInfoStage.Registered)
            {
                return string.Empty;
            }
            UserData info = GetInfo();
            if ((info == null) || !info.IsValid)
            {
                return string.Empty;
            }
            if ((kind != ProductKind.Default) && !info.IsLicensed(kind))
            {
                if ((kind & (ProductKind.Default | ProductKind.FreeOffer)) != (ProductKind.Default | ProductKind.FreeOffer))
                {
                    return string.Empty;
                }
                if (!info.IsLicensed(ProductKind.Default | ProductKind.FreeOffer) && !info.IsLicensed(kind & ~(ProductKind.Default | ProductKind.FreeOffer)))
                {
                    return string.Empty;
                }
            }
            return string.Format("{2}, {0}/{1} (#{3})", new object[] { info.UserNo, info.KeyNumber, info.UserName, info.licensedProducts });
        }

        internal static DateTime GetTicks()
        {
            Guid id = new Guid(0x6f0f8269, 0x1516, 0x44c6, 0xbd, 0x30, 14, 0x90, 190, 0x27, 0x87, 0x1c);
            int num = ReadVersionEx(id, 0xc0);
            try
            {
                return ((num >= 1) ? new DateTime(0x7d0 + ((num % 0x4d2) % 0x26), (num % 0x4d2) / 0x26, num / 0x4d2).AddDays(30.0) : new DateTime(0x7dd, 5, 0x16).AddDays(30.0));
            }
            catch
            {
                return new DateTime(0x7dd, 5, 0x16).AddDays(30.0);
            }
        }

        public static string GetTrialText()
        {
            string str = string.Empty;
            UserData info = GetInfo();
            for (int i = 0; i < productList.Length; i++)
            {
                ProductKind kind = productList[i];
                if ((info == null) || !info.IsLicensed(kind))
                {
                    str = str + productText[i] + "\r\n";
                }
            }
            return str;
        }

        public static bool IsExpired()
        {
            if (exp == null)
            {
                UserData infoEx = GetInfoEx();
                exp = new bool?((infoEx == null) || infoEx.IsExpired);
            }
            return exp.Value;
        }

        public static bool IsFreeOfferOnly(ProductKind kind, ProductInfoStage stage)
        {
            if (stage != ProductInfoStage.Registered)
            {
                return false;
            }
            UserData info = GetInfo();
            if ((info == null) || !info.IsValid)
            {
                return false;
            }
            if (!info.IsLicensed(ProductKind.Default | ProductKind.FreeOffer))
            {
                return false;
            }
            if (kind == (ProductKind.Default | ProductKind.FreeOffer))
            {
                return false;
            }
            kind &= ~(ProductKind.Default | ProductKind.FreeOffer);
            return (!info.IsLicensed(kind) || (kind == (ProductKind.Default | ProductKind.FreeOffer)));
        }

        public static bool IsLic()
        {
            UserData infoEx = GetInfoEx();
            return ((infoEx != null) && (!infoEx.IsTrial && (infoEx.UserNo >= 0)));
        }

        public static int IsOnly(string platform)
        {
            try
            {
                int num;
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"Licenses\" + new Guid("{BAA304DF-1198-4BB5-AFCB-14801F755AA6}").ToString());
                if (key != null)
                {
                    object obj2 = key.GetValue(0xc0.ToString() + platform, 0);
                    key.Close();
                    if (obj2 != null)
                    {
                        if (((int) obj2) != 1)
                        {
                            if (((int) obj2) != 2)
                            {
                                goto TR_0000;
                            }
                            else
                            {
                                num = 2;
                            }
                        }
                        else
                        {
                            num = 1;
                        }
                    }
                    else
                    {
                        num = 0;
                    }
                }
                else
                {
                    num = 0;
                }
                return num;
            }
            catch
            {
            }
        TR_0000:
            return 0;
        }

        public static int IsOnlyWeb() => 
            IsOnly("ASP.NET");

        public static int IsOnlyWin() => 
            IsOnly("Windows Forms");

        public static int IsOnlyWpf() => 
            IsOnly("WPF Components");

        private static string ReadVersion(Guid id, int version)
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"Licenses\" + id.ToString());
            object obj2 = null;
            if (key == null)
            {
                return null;
            }
            try
            {
                obj2 = key.GetValue(version.ToString());
            }
            finally
            {
                key.Close();
            }
            return obj2?.ToString().Trim();
        }

        private static int ReadVersionEx(Guid id, int version)
        {
            int num;
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"Licenses\" + id.ToString());
            if (key == null)
            {
                return -1;
            }
            try
            {
                num = (int) key.GetValue(version.ToString(), -1);
            }
            catch
            {
                num = -1;
            }
            finally
            {
                key.Close();
            }
            return num;
        }

        public static bool ShouldShowAbout()
        {
            try
            {
                return ShouldShowAboutCore();
            }
            catch
            {
                return true;
            }
        }

        private static bool ShouldShowAboutCore()
        {
            TraceWithCallStack("ShouldShowAboutCore invoked");
            if (!IsDesignMode && !IsDebuggerAttached)
            {
                return true;
            }
            if (IsExpired())
            {
                return true;
            }
            bool flag = true;
            string name = "LastAboutShowedTime";
            RegistryKey key = null;
            try
            {
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DevExpress\Components\", true);
                key ??= Registry.CurrentUser.CreateSubKey(@"SOFTWARE\DevExpress\Components\");
            }
            catch
            {
            }
            if (key == null)
            {
                return true;
            }
            string text = key.GetValue(name, null) as string;
            DateTimeConverter converter = new DateTimeConverter();
            if (text == null)
            {
                flag = true;
            }
            else
            {
                try
                {
                    DateTime time = (DateTime) converter.ConvertFromString(null, CultureInfo.InvariantCulture, text);
                    flag = Math.Abs((DateTime.Now - time).TotalMinutes) >= 30.0;
                }
                catch
                {
                    flag = true;
                }
            }
            if (flag)
            {
                key.SetValue(name, converter.ConvertToString(null, CultureInfo.InvariantCulture, DateTime.Now), RegistryValueKind.String);
            }
            TraceWithCallStack("ShouldShowAboutCore result is:" + flag.ToString());
            return flag;
        }

        public static void TraceWithCallStack(string message)
        {
            if (licensingSwitch.TraceInfo)
            {
                StackTrace trace = new StackTrace();
                Trace.WriteLine(DateTime.Now.ToString());
                Trace.WriteLine(message + Environment.NewLine + trace.ToString());
            }
        }

        public static bool IsDesignMode
        {
            get => 
                isDesignMode;
            set
            {
                isDesignMode = value;
                if (value)
                {
                    UAlgo.DesignMode();
                }
            }
        }

        public static bool IsDebuggerAttached
        {
            get
            {
                if (isDebuggerAttached == null)
                {
                    try
                    {
                        isDebuggerAttached = new bool?(Debugger.IsAttached);
                    }
                    catch
                    {
                        isDebuggerAttached = false;
                    }
                }
                return isDebuggerAttached.Value;
            }
        }
    }
}

