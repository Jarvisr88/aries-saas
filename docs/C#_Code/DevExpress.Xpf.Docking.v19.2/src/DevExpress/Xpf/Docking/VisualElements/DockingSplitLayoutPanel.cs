namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;
    using System.Windows.Media;

    [DXToolboxBrowsable(false)]
    public class DockingSplitLayoutPanel : SplitLayoutPanel
    {
        public static readonly DependencyProperty ClipMarginProperty;
        public static readonly DependencyProperty UseClipMarginProperty;

        static DockingSplitLayoutPanel()
        {
            DependencyPropertyRegistrator<DockingSplitLayoutPanel> registrator = new DependencyPropertyRegistrator<DockingSplitLayoutPanel>();
            Thickness defValue = new Thickness();
            registrator.Register<Thickness>("ClipMargin", ref ClipMarginProperty, defValue, FrameworkPropertyMetadataOptions.AffectsArrange, null, null);
            registrator.Register<bool>("UseClipMargin", ref UseClipMarginProperty, false, FrameworkPropertyMetadataOptions.AffectsArrange, null, null);
        }

        public DockingSplitLayoutPanel()
        {
            base.SnapsToDevicePixels = true;
        }

        protected override Geometry GetLayoutClip(Size layoutSlotSize)
        {
            if (!this.UseClipMargin)
            {
                return base.GetLayoutClip(layoutSlotSize);
            }
            Rect rect = new Rect(this.ClipMargin.Left, this.ClipMargin.Top, layoutSlotSize.Width - (this.ClipMargin.Left + this.ClipMargin.Right), layoutSlotSize.Height - (this.ClipMargin.Top + this.ClipMargin.Bottom));
            RectangleGeometry geometry1 = new RectangleGeometry();
            geometry1.Rect = rect;
            return geometry1;
        }

        public Thickness ClipMargin
        {
            get => 
                (Thickness) base.GetValue(ClipMarginProperty);
            set => 
                base.SetValue(ClipMarginProperty, value);
        }

        public bool UseClipMargin
        {
            get => 
                (bool) base.GetValue(UseClipMarginProperty);
            set => 
                base.SetValue(UseClipMarginProperty, value);
        }
    }
}

