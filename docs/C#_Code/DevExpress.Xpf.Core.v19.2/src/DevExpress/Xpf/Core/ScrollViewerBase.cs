namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    public class ScrollViewerBase : ScrollViewer
    {
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ScrollBar templateChild = base.GetTemplateChild("PART_VerticalScrollBar") as ScrollBar;
            if (templateChild != null)
            {
                templateChild.Orientation = Orientation.Vertical;
                this.SetScrollBarBinding(templateChild, RangeBase.ValueProperty, ScrollViewer.VerticalOffsetProperty);
                this.SetScrollBarBinding(templateChild, RangeBase.MaximumProperty, ScrollViewer.ScrollableHeightProperty);
                this.SetScrollBarBinding(templateChild, ScrollBar.ViewportSizeProperty, ScrollViewer.ViewportHeightProperty);
                this.SetScrollBarBinding(templateChild, UIElement.VisibilityProperty, ScrollViewer.ComputedVerticalScrollBarVisibilityProperty);
            }
            ScrollBar scrollBar = base.GetTemplateChild("PART_HorizontalScrollBar") as ScrollBar;
            if (scrollBar != null)
            {
                scrollBar.Orientation = Orientation.Horizontal;
                this.SetScrollBarBinding(scrollBar, RangeBase.ValueProperty, ScrollViewer.HorizontalOffsetProperty);
                this.SetScrollBarBinding(scrollBar, RangeBase.MaximumProperty, ScrollViewer.ScrollableWidthProperty);
                this.SetScrollBarBinding(scrollBar, ScrollBar.ViewportSizeProperty, ScrollViewer.ViewportWidthProperty);
                this.SetScrollBarBinding(scrollBar, UIElement.VisibilityProperty, ScrollViewer.ComputedHorizontalScrollBarVisibilityProperty);
            }
        }

        private void SetScrollBarBinding(ScrollBar scrollBar, DependencyProperty scrollBarProperty, DependencyProperty scrollViewerProperty)
        {
            Binding binding = new Binding(scrollViewerProperty.Name);
            binding.Source = this;
            binding.Mode = BindingMode.OneWay;
            scrollBar.SetBinding(scrollBarProperty, binding);
        }
    }
}

