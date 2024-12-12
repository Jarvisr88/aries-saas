namespace DevExpress.Xpf
{
    using DevExpress.Internal;
    using DevExpress.Utils.About;
    using Microsoft.Win32;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Threading;

    public class About
    {
        private static TraceSource LicenseTraceSource = new TraceSource("LicenseTrace");
        private static string[] vs2010names = new string[] { "devenv", "vcsexpress", "vbexpress", "vwdexpress" };
        private static bool aboutShown = false;

        static About()
        {
            string logFileName = GetLogFileName();
            LicenseTraceSource.Switch = new SourceSwitch("LicenseSwitch");
            LicenseTraceSource.Switch.Level = (logFileName != null) ? SourceLevels.Information : SourceLevels.Off;
            TextWriterTraceListener listener = new TextWriterTraceListener(logFileName) {
                TraceOutputOptions = TraceOptions.Callstack | TraceOptions.ProcessId | TraceOptions.DateTime
            };
            LicenseTraceSource.Listeners.Clear();
            LicenseTraceSource.Listeners.Add(listener);
        }

        public static void CheckLicenseShowNagScreen(Type type)
        {
            DXLicense license = LicenseManager.Validate(type, 0) as DXLicense;
            if ((license != null) && (license.LicType == DXLicenseType.Trial))
            {
                object[] objArray2 = new object[2];
                object[] objArray3 = new object[2];
                objArray3[0] = license.LicType.ToString() + (license.IsExpired ? " Expired" : "");
                object[] args = objArray3;
                args[1] = type;
                LicenseTraceSource.TraceInformation("license {0} check failed for type: {1} ", args);
                object[] objArray1 = new object[] { license.IsExpired };
                Dispatcher.CurrentDispatcher.BeginInvoke(new AboutInvoker(About.ShowAbout), objArray1);
            }
        }

        private static bool CheckProcessNameFor2010VS(string processName)
        {
            foreach (string str in vs2010names)
            {
                if (processName.ToLower().Contains(str.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        public static ProductKind GetDefaultProductKind() => 
            ProductKind.Default | ProductKind.DXperienceWPF;

        private static string GetLogFileName()
        {
            string name = "LicenseLogFile";
            RegistryKey key = null;
            try
            {
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\DevExpress\Components\", false);
            }
            catch
            {
            }
            return ((key != null) ? (key.GetValue(name, null) as string) : null);
        }

        private static ProductStringInfo GetProductInfo(ProductKind productKind) => 
            (productKind == (ProductKind.Default | ProductKind.DXperienceWPF)) ? new ProductStringInfo("DevExpress WPF Subscription") : ((productKind == (ProductKind.Default | ProductKind.DXperienceSliverlight)) ? new ProductStringInfo("DevExpress Silverlight Subscription") : ((productKind != (ProductKind.Default | ProductKind.FreeOffer)) ? ProductInfoHelper.GetProductInfo(productKind) : new ProductStringInfo("DevExpress Free Controls")));

        public static bool IsDesignMode()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            string processName = Process.GetCurrentProcess().ProcessName;
            if ((entryAssembly == null) && CheckProcessNameFor2010VS(processName))
            {
                return true;
            }
            string location = entryAssembly.Location;
            string[] textArray1 = new string[] { "VisualStudio", "Blend", "xdesproc" };
            foreach (string str3 in textArray1)
            {
                if (location.ToLower().Contains(str3.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool ShouldShowAbout()
        {
            bool flag = true;
            if ((aboutShown || (BrowserInteropHelper.IsBrowserHosted || ((Application.Current == null) || (!ReferenceEquals(Application.Current.Dispatcher, Dispatcher.CurrentDispatcher) || ((Application.Current.Windows.Count == 0) && !IsDesignMode()))))) || (!IsDesignMode() && Utility.IsDebuggerAttached))
            {
                aboutShown = true;
                return false;
            }
            if (!IsDesignMode())
            {
                bool flag2;
                try
                {
                    flag2 = true;
                }
                finally
                {
                    aboutShown = true;
                }
                return flag2;
            }
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
                catch (Exception)
                {
                    flag = true;
                }
            }
            if (flag)
            {
                key.SetValue(name, converter.ConvertToString(null, CultureInfo.InvariantCulture, DateTime.Now), RegistryValueKind.String);
            }
            return flag;
        }

        [SecuritySafeCritical]
        public static void ShowAbout(ProductKind kind)
        {
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                MessageBox.Show("You're using the trial version. For information on buying DevExpress products, please visit us at www.devexpress.com.", "Info", MessageBoxButton.OK);
            }
            else
            {
                AppDomain domain = AppDomain.CreateDomain("About window domain");
                domain.SetData("ProductKind", kind);
                domain.DoCallBack(<>c.<>9__14_0 ??= delegate {
                    ProductKind productKind = (ProductKind) AppDomain.CurrentDomain.GetData("ProductKind");
                    aboutShown = true;
                    AboutInfoBase base2 = new AboutInfo();
                    Application application = new Application {
                        ShutdownMode = ShutdownMode.OnMainWindowClose
                    };
                    AboutWindow window = new AboutWindow();
                    AboutInfo info = new AboutInfo();
                    ProductStringInfo productInfo = GetProductInfo(productKind);
                    info.ProductPlatform = productInfo.ProductPlatform;
                    info.ProductName = productInfo.ProductName;
                    UserData data = Utility.GetInfo();
                    if ((data == null) || (!data.IsValid || !data.IsLicensed(productKind)))
                    {
                        info.LicenseState = !Utility.IsExpired() ? LicenseState.Trial : LicenseState.TrialExpired;
                    }
                    else
                    {
                        info.LicenseState = LicenseState.Licensed;
                        info.RegistrationCode = Utility.GetSerial(productKind, ProductInfoStage.Registered);
                    }
                    info.Version = "v2019 vol 2";
                    window.Content = new ControlAbout(info);
                    application.MainWindow = window;
                    application.MainWindow.Show();
                    application.Run();
                });
            }
        }

        public static void ShowAbout(bool isExp)
        {
            if (ShouldShowAbout())
            {
                ShowTrial(isExp);
            }
        }

        public static void ShowAbout(Type type)
        {
            DXLicense license = LicenseManager.Validate(type, 0) as DXLicense;
            ProductKind defaultProductKind = GetDefaultProductKind();
            if (license != null)
            {
                defaultProductKind = license.Kind;
            }
            object[] args = new object[] { defaultProductKind };
            Dispatcher.CurrentDispatcher.BeginInvoke(new Action<ProductKind>(About.ShowAbout), args);
        }

        public static void ShowTrial(bool isExp)
        {
            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                AppDomain domain = AppDomain.CreateDomain("About window domain");
                CrossAppDomainDelegate callBackDelegate = <>c.<>9__9_0 ??= delegate {
                    aboutShown = true;
                    AboutInfoBase base2 = new AboutInfo();
                    Application application = new Application {
                        ShutdownMode = ShutdownMode.OnMainWindowClose
                    };
                    AboutWindow window = new AboutWindow();
                    AboutInfo info = new AboutInfo();
                    UserData data = Utility.GetInfo();
                    info.ProductName = "DevExpress WPF Subscription";
                    info.LicenseState = LicenseState.Trial;
                    info.Version = "v2019 vol 2";
                    window.Content = new ControlAbout(info);
                    application.MainWindow = window;
                    application.MainWindow.Show();
                    application.Run();
                };
                if (isExp && Utility.IsExpired())
                {
                    callBackDelegate = <>c.<>9__9_1 ??= delegate {
                        aboutShown = true;
                        AboutInfoBase base2 = new AboutInfo();
                        Application application = new Application {
                            ShutdownMode = ShutdownMode.OnMainWindowClose
                        };
                        AboutWindow window = new AboutWindow();
                        AboutInfo info = new AboutInfo();
                        UserData data = Utility.GetInfo();
                        info.ProductName = "DevExpress WPF Subscription";
                        info.LicenseState = LicenseState.TrialExpired;
                        info.Version = "v2019 vol 2";
                        window.Content = new ControlAbout(info);
                        application.MainWindow = window;
                        application.MainWindow.Show();
                        application.Run();
                    };
                }
                try
                {
                    domain.DoCallBack(callBackDelegate);
                }
                catch (Exception)
                {
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly About.<>c <>9 = new About.<>c();
            public static CrossAppDomainDelegate <>9__9_0;
            public static CrossAppDomainDelegate <>9__9_1;
            public static CrossAppDomainDelegate <>9__14_0;

            internal void <ShowAbout>b__14_0()
            {
                ProductKind productKind = (ProductKind) AppDomain.CurrentDomain.GetData("ProductKind");
                About.aboutShown = true;
                AboutInfoBase base2 = new AboutInfo();
                Application application = new Application {
                    ShutdownMode = ShutdownMode.OnMainWindowClose
                };
                AboutWindow window = new AboutWindow();
                AboutInfo info = new AboutInfo();
                ProductStringInfo productInfo = About.GetProductInfo(productKind);
                info.ProductPlatform = productInfo.ProductPlatform;
                info.ProductName = productInfo.ProductName;
                UserData data = Utility.GetInfo();
                if ((data == null) || (!data.IsValid || !data.IsLicensed(productKind)))
                {
                    info.LicenseState = !Utility.IsExpired() ? LicenseState.Trial : LicenseState.TrialExpired;
                }
                else
                {
                    info.LicenseState = LicenseState.Licensed;
                    info.RegistrationCode = Utility.GetSerial(productKind, ProductInfoStage.Registered);
                }
                info.Version = "v2019 vol 2";
                window.Content = new ControlAbout(info);
                application.MainWindow = window;
                application.MainWindow.Show();
                application.Run();
            }

            internal void <ShowTrial>b__9_0()
            {
                About.aboutShown = true;
                AboutInfoBase base2 = new AboutInfo();
                Application application = new Application {
                    ShutdownMode = ShutdownMode.OnMainWindowClose
                };
                AboutWindow window = new AboutWindow();
                AboutInfo info = new AboutInfo();
                UserData data = Utility.GetInfo();
                info.ProductName = "DevExpress WPF Subscription";
                info.LicenseState = LicenseState.Trial;
                info.Version = "v2019 vol 2";
                window.Content = new ControlAbout(info);
                application.MainWindow = window;
                application.MainWindow.Show();
                application.Run();
            }

            internal void <ShowTrial>b__9_1()
            {
                About.aboutShown = true;
                AboutInfoBase base2 = new AboutInfo();
                Application application = new Application {
                    ShutdownMode = ShutdownMode.OnMainWindowClose
                };
                AboutWindow window = new AboutWindow();
                AboutInfo info = new AboutInfo();
                UserData data = Utility.GetInfo();
                info.ProductName = "DevExpress WPF Subscription";
                info.LicenseState = LicenseState.TrialExpired;
                info.Version = "v2019 vol 2";
                window.Content = new ControlAbout(info);
                application.MainWindow = window;
                application.MainWindow.Show();
                application.Run();
            }
        }

        private delegate void AboutInvoker(bool isExp);
    }
}

