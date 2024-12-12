namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class ScrollBarThumbDragDeltaBehavior : Behavior<FrameworkElement>
    {
        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            this.ScrollBar = this.FindScrollBar();
            this.CreateListener();
        }

        private void CreateListener()
        {
            if (this.ScrollBar != null)
            {
                Predicate<DependencyObject> predicate = <>c.<>9__15_0;
                if (<>c.<>9__15_0 == null)
                {
                    Predicate<DependencyObject> local1 = <>c.<>9__15_0;
                    predicate = <>c.<>9__15_0 = x => (bool) x.GetValue(ScrollBarExtensions.IsScrollBarThumbDragDeltaListenerProperty);
                }
                DependencyObject obj2 = LayoutHelper.FindLayoutOrVisualParentObject(this.ScrollBar, predicate, false, null);
                this.Listener = obj2 as IScrollBarThumbDragDeltaListener;
                if ((this.Listener != null) && (this.ScrollBar.Orientation == this.Listener.Orientation))
                {
                    this.Listener.ScrollBar = this.ScrollBar;
                }
            }
        }

        private System.Windows.Controls.Primitives.ScrollBar FindScrollBar() => 
            LayoutHelper.FindLayoutOrVisualParentObject<System.Windows.Controls.Primitives.ScrollBar>(this.ScrollBarThumb, false, null);

        protected override void OnAttached()
        {
            base.AssociatedObject.Loaded += new RoutedEventHandler(this.AssociatedObject_Loaded);
            if (this.ScrollBarThumb != null)
            {
                this.ScrollBarThumb.DragDelta += new DragDeltaEventHandler(this.ScrollBarThumbDragDelta);
                this.ScrollBarThumb.MouseMove += new MouseEventHandler(this.ScrollBarThumbMouseMove);
            }
            base.OnAttached();
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.Loaded -= new RoutedEventHandler(this.AssociatedObject_Loaded);
            if (this.ScrollBarThumb != null)
            {
                this.ScrollBarThumb.DragDelta -= new DragDeltaEventHandler(this.ScrollBarThumbDragDelta);
                this.ScrollBarThumb.MouseMove -= new MouseEventHandler(this.ScrollBarThumbMouseMove);
            }
            base.OnDetaching();
        }

        private void ScrollBarThumbDragDelta(object sender, DragDeltaEventArgs e)
        {
            if ((this.Listener != null) && (this.Listener.Orientation == this.ScrollBar.Orientation))
            {
                this.Listener.OnScrollBarThumbDragDelta(e);
            }
        }

        private void ScrollBarThumbMouseMove(object sender, MouseEventArgs e)
        {
            if (this.Listener != null)
            {
                this.Listener.OnScrollBarThumbMouseMove(e);
            }
        }

        private IScrollBarThumbDragDeltaListener Listener { get; set; }

        private Thumb ScrollBarThumb =>
            base.AssociatedObject as Thumb;

        private System.Windows.Controls.Primitives.ScrollBar ScrollBar { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ScrollBarThumbDragDeltaBehavior.<>c <>9 = new ScrollBarThumbDragDeltaBehavior.<>c();
            public static Predicate<DependencyObject> <>9__15_0;

            internal bool <CreateListener>b__15_0(DependencyObject x) => 
                (bool) x.GetValue(ScrollBarExtensions.IsScrollBarThumbDragDeltaListenerProperty);
        }
    }
}

