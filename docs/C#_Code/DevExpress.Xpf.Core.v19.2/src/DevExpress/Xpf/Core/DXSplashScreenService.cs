namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    [TargetType(typeof(UserControl)), TargetType(typeof(Window))]
    public class DXSplashScreenService : ViewServiceBase, ISplashScreenService, DXSplashScreen.ISplashScreenStateAware
    {
        public static readonly DependencyProperty SplashScreenTypeProperty;
        public static readonly DependencyProperty SplashScreenWindowStyleProperty;
        public static readonly DependencyProperty SplashScreenStartupLocationProperty;
        public static readonly DependencyProperty ShowSplashScreenOnLoadingProperty;
        public static readonly DependencyProperty ProgressProperty;
        public static readonly DependencyProperty MaxProgressProperty;
        public static readonly DependencyProperty StateProperty;
        public static readonly DependencyProperty SplashScreenOwnerProperty;
        public static readonly DependencyProperty SplashScreenClosingModeProperty;
        public static readonly DependencyProperty OwnerSearchModeProperty;
        public static readonly DependencyProperty FadeInDurationProperty;
        public static readonly DependencyProperty FadeOutDurationProperty;
        public static readonly DependencyProperty UseIndependentWindowProperty;
        public static readonly DependencyProperty IsSplashScreenActiveProperty;
        private static readonly DependencyPropertyKey IsSplashScreenActivePropertyKey;
        private bool SplashScreenIsShownOnLoading;
        private bool isSplashScreenShown;
        private DXSplashScreen.SplashScreenContainer splashContainer;

        static DXSplashScreenService()
        {
            ParameterExpression expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            FrameworkPropertyMetadataOptions? frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator1 = DependencyPropertyRegistrator<DXSplashScreenService>.New().Register<Type>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, Type>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_SplashScreenType)), parameters), out SplashScreenTypeProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator2 = registrator1.Register<Style>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, Style>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_SplashScreenWindowStyle)), expressionArray2), out SplashScreenWindowStyleProperty, null, (d, e) => d.OnSplashScreenWindowStyleChanged((Style) e.OldValue, (Style) e.NewValue), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator3 = registrator2.Register<WindowStartupLocation>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, WindowStartupLocation>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_SplashScreenStartupLocation)), expressionArray3), out SplashScreenStartupLocationProperty, WindowStartupLocation.CenterScreen, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator4 = registrator3.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_ShowSplashScreenOnLoading)), expressionArray4), out ShowSplashScreenOnLoadingProperty, false, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray5 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator5 = registrator4.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_Progress)), expressionArray5), out ProgressProperty, 0.0, d => d.OnProgressChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray6 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator6 = registrator5.Register<double>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, double>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_MaxProgress)), expressionArray6), out MaxProgressProperty, 100.0, d => d.OnMaxProgressChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray7 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator7 = registrator6.Register<object>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, object>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_State)), expressionArray7), out StateProperty, "Loading...", d => d.OnStateChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray8 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator8 = registrator7.Register<FrameworkElement>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, FrameworkElement>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_SplashScreenOwner)), expressionArray8), out SplashScreenOwnerProperty, null, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray9 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator9 = registrator8.Register<DevExpress.Xpf.Core.SplashScreenClosingMode>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, DevExpress.Xpf.Core.SplashScreenClosingMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_SplashScreenClosingMode)), expressionArray9), out SplashScreenClosingModeProperty, DevExpress.Xpf.Core.SplashScreenClosingMode.Default, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray10 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator10 = registrator9.Register<SplashScreenOwnerSearchMode>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, SplashScreenOwnerSearchMode>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_OwnerSearchMode)), expressionArray10), out OwnerSearchModeProperty, SplashScreenOwnerSearchMode.Full, frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray11 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator11 = registrator10.Register<TimeSpan>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, TimeSpan>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_FadeInDuration)), expressionArray11), out FadeInDurationProperty, TimeSpan.FromSeconds(0.2), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray12 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator12 = registrator11.Register<TimeSpan>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, TimeSpan>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_FadeOutDuration)), expressionArray12), out FadeOutDurationProperty, TimeSpan.FromSeconds(0.2), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray13 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            DependencyPropertyRegistrator<DXSplashScreenService> registrator13 = registrator12.Register<bool>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_UseIndependentWindow)), expressionArray13), out UseIndependentWindowProperty, false, d => d.OnUseIndependentWindowChanged(), frameworkOptions);
            expression = System.Linq.Expressions.Expression.Parameter(typeof(DXSplashScreenService), "d");
            ParameterExpression[] expressionArray14 = new ParameterExpression[] { expression };
            frameworkOptions = null;
            registrator13.RegisterReadOnly<bool>(System.Linq.Expressions.Expression.Lambda<Func<DXSplashScreenService, bool>>(System.Linq.Expressions.Expression.Property(expression, (MethodInfo) methodof(DXSplashScreenService.get_IsSplashScreenActive)), expressionArray14), out IsSplashScreenActivePropertyKey, out IsSplashScreenActiveProperty, false, frameworkOptions);
        }

        public DXSplashScreenService()
        {
            this.OnUseIndependentWindowChanged();
        }

        private static object CreateSplashScreen(object parameter)
        {
            object[] objArray = (object[]) parameter;
            string documentType = objArray[0] as string;
            IViewLocator viewLocator = objArray[1] as IViewLocator;
            DataTemplate viewTemplate = objArray[2] as DataTemplate;
            SplashScreenViewModel model = SplashScreenHelper.FindParameter<SplashScreenViewModel>(parameter, null);
            return ViewHelper.CreateAndInitializeView(viewLocator, documentType, (model == null) ? new SplashScreenViewModel() : model.Clone(), null, null, viewTemplate, null);
        }

        private SplashScreenViewModel CreateSplashScreenViewModel()
        {
            SplashScreenViewModel model1 = new SplashScreenViewModel();
            model1.State = this.State;
            SplashScreenViewModel model = model1;
            if (Math.Abs((double) (this.Progress - 0.0)) > 0.0001)
            {
                model.Progress = this.Progress;
            }
            if (Math.Abs((double) (this.MaxProgress - 100.0)) > 0.0001)
            {
                model.MaxProgress = this.MaxProgress;
            }
            return model;
        }

        private static Window CreateSplashScreenWindow(object parameter)
        {
            Type type = SplashScreenHelper.FindParameter<Type>(parameter, null);
            Style style = SplashScreenHelper.FindParameter<Style>(parameter, null);
            IList<TimeSpan> source = SplashScreenHelper.FindParameters<TimeSpan>(parameter);
            Window d = (type == null) ? ((style == null) ? DXSplashScreen.DefaultSplashScreenWindowCreator(parameter) : new DXSplashScreen.SplashScreenWindow()) : ((Window) Activator.CreateInstance(type));
            d.WindowStartupLocation = SplashScreenHelper.FindParameter<WindowStartupLocation>(parameter, WindowStartupLocation.CenterScreen);
            if (style != null)
            {
                d.Style = style;
            }
            Func<TimeSpan, bool> predicate = <>c.<>9__89_0;
            if (<>c.<>9__89_0 == null)
            {
                Func<TimeSpan, bool> local1 = <>c.<>9__89_0;
                predicate = <>c.<>9__89_0 = x => x.TotalMilliseconds > 0.0;
            }
            if (source.Any<TimeSpan>(predicate))
            {
                Func<Behavior, bool> func2 = <>c.<>9__89_1;
                if (<>c.<>9__89_1 == null)
                {
                    Func<Behavior, bool> local2 = <>c.<>9__89_1;
                    func2 = <>c.<>9__89_1 = x => x is WindowFadeAnimationBehavior;
                }
                if (!Interaction.GetBehaviors(d).Any<Behavior>(func2))
                {
                    WindowFadeAnimationBehavior behavior1 = new WindowFadeAnimationBehavior();
                    behavior1.FadeInDuration = source[0];
                    behavior1.FadeOutDuration = source[1];
                    Interaction.GetBehaviors(d).Add(behavior1);
                }
            }
            return d;
        }

        void ISplashScreenService.HideSplashScreen()
        {
            if (!this.IsSplashScreenActive)
            {
                this.isSplashScreenShown = false;
            }
            else if (!this.UseIndependentWindow || !this.splashContainer.IsActive)
            {
                if (DXSplashScreen.IsActive)
                {
                    DXSplashScreen.Close();
                }
                this.isSplashScreenShown = false;
            }
            else
            {
                DXSplashScreen.SplashScreenContainer splashContainer = this.GetSplashContainer(true);
                splashContainer.Closed -= new EventHandler(this.OnSplashScreenClosed);
                if (splashContainer.IsActive)
                {
                    splashContainer.Close();
                }
                this.isSplashScreenShown = false;
                this.IsSplashScreenActive = false;
            }
        }

        void ISplashScreenService.SetSplashScreenProgress(double progress, double maxProgress)
        {
            if (this.IsSplashScreenActive)
            {
                if (this.UseIndependentWindow)
                {
                    this.GetSplashContainer(false).Progress(progress, maxProgress);
                }
                else
                {
                    DXSplashScreen.Progress(progress, maxProgress);
                }
            }
        }

        void ISplashScreenService.SetSplashScreenState(object state)
        {
            if (this.IsSplashScreenActive)
            {
                if (this.UseIndependentWindow)
                {
                    this.GetSplashContainer(false).SetState(state);
                }
                else
                {
                    DXSplashScreen.SetState(state);
                }
            }
        }

        void ISplashScreenService.ShowSplashScreen(string documentType)
        {
            if ((this.SplashScreenType != null) && (!string.IsNullOrEmpty(documentType) || ((base.ViewTemplate != null) || (base.ViewLocator != null))))
            {
                throw new InvalidOperationException("Cannot use ViewLocator, ViewTemplate and DocumentType if SplashScreenType is set. If you set the SplashScreenType property, do not set the other properties.");
            }
            if (!this.IsSplashScreenActive)
            {
                Func<object, object> splashScreenCreator = null;
                object splashScreenCreatorParameter = null;
                SplashScreenViewModel model = this.CreateSplashScreenViewModel();
                List<object> list1 = new List<object>();
                list1.Add(this.SplashScreenWindowStyle);
                list1.Add(this.SplashScreenStartupLocation);
                List<object> list2 = list1;
                Func<DependencyObject, DevExpress.Xpf.Core.SplashScreenOwner> evaluator = <>c.<>9__83_0;
                if (<>c.<>9__83_0 == null)
                {
                    Func<DependencyObject, DevExpress.Xpf.Core.SplashScreenOwner> local1 = <>c.<>9__83_0;
                    evaluator = <>c.<>9__83_0 = x => new DevExpress.Xpf.Core.SplashScreenOwner(x);
                }
                list1.Add(this.Owner.With<DependencyObject, DevExpress.Xpf.Core.SplashScreenOwner>(evaluator));
                List<object> local2 = list1;
                local2.Add(this.SplashScreenClosingMode);
                local2.Add(this.FadeInDuration);
                local2.Add(this.FadeOutDuration);
                IList<object> source = local2;
                if (this.SplashScreenType == null)
                {
                    splashScreenCreator = new Func<object, object>(DXSplashScreenService.CreateSplashScreen);
                    object[] objArray1 = new object[] { documentType, base.ViewLocator, base.ViewTemplate, model };
                    splashScreenCreatorParameter = objArray1;
                }
                else
                {
                    DXSplashScreen.CheckSplashScreenType(this.SplashScreenType);
                    if (typeof(Window).IsAssignableFrom(this.SplashScreenType))
                    {
                        source.Add(this.SplashScreenType);
                    }
                    else if (typeof(FrameworkElement).IsAssignableFrom(this.SplashScreenType))
                    {
                        splashScreenCreator = DXSplashScreen.DefaultSplashScreenContentCreator;
                        object[] objArray2 = new object[] { this.SplashScreenType, model };
                        splashScreenCreatorParameter = objArray2;
                    }
                }
                this.isSplashScreenShown = true;
                if (!this.UseIndependentWindow)
                {
                    DXSplashScreen.Show(new Func<object, Window>(DXSplashScreenService.CreateSplashScreenWindow), splashScreenCreator, source.ToArray<object>(), splashScreenCreatorParameter);
                    this.isSplashScreenShown = DXSplashScreen.IsActive;
                }
                else
                {
                    DXSplashScreen.SplashScreenContainer splashContainer = this.GetSplashContainer(true);
                    splashContainer.Closed += new EventHandler(this.OnSplashScreenClosed);
                    splashContainer.Show(new Func<object, Window>(DXSplashScreenService.CreateSplashScreenWindow), splashScreenCreator, source.ToArray<object>(), splashScreenCreatorParameter, null);
                    this.IsSplashScreenActive = true;
                }
            }
        }

        void DXSplashScreen.ISplashScreenStateAware.OnIsActiveChanged(bool newValue)
        {
            this.UpdateIsSplashScreenActive(new bool?(newValue));
        }

        internal DXSplashScreen.SplashScreenContainer GetSplashContainer(bool ensureInstance)
        {
            if (ReferenceEquals(this.splashContainer, null) & ensureInstance)
            {
                this.splashContainer = new DXSplashScreen.SplashScreenContainer();
            }
            return this.splashContainer;
        }

        private void HideSplashScreenOnAssociatedObjectLoaded()
        {
            if (this.SplashScreenIsShownOnLoading)
            {
                base.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnAssociatedObjectLoaded);
                this.SplashScreenIsShownOnLoading = false;
                ((ISplashScreenService) this).HideSplashScreen();
            }
        }

        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            this.HideSplashScreenOnAssociatedObjectLoaded();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.ShowSplashScreenOnLoading && (!base.AssociatedObject.IsLoaded && !this.IsSplashScreenActive))
            {
                base.AssociatedObject.Loaded += new RoutedEventHandler(this.OnAssociatedObjectLoaded);
                this.SplashScreenIsShownOnLoading = true;
                this.ShowSplashScreen();
            }
        }

        protected override void OnDetaching()
        {
            this.HideSplashScreenOnAssociatedObjectLoaded();
            base.OnDetaching();
        }

        private void OnMaxProgressChanged()
        {
            if (this.isSplashScreenShown)
            {
                ((ISplashScreenService) this).SetSplashScreenProgress(this.Progress, this.MaxProgress);
            }
        }

        private void OnProgressChanged()
        {
            if (this.isSplashScreenShown)
            {
                ((ISplashScreenService) this).SetSplashScreenProgress(this.Progress, this.MaxProgress);
            }
        }

        private void OnSplashScreenClosed(object sender, EventArgs e)
        {
            DXSplashScreen.SplashScreenContainer container;
            ((DXSplashScreen.SplashScreenContainer) sender).Closed = container.Closed - new EventHandler(this.OnSplashScreenClosed);
            this.isSplashScreenShown = false;
            bool? newIsActive = null;
            this.UpdateIsSplashScreenActive(newIsActive);
        }

        private void OnSplashScreenWindowStyleChanged(Style oldValue, Style newValue)
        {
            if (newValue != null)
            {
                newValue.Seal();
            }
        }

        private void OnStateChanged()
        {
            if (this.isSplashScreenShown)
            {
                ((ISplashScreenService) this).SetSplashScreenState(this.State);
            }
        }

        private void OnUseIndependentWindowChanged()
        {
            if (this.isSplashScreenShown)
            {
                throw new InvalidOperationException("The property value cannot be changed while the DXSplashScreenService is active.");
            }
            if (!this.UseIndependentWindow)
            {
                DXSplashScreen.WeakSplashScreenStateAwareContainer.Default.Register(this);
            }
            else
            {
                DXSplashScreen.WeakSplashScreenStateAwareContainer.Default.Unregister(this);
            }
            bool? newIsActive = null;
            this.UpdateIsSplashScreenActive(newIsActive);
        }

        protected override void OnViewTemplateChanged(DataTemplate oldValue, DataTemplate newValue)
        {
            base.OnViewTemplateChanged(oldValue, newValue);
            if (newValue != null)
            {
                newValue.Seal();
            }
        }

        protected override void OnViewTemplateSelectorChanged(DataTemplateSelector oldValue, DataTemplateSelector newValue)
        {
            throw new InvalidOperationException("ViewTemplateSelector is not supported by DXSplashScreenService");
        }

        private void UpdateIsSplashScreenActive(bool? newIsActive)
        {
            if (base.Dispatcher != null)
            {
                if (!base.Dispatcher.CheckAccess())
                {
                    base.Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action<bool?>(this.UpdateIsSplashScreenActive), newIsActive);
                }
                else
                {
                    bool flag = (newIsActive != null) ? newIsActive.Value : (this.UseIndependentWindow ? ((this.splashContainer != null) && this.splashContainer.IsActive) : DXSplashScreen.IsActive);
                    this.IsSplashScreenActive = flag;
                    if (!flag)
                    {
                        this.isSplashScreenShown = false;
                    }
                }
            }
        }

        public Type SplashScreenType
        {
            get => 
                (Type) base.GetValue(SplashScreenTypeProperty);
            set => 
                base.SetValue(SplashScreenTypeProperty, value);
        }

        public Style SplashScreenWindowStyle
        {
            get => 
                (Style) base.GetValue(SplashScreenWindowStyleProperty);
            set => 
                base.SetValue(SplashScreenWindowStyleProperty, value);
        }

        public WindowStartupLocation SplashScreenStartupLocation
        {
            get => 
                (WindowStartupLocation) base.GetValue(SplashScreenStartupLocationProperty);
            set => 
                base.SetValue(SplashScreenStartupLocationProperty, value);
        }

        public bool ShowSplashScreenOnLoading
        {
            get => 
                (bool) base.GetValue(ShowSplashScreenOnLoadingProperty);
            set => 
                base.SetValue(ShowSplashScreenOnLoadingProperty, value);
        }

        public double Progress
        {
            get => 
                (double) base.GetValue(ProgressProperty);
            set => 
                base.SetValue(ProgressProperty, value);
        }

        public double MaxProgress
        {
            get => 
                (double) base.GetValue(MaxProgressProperty);
            set => 
                base.SetValue(MaxProgressProperty, value);
        }

        public object State
        {
            get => 
                base.GetValue(StateProperty);
            set => 
                base.SetValue(StateProperty, value);
        }

        public FrameworkElement SplashScreenOwner
        {
            get => 
                (FrameworkElement) base.GetValue(SplashScreenOwnerProperty);
            set => 
                base.SetValue(SplashScreenOwnerProperty, value);
        }

        public DevExpress.Xpf.Core.SplashScreenClosingMode SplashScreenClosingMode
        {
            get => 
                (DevExpress.Xpf.Core.SplashScreenClosingMode) base.GetValue(SplashScreenClosingModeProperty);
            set => 
                base.SetValue(SplashScreenClosingModeProperty, value);
        }

        public SplashScreenOwnerSearchMode OwnerSearchMode
        {
            get => 
                (SplashScreenOwnerSearchMode) base.GetValue(OwnerSearchModeProperty);
            set => 
                base.SetValue(OwnerSearchModeProperty, value);
        }

        public TimeSpan FadeInDuration
        {
            get => 
                (TimeSpan) base.GetValue(FadeInDurationProperty);
            set => 
                base.SetValue(FadeInDurationProperty, value);
        }

        public TimeSpan FadeOutDuration
        {
            get => 
                (TimeSpan) base.GetValue(FadeOutDurationProperty);
            set => 
                base.SetValue(FadeOutDurationProperty, value);
        }

        public bool UseIndependentWindow
        {
            get => 
                (bool) base.GetValue(UseIndependentWindowProperty);
            set => 
                base.SetValue(UseIndependentWindowProperty, value);
        }

        public bool IsSplashScreenActive
        {
            get => 
                (bool) base.GetValue(IsSplashScreenActiveProperty);
            private set => 
                base.SetValue(IsSplashScreenActivePropertyKey, value);
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public DataTemplateSelector ViewTemplateSelector
        {
            get => 
                null;
            set => 
                this.OnViewTemplateSelectorChanged(null, null);
        }

        private DependencyObject Owner =>
            (this.SplashScreenOwner == null) ? ((this.OwnerSearchMode != SplashScreenOwnerSearchMode.Full) ? ((this.OwnerSearchMode != SplashScreenOwnerSearchMode.IgnoreAssociatedObject) ? null : SplashScreenHelper.GetApplicationActiveWindow(true)) : (base.AssociatedObject ?? SplashScreenHelper.GetApplicationActiveWindow(true))) : this.SplashScreenOwner;

        bool ISplashScreenService.IsSplashScreenActive =>
            this.IsSplashScreenActive;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXSplashScreenService.<>c <>9 = new DXSplashScreenService.<>c();
            public static Func<DependencyObject, SplashScreenOwner> <>9__83_0;
            public static Func<TimeSpan, bool> <>9__89_0;
            public static Func<Behavior, bool> <>9__89_1;

            internal void <.cctor>b__15_0(DXSplashScreenService d, DependencyPropertyChangedEventArgs e)
            {
                d.OnSplashScreenWindowStyleChanged((Style) e.OldValue, (Style) e.NewValue);
            }

            internal void <.cctor>b__15_1(DXSplashScreenService d)
            {
                d.OnProgressChanged();
            }

            internal void <.cctor>b__15_2(DXSplashScreenService d)
            {
                d.OnMaxProgressChanged();
            }

            internal void <.cctor>b__15_3(DXSplashScreenService d)
            {
                d.OnStateChanged();
            }

            internal void <.cctor>b__15_4(DXSplashScreenService d)
            {
                d.OnUseIndependentWindowChanged();
            }

            internal bool <CreateSplashScreenWindow>b__89_0(TimeSpan x) => 
                x.TotalMilliseconds > 0.0;

            internal bool <CreateSplashScreenWindow>b__89_1(Behavior x) => 
                x is WindowFadeAnimationBehavior;

            internal SplashScreenOwner <DevExpress.Mvvm.ISplashScreenService.ShowSplashScreen>b__83_0(DependencyObject x) => 
                new SplashScreenOwner(x);
        }
    }
}

