namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    [TemplatePart(Name="PART_SplitThumb", Type=typeof(UIElement))]
    public class BaseSplitterControl : BaseSplitterItem
    {
        public static readonly DependencyProperty IsDragDropOverProperty;
        protected double SplitterWidth;
        protected double SplitterHeight;

        static BaseSplitterControl()
        {
            new DependencyPropertyRegistrator<BaseSplitterControl>().Register<bool>("IsDragDropOver", ref IsDragDropOverProperty, false, null, null);
        }

        public BaseSplitterControl(LayoutGroup group)
        {
            base.LayoutGroup = group;
            Panel.SetZIndex(this, 1);
            this.SplitterWidth = DockLayoutManagerParameters.DockingItemIntervalHorz;
            this.SplitterHeight = DockLayoutManagerParameters.DockingItemIntervalVert;
        }

        public override void Deactivate()
        {
            DockLayoutManager.Release(this);
            base.Deactivate();
        }

        public void InitSplitThumb(bool horizontal)
        {
            if (this.PartSplitThumb != null)
            {
                this.PartSplitThumb.SetValue(horizontal ? FrameworkElement.WidthProperty : FrameworkElement.HeightProperty, horizontal ? this.SplitterWidth : this.SplitterHeight);
                this.PartSplitThumb.ClearValue(horizontal ? FrameworkElement.HeightProperty : FrameworkElement.WidthProperty);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.PartSplitThumb = base.GetTemplateChild("PART_SplitThumb") as UIElement;
        }

        protected UIElement PartSplitThumb { get; private set; }

        public bool IsDragDropOver
        {
            get => 
                (bool) base.GetValue(IsDragDropOverProperty);
            set => 
                base.SetValue(IsDragDropOverProperty, value);
        }
    }
}

