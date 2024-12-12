namespace DevExpress.Xpf.Core
{
    using DevExpress.Data.Utils;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public static class ApplicationThemeHelper
    {
        private static string applicationThemeName = null;
        private static bool coreAssemblyLoaded = false;
        private static object coreObbjectInitializer = null;
        public static bool UseLegacyDefaultTheme = false;

        internal static event EventHandler<ApplicationThemeNameChangedEventArgs> ApplicationThemeNameChanged;

        static ApplicationThemeHelper()
        {
            UseDefaultSvgImages = true;
        }

        private static string GetDefaultThemeName()
        {
            Assembly xpfCoreAssembly = GetXpfCoreAssembly();
            if (xpfCoreAssembly == null)
            {
                return string.Empty;
            }
            Type type = xpfCoreAssembly.GetType("DevExpress.Xpf.Core.Theme", false, false);
            if (type == null)
            {
                return string.Empty;
            }
            FieldInfo field = type.GetField("Default", BindingFlags.Public | BindingFlags.Static);
            if (field == null)
            {
                return string.Empty;
            }
            object obj2 = field.GetValue(null);
            return ((obj2 != null) ? (obj2.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Instance).GetValue(obj2, new object[0]) as string) : string.Empty);
        }

        internal static Assembly GetXpfCoreAssembly()
        {
            Func<Assembly, bool> predicate = <>c.<>9__25_0;
            if (<>c.<>9__25_0 == null)
            {
                Func<Assembly, bool> local1 = <>c.<>9__25_0;
                predicate = <>c.<>9__25_0 = assembly => assembly.FullName == "DevExpress.Xpf.Core.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a";
            }
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault<Assembly>(predicate);
            if (assembly == null)
            {
                assembly = Assembly.Load("DevExpress.Xpf.Core.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a");
            }
            return assembly;
        }

        private static bool IsWebApp()
        {
            string appDomainManagerType = AppDomain.CurrentDomain.SetupInformation.AppDomainManagerType;
            if (appDomainManagerType != null)
            {
                return appDomainManagerType.ToLower().StartsWith("system.web");
            }
            string local1 = appDomainManagerType;
            return false;
        }

        public static void SaveApplicationThemeName()
        {
            try
            {
                if (!string.IsNullOrEmpty(ApplicationThemeName))
                {
                    bool useNetCoreVersionFormat = false;
                    ConfigurationHelper.SaveApplicationThemeNameToConfig(Config, ApplicationThemeName, useNetCoreVersionFormat);
                }
            }
            catch
            {
            }
        }

        public static void UpdateApplicationThemeName()
        {
            if ((!UseLegacyDefaultTheme && (string.IsNullOrEmpty(ApplicationThemeName) && (!IsWebApp() || (ApplicationThemeName == GetDefaultThemeName())))) && !DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                bool useNetCoreVersionFormat = false;
                string applicationThemeNameFromConfig = ConfigurationHelper.GetApplicationThemeNameFromConfig(Config, useNetCoreVersionFormat);
                ApplicationThemeName = string.IsNullOrEmpty(applicationThemeNameFromConfig) ? GetDefaultThemeName() : applicationThemeNameFromConfig;
            }
        }

        public static bool UseDefaultSvgImages { get; set; }

        private static System.Configuration.Configuration Config =>
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel);

        public static System.Configuration.ConfigurationUserLevel ConfigurationUserLevel { get; set; }

        [Description("Gets or sets the name of the theme applied to the entire application.")]
        public static string ApplicationThemeName
        {
            get => 
                applicationThemeName;
            set
            {
                applicationThemeName = value;
                if (!coreAssemblyLoaded)
                {
                    Assembly xpfCoreAssembly = GetXpfCoreAssembly();
                    if (xpfCoreAssembly != null)
                    {
                        if (FrameworkVersions.IsNetCore3AndAbove())
                        {
                            Type type = xpfCoreAssembly.GetType("DevExpress.Xpf.Utils.NetCore3ModuleInitializerData", false, false);
                            if (type != null)
                            {
                                coreObbjectInitializer = Activator.CreateInstance(type);
                            }
                        }
                        else
                        {
                            Type type = xpfCoreAssembly.GetType("DevExpress.Xpf.Utils.ModuleInitializerData", false, false);
                            if (type != null)
                            {
                                coreObbjectInitializer = Activator.CreateInstance(type);
                            }
                        }
                        coreAssemblyLoaded = true;
                    }
                }
                if (ApplicationThemeNameChanged != null)
                {
                    ApplicationThemeNameChanged(null, new ApplicationThemeNameChangedEventArgs(value));
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ApplicationThemeHelper.<>c <>9 = new ApplicationThemeHelper.<>c();
            public static Func<Assembly, bool> <>9__25_0;

            internal bool <GetXpfCoreAssembly>b__25_0(Assembly assembly) => 
                assembly.FullName == "DevExpress.Xpf.Core.v19.2, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a";
        }
    }
}

