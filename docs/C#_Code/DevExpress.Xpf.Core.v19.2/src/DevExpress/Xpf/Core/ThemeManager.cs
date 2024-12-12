namespace DevExpress.Xpf.Core
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;

    [LicenseProvider(typeof(DX_WPFEditors_LicenseProvider))]
    public class ThemeManager : DependencyObject, IThemeManager
    {
        public const string TraceSwitchName = "ThemeManagerTracing";
        public const string TouchDelimiter = ";";
        public const string TouchDefinition = "touch";
        public const string TouchDelimiterAndDefinition = ";touch";
        internal const string MethodNameString = "MethodName";
        internal const string ThemeNameTraceString = "ThemeName";
        internal const string AssemblyNameTraceString = "AssemblyName";
        internal const string KeyTraceString = "Key";
        internal const string ObjectTraceString = "Object";
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ThemeProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ThemeNameProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty TreeWalkerProperty;
        public static readonly DependencyProperty IsTouchEnabledProperty;
        private static readonly DependencyPropertyKey IsTouchEnabledPropertyKey;
        public static readonly DependencyProperty IsTouchlineThemeProperty;
        private static readonly DependencyPropertyKey IsTouchlineThemePropertyKey;
        public static readonly RoutedEvent ThemeChangedEvent;
        public static readonly RoutedEvent ThemeChangingEvent;
        private static readonly object TreeWalkerLocker = new object();
        private static bool ignoreManifest = false;
        private static ThemeManager instance;
        private static double defaultTouchPaddingScale = 2.0;
        private static bool enableDefaultThemeLoadingCore = false;
        private static Dictionary<Assembly, bool> IsDXAssembly = new Dictionary<Assembly, bool>();
        private static readonly object LockObject = new object();

        public static event ThemeChangedRoutedEventHandler ApplicationThemeChanged;

        public static event ThemeChangingRoutedEventHandler ApplicationThemeChanging;

        public static event ThemeChangedRoutedEventHandler ThemeChanged;

        public static event ThemeChangingRoutedEventHandler ThemeChanging;

        [SecuritySafeCritical]
        static ThemeManager()
        {
            Type ownerType = typeof(ThemeManager);
            ThemeNameProperty = DependencyProperty.RegisterAttached("ThemeName", typeof(string), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ThemeManager.ThemeNamePropertyChanged)));
            ThemeProperty = DependencyProperty.RegisterAttached("Theme", typeof(Theme), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ThemeManager.ThemePropertyChanged)));
            DependencyProperty foregroundProperty = TextBlock.ForegroundProperty;
            foregroundProperty = FloatingContainerControl.ContentOffsetProperty;
            IsTouchEnabledPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsTouchEnabled", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
            IsTouchEnabledProperty = IsTouchEnabledPropertyKey.DependencyProperty;
            IsTouchlineThemePropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsTouchlineTheme", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));
            IsTouchlineThemeProperty = IsTouchlineThemePropertyKey.DependencyProperty;
            TreeWalkerProperty = DependencyProperty.RegisterAttached("TreeWalker", typeof(ThemeTreeWalker), ownerType, new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.AffectsMeasure, new PropertyChangedCallback(ThemeManager.TreeWalkerChanged)));
            ThemeChangedEvent = EventManager.RegisterRoutedEvent("ThemeChanged", RoutingStrategy.Direct, typeof(ThemeChangedRoutedEventHandler), ownerType);
            ThemeChangingEvent = EventManager.RegisterRoutedEvent("ThemeChanging", RoutingStrategy.Direct, typeof(ThemeChangingRoutedEventHandler), ownerType);
            EventManager.RegisterClassHandler(typeof(DataGrid), FrameworkElement.LoadedEvent, new RoutedEventHandler(ThemeManager.OnDataGridLoaded));
            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(ThemeManager.CurrentDomain_AssemblyLoad);
            }
        }

        protected ThemeManager()
        {
        }

        public static void AddThemeChangedHandler(DependencyObject d, ThemeChangedRoutedEventHandler handler)
        {
            DependencyObjectWrapper.AddHandler(d, ThemeChangedEvent, handler);
        }

        public static void AddThemeChangingHandler(DependencyObject d, ThemeChangingRoutedEventHandler handler)
        {
            DependencyObjectWrapper.AddHandler(d, ThemeChangingEvent, handler);
        }

        private static void AfterApplicationThemeChanged(ThemeChangedRoutedEventArgs eventArgs)
        {
            if (ApplicationThemeChanged != null)
            {
                ApplicationThemeChanged(null, eventArgs);
            }
        }

        private static void AfterThemeNameChanged(DependencyObject obj, string themeName)
        {
            if (ThemeChanged != null)
            {
                ThemeChangedRoutedEventArgs args1 = new ThemeChangedRoutedEventArgs(themeName);
                args1.RoutedEvent = ThemeChangedEvent;
                ThemeChangedRoutedEventArgs e = args1;
                ThemeChanged(obj, e);
            }
        }

        private static void ApplyTheme(string themeName)
        {
            if (!string.IsNullOrEmpty(themeName) && PredefinedThemePalettes.StandardPalettes.Any<PredefinedThemePalette>(x => themeName.StartsWith(x.Name)))
            {
                Theme.RegisterPredefinedPaletteThemes();
            }
            BeforeApplicationThemeChanged(new ThemeChangingRoutedEventArgs(themeName));
            GlobalThemeHelper.Instance.ApplicationThemeName = themeName;
            AfterApplicationThemeChanged(new ThemeChangedRoutedEventArgs(themeName));
        }

        private static void BeforeApplicationThemeChanged(ThemeChangingRoutedEventArgs eventArgs)
        {
            if (ApplicationThemeChanging != null)
            {
                ApplicationThemeChanging(null, eventArgs);
            }
        }

        private static void BeforeThemeNameChanged(DependencyObject obj, string themeName)
        {
            if (ThemeChanging != null)
            {
                ThemeChangingRoutedEventArgs args1 = new ThemeChangingRoutedEventArgs(themeName);
                args1.RoutedEvent = ThemeChangingEvent;
                ThemeChangingRoutedEventArgs e = args1;
                ThemeChanging(obj, e);
            }
        }

        internal static void ChangeTheme(DependencyObject obj, string themeName, bool isTouch, bool isTouchLineTheme, string oldThemeName)
        {
            object lockObject = LockObject;
            lock (lockObject)
            {
                ForceThemeKeyCreating(themeName);
                SetTreeWalker(obj, new ThemeTreeWalker(themeName, isTouch, obj));
                SetIsTouchEnabled(obj, isTouch);
                SetIsTouchlineTheme(obj, isTouchLineTheme);
            }
        }

        private static void ClearTreeWalker(DependencyObject obj)
        {
            SetTreeWalker(obj, new ThemeTreeWalker("", false, obj));
            obj.ClearValue(TreeWalkerProperty);
            Window window = obj as Window;
            if (window != null)
            {
                object[] args = new object[] { window };
                window.Dispatcher.Invoke(new Action<Window>(GlobalThemeHelper.Instance.AssignApplicationThemeName), args);
            }
        }

        [SecuritySafeCritical]
        private static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            if (!args.LoadedAssembly.ReflectionOnly)
            {
                if (AssemblyHelper.IsDXProductAssembly(args.LoadedAssembly))
                {
                    EnableResource(AssemblyHelper.GetShortNameWithoutVersion(args.LoadedAssembly));
                }
                if (ThemedElementsDictionary.IsCustomThemeAssembly(args.LoadedAssembly))
                {
                    ThemedElementsDictionary.ForceThemeKeysLoadingForAssembly(Theme.Default.Name, args.LoadedAssembly.FullName);
                }
            }
        }

        void IThemeManager.ClearThemeName(DependencyObject d)
        {
            d.ClearValue(ThemeNameProperty);
        }

        void IThemeManager.SetThemeName(DependencyObject d, string value)
        {
            d.SetCurrentValue(ThemeNameProperty, value);
        }

        private static bool EnableResource(string assemblyName)
        {
            ThemePartKeyExtension key = new ThemePartKeyExtension();
            key.AssemblyName = assemblyName;
            return ThemePartResourceDictionary.EnableSource(key);
        }

        private static void EnableResourcesForLoadedAssemblies()
        {
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                foreach (string str in ApplicationHelper.GetAvailableAssemblies())
                {
                    if (AssemblyHelper.IsDXProductAssembly(str))
                    {
                        EnableResource(AssemblyHelper.GetShortNameWithoutVersion(str));
                    }
                }
            }
            else
            {
                foreach (Assembly assembly in AssemblyHelper.GetLoadedAssemblies())
                {
                    if (AssemblyHelper.IsDXProductAssembly(assembly))
                    {
                        EnableResource(AssemblyHelper.GetShortNameWithoutVersion(assembly));
                    }
                }
            }
        }

        private static void ExcludeFromTheming(DependencyObject obj)
        {
            SetTreeWalker(obj, null);
        }

        private static void ForceThemeKeyCreating(string themeName)
        {
            ThemedElementsDictionary.ForceThemeKeysLoading(themeName);
            EnableResourcesForLoadedAssemblies();
        }

        public static bool GetIsTouchEnabled(DependencyObject d) => 
            (bool) d.GetValue(IsTouchEnabledProperty);

        public static bool GetIsTouchlineTheme(DependencyObject d) => 
            (bool) d.GetValue(IsTouchlineThemeProperty);

        public static Theme GetTheme(DependencyObject obj) => 
            (Theme) obj.GetValue(ThemeProperty);

        [TypeConverter(typeof(ThemeNameTypeConverter))]
        public static string GetThemeName(DependencyObject obj) => 
            (string) obj.GetValue(ThemeNameProperty);

        private static object GetThemeOrDefaultKey(string themeName, DependencyObject obj, DependencyObjectWrapper wrapper)
        {
            if (wrapper.OverridesDefaultStyleKey)
            {
                return null;
            }
            string typeNameFromKey = GetTypeNameFromKey(wrapper);
            if (typeNameFromKey == null)
            {
                return null;
            }
            object cachedResourceKey = ThemedElementsDictionary.GetCachedResourceKey(themeName, typeNameFromKey);
            if (cachedResourceKey != null)
            {
                return cachedResourceKey;
            }
            object obj3 = ThemedElementsDictionary.GetCachedResourceKey(string.Empty, typeNameFromKey);
            return ((obj3 == null) ? ThemedElementsDictionary.GetCachedResourceKey(string.Empty, obj.GetType()) : obj3);
        }

        public static ThemeTreeWalker GetTreeWalker(DependencyObject obj) => 
            (ThemeTreeWalker) obj.GetValue(TreeWalkerProperty);

        private static string GetTypeName(DefaultStyleThemeKeyExtension key) => 
            key?.FullName;

        private static string GetTypeName(Type key) => 
            key?.FullName;

        private static string GetTypeNameFromKey(DependencyObjectWrapper wrapper)
        {
            object defaultStyleKey = wrapper.GetDefaultStyleKey();
            string typeName = GetTypeName(defaultStyleKey as Type);
            return ((typeName == null) ? GetTypeName(defaultStyleKey as DefaultStyleThemeKeyExtension) : typeName);
        }

        internal static void Initialize()
        {
            SubscribeApplicationThemeNameChanged();
            if (ApplicationThemeHelper.ApplicationThemeName != null)
            {
                ApplyTheme(ApplicationThemeHelper.ApplicationThemeName);
            }
            else if (!ApplicationThemeHelper.UseLegacyDefaultTheme && !new DependencyObject().IsInDesignTool())
            {
                ApplicationThemeHelper.UpdateApplicationThemeName();
            }
        }

        private static bool IsDXDefaultStyleThemable(Assembly assembly)
        {
            bool flag;
            if (!IsDXAssembly.TryGetValue(assembly, out flag))
            {
                flag = assembly.FullName.StartsWith("DevExpress", StringComparison.InvariantCultureIgnoreCase);
                IsDXAssembly[assembly] = flag;
            }
            return flag;
        }

        private static void OnDataGridLoaded(object sender, RoutedEventArgs e)
        {
            ExcludeFromTheming(sender as DependencyObject);
        }

        private static void RaiseThemeNameChanged(DependencyObjectWrapper wrapper, string themeName)
        {
            ThemeChangedRoutedEventArgs args1 = new ThemeChangedRoutedEventArgs(themeName);
            args1.RoutedEvent = ThemeChangedEvent;
            ThemeChangedRoutedEventArgs e = args1;
            wrapper.RaiseEvent(e);
        }

        private static void RaiseThemeNameChanging(DependencyObjectWrapper wrapper, string themeName)
        {
            ThemeChangingRoutedEventArgs args1 = new ThemeChangingRoutedEventArgs(themeName);
            args1.RoutedEvent = ThemeChangingEvent;
            ThemeChangingRoutedEventArgs e = args1;
            wrapper.RaiseEvent(e);
        }

        public static void RemoveThemeChangedHandler(DependencyObject d, ThemeChangedRoutedEventHandler handler)
        {
            DependencyObjectWrapper.RemoveHandler(d, ThemeChangedEvent, handler);
        }

        public static void RemoveThemeChangingHandler(DependencyObject d, ThemeChangingRoutedEventHandler handler)
        {
            DependencyObjectWrapper.RemoveHandler(d, ThemeChangingEvent, handler);
        }

        internal static void SetIsTouchEnabled(DependencyObject d, bool value)
        {
            d.SetValue(IsTouchEnabledPropertyKey, value);
        }

        internal static void SetIsTouchlineTheme(DependencyObject d, bool value)
        {
            d.SetValue(IsTouchlineThemePropertyKey, value);
        }

        public static void SetTheme(DependencyObject obj, Theme value)
        {
            obj.SetValue(ThemeProperty, value);
        }

        public static void SetThemeName(DependencyObject obj, string value)
        {
            obj.SetValue(ThemeNameProperty, value);
        }

        private static void SetTreeWalker(DependencyObject d, ThemeTreeWalker value)
        {
            d.SetValue(TreeWalkerProperty, value);
        }

        private static void SubscribeApplicationThemeNameChanged()
        {
            EventInfo info = typeof(ApplicationThemeHelper).GetEvent("ApplicationThemeNameChanged", BindingFlags.NonPublic | BindingFlags.Static);
            MethodInfo method = typeof(ThemeManager).GetMethod("ThemeManager_ApplicationThemeNameChanged", BindingFlags.NonPublic | BindingFlags.Static);
            Delegate delegate2 = Delegate.CreateDelegate(info.EventHandlerType, method);
            object[] parameters = new object[] { delegate2 };
            info.GetAddMethod(true).Invoke(null, parameters);
        }

        private static void ThemeManager_ApplicationThemeNameChanged(object sender, ApplicationThemeNameChangedEventArgs e)
        {
            ApplyTheme(e.ThemeName);
        }

        private static void ThemeNamePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            string oldValue = (string) e.OldValue;
            string newValue = (string) e.NewValue;
            string fromAlias = ThemeProperties.GetFromAlias(newValue, true);
            bool isTouch = ThemeProperties.IsTouch(newValue);
            bool isTouchLineTheme = ThemeProperties.IsTouchlineTheme(newValue);
            if (!string.IsNullOrEmpty(oldValue) || !string.IsNullOrEmpty(newValue))
            {
                BeforeThemeNameChanged(obj, fromAlias);
                if (string.IsNullOrEmpty(fromAlias))
                {
                    ClearTreeWalker(obj);
                    AfterThemeNameChanged(obj, fromAlias);
                }
                else
                {
                    if (fromAlias == "None")
                    {
                        fromAlias = string.Empty;
                    }
                    ChangeTheme(obj, fromAlias, isTouch, isTouchLineTheme, oldValue);
                    AfterThemeNameChanged(obj, fromAlias);
                }
            }
        }

        private static void ThemePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Theme newValue = e.NewValue as Theme;
            SetThemeName(obj, newValue?.Name);
        }

        private static void TreeWalkerChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ThemeTreeWalker newValue = e.NewValue as ThemeTreeWalker;
            if (newValue != null)
            {
                DependencyObjectWrapper wrapper = new DependencyObjectWrapper(obj);
                ThemeTreeWalker oldValue = e.OldValue as ThemeTreeWalker;
                string str = (oldValue != null) ? oldValue.ThemeName : string.Empty;
                string themeName = (newValue != null) ? newValue.ThemeName : string.Empty;
                if (string.IsNullOrEmpty(str))
                {
                    Type defaultStyleKey = wrapper.GetDefaultStyleKey() as Type;
                    if (defaultStyleKey != null)
                    {
                        object treeWalkerLocker = TreeWalkerLocker;
                        lock (treeWalkerLocker)
                        {
                            if (IsDXDefaultStyleThemable(defaultStyleKey.Assembly))
                            {
                                ThemedElementsDictionary.RegisterThemeTypeCore(string.Empty, GetTypeName(defaultStyleKey), defaultStyleKey);
                            }
                            else
                            {
                                ThemedElementsDictionary.RegisterThemeTypeCore(string.Empty, defaultStyleKey, defaultStyleKey);
                            }
                        }
                    }
                }
                UpdateDefaultStyleKey(obj, themeName, wrapper);
                wrapper.Clear();
            }
        }

        private static void UpdateDefaultStyleKey(DependencyObject obj, string themeName, DependencyObjectWrapper wrapper)
        {
            object obj2 = GetThemeOrDefaultKey(themeName, obj, wrapper);
            if (obj2 != null)
            {
                RaiseThemeNameChanging(wrapper, themeName);
                wrapper.SetDefaultStyleKey(obj2);
                RaiseThemeNameChanged(wrapper, themeName);
            }
        }

        public static bool? EnableDPICorrection
        {
            get => 
                new bool?(CompatibilitySettings.EnableDPICorrection);
            set
            {
                bool? nullable = value;
                CompatibilitySettings.EnableDPICorrection = (nullable != null) ? nullable.GetValueOrDefault() : false;
            }
        }

        public static double DefaultTouchPaddingScale
        {
            get => 
                defaultTouchPaddingScale;
            set => 
                defaultTouchPaddingScale = Math.Max(0.1, value);
        }

        internal static ThemeManager Instance
        {
            get
            {
                instance ??= new ThemeManager();
                return instance;
            }
        }

        [Obsolete("Use the DevExpress.Xpf.Core.ApplicationThemeHelper.ApplicationThemeName property instead."), TypeConverter(typeof(ThemeNameTypeConverter)), Description("Gets or sets the name of the theme applied to the entire application.")]
        public static string ApplicationThemeName
        {
            get => 
                GlobalThemeHelper.Instance.ApplicationThemeName;
            set => 
                ApplicationThemeHelper.ApplicationThemeName = value;
        }

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code.")]
        public static bool EnableDefaultThemeLoading
        {
            get => 
                enableDefaultThemeLoadingCore;
            set => 
                enableDefaultThemeLoadingCore = value;
        }

        [Browsable(false)]
        public static bool IgnoreManifest
        {
            get => 
                ignoreManifest;
            set => 
                ignoreManifest = value;
        }

        [Browsable(false)]
        public static string ActualApplicationThemeName =>
            ApplicationThemeHelper.ApplicationThemeName;
    }
}

