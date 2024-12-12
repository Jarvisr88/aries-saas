namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DXTabbedWindow : DXWindow, ICloneable
    {
        public static readonly DependencyProperty TabbedWindowModeProperty;
        public static readonly DependencyProperty HeaderIndentInNormalStateProperty;
        public static readonly DependencyProperty HeaderIndentInMaximizedStateProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty CaptionActualWidthProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty ButtonContainerActualWidthProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyPropertyKey TabHeaderSizePropertyKey;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty TabHeaderSizeProperty;
        private DXTabControl tabControl;
        private TabPanelContainer tabPanel;

        static DXTabbedWindow()
        {
            TabbedWindowModeProperty = DependencyProperty.Register("TabbedWindowMode", typeof(DevExpress.Xpf.Core.TabbedWindowMode), typeof(DXTabbedWindow), new FrameworkPropertyMetadata(DevExpress.Xpf.Core.TabbedWindowMode.Compact, (d, e) => ((DXTabbedWindow) d).OnTabbedWindowModeChanged()));
            HeaderIndentInNormalStateProperty = DependencyProperty.Register("HeaderIndentInNormalState", typeof(double), typeof(DXTabbedWindow), new PropertyMetadata(14.0));
            HeaderIndentInMaximizedStateProperty = DependencyProperty.Register("HeaderIndentInMaximizedState", typeof(double), typeof(DXTabbedWindow), new PropertyMetadata(4.0));
            CaptionActualWidthProperty = DependencyProperty.Register("CaptionActualWidth", typeof(double), typeof(DXTabbedWindow), new FrameworkPropertyMetadata(0.0, (d, e) => ((DXTabbedWindow) d).OnCaptionAndButtonContainerActualWidthChanged()));
            ButtonContainerActualWidthProperty = DependencyProperty.Register("ButtonContainerActualWidth", typeof(double), typeof(DXTabbedWindow), new FrameworkPropertyMetadata(0.0, (d, e) => ((DXTabbedWindow) d).OnCaptionAndButtonContainerActualWidthChanged()));
            TabHeaderSizePropertyKey = DependencyProperty.RegisterReadOnly("TabHeaderSize", typeof(double), typeof(DXTabbedWindow), new PropertyMetadata(0.0));
            TabHeaderSizeProperty = TabHeaderSizePropertyKey.DependencyProperty;
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DXTabbedWindow), new FrameworkPropertyMetadata(typeof(DXTabbedWindow)));
        }

        public DXTabbedWindow()
        {
            DXWindowsHelper.SetWindowKind(this, "DXTabbedWindow");
            DXWindowsHelper.SetWindow(this, this);
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.Unloaded += new RoutedEventHandler(this.OnUnloaded);
            base.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        }

        internal static Window CloneCore(Window baseWindow)
        {
            Window window = (Window) Activator.CreateInstance(baseWindow.GetType());
            window.SetCurrentValue(Control.BackgroundProperty, baseWindow.Background);
            window.SetCurrentValue(Control.BorderBrushProperty, baseWindow.BorderBrush);
            window.SetCurrentValue(Control.BorderThicknessProperty, baseWindow.BorderThickness);
            window.SetCurrentValue(Control.ForegroundProperty, baseWindow.Foreground);
            window.SetCurrentValue(Control.FontFamilyProperty, baseWindow.FontFamily);
            window.SetCurrentValue(Control.FontSizeProperty, baseWindow.FontSize);
            window.SetCurrentValue(Control.FontStretchProperty, baseWindow.FontStretch);
            window.SetCurrentValue(Control.FontStyleProperty, baseWindow.FontStyle);
            window.SetCurrentValue(Control.FontWeightProperty, baseWindow.FontWeight);
            window.SetCurrentValue(FrameworkElement.HeightProperty, baseWindow.Height);
            window.SetCurrentValue(FrameworkElement.WidthProperty, baseWindow.Width);
            window.SetCurrentValue(FrameworkElement.MaxHeightProperty, baseWindow.MaxHeight);
            window.SetCurrentValue(FrameworkElement.MaxWidthProperty, baseWindow.MaxWidth);
            window.SetCurrentValue(FrameworkElement.MinHeightProperty, baseWindow.MinHeight);
            window.SetCurrentValue(FrameworkElement.MinWidthProperty, baseWindow.MinWidth);
            window.SetCurrentValue(Window.AllowsTransparencyProperty, baseWindow.AllowsTransparency);
            window.SetCurrentValue(FrameworkElement.CursorProperty, baseWindow.Cursor);
            window.SetCurrentValue(FrameworkElement.FlowDirectionProperty, baseWindow.FlowDirection);
            window.SetCurrentValue(UIElement.SnapsToDevicePixelsProperty, baseWindow.SnapsToDevicePixels);
            window.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, baseWindow.UseLayoutRounding);
            window.SetCurrentValue(Window.ResizeModeProperty, baseWindow.ResizeMode);
            window.SetCurrentValue(Window.ShowInTaskbarProperty, baseWindow.ShowInTaskbar);
            window.SetCurrentValue(Window.SizeToContentProperty, baseWindow.SizeToContent);
            window.SetCurrentValue(Window.WindowStyleProperty, baseWindow.WindowStyle);
            window.SetCurrentValue(FrameworkElement.LanguageProperty, baseWindow.Language);
            window.SetCurrentValue(Window.IconProperty, baseWindow.Icon);
            window.SetCurrentValue(FrameworkElement.TagProperty, baseWindow.Tag);
            window.SetCurrentValue(Window.TitleProperty, baseWindow.Title);
            window.SetCurrentValue(FrameworkElement.ToolTipProperty, baseWindow.ToolTip);
            window.SetCurrentValue(Window.TopmostProperty, baseWindow.Topmost);
            window.SetCurrentValue(Window.WindowStateProperty, WindowState.Normal);
            DXWindow window2 = baseWindow as DXWindow;
            if (window2 != null)
            {
                window.SetCurrentValue(DXWindow.ShowTitleProperty, window2.ShowTitle);
                window.SetCurrentValue(DXWindow.ShowIconProperty, window2.ShowIcon);
                window.SetCurrentValue(DXWindow.SmallIconProperty, window2.SmallIcon);
            }
            DXTabbedWindow window3 = baseWindow as DXTabbedWindow;
            if (window3 != null)
            {
                window.SetCurrentValue(TabbedWindowModeProperty, window3.TabbedWindowMode);
                window.SetCurrentValue(HeaderIndentInNormalStateProperty, window3.HeaderIndentInNormalState);
                window.SetCurrentValue(HeaderIndentInMaximizedStateProperty, window3.HeaderIndentInMaximizedState);
                window.SetCurrentValue(FrameworkElement.HeightProperty, window3.NormalStateSize.Height);
                window.SetCurrentValue(FrameworkElement.WidthProperty, window3.NormalStateSize.Width);
                Size titleSize = window3.GetTitleSize();
                window.SetCurrentValue(Window.LeftProperty, window3.Left + titleSize.Width);
                window.SetCurrentValue(Window.TopProperty, window3.Top + titleSize.Height);
            }
            return window;
        }

        private Size GetTitleSize()
        {
            Func<FrameworkElement, bool> predicate = <>c.<>9__54_0;
            if (<>c.<>9__54_0 == null)
            {
                Func<FrameworkElement, bool> local1 = <>c.<>9__54_0;
                predicate = <>c.<>9__54_0 = x => x.Name == "PART_Header";
            }
            FrameworkElement element = LayoutTreeHelper.GetVisualChildren(this).OfType<FrameworkElement>().FirstOrDefault<FrameworkElement>(predicate);
            if (element != null)
            {
                return new Size(element.ActualWidth, element.ActualHeight);
            }
            return new Size();
        }

        private void LoadTabControl()
        {
            this.TabControl = LayoutTreeHelper.GetVisualChildren(this).OfType<DXTabControl>().FirstOrDefault<DXTabControl>();
        }

        private void LoadTabPanel()
        {
            Func<DXTabControl, TabPanelContainer> evaluator = <>c.<>9__49_0;
            if (<>c.<>9__49_0 == null)
            {
                Func<DXTabControl, TabPanelContainer> local1 = <>c.<>9__49_0;
                evaluator = <>c.<>9__49_0 = x => x.TabPanel;
            }
            this.TabPanel = this.TabControl.Return<DXTabControl, TabPanelContainer>(evaluator, <>c.<>9__49_1 ??= ((Func<TabPanelContainer>) (() => null)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.TabControl = null;
            this.SubscribeLayoutUpdated();
        }

        private void OnCaptionAndButtonContainerActualWidthChanged()
        {
            if (this.TabbedWindowMode != DevExpress.Xpf.Core.TabbedWindowMode.Normal)
            {
                this.TabControl.Do<DXTabControl>(x => x.SetValue(DXTabControl.PanelIndentProperty, new Thickness(this.CaptionActualWidth, 0.0, this.ButtonContainerActualWidth, 0.0)));
            }
            else
            {
                Action<DXTabControl> action = <>c.<>9__37_0;
                if (<>c.<>9__37_0 == null)
                {
                    Action<DXTabControl> local1 = <>c.<>9__37_0;
                    action = <>c.<>9__37_0 = x => x.SetValue(DXTabControl.PanelIndentProperty, new Thickness(0.0, 0.0, 0.0, 0.0));
                }
                this.TabControl.Do<DXTabControl>(action);
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.NormalStateSize = new Size(base.Width, base.Height);
        }

        private void OnLayoutUpdated(object sender, EventArgs e)
        {
            this.LoadTabControl();
            if (this.TabControl != null)
            {
                this.UnsubscribeLayoutUpdated();
            }
            if (this.TabHeaderSize.IsZero())
            {
                this.OnTabPanelSizeChanged(null, null);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.SubscribeLayoutUpdated();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (base.WindowState == WindowState.Normal)
            {
                this.NormalStateSize = e.NewSize;
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
        }

        private void OnTabbedWindowModeChanged()
        {
            this.OnCaptionAndButtonContainerActualWidthChanged();
        }

        private void OnTabControlChanged(DXTabControl old)
        {
            old.Do<DXTabControl>(delegate (DXTabControl x) {
                x.LayoutUpdated -= new EventHandler(this.OnTabControlLayoutUpdated);
            });
            old.Do<DXTabControl>(delegate (DXTabControl x) {
                x.Unloaded -= new RoutedEventHandler(this.OnTabControlUnloaded);
            });
            this.TabPanel = null;
            if (this.TabControl != null)
            {
                this.TabControl.Unloaded += new RoutedEventHandler(this.OnTabControlUnloaded);
                this.OnCaptionAndButtonContainerActualWidthChanged();
                this.LoadTabPanel();
                if (this.TabPanel == null)
                {
                    this.TabControl.LayoutUpdated += new EventHandler(this.OnTabControlLayoutUpdated);
                }
            }
        }

        private void OnTabControlLayoutUpdated(object sender, EventArgs e)
        {
            this.LoadTabPanel();
            if (this.TabPanel != null)
            {
                this.TabControl.LayoutUpdated -= new EventHandler(this.OnTabControlLayoutUpdated);
            }
        }

        private void OnTabControlUnloaded(object sender, RoutedEventArgs e)
        {
            this.TabControl = null;
            this.SubscribeLayoutUpdated();
        }

        private void OnTabPanelChanged(TabPanelContainer old)
        {
            old.Do<TabPanelContainer>(delegate (TabPanelContainer x) {
                x.SizeChanged -= new SizeChangedEventHandler(this.OnTabPanelSizeChanged);
            });
            this.TabPanel.Do<TabPanelContainer>(delegate (TabPanelContainer x) {
                x.SizeChanged += new SizeChangedEventHandler(this.OnTabPanelSizeChanged);
            });
            this.OnTabPanelSizeChanged(null, null);
        }

        private void OnTabPanelSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if ((this.TabControl == null) || (this.TabControl.View == null))
            {
                this.TabHeaderSize = 0.0;
            }
            else if (this.TabControl.View.HeaderLocation == HeaderLocation.Top)
            {
                this.TabHeaderSize = this.TabPanel.ActualHeight;
            }
            else
            {
                this.TabHeaderSize = 0.0;
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.TabControl = null;
            this.UnsubscribeLayoutUpdated();
        }

        private void SubscribeLayoutUpdated()
        {
            this.UnsubscribeLayoutUpdated();
            this.LoadTabControl();
            if (this.TabControl == null)
            {
                base.LayoutUpdated += new EventHandler(this.OnLayoutUpdated);
            }
        }

        object ICloneable.Clone() => 
            CloneCore(this);

        private void UnsubscribeLayoutUpdated()
        {
            base.LayoutUpdated -= new EventHandler(this.OnLayoutUpdated);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public double TabHeaderSize
        {
            get => 
                (double) base.GetValue(TabHeaderSizeProperty);
            private set => 
                base.SetValue(TabHeaderSizePropertyKey, value);
        }

        public DXTabControl TabControl
        {
            get => 
                this.tabControl;
            private set
            {
                if (!ReferenceEquals(this.tabControl, value))
                {
                    DXTabControl tabControl = this.tabControl;
                    this.tabControl = value;
                    this.OnTabControlChanged(tabControl);
                }
            }
        }

        private TabPanelContainer TabPanel
        {
            get => 
                this.tabPanel;
            set
            {
                if (!ReferenceEquals(this.tabPanel, value))
                {
                    TabPanelContainer tabPanel = this.tabPanel;
                    this.tabPanel = value;
                    this.OnTabPanelChanged(tabPanel);
                }
            }
        }

        public DevExpress.Xpf.Core.TabbedWindowMode TabbedWindowMode
        {
            get => 
                (DevExpress.Xpf.Core.TabbedWindowMode) base.GetValue(TabbedWindowModeProperty);
            set => 
                base.SetValue(TabbedWindowModeProperty, value);
        }

        public double HeaderIndentInNormalState
        {
            get => 
                (double) base.GetValue(HeaderIndentInNormalStateProperty);
            set => 
                base.SetValue(HeaderIndentInNormalStateProperty, value);
        }

        public double HeaderIndentInMaximizedState
        {
            get => 
                (double) base.GetValue(HeaderIndentInMaximizedStateProperty);
            set => 
                base.SetValue(HeaderIndentInMaximizedStateProperty, value);
        }

        private double CaptionActualWidth =>
            (double) base.GetValue(CaptionActualWidthProperty);

        private double ButtonContainerActualWidth =>
            (double) base.GetValue(ButtonContainerActualWidthProperty);

        private Size NormalStateSize { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXTabbedWindow.<>c <>9 = new DXTabbedWindow.<>c();
            public static Action<DXTabControl> <>9__37_0;
            public static Func<DXTabControl, TabPanelContainer> <>9__49_0;
            public static Func<TabPanelContainer> <>9__49_1;
            public static Func<FrameworkElement, bool> <>9__54_0;

            internal void <.cctor>b__35_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabbedWindow) d).OnTabbedWindowModeChanged();
            }

            internal void <.cctor>b__35_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabbedWindow) d).OnCaptionAndButtonContainerActualWidthChanged();
            }

            internal void <.cctor>b__35_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((DXTabbedWindow) d).OnCaptionAndButtonContainerActualWidthChanged();
            }

            internal bool <GetTitleSize>b__54_0(FrameworkElement x) => 
                x.Name == "PART_Header";

            internal TabPanelContainer <LoadTabPanel>b__49_0(DXTabControl x) => 
                x.TabPanel;

            internal TabPanelContainer <LoadTabPanel>b__49_1() => 
                null;

            internal void <OnCaptionAndButtonContainerActualWidthChanged>b__37_0(DXTabControl x)
            {
                x.SetValue(DXTabControl.PanelIndentProperty, new Thickness(0.0, 0.0, 0.0, 0.0));
            }
        }
    }
}

