namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.HandleDecorator;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Utils.Themes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Threading;

    [ContentProperty("LoadingChild")]
    public class LoadingDecorator : System.Windows.Controls.Decorator
    {
        private const string Exception1 = "LoadingDecorator shows its SplashScreen in a separate thread, so it is impossible to put a DependencyObject into the SplashScreenDataContext.";
        private const string LOADING_CHILD_SET_TWICE_EXCEPTION = "The LoadingChild and LoadingChildTemplate properties cannot be used simultaneously.";
        private const string LOADING_CHILD_WRONG_TEMPLATE_EXCEPTION = "The LoadingChild template must contain FrameworkElement as the visual root.";
        public static readonly DependencyProperty UseFadeEffectProperty = DependencyProperty.Register("UseFadeEffect", typeof(bool), typeof(LoadingDecorator), new PropertyMetadata(true));
        public static readonly DependencyProperty FadeInDurationProperty = DependencyProperty.Register("FadeInDuration", typeof(TimeSpan), typeof(LoadingDecorator), new PropertyMetadata(TimeSpan.FromSeconds(0.2)));
        public static readonly DependencyProperty FadeOutDurationProperty = DependencyProperty.Register("FadeOutDuration", typeof(TimeSpan), typeof(LoadingDecorator), new PropertyMetadata(TimeSpan.FromSeconds(0.2)));
        public static readonly DependencyProperty UseSplashScreenProperty = DependencyProperty.Register("UseSplashScreen", typeof(bool), typeof(LoadingDecorator), new PropertyMetadata(true));
        public static readonly DependencyProperty SplashScreenTemplateProperty;
        public static readonly DependencyProperty SplashScreenDataContextProperty;
        public static readonly DependencyProperty IsSplashScreenShownProperty;
        public static readonly DependencyProperty OwnerLockProperty;
        public static readonly DependencyProperty SplashScreenLocationProperty;
        public static readonly DependencyProperty SplashScreenWindowStyleProperty;
        public static readonly DependencyProperty LoadingChildTemplateProperty;
        public static readonly DependencyProperty BorderEffectProperty;
        public static readonly DependencyProperty BorderEffectColorProperty;
        private DXSplashScreen.SplashScreenContainer splashContainer;
        private FrameworkElement loadingChild;

        static LoadingDecorator()
        {
            SplashScreenTemplateProperty = DependencyProperty.Register("SplashScreenTemplate", typeof(DataTemplate), typeof(LoadingDecorator), new PropertyMetadata(null, (d, e) => ((LoadingDecorator) d).OnSplashScreenTemplateChanged()));
            SplashScreenDataContextProperty = DependencyProperty.Register("SplashScreenDataContext", typeof(object), typeof(LoadingDecorator), new PropertyMetadata(null));
            IsSplashScreenShownProperty = DependencyProperty.Register("IsSplashScreenShown", typeof(bool?), typeof(LoadingDecorator), new PropertyMetadata(null, (d, e) => ((LoadingDecorator) d).OnIsSplashScreenShownChanged()));
            OwnerLockProperty = DependencyProperty.Register("OwnerLock", typeof(SplashScreenLock), typeof(LoadingDecorator), new PropertyMetadata(SplashScreenLock.Full));
            SplashScreenLocationProperty = DependencyProperty.Register("SplashScreenLocation", typeof(DevExpress.Xpf.Core.SplashScreenLocation), typeof(LoadingDecorator), new PropertyMetadata(DevExpress.Xpf.Core.SplashScreenLocation.CenterContainer));
            SplashScreenWindowStyleProperty = DependencyProperty.Register("SplashScreenWindowStyle", typeof(Style), typeof(LoadingDecorator), new PropertyMetadata(null, (d, e) => ((LoadingDecorator) d).OnIsSplashScreenWindowStyleChanged()));
            LoadingChildTemplateProperty = DependencyProperty.Register("LoadingChildTemplate", typeof(DataTemplate), typeof(LoadingDecorator), new PropertyMetadata(null, (d, e) => ((LoadingDecorator) d).OnLoadingChildTemplateChanged()));
            BorderEffectProperty = DependencyProperty.Register("BorderEffect", typeof(DevExpress.Xpf.Core.BorderEffect), typeof(LoadingDecorator), new PropertyMetadata(DevExpress.Xpf.Core.BorderEffect.None));
            BorderEffectColorProperty = DependencyProperty.Register("BorderEffectColor", typeof(SolidColorBrush), typeof(LoadingDecorator), new PropertyMetadata(null));
        }

        public LoadingDecorator()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
        }

        internal virtual SplashScreenArrangeMode ArrangeMode() => 
            SplashScreenArrangeMode.Default;

        protected virtual void CloseSplashScreen()
        {
            if (this.IsActive)
            {
                this.SplashContainer.Close();
            }
        }

        private void CloseSplashScreenOnLoading()
        {
            if ((this.IsSplashScreenShown == null) && this.IsActive)
            {
                SplashScreenHelper.InvokeAsync(this, new Action(this.CloseSplashScreen), DispatcherPriority.Render, AsyncInvokeMode.AsyncOnly);
            }
        }

        private UIElement CreateFallbackView(string errorMessage)
        {
            TextBlock block1 = new TextBlock();
            block1.Text = errorMessage;
            block1.TextWrapping = TextWrapping.Wrap;
            block1.FontSize = 25.0;
            block1.Foreground = new SolidColorBrush(Colors.Red);
            block1.HorizontalAlignment = HorizontalAlignment.Center;
            block1.VerticalAlignment = VerticalAlignment.Center;
            return block1;
        }

        private static object CreateSplashScreen(object parameter)
        {
            object[] objArray = (object[]) parameter;
            DataTemplate template = (DataTemplate) objArray[0];
            object splashScreenDataContext = objArray[1];
            if (template != null)
            {
                DependencyObject obj2 = template.LoadContent();
                (obj2 as FrameworkElement).Do<FrameworkElement>(delegate (FrameworkElement x) {
                    x.DataContext = splashScreenDataContext;
                });
                return obj2;
            }
            WaitIndicator indicator1 = new WaitIndicator();
            indicator1.HorizontalAlignment = HorizontalAlignment.Center;
            indicator1.VerticalAlignment = VerticalAlignment.Center;
            indicator1.DeferedVisibility = true;
            indicator1.ShowShadow = false;
            Thickness thickness = new Thickness();
            indicator1.Margin = thickness;
            thickness = new Thickness();
            indicator1.ContentPadding = thickness;
            return indicator1;
        }

        private static Window CreateSplashScreenWindow(object parameter)
        {
            object[] objArray = (object[]) parameter;
            bool flag = (bool) objArray[0];
            WindowArrangerContainer parentContainer = (WindowArrangerContainer) objArray[1];
            SplashScreenLock lockMode = (SplashScreenLock) objArray[2];
            IList<TimeSpan> source = SplashScreenHelper.FindParameters<TimeSpan>(parameter);
            FlowDirection direction = SplashScreenHelper.FindParameter<FlowDirection>(parameter, FlowDirection.LeftToRight);
            Style style = SplashScreenHelper.FindParameter<Style>(parameter, null);
            string themeName = (string) objArray[3];
            DevExpress.Xpf.Core.BorderEffect borderEffect = (DevExpress.Xpf.Core.BorderEffect) objArray[4];
            Color? nullable = (Color?) objArray[5];
            LoadingDecoratorWindow d = new LoadingDecoratorWindow(parentContainer, lockMode, themeName, borderEffect, (nullable != null) ? new SolidColorBrush(nullable.Value) : null);
            if (style != null)
            {
                d.Style = style;
            }
            else
            {
                d.ApplyDefaultSettings();
            }
            d.SetCurrentValue(FrameworkElement.FlowDirectionProperty, direction);
            if (flag)
            {
                Func<TimeSpan, bool> predicate = <>c.<>9__84_0;
                if (<>c.<>9__84_0 == null)
                {
                    Func<TimeSpan, bool> local1 = <>c.<>9__84_0;
                    predicate = <>c.<>9__84_0 = x => x.TotalMilliseconds > 0.0;
                }
                if (source.Any<TimeSpan>(predicate))
                {
                    WindowFadeAnimationBehavior behavior1 = new WindowFadeAnimationBehavior();
                    behavior1.FadeInDuration = source[0];
                    behavior1.FadeOutDuration = source[1];
                    Interaction.GetBehaviors(d).Add(behavior1);
                }
            }
            return d;
        }

        private object[] GetSplashScreenCreatorParams()
        {
            string windowThemeName = ThemeHelper.GetWindowThemeName(this);
            if (string.IsNullOrEmpty(windowThemeName))
            {
                windowThemeName = ApplicationThemeHelper.ApplicationThemeName;
            }
            Func<SolidColorBrush, Color?> evaluator = <>c.<>9__82_0;
            if (<>c.<>9__82_0 == null)
            {
                Func<SolidColorBrush, Color?> local1 = <>c.<>9__82_0;
                evaluator = <>c.<>9__82_0 = x => new Color?(x.Color);
            }
            Color? nullable = this.BorderEffectColor.Return<SolidColorBrush, Color?>(evaluator, null);
            object[] objArray1 = new object[10];
            objArray1[0] = this.UseFadeEffect;
            WindowArrangerContainer container1 = new WindowArrangerContainer(this, this.SplashScreenLocation);
            container1.ArrangeMode = this.ArrangeMode();
            objArray1[1] = container1;
            objArray1[2] = this.OwnerLock;
            objArray1[3] = windowThemeName;
            objArray1[4] = this.BorderEffect;
            objArray1[5] = nullable;
            objArray1[6] = this.FadeInDuration;
            objArray1[7] = this.FadeOutDuration;
            objArray1[8] = base.FlowDirection;
            objArray1[9] = this.SplashScreenWindowStyle;
            return objArray1;
        }

        private void LoadChild()
        {
            if (this.LoadingChild != null)
            {
                this.LoadingChild.Loaded += new RoutedEventHandler(this.OnLoadingChildLoaded);
                this.Child = this.LoadingChild;
            }
            else if (this.LoadingChildTemplate != null)
            {
                FrameworkElement element = this.LoadingChildTemplate.LoadContent() as FrameworkElement;
                if (element == null)
                {
                    throw new InvalidOperationException("The LoadingChild template must contain FrameworkElement as the visual root.");
                }
                element.Loaded += new RoutedEventHandler(this.OnLoadingChildLoaded);
                this.Child = element;
            }
        }

        private void LoadChildInDesignTime()
        {
            if ((this.LoadingChild != null) && (this.LoadingChildTemplate != null))
            {
                this.Child = this.CreateFallbackView("The LoadingChild and LoadingChildTemplate properties cannot be used simultaneously.");
            }
            else if (this.LoadingChild != null)
            {
                this.Child = this.LoadingChild;
            }
            else if (this.LoadingChildTemplate != null)
            {
                FrameworkElement element = this.LoadingChildTemplate.LoadContent() as FrameworkElement;
                if (element != null)
                {
                    this.Child = element;
                }
                else
                {
                    this.Child = this.CreateFallbackView("The LoadingChild template must contain FrameworkElement as the visual root.");
                }
            }
        }

        private void OnIsSplashScreenShownChanged()
        {
            bool? isSplashScreenShown = this.IsSplashScreenShown;
            bool flag = true;
            if ((isSplashScreenShown.GetValueOrDefault() == flag) ? (isSplashScreenShown != null) : false)
            {
                if (base.IsLoaded)
                {
                    this.ShowSplashScreen();
                }
                else
                {
                    SplashScreenHelper.InvokeAsync(this, delegate {
                        bool? nullable = this.IsSplashScreenShown;
                        bool flag = true;
                        if ((nullable.GetValueOrDefault() == flag) ? ((Action) (nullable != null)) : ((Action) false))
                        {
                            this.ShowSplashScreen();
                        }
                    }, DispatcherPriority.Render, AsyncInvokeMode.AsyncOnly);
                }
            }
            isSplashScreenShown = this.IsSplashScreenShown;
            flag = false;
            if ((isSplashScreenShown.GetValueOrDefault() == flag) ? (isSplashScreenShown != null) : false)
            {
                this.CloseSplashScreen();
            }
        }

        private void OnIsSplashScreenWindowStyleChanged()
        {
            Action<Style> action = <>c.<>9__67_0;
            if (<>c.<>9__67_0 == null)
            {
                Action<Style> local1 = <>c.<>9__67_0;
                action = <>c.<>9__67_0 = x => x.Seal();
            }
            this.SplashScreenWindowStyle.Do<Style>(action);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (base.IsLoaded)
            {
                base.Loaded -= new RoutedEventHandler(this.OnLoaded);
                if (!ViewModelBase.IsInDesignMode)
                {
                    this.OnLoadingChildChanged();
                }
            }
        }

        private void OnLoadingChildChanged()
        {
            if (ViewModelBase.IsInDesignMode)
            {
                this.LoadChildInDesignTime();
            }
            else
            {
                this.Child = null;
                if (((this.LoadingChild != null) || (this.LoadingChildTemplate != null)) && base.IsLoaded)
                {
                    if ((this.IsSplashScreenShown == null) && base.IsVisible)
                    {
                        this.ShowSplashScreen();
                    }
                    SplashScreenHelper.InvokeAsync(this, new Action(this.LoadChild), DispatcherPriority.Normal, AsyncInvokeMode.AsyncOnly);
                }
            }
        }

        private void OnLoadingChildLoaded(object sender, RoutedEventArgs e)
        {
            ((FrameworkElement) sender).Loaded -= new RoutedEventHandler(this.OnLoadingChildLoaded);
            this.CloseSplashScreenOnLoading();
        }

        private void OnLoadingChildTemplateChanged()
        {
            this.ValidateLoadingChild();
            Action<DataTemplate> action = <>c.<>9__72_0;
            if (<>c.<>9__72_0 == null)
            {
                Action<DataTemplate> local1 = <>c.<>9__72_0;
                action = <>c.<>9__72_0 = x => x.Seal();
            }
            this.LoadingChildTemplate.Do<DataTemplate>(action);
            this.OnLoadingChildChanged();
        }

        private void OnSplashScreenTemplateChanged()
        {
            Action<DataTemplate> action = <>c.<>9__68_0;
            if (<>c.<>9__68_0 == null)
            {
                Action<DataTemplate> local1 = <>c.<>9__68_0;
                action = <>c.<>9__68_0 = x => x.Seal();
            }
            this.SplashScreenTemplate.Do<DataTemplate>(action);
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.CloseSplashScreenOnLoading();
        }

        protected virtual void ShowSplashScreen()
        {
            if (this.UseSplashScreen && !this.IsActive)
            {
                object[] splashScreenCreatorParameter = new object[] { this.SplashScreenTemplate, this.SplashScreenDataContext };
                this.SplashContainer.Show(new Func<object, Window>(LoadingDecorator.CreateSplashScreenWindow), new Func<object, object>(LoadingDecorator.CreateSplashScreen), this.GetSplashScreenCreatorParams(), splashScreenCreatorParameter, null);
            }
        }

        private void SplashScreenDataContextChanged()
        {
            if (this.SplashScreenDataContext is DependencyObject)
            {
                throw new InvalidOperationException("LoadingDecorator shows its SplashScreen in a separate thread, so it is impossible to put a DependencyObject into the SplashScreenDataContext.");
            }
        }

        private void ValidateLoadingChild()
        {
            if ((this.LoadingChild != null) && ((this.LoadingChildTemplate != null) && !ViewModelBase.IsInDesignMode))
            {
                throw new InvalidOperationException("The LoadingChild and LoadingChildTemplate properties cannot be used simultaneously.");
            }
        }

        public bool UseFadeEffect
        {
            get => 
                (bool) base.GetValue(UseFadeEffectProperty);
            set => 
                base.SetValue(UseFadeEffectProperty, value);
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

        public bool UseSplashScreen
        {
            get => 
                (bool) base.GetValue(UseSplashScreenProperty);
            set => 
                base.SetValue(UseSplashScreenProperty, value);
        }

        public DataTemplate SplashScreenTemplate
        {
            get => 
                (DataTemplate) base.GetValue(SplashScreenTemplateProperty);
            set => 
                base.SetValue(SplashScreenTemplateProperty, value);
        }

        public object SplashScreenDataContext
        {
            get => 
                base.GetValue(SplashScreenDataContextProperty);
            set => 
                base.SetValue(SplashScreenDataContextProperty, value);
        }

        public bool? IsSplashScreenShown
        {
            get => 
                (bool?) base.GetValue(IsSplashScreenShownProperty);
            set => 
                base.SetValue(IsSplashScreenShownProperty, value);
        }

        public Style SplashScreenWindowStyle
        {
            get => 
                (Style) base.GetValue(SplashScreenWindowStyleProperty);
            set => 
                base.SetValue(SplashScreenWindowStyleProperty, value);
        }

        public DataTemplate LoadingChildTemplate
        {
            get => 
                (DataTemplate) base.GetValue(LoadingChildTemplateProperty);
            set => 
                base.SetValue(LoadingChildTemplateProperty, value);
        }

        public DevExpress.Xpf.Core.BorderEffect BorderEffect
        {
            get => 
                (DevExpress.Xpf.Core.BorderEffect) base.GetValue(BorderEffectProperty);
            set => 
                base.SetValue(BorderEffectProperty, value);
        }

        public SolidColorBrush BorderEffectColor
        {
            get => 
                (SolidColorBrush) base.GetValue(BorderEffectColorProperty);
            set => 
                base.SetValue(BorderEffectColorProperty, value);
        }

        public SplashScreenLock OwnerLock
        {
            get => 
                (SplashScreenLock) base.GetValue(OwnerLockProperty);
            set => 
                base.SetValue(OwnerLockProperty, value);
        }

        public DevExpress.Xpf.Core.SplashScreenLocation SplashScreenLocation
        {
            get => 
                (DevExpress.Xpf.Core.SplashScreenLocation) base.GetValue(SplashScreenLocationProperty);
            set => 
                base.SetValue(SplashScreenLocationProperty, value);
        }

        public FrameworkElement LoadingChild
        {
            get => 
                this.loadingChild;
            set
            {
                if (!ReferenceEquals(this.loadingChild, value))
                {
                    this.loadingChild = value;
                    this.ValidateLoadingChild();
                    this.OnLoadingChildChanged();
                }
            }
        }

        protected bool IsActive =>
            (this.splashContainer != null) && this.splashContainer.IsActive;

        private DXSplashScreen.SplashScreenContainer SplashContainer
        {
            get
            {
                this.splashContainer ??= new DXSplashScreen.SplashScreenContainer();
                return this.splashContainer;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LoadingDecorator.<>c <>9 = new LoadingDecorator.<>c();
            public static Action<Style> <>9__67_0;
            public static Action<DataTemplate> <>9__68_0;
            public static Action<DataTemplate> <>9__72_0;
            public static Func<SolidColorBrush, Color?> <>9__82_0;
            public static Func<TimeSpan, bool> <>9__84_0;

            internal void <.cctor>b__16_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LoadingDecorator) d).OnSplashScreenTemplateChanged();
            }

            internal void <.cctor>b__16_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LoadingDecorator) d).OnIsSplashScreenShownChanged();
            }

            internal void <.cctor>b__16_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LoadingDecorator) d).OnIsSplashScreenWindowStyleChanged();
            }

            internal void <.cctor>b__16_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LoadingDecorator) d).OnLoadingChildTemplateChanged();
            }

            internal bool <CreateSplashScreenWindow>b__84_0(TimeSpan x) => 
                x.TotalMilliseconds > 0.0;

            internal Color? <GetSplashScreenCreatorParams>b__82_0(SolidColorBrush x) => 
                new Color?(x.Color);

            internal void <OnIsSplashScreenWindowStyleChanged>b__67_0(Style x)
            {
                x.Seal();
            }

            internal void <OnLoadingChildTemplateChanged>b__72_0(DataTemplate x)
            {
                x.Seal();
            }

            internal void <OnSplashScreenTemplateChanged>b__68_0(DataTemplate x)
            {
                x.Seal();
            }
        }

        internal class LoadingDecoratorWindow : LoadingDecorator.LoadingDecoratorWindowFree
        {
            private DevExpress.Xpf.Core.HandleDecorator.Decorator decorator;

            public LoadingDecoratorWindow(WindowArrangerContainer parentContainer, SplashScreenLock lockMode, string themeName, BorderEffect borderEffect, SolidColorBrush borderBrush) : base(parentContainer, lockMode)
            {
                this.CreateBorderDecorator(borderEffect, borderBrush, themeName);
                ThemeManager.SetThemeName(this, themeName);
            }

            private void CreateBorderDecorator(BorderEffect borderEffect, SolidColorBrush brush, string themeName)
            {
                if (borderEffect != BorderEffect.None)
                {
                    Thickness thickness;
                    Thickness local2;
                    Thickness local3;
                    Thickness local4;
                    Thickness local5;
                    Thickness local6;
                    if (!string.IsNullOrEmpty(themeName) && themeName.Contains(";Touch"))
                    {
                        themeName = themeName.Substring(0, themeName.IndexOf(";Touch"));
                    }
                    Thickness? nullable = this.FindDxWindowResource<Thickness?>(DXWindowThemeKey.BorderEffectOffset, themeName);
                    Thickness? nullable2 = this.FindDxWindowResource<Thickness?>(DXWindowThemeKey.BorderEffectLeftMargins, themeName);
                    Thickness? nullable3 = this.FindDxWindowResource<Thickness?>(DXWindowThemeKey.BorderEffectRightMargins, themeName);
                    Thickness? nullable4 = this.FindDxWindowResource<Thickness?>(DXWindowThemeKey.BorderEffectTopMargins, themeName);
                    Thickness? nullable5 = this.FindDxWindowResource<Thickness?>(DXWindowThemeKey.BorderEffectBottomMargins, themeName);
                    brush = brush ?? this.FindDxWindowResource<SolidColorBrush>(DXWindowThemeKey.BorderEffectActiveColor, themeName);
                    if (nullable != null)
                    {
                        local2 = nullable.Value;
                    }
                    else
                    {
                        thickness = new Thickness();
                        local2 = thickness;
                    }
                    StructDecoratorMargins structDecoratorMargins = new StructDecoratorMargins();
                    if (nullable2 != null)
                    {
                        local3 = nullable2.Value;
                    }
                    else
                    {
                        thickness = new Thickness();
                        local3 = thickness;
                    }
                    structDecoratorMargins.LeftMargins = local3;
                    if (nullable3 != null)
                    {
                        local4 = nullable3.Value;
                    }
                    else
                    {
                        thickness = new Thickness();
                        local4 = thickness;
                    }
                    structDecoratorMargins.RightMargins = local4;
                    if (nullable4 != null)
                    {
                        local5 = nullable4.Value;
                    }
                    else
                    {
                        thickness = new Thickness();
                        local5 = thickness;
                    }
                    structDecoratorMargins.TopMargins = local5;
                    if (nullable5 != null)
                    {
                        local6 = nullable5.Value;
                    }
                    else
                    {
                        thickness = new Thickness();
                        local6 = thickness;
                    }
                    structDecoratorMargins.BottomMargins = local6;
                    brush.decorator = new FormHandleDecorator(brush, (SolidColorBrush) this, local2, structDecoratorMargins, true);
                    this.decorator.Control = this;
                }
            }

            private T FindDxWindowResource<T>(DXWindowThemeKey resourceKey, string themeName)
            {
                DXWindowThemeKeyExtension extension1 = new DXWindowThemeKeyExtension();
                extension1.ThemeName = themeName;
                extension1.ResourceKey = resourceKey;
                DXWindowThemeKeyExtension extension = extension1;
                return (T) base.TryFindResource(extension);
            }

            protected override void OnClosed(EventArgs e)
            {
                this.ReleaseBorderDecorator();
                base.OnClosed(e);
            }

            protected override void OnClosing(CancelEventArgs e)
            {
                if (!e.Cancel)
                {
                    this.ReleaseBorderDecorator();
                }
                base.OnClosing(e);
            }

            private void ReleaseBorderDecorator()
            {
                if (this.decorator != null)
                {
                    this.decorator.Hide();
                    this.decorator.Dispose();
                    this.decorator = null;
                }
            }
        }

        internal class LoadingDecoratorWindowFree : DXSplashScreen.SplashScreenWindow
        {
            public LoadingDecoratorWindowFree(WindowArrangerContainer parentContainer, SplashScreenLock lockMode)
            {
                base.WindowStartupLocation = WindowStartupLocation.Manual;
                this.SetWindowStartupPosition(parentContainer.ControlStartupPosition.IsEmpty ? parentContainer.WindowStartupPosition : parentContainer.ControlStartupPosition);
                this.CreateLocker(parentContainer, lockMode);
                base.ClearValue(Window.ShowActivatedProperty);
            }

            internal void ApplyDefaultSettings()
            {
                base.WindowStyle = WindowStyle.None;
                base.ResizeMode = ResizeMode.NoResize;
                base.AllowsTransparency = true;
                base.Topmost = false;
                base.Focusable = false;
                base.ShowInTaskbar = false;
                base.ShowActivated = false;
                base.Background = new SolidColorBrush(Colors.Transparent);
                base.SizeToContent = SizeToContent.WidthAndHeight;
            }

            private void CreateLocker(WindowContainer parentContainer, SplashScreenLock lockMode)
            {
                this.ParentLocker = new ContainerLocker(parentContainer, lockMode);
            }

            protected override void OnClosed(EventArgs e)
            {
                this.ReleaseLocker();
                base.OnClosed(e);
            }

            private void ReleaseLocker()
            {
                this.ParentLocker.Release(base.IsActiveOnClosing);
                this.ParentLocker = null;
            }

            private void SetWindowStartupPosition(Rect bounds)
            {
                if (!bounds.IsEmpty)
                {
                    base.Left = bounds.Left;
                    base.Top = bounds.Top;
                    base.Width = bounds.Width;
                    base.Height = bounds.Height;
                }
            }

            protected ContainerLocker ParentLocker { get; private set; }
        }
    }
}

