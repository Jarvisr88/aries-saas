namespace DevExpress.Utils.About
{
    using DevExpress.Logify;
    using Microsoft.Win32;
    using System;
    using System.Runtime.CompilerServices;
    using System.Security;

    public abstract class UAlgo
    {
        private static byte? status = null;
        private static bool? enabled = null;
        private static byte? defaultPlatform = 0;
        private static UAlgoPathInfo pathInfo;
        public const byte pUnknown = 0;
        public const byte pWinForms = 1;
        public const byte pWeb = 2;
        public const byte pWPF = 3;
        public const byte pReporting = 8;
        public const byte pSilverlight = 4;
        public const byte pWinRT = 5;
        public const byte pXAF = 6;
        public const byte pCodeRush = 10;
        public const byte pDataAccess = 14;
        public const byte pXPO = 15;
        public const byte kDemo = 1;
        public const byte kDemoMain = 2;
        public const byte kDemoModule = 3;
        public const byte kDemoMainSearch = 4;
        public const byte kDesignTime = 10;
        public const byte kDesignTimeFrame = 11;
        public const byte kDesignTimeWizard = 12;
        public const byte kDesignTimeTemplate = 13;
        public const byte kInstantLayoutAssistant = 14;
        public const byte kStack = 20;
        public const byte kExceptionDemo = 30;
        public const byte kExceptionVisualStudio = 0x1f;
        public const byte kInstall = 90;
        public const byte kUnInstall = 0x5b;
        public const byte kExceptionPaintDesign = 0x20;
        public const byte kExceptionPaint = 0x21;
        public const byte kProject = 40;
        public const byte kCustomAction = 110;
        [ThreadStatic]
        private static UAlgo _default;

        protected UAlgo()
        {
        }

        public static void AddDesktopTraceListener(string apiKey, string applicationName)
        {
            if (Status == null)
            {
                DemoMode();
            }
            if (LogifyTraceListenerEnabled)
            {
                LogifyExceptionHandler.Instance.AddDesktopTraceListener(apiKey, applicationName);
            }
        }

        public static void AddWebFormsTraceListener(string apiKey, string applicationName)
        {
            if (Status == null)
            {
                DemoMode();
            }
            LogifyExceptionHandler.SetWebEnvironment(true);
            if (LogifyTraceListenerEnabled)
            {
                LogifyExceptionHandler.Instance.AddWebFormsTraceListener(apiKey, applicationName);
            }
        }

        public static void ASPDesigner()
        {
            status = 10;
            defaultPlatform = 2;
            bool enabled = Enabled;
        }

        [SecuritySafeCritical]
        private static bool CheckEnabled()
        {
            bool flag;
            try
            {
                if (!CheckValidRegistry())
                {
                    flag = false;
                }
                else if (AlgoProvider.Id == null)
                {
                    flag = false;
                }
                else
                {
                    ShouldUseLogify();
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UAlgo.CurrentDomain_UnhandledException);
                    flag = true;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        private static bool CheckValidRegistry()
        {
            RegistryKey key = GetKey(false);
            if (key == null)
            {
                return false;
            }
            object obj2 = key.GetValue("CustomerExperienceProgram");
            return ((obj2 != null) ? !string.IsNullOrEmpty(obj2.ToString()) : false);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Default.DoEventException(e);
        }

        public static void DemoMode()
        {
            status = 1;
        }

        public static void DesignMode()
        {
            status = 10;
        }

        public abstract void DoCustomEvent(byte platform, string json);
        public abstract void DoEvent(byte kind, string action);
        public abstract void DoEvent(byte kind, Type action);
        public abstract void DoEvent(byte kind, byte platform, string action);
        public abstract void DoEvent(byte kind, byte platform, Type action);
        public abstract void DoEventException(Exception e);
        public abstract void DoEventException(UnhandledExceptionEventArgs e);
        public abstract void DoEventInstall(byte kind);
        public abstract void DoEventObject(byte kind, object instance);
        public abstract void DoEventObject(byte kind, byte platform, object instance);
        public abstract void DoEventProject(string json);
        public abstract void DoEventTemplate(byte platform, object instance, object[] customParams);
        public abstract void DoStackEvent(byte platform);
        protected static RegistryKey GetKey(bool writable) => 
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\DevExpress", writable);

        public static void Project()
        {
            if (status == null)
            {
                status = 40;
            }
        }

        private static bool ShouldUseLogify()
        {
            try
            {
                Guid? id = AlgoProvider.Id;
                string logId = (id != null) ? id.ToString() : "dxinternal";
                return LogifyExceptionHandler.Instance.Initialize(logId, PathInfo.LastExceptionReportFileName);
            }
            catch
            {
                return false;
            }
        }

        public static void Web()
        {
            defaultPlatform = 2;
        }

        public static void WinForms()
        {
            defaultPlatform = 1;
        }

        public static void WinFormsDesigner()
        {
            status = 10;
            defaultPlatform = 1;
            bool enabled = Enabled;
        }

        public static void Wpf()
        {
            defaultPlatform = 3;
        }

        internal static UAlgoPathInfo PathInfo
        {
            get
            {
                if (pathInfo == null)
                {
                    Type type = typeof(UAlgo);
                    lock (type)
                    {
                        pathInfo ??= new UAlgoPathInfo();
                    }
                }
                return pathInfo;
            }
        }

        public static bool Enabled
        {
            get
            {
                if (status == null)
                {
                    return false;
                }
                if (enabled == null)
                {
                    enabled = new bool?(CheckEnabled());
                }
                return enabled.Value;
            }
            set => 
                enabled = new bool?(value);
        }

        public static UAlgo Default
        {
            get
            {
                _default ??= (!ShouldUseLogify() ? new UAlgoDefault() : new UAlgoLogify());
                return _default;
            }
        }

        protected static byte? DefaultPlatform
        {
            get => 
                defaultPlatform;
            set => 
                defaultPlatform = value;
        }

        protected static byte? Status
        {
            get => 
                status;
            set => 
                status = value;
        }

        protected byte LastPlatform { get; set; }

        internal static int VersionId =>
            (VersionIdExt == null) ? 0x4b09 : VersionIdExt.Value;

        internal static string Version =>
            "19.2";

        public static int? VersionIdExt { get; set; }

        public static bool LogifyTraceListenerEnabled
        {
            get
            {
                UAlgoLogify logify = Default as UAlgoLogify;
                return (Enabled && (logify != null));
            }
        }
    }
}

