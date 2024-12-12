namespace DevExpress.Xpf.Docking.Platform.Win32
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Xml.Linq;

    internal static class DpiHelper
    {
        private static Matrix _transformToDevice;
        private static bool isPerMonitorDpiAware;
        private static readonly Dpi SystemDpi = GetSystemDpi();

        [SecuritySafeCritical]
        static DpiHelper()
        {
            IntPtr dC = NativeMethods.GetDC(IntPtr.Zero);
            int deviceCaps = NativeMethods.GetDeviceCaps(dC, 0x58);
            int num2 = NativeMethods.GetDeviceCaps(dC, 90);
            _transformToDevice = Matrix.Identity;
            _transformToDevice.Scale(((double) deviceCaps) / 96.0, ((double) num2) / 96.0);
            isPerMonitorDpiAware = (IsProcessPerMonitorDpiAware && EnvironmentHelper.IsRedstoneOneOrNewer) && IsScalingSupported();
        }

        private static bool? GetDoNotScaleForDpiChanges()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null)
            {
                return null;
            }
            string exePath = Uri.UnescapeDataString(new UriBuilder(entryAssembly.CodeBase).Path);
            try
            {
                return GetDoNotScaleForDpiChanges(ConfigurationManager.OpenExeConfiguration(exePath));
            }
            catch
            {
                return null;
            }
        }

        private static bool? GetDoNotScaleForDpiChanges(System.Configuration.Configuration config)
        {
            ConfigurationSection section = config.GetSection("runtime");
            if (section != null)
            {
                bool flag;
                SectionInformation sectionInformation = section.SectionInformation;
                if (sectionInformation == null)
                {
                    return null;
                }
                string rawXml = sectionInformation.GetRawXml();
                if (rawXml == null)
                {
                    return null;
                }
                XDocument document = XDocument.Parse(rawXml);
                if (document == null)
                {
                    return null;
                }
                IEnumerable<XElement> source = document.Descendants();
                if (source == null)
                {
                    return null;
                }
                Func<XElement, bool> predicate = <>c.<>9__11_0;
                if (<>c.<>9__11_0 == null)
                {
                    Func<XElement, bool> local1 = <>c.<>9__11_0;
                    predicate = <>c.<>9__11_0 = x => x.Name.LocalName.Equals("AppContextSwitchOverrides", StringComparison.OrdinalIgnoreCase);
                }
                XElement element = source.SingleOrDefault<XElement>(predicate);
                if (element == null)
                {
                    return null;
                }
                IEnumerable<XAttribute> enumerable2 = element.Attributes();
                if (enumerable2 == null)
                {
                    return null;
                }
                Func<XAttribute, bool> func2 = <>c.<>9__11_1;
                if (<>c.<>9__11_1 == null)
                {
                    Func<XAttribute, bool> local2 = <>c.<>9__11_1;
                    func2 = <>c.<>9__11_1 = x => x.Name.LocalName.Equals("value", StringComparison.OrdinalIgnoreCase);
                }
                char[] separator = new char[] { '=' };
                Func<string, string> selector = <>c.<>9__11_2;
                if (<>c.<>9__11_2 == null)
                {
                    Func<string, string> local3 = <>c.<>9__11_2;
                    selector = <>c.<>9__11_2 = x => x.Trim();
                }
                string[] strArray = enumerable2.SingleOrDefault<XAttribute>(func2).Value.Split(separator).Select<string, string>(selector).ToArray<string>();
                if (strArray.Length != 2)
                {
                    return null;
                }
                if (!strArray[0].Equals("Switch.System.Windows.DoNotScaleForDpiChanges", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }
                if (bool.TryParse(strArray[1], out flag))
                {
                    return new bool?(flag);
                }
            }
            return null;
        }

        [SecuritySafeCritical]
        private static Dpi GetDpi(IntPtr handleMonitor)
        {
            if (handleMonitor == IntPtr.Zero)
            {
                return SystemDpi;
            }
            int dpiX = 1;
            int dpiY = 1;
            return ((NativeHelper.GetDpiForMonitor(handleMonitor, NativeHelper.MONITOR_DPI_TYPE.MDT_Effective_DPI, ref dpiX, ref dpiY) == 0) ? new Dpi(dpiX, dpiY) : SystemDpi);
        }

        [SecuritySafeCritical]
        private static NativeHelper.PROCESS_DPI_AWARENESS? GetDpiAwareness()
        {
            NativeHelper.PROCESS_DPI_AWARENESS process_dpi_awareness;
            if (EnvironmentHelper.IsEightOneOrNewer && (NativeHelper.GetProcessDpiAwareness(IntPtr.Zero, out process_dpi_awareness) == 0))
            {
                return new NativeHelper.PROCESS_DPI_AWARENESS?(process_dpi_awareness);
            }
            return null;
        }

        public static double GetDpiFactor(Visual container, Visual window)
        {
            Dpi dpiFromVisual = GetDpiFromVisual(container);
            Dpi dpi2 = GetDpiFromVisual(window);
            return (((double) dpiFromVisual.X) / ((double) dpi2.X));
        }

        [SecuritySafeCritical]
        public static Dpi GetDpiFromVisual(Visual sourceVisual)
        {
            if (sourceVisual == null)
            {
                throw new ArgumentNullException("sourceVisual");
            }
            if (!EnvironmentHelper.IsEightOneOrNewer)
            {
                return SystemDpi;
            }
            HwndSource source = PresentationSource.FromVisual(sourceVisual) as HwndSource;
            return ((source != null) ? GetDpi(NativeHelper.MonitorFromWindow(source.Handle, 2)) : SystemDpi);
        }

        [SecuritySafeCritical]
        private static Dpi GetSystemDpi()
        {
            Dpi dpi;
            IntPtr zero = IntPtr.Zero;
            try
            {
                zero = NativeMethods.GetDC(IntPtr.Zero);
                dpi = !(zero == IntPtr.Zero) ? new Dpi(NativeMethods.GetDeviceCaps(zero, 0x58), NativeMethods.GetDeviceCaps(zero, 90)) : Dpi.Default;
            }
            finally
            {
                if (zero != IntPtr.Zero)
                {
                    NativeMethods.ReleaseDC(IntPtr.Zero, zero);
                }
            }
            return dpi;
        }

        [SecuritySafeCritical]
        private static Dpi GetSystemDpi(Visual sourceVisual)
        {
            if (sourceVisual == null)
            {
                throw new ArgumentNullException("sourceVisual");
            }
            PresentationSource source = PresentationSource.FromVisual(sourceVisual);
            return (((source == null) || (source.CompositionTarget == null)) ? Dpi.Default : new Dpi((int) (Dpi.Default.X * source.CompositionTarget.TransformToDevice.M11), (int) (Dpi.Default.Y * source.CompositionTarget.TransformToDevice.M22)));
        }

        private static bool IsScalingSupported()
        {
            bool? doNotScaleForDpiChanges = GetDoNotScaleForDpiChanges();
            if (doNotScaleForDpiChanges != null)
            {
                return !doNotScaleForDpiChanges.Value;
            }
            Version frameworkVersion = TargetFramework.GetFrameworkVersion();
            return ((frameworkVersion != null) ? (new Version(4, 6, 2) <= frameworkVersion) : true);
        }

        public static Point LogicalPixelsToDevice(Point logicalPoint) => 
            _transformToDevice.Transform(logicalPoint);

        public static bool IsPerMonitorDpiAware =>
            isPerMonitorDpiAware;

        private static bool IsProcessPerMonitorDpiAware
        {
            get
            {
                NativeHelper.PROCESS_DPI_AWARENESS? dpiAwareness = GetDpiAwareness();
                NativeHelper.PROCESS_DPI_AWARENESS process_dpi_awareness = NativeHelper.PROCESS_DPI_AWARENESS.Process_Per_Monitor_DPI_Aware;
                return ((((NativeHelper.PROCESS_DPI_AWARENESS) dpiAwareness.GetValueOrDefault()) == process_dpi_awareness) ? (dpiAwareness != null) : false);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DpiHelper.<>c <>9 = new DpiHelper.<>c();
            public static Func<XElement, bool> <>9__11_0;
            public static Func<XAttribute, bool> <>9__11_1;
            public static Func<string, string> <>9__11_2;

            internal bool <GetDoNotScaleForDpiChanges>b__11_0(XElement x) => 
                x.Name.LocalName.Equals("AppContextSwitchOverrides", StringComparison.OrdinalIgnoreCase);

            internal bool <GetDoNotScaleForDpiChanges>b__11_1(XAttribute x) => 
                x.Name.LocalName.Equals("value", StringComparison.OrdinalIgnoreCase);

            internal string <GetDoNotScaleForDpiChanges>b__11_2(string x) => 
                x.Trim();
        }
    }
}

