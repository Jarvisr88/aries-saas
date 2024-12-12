namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    [StyleTypedProperty(Property="VerticalScrollBarStyle", StyleTargetType=typeof(ScrollBar)), StyleTypedProperty(Property="CornerBoxStyle", StyleTargetType=typeof(CornerBox)), StyleTypedProperty(Property="HorizontalScrollBarStyle", StyleTargetType=typeof(ScrollBar))]
    public abstract class ScrollControl : PanelBase, IScrollControl, IPanel, IControl
    {
        public static int AnimatedScrollingDuration = 500;
        public static readonly DependencyProperty AllowBringChildIntoViewProperty = DependencyProperty.Register("AllowBringChildIntoView", typeof(bool), typeof(ScrollControl), new PropertyMetadata(true));
        public static readonly DependencyProperty AnimateScrollingProperty;
        public static readonly DependencyProperty DragScrollingProperty;
        public static readonly DependencyProperty HorizontalOffsetProperty;
        public static readonly DependencyProperty VerticalOffsetProperty;
        public static readonly DependencyProperty HorizontalScrollBarStyleProperty;
        public static readonly DependencyProperty VerticalScrollBarStyleProperty;
        public static readonly DependencyProperty CornerBoxStyleProperty;
        public static readonly DependencyProperty ScrollBarsProperty;

        static ScrollControl()
        {
            AnimateScrollingProperty = DependencyProperty.Register("AnimateScrolling", typeof(bool), typeof(ScrollControl), new PropertyMetadata(true, (o, e) => ((ScrollControl) o).Controller.AnimateScrolling = (bool) e.NewValue));
            DragScrollingProperty = DependencyProperty.Register("DragScrolling", typeof(bool), typeof(ScrollControl), new PropertyMetadata(true, (o, e) => ((ScrollControl) o).Controller.DragScrolling = (bool) e.NewValue));
            HorizontalOffsetProperty = DependencyProperty.Register("HorizontalOffset", typeof(double), typeof(ScrollControl), new PropertyMetadata((o, e) => ((ScrollControl) o).OnOffsetChanged(true, (double) e.OldValue, (double) e.NewValue)));
            VerticalOffsetProperty = DependencyProperty.Register("VerticalOffset", typeof(double), typeof(ScrollControl), new PropertyMetadata((o, e) => ((ScrollControl) o).OnOffsetChanged(false, (double) e.OldValue, (double) e.NewValue)));
            HorizontalScrollBarStyleProperty = DependencyProperty.Register("HorizontalScrollBarStyle", typeof(Style), typeof(ScrollControl), new PropertyMetadata((o, e) => ((ScrollControl) o).Controller.HorzScrollBarStyle = (Style) e.NewValue));
            VerticalScrollBarStyleProperty = DependencyProperty.Register("VerticalScrollBarStyle", typeof(Style), typeof(ScrollControl), new PropertyMetadata((o, e) => ((ScrollControl) o).Controller.VertScrollBarStyle = (Style) e.NewValue));
            CornerBoxStyleProperty = DependencyProperty.Register("CornerBoxStyle", typeof(Style), typeof(ScrollControl), new PropertyMetadata((o, e) => ((ScrollControl) o).Controller.CornerBoxStyle = (Style) e.NewValue));
            ScrollBarsProperty = DependencyProperty.Register("ScrollBars", typeof(DevExpress.Xpf.Core.ScrollBars), typeof(ScrollControl), new PropertyMetadata(DevExpress.Xpf.Core.ScrollBars.Auto, (o, e) => ((ScrollControl) o).Controller.ScrollBars = (DevExpress.Xpf.Core.ScrollBars) e.NewValue));
            Panel.BackgroundProperty.OverrideMetadata(typeof(ScrollControl), new FrameworkPropertyMetadata(Brushes.Transparent));
            EventManager.RegisterClassHandler(typeof(ScrollControl), FrameworkElement.RequestBringIntoViewEvent, (o, e) => ((ScrollControl) o).OnRequestBringIntoView(e));
        }

        protected ScrollControl()
        {
        }

        public bool BringChildIntoView(FrameworkElement child, bool allowAnimation = false)
        {
            if (!this.Controller.IsScrollable() || ((child == null) || (ReferenceEquals(child, this) || (!child.FindIsInParents(this) || (!child.IsInVisualTree() || base.IsInternalElement(child))))))
            {
                return false;
            }
            if (!base.IsArrangeValid)
            {
                base.UpdateLayout();
            }
            if (!child.IsInVisualTree())
            {
                return false;
            }
            bool flag = false;
            Rect contentBounds = base.ContentBounds;
            Rect bounds = child.GetBounds(this);
            if ((bounds.Left < contentBounds.Left) || (bounds.Right > contentBounds.Right))
            {
                flag = true;
                double num = (bounds.Left >= contentBounds.Left) ? Math.Min((double) (bounds.Right - contentBounds.Right), (double) (bounds.Left - contentBounds.Left)) : -(contentBounds.Left - bounds.Left);
                this.Controller.Scroll(Orientation.Horizontal, this.HorizontalOffset + num, allowAnimation, false);
            }
            if ((bounds.Top < contentBounds.Top) || (bounds.Bottom > contentBounds.Bottom))
            {
                flag = true;
                double num2 = (bounds.Top >= contentBounds.Top) ? Math.Min((double) (bounds.Bottom - contentBounds.Bottom), (double) (bounds.Top - contentBounds.Top)) : -(contentBounds.Top - bounds.Top);
                this.Controller.Scroll(Orientation.Vertical, this.VerticalOffset + num2, allowAnimation, false);
            }
            return flag;
        }

        protected override PanelControllerBase CreateController() => 
            new ScrollControlController(this);

        protected virtual FrameworkElement GetChildContainer(FrameworkElement child) => 
            child;

        protected virtual void OnOffsetChanged(bool isHorizontal, double oldValue, double newValue)
        {
            base.InvalidateArrange();
        }

        protected virtual void OnRequestBringIntoView(RequestBringIntoViewEventArgs e)
        {
            if (this.AllowBringChildIntoView)
            {
                FrameworkElement targetObject = e.TargetObject as FrameworkElement;
                if ((targetObject != null) && this.BringChildIntoView(this.GetChildContainer(targetObject), true))
                {
                    e.Handled = true;
                }
            }
        }

        public void SetOffset(Point offset)
        {
            this.HorizontalOffset = offset.X;
            this.VerticalOffset = offset.Y;
        }

        public bool AllowBringChildIntoView
        {
            get => 
                (bool) base.GetValue(AllowBringChildIntoViewProperty);
            set => 
                base.SetValue(AllowBringChildIntoViewProperty, value);
        }

        public bool AnimateScrolling
        {
            get => 
                (bool) base.GetValue(AnimateScrollingProperty);
            set => 
                base.SetValue(AnimateScrollingProperty, value);
        }

        public ScrollControlController Controller =>
            (ScrollControlController) base.Controller;

        public bool DragScrolling
        {
            get => 
                (bool) base.GetValue(DragScrollingProperty);
            set => 
                base.SetValue(DragScrollingProperty, value);
        }

        public double HorizontalOffset
        {
            get => 
                (double) base.GetValue(HorizontalOffsetProperty);
            set => 
                base.SetValue(HorizontalOffsetProperty, value);
        }

        public double VerticalOffset
        {
            get => 
                (double) base.GetValue(VerticalOffsetProperty);
            set => 
                base.SetValue(VerticalOffsetProperty, value);
        }

        public Point Offset =>
            new Point(this.HorizontalOffset, this.VerticalOffset);

        public Style HorizontalScrollBarStyle
        {
            get => 
                (Style) base.GetValue(HorizontalScrollBarStyleProperty);
            set => 
                base.SetValue(HorizontalScrollBarStyleProperty, value);
        }

        public Style VerticalScrollBarStyle
        {
            get => 
                (Style) base.GetValue(VerticalScrollBarStyleProperty);
            set => 
                base.SetValue(VerticalScrollBarStyleProperty, value);
        }

        public Style CornerBoxStyle
        {
            get => 
                (Style) base.GetValue(CornerBoxStyleProperty);
            set => 
                base.SetValue(CornerBoxStyleProperty, value);
        }

        public Size ScrollAreaSize =>
            this.Controller.ScrollAreaSize;

        public DevExpress.Xpf.Core.ScrollBars ScrollBars
        {
            get => 
                (DevExpress.Xpf.Core.ScrollBars) base.GetValue(ScrollBarsProperty);
            set => 
                base.SetValue(ScrollBarsProperty, value);
        }

        UIElementCollection IPanel.Children =>
            base.Children;

        bool IControl.IsLoaded =>
            base.IsLoaded;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScrollControl.<>c <>9 = new ScrollControl.<>c();

            internal void <.cctor>b__10_0(object o, RequestBringIntoViewEventArgs e)
            {
                ((ScrollControl) o).OnRequestBringIntoView(e);
            }

            internal void <.cctor>b__10_1(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollControl) o).Controller.AnimateScrolling = (bool) e.NewValue;
            }

            internal void <.cctor>b__10_2(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollControl) o).Controller.DragScrolling = (bool) e.NewValue;
            }

            internal void <.cctor>b__10_3(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollControl) o).OnOffsetChanged(true, (double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__10_4(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollControl) o).OnOffsetChanged(false, (double) e.OldValue, (double) e.NewValue);
            }

            internal void <.cctor>b__10_5(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollControl) o).Controller.HorzScrollBarStyle = (Style) e.NewValue;
            }

            internal void <.cctor>b__10_6(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollControl) o).Controller.VertScrollBarStyle = (Style) e.NewValue;
            }

            internal void <.cctor>b__10_7(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollControl) o).Controller.CornerBoxStyle = (Style) e.NewValue;
            }

            internal void <.cctor>b__10_8(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                ((ScrollControl) o).Controller.ScrollBars = (ScrollBars) e.NewValue;
            }
        }
    }
}

