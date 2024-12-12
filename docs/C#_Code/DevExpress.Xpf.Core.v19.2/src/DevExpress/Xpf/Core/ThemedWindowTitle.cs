namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class ThemedWindowTitle : ContentControl
    {
        public static readonly DependencyProperty TitleAlignmentProperty;
        private ThemedWindow window;
        private TextBlock title;
        private DockPanel headerDock;

        static ThemedWindowTitle()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindowTitle), new FrameworkPropertyMetadata(typeof(ThemedWindowTitle)));
            TitleAlignmentProperty = DependencyProperty.Register("TitleAlignment", typeof(WindowTitleAlignment), typeof(ThemedWindowTitle), new FrameworkPropertyMetadata(WindowTitleAlignment.Left, new PropertyChangedCallback(ThemedWindowTitle.OnTitleAlignmentPropertyChanged)));
        }

        private void ApplyTitleAlignment(WindowTitleAlignment titleAlignment)
        {
            if (this.Title != null)
            {
                switch (titleAlignment)
                {
                    case WindowTitleAlignment.Left:
                        this.Title.HorizontalAlignment = HorizontalAlignment.Left;
                        break;

                    case WindowTitleAlignment.Right:
                        this.Title.HorizontalAlignment = HorizontalAlignment.Right;
                        break;

                    default:
                        break;
                }
                base.InvalidateMeasure();
            }
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (!this.IsCustomArrange())
            {
                return base.ArrangeOverride(arrangeBounds);
            }
            Rect bounds = this.GetBounds();
            double x = (this.HeaderDock.ActualWidth - this.Title.DesiredSize.Width) / 2.0;
            if ((x + this.Title.DesiredSize.Width) >= bounds.Right)
            {
                this.Title.HorizontalAlignment = HorizontalAlignment.Center;
                return base.ArrangeOverride(arrangeBounds);
            }
            x -= bounds.Left;
            if (x < 0.0)
            {
                this.Title.HorizontalAlignment = HorizontalAlignment.Left;
                return base.ArrangeOverride(arrangeBounds);
            }
            Rect finalRect = new Rect(new Point(x, 0.0), this.Title.DesiredSize);
            this.Title.Arrange(finalRect);
            return arrangeBounds;
        }

        private DockPanel GetHeaderDock()
        {
            Func<DockPanel, bool> predicate = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<DockPanel, bool> local1 = <>c.<>9__14_0;
                predicate = <>c.<>9__14_0 = x => x.Name == 8.ToString();
            }
            return TreeHelper.GetParent<DockPanel>(this, predicate, true, true);
        }

        private TextBlock GetTitle()
        {
            Func<TextBlock, bool> predicate = <>c.<>9__13_0;
            if (<>c.<>9__13_0 == null)
            {
                Func<TextBlock, bool> local1 = <>c.<>9__13_0;
                predicate = <>c.<>9__13_0 = x => x.Name == 7.ToString();
            }
            return TreeHelper.GetChild<TextBlock>(this, predicate);
        }

        private ThemedWindow GetWindow() => 
            TreeHelper.GetParent<ThemedWindow>(this, null, true, true);

        private bool IsCustomArrange() => 
            (this.TitleAlignment == WindowTitleAlignment.Center) && ((this.HeaderDock != null) && (this.Title != null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            base.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.OnThemedWindowTitleIsVisibleChanged);
            this.ApplyTitleAlignment(this.TitleAlignment);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            base.InvalidateArrange();
            this.SetTabControlMarginIfTabbed();
        }

        private void OnThemedWindowTitleIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.SetTabControlMarginIfTabbed();
        }

        public void OnTitleAlignmentChanged(WindowTitleAlignment newValue)
        {
            this.ApplyTitleAlignment(newValue);
        }

        private static void OnTitleAlignmentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ThemedWindowTitle) d).OnTitleAlignmentChanged((WindowTitleAlignment) e.NewValue);
        }

        private void SetTabControlMarginIfTabbed()
        {
            if ((this.Window != null) && (this.Window.ActualWindowKind == WindowKind.Tabbed))
            {
                WindowUtility.SetTabControlMargin(this.Window);
            }
        }

        public ThemedWindow Window
        {
            get
            {
                ThemedWindow window = this.window;
                if (this.window == null)
                {
                    ThemedWindow local1 = this.window;
                    window = this.window = this.GetWindow();
                }
                return window;
            }
        }

        public TextBlock Title
        {
            get
            {
                TextBlock title = this.title;
                if (this.title == null)
                {
                    TextBlock local1 = this.title;
                    title = this.title = this.GetTitle();
                }
                return title;
            }
        }

        public DockPanel HeaderDock
        {
            get
            {
                DockPanel headerDock = this.headerDock;
                if (this.headerDock == null)
                {
                    DockPanel local1 = this.headerDock;
                    headerDock = this.headerDock = this.GetHeaderDock();
                }
                return headerDock;
            }
        }

        public WindowTitleAlignment TitleAlignment
        {
            get => 
                (WindowTitleAlignment) base.GetValue(TitleAlignmentProperty);
            set => 
                base.SetValue(TitleAlignmentProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ThemedWindowTitle.<>c <>9 = new ThemedWindowTitle.<>c();
            public static Func<TextBlock, bool> <>9__13_0;
            public static Func<DockPanel, bool> <>9__14_0;

            internal bool <GetHeaderDock>b__14_0(DockPanel x) => 
                x.Name == 8.ToString();

            internal bool <GetTitle>b__13_0(TextBlock x) => 
                x.Name == 7.ToString();
        }
    }
}

