namespace DevExpress.Mvvm.UI.ModuleInjection
{
    using DevExpress.Mvvm.ModuleInjection.Native;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class UIWindowRegion : UIRegionBase, IUIWindowRegion, IUIRegion
    {
        public static readonly DependencyProperty WindowFactoryProperty;
        public static readonly DependencyProperty WindowStyleProperty;
        public static readonly DependencyProperty WindowStyleSelectorProperty;
        public static readonly DependencyProperty WindowShowModeProperty;
        public static readonly DependencyProperty WindowStartupLocationProperty;
        public static readonly DependencyProperty SetWindowOwnerProperty;
        public static readonly DependencyProperty IsMainWindowProperty;
        private readonly Dictionary<object, IWindowStrategy> strategies = new Dictionary<object, IWindowStrategy>();
        private MessageBoxResult? setResult;
        private MessageBoxResult? result;

        static UIWindowRegion()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(UIWindowRegion), "x");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<UIWindowRegion> registrator1 = DependencyPropertyRegistrator<UIWindowRegion>.New().Register<DataTemplate>(System.Linq.Expressions.Expression.Lambda<Func<UIWindowRegion, DataTemplate>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIWindowRegion.get_WindowFactory)), parameters), out WindowFactoryProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIWindowRegion), "x");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<UIWindowRegion> registrator2 = registrator1.Register<Style>(System.Linq.Expressions.Expression.Lambda<Func<UIWindowRegion, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIWindowRegion.get_WindowStyle)), expressionArray2), out WindowStyleProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIWindowRegion), "x");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<UIWindowRegion> registrator3 = registrator2.Register<StyleSelector>(System.Linq.Expressions.Expression.Lambda<Func<UIWindowRegion, StyleSelector>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIWindowRegion.get_WindowStyleSelector)), expressionArray3), out WindowStyleSelectorProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIWindowRegion), "x");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<UIWindowRegion> registrator4 = registrator3.Register<DevExpress.Mvvm.UI.WindowShowMode>(System.Linq.Expressions.Expression.Lambda<Func<UIWindowRegion, DevExpress.Mvvm.UI.WindowShowMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIWindowRegion.get_WindowShowMode)), expressionArray4), out WindowShowModeProperty, DevExpress.Mvvm.UI.WindowShowMode.Default, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIWindowRegion), "x");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<UIWindowRegion> registrator5 = registrator4.Register<System.Windows.WindowStartupLocation>(System.Linq.Expressions.Expression.Lambda<Func<UIWindowRegion, System.Windows.WindowStartupLocation>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIWindowRegion.get_WindowStartupLocation)), expressionArray5), out WindowStartupLocationProperty, System.Windows.WindowStartupLocation.CenterScreen, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIWindowRegion), "x");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<UIWindowRegion> registrator6 = registrator5.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<UIWindowRegion, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIWindowRegion.get_SetWindowOwner)), expressionArray6), out SetWindowOwnerProperty, true, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(UIWindowRegion), "x");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator6.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<UIWindowRegion, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(UIWindowRegion.get_IsMainWindow)), expressionArray7), out IsMainWindowProperty, false, frameworkOptions);
        }

        private void ClearStrategies()
        {
            foreach (IWindowStrategy strategy in this.strategies.Values)
            {
                strategy.Close();
            }
            this.strategies.Clear();
            this.result = null;
            this.setResult = null;
        }

        private IWindowStrategy CreateStrategy(object vm)
        {
            FrameworkElement target = this.CreateWindow(vm);
            IWindowStrategy strategy = base.ActualStrategyManager.CreateWindowStrategy(target);
            strategy.Initialize(new StrategyOwner(this, target));
            this.strategies.Add(vm, strategy);
            return strategy;
        }

        protected virtual FrameworkElement CreateWindow(object vm)
        {
            Window container = (this.WindowFactory == null) ? new DXWindow() : ((Window) this.WindowFactory.LoadContent());
            container.WindowStartupLocation = this.WindowStartupLocation;
            if (this.WindowStyle != null)
            {
                container.Style = this.WindowStyle;
            }
            if (this.WindowStyleSelector != null)
            {
                container.Style = this.WindowStyleSelector.SelectStyle(vm, container);
            }
            if (this.SetWindowOwner && (!this.IsMainWindow && (Application.Current != null)))
            {
                Func<Window, bool> predicate = <>c.<>9__34_0;
                if (<>c.<>9__34_0 == null)
                {
                    Func<Window, bool> local1 = <>c.<>9__34_0;
                    predicate = <>c.<>9__34_0 = x => x.IsActive;
                }
                Window local2 = Application.Current.Windows.OfType<Window>().FirstOrDefault<Window>(predicate);
                Window mainWindow = local2;
                if (local2 == null)
                {
                    Window local3 = local2;
                    mainWindow = Application.Current.MainWindow;
                }
                container.Owner = mainWindow;
            }
            if (!this.IsMainWindow && (base.AssociatedObject is FrameworkElement))
            {
                ViewServiceBase.UpdateThemeName(container, (FrameworkElement) base.AssociatedObject);
            }
            if (this.IsMainWindow && (Application.Current != null))
            {
                Application.Current.MainWindow = container;
            }
            return container;
        }

        void IUIWindowRegion.SetResult(MessageBoxResult result)
        {
            this.setResult = new MessageBoxResult?(result);
        }

        protected override void DoClear()
        {
            Action<IWindowStrategy> action = <>c.<>9__32_0;
            if (<>c.<>9__32_0 == null)
            {
                Action<IWindowStrategy> local1 = <>c.<>9__32_0;
                action = <>c.<>9__32_0 = x => x.Close();
            }
            this.DoForeachStrategy(action);
            this.ClearStrategies();
        }

        private void DoForeachStrategy(Action<IWindowStrategy> action)
        {
            this.strategies.Values.ToList<IWindowStrategy>().ForEach(action);
        }

        protected override void DoInject(object vm, Type viewType)
        {
            IWindowStrategy strategy = this.CreateStrategy(vm);
            if (this.WindowShowMode == DevExpress.Mvvm.UI.WindowShowMode.Default)
            {
                strategy.Show(vm, viewType);
            }
            else if (this.WindowShowMode == DevExpress.Mvvm.UI.WindowShowMode.Dialog)
            {
                strategy.ShowDialog(vm, viewType);
            }
        }

        protected override void DoUninject(object vm)
        {
            Action<IWindowStrategy> action = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Action<IWindowStrategy> local1 = <>c.<>9__31_0;
                action = <>c.<>9__31_0 = x => x.Close();
            }
            this.GetStrategy(vm).Do<IWindowStrategy>(action);
            this.RemoveStrategy(vm);
        }

        private IWindowStrategy GetStrategy(object vm) => 
            ((vm == null) || !this.strategies.ContainsKey(vm)) ? null : this.strategies[vm];

        protected override object GetView(object viewModel) => 
            this.GetStrategy(viewModel).With<IWindowStrategy, object>(x => x.GetView(viewModel));

        protected override void OnSelectedViewModelChanged(object oldValue, object newValue, bool focus)
        {
            Action<IWindowStrategy> action = <>c.<>9__33_0;
            if (<>c.<>9__33_0 == null)
            {
                Action<IWindowStrategy> local1 = <>c.<>9__33_0;
                action = <>c.<>9__33_0 = x => x.Activate();
            }
            this.GetStrategy(newValue).Do<IWindowStrategy>(action);
        }

        private void RemoveStrategy(object vm)
        {
            IWindowStrategy strategy = this.GetStrategy(vm);
            if (strategy != null)
            {
                this.result = strategy.Result;
                this.strategies.Remove(vm);
                if (base.SelectedViewModel == vm)
                {
                    base.SelectedViewModel = null;
                }
            }
        }

        public DataTemplate WindowFactory
        {
            get => 
                (DataTemplate) base.GetValue(WindowFactoryProperty);
            set => 
                base.SetValue(WindowFactoryProperty, value);
        }

        public Style WindowStyle
        {
            get => 
                (Style) base.GetValue(WindowStyleProperty);
            set => 
                base.SetValue(WindowStyleProperty, value);
        }

        public StyleSelector WindowStyleSelector
        {
            get => 
                (StyleSelector) base.GetValue(WindowStyleSelectorProperty);
            set => 
                base.SetValue(WindowStyleSelectorProperty, value);
        }

        public DevExpress.Mvvm.UI.WindowShowMode WindowShowMode
        {
            get => 
                (DevExpress.Mvvm.UI.WindowShowMode) base.GetValue(WindowShowModeProperty);
            set => 
                base.SetValue(WindowShowModeProperty, value);
        }

        public System.Windows.WindowStartupLocation WindowStartupLocation
        {
            get => 
                (System.Windows.WindowStartupLocation) base.GetValue(WindowStartupLocationProperty);
            set => 
                base.SetValue(WindowStartupLocationProperty, value);
        }

        public bool SetWindowOwner
        {
            get => 
                (bool) base.GetValue(SetWindowOwnerProperty);
            set => 
                base.SetValue(SetWindowOwnerProperty, value);
        }

        public bool IsMainWindow
        {
            get => 
                (bool) base.GetValue(IsMainWindowProperty);
            set => 
                base.SetValue(IsMainWindowProperty, value);
        }

        MessageBoxResult? IUIWindowRegion.Result
        {
            get
            {
                MessageBoxResult? setResult = this.setResult;
                return ((setResult != null) ? setResult : this.result);
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly UIWindowRegion.<>c <>9 = new UIWindowRegion.<>c();
            public static Action<IWindowStrategy> <>9__31_0;
            public static Action<IWindowStrategy> <>9__32_0;
            public static Action<IWindowStrategy> <>9__33_0;
            public static Func<Window, bool> <>9__34_0;

            internal bool <CreateWindow>b__34_0(Window x) => 
                x.IsActive;

            internal void <DoClear>b__32_0(IWindowStrategy x)
            {
                x.Close();
            }

            internal void <DoUninject>b__31_0(IWindowStrategy x)
            {
                x.Close();
            }

            internal void <OnSelectedViewModelChanged>b__33_0(IWindowStrategy x)
            {
                x.Activate();
            }
        }

        private class EnforceSaveLayoutOnThemeChangingBehavior : Behavior<FrameworkElement>
        {
            private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
            {
                this.SubscribeThemeChanging();
            }

            private void OnAssociatedObjectThemeChanging(DependencyObject sender, ThemeChangingRoutedEventArgs e)
            {
                UIRegionBase.EnforceSaveLayout(base.AssociatedObject.DataContext);
            }

            private void OnAssociatedObjectUnloaded(object sender, RoutedEventArgs e)
            {
                this.UnsubscribeThemeChanging();
            }

            protected override void OnAttached()
            {
                base.OnAttached();
                base.AssociatedObject.Loaded += new RoutedEventHandler(this.OnAssociatedObjectLoaded);
                base.AssociatedObject.Unloaded += new RoutedEventHandler(this.OnAssociatedObjectUnloaded);
                this.SubscribeThemeChanging();
            }

            protected override void OnDetaching()
            {
                this.UnsubscribeThemeChanging();
                base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectLoaded);
                base.AssociatedObject.Unloaded -= new RoutedEventHandler(this.OnAssociatedObjectUnloaded);
                base.OnDetaching();
            }

            private void SubscribeThemeChanging()
            {
                this.UnsubscribeThemeChanging();
                ThemeManager.AddThemeChangingHandler(base.AssociatedObject, new ThemeChangingRoutedEventHandler(this.OnAssociatedObjectThemeChanging));
            }

            private void UnsubscribeThemeChanging()
            {
                ThemeManager.RemoveThemeChangingHandler(base.AssociatedObject, new ThemeChangingRoutedEventHandler(this.OnAssociatedObjectThemeChanging));
            }
        }

        private class StrategyOwner : UIRegionBase.StrategyOwnerBase
        {
            public StrategyOwner(UIWindowRegion owner, FrameworkElement window) : base(owner, window)
            {
            }

            public override void ConfigureChild(DependencyObject child)
            {
                base.ConfigureChild(child);
                Interaction.GetBehaviors(child).Add(new UIWindowRegion.EnforceSaveLayoutOnThemeChangingBehavior());
            }

            public override void RemoveViewModel(object viewModel)
            {
                this.Owner.RemoveStrategy(viewModel);
                base.RemoveViewModel(viewModel);
            }

            public UIWindowRegion Owner =>
                (UIWindowRegion) base.Owner;
        }
    }
}

