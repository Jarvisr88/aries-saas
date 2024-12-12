namespace DevExpress.Xpf.Docking
{
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class DockLayoutManagerThemeDependentValuesProvider : FrameworkElement
    {
        public static readonly DependencyProperty Win32ResourcePathProperty;
        public static readonly DependencyProperty IsDarkThemeProperty;

        static DockLayoutManagerThemeDependentValuesProvider()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DockLayoutManagerThemeDependentValuesProvider), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DockLayoutManagerThemeDependentValuesProvider> registrator1 = DevExpress.Mvvm.UI.Native.DependencyPropertyRegistrator<DockLayoutManagerThemeDependentValuesProvider>.New().OverrideDefaultStyleKey().Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DockLayoutManagerThemeDependentValuesProvider, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DockLayoutManagerThemeDependentValuesProvider.get_IsDarkTheme)), parameters), out IsDarkThemeProperty, false, (d, oldValue, newValue) => d.OnIsDarkThemeChanged(oldValue, newValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DockLayoutManagerThemeDependentValuesProvider), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator1.Register<Uri>(System.Linq.Expressions.Expression.Lambda<Func<DockLayoutManagerThemeDependentValuesProvider, Uri>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DockLayoutManagerThemeDependentValuesProvider.get_Win32ResourcePath)), expressionArray2), out Win32ResourcePathProperty, null, (d, oldValue, newValue) => d.OnWin32ResourcePathChanged(oldValue, newValue), frameworkOptions);
        }

        public DockLayoutManagerThemeDependentValuesProvider(DockLayoutManager manager)
        {
            this.Manager = manager;
        }

        protected virtual void OnIsDarkThemeChanged(bool oldValue, bool newValue)
        {
            this.Manager.IsDarkTheme = newValue;
        }

        protected virtual void OnWin32ResourcePathChanged(Uri oldValue, Uri newValue)
        {
            this.Manager.InvokeUpdateFloatingPaneResources();
        }

        public bool IsDarkTheme
        {
            get => 
                (bool) base.GetValue(IsDarkThemeProperty);
            set => 
                base.SetValue(IsDarkThemeProperty, value);
        }

        public Uri Win32ResourcePath
        {
            get => 
                (Uri) base.GetValue(Win32ResourcePathProperty);
            set => 
                base.SetValue(Win32ResourcePathProperty, value);
        }

        protected DockLayoutManager Manager { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DockLayoutManagerThemeDependentValuesProvider.<>c <>9 = new DockLayoutManagerThemeDependentValuesProvider.<>c();

            internal void <.cctor>b__3_0(DockLayoutManagerThemeDependentValuesProvider d, bool oldValue, bool newValue)
            {
                d.OnIsDarkThemeChanged(oldValue, newValue);
            }

            internal void <.cctor>b__3_1(DockLayoutManagerThemeDependentValuesProvider d, Uri oldValue, Uri newValue)
            {
                d.OnWin32ResourcePathChanged(oldValue, newValue);
            }
        }
    }
}

