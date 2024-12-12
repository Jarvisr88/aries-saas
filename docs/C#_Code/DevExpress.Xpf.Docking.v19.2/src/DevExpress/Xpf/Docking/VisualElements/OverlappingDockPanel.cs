namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking.Base;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class OverlappingDockPanel : DockPanel
    {
        public static readonly DependencyProperty DisplayModeProperty = DependencyProperty.Register("DisplayMode", typeof(AutoHideMode), typeof(OverlappingDockPanel), new FrameworkPropertyMetadata(AutoHideMode.Default, FrameworkPropertyMetadataOptions.AffectsMeasure));

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            if (this.DisplayMode == AutoHideMode.Inline)
            {
                return base.ArrangeOverride(arrangeSize);
            }
            UIElementCollection internalChildren = base.InternalChildren;
            int num = 0;
            int count = internalChildren.Count;
            while (num < count)
            {
                UIElement element = internalChildren[num];
                if (element != null)
                {
                    element.Arrange(new Rect(arrangeSize));
                }
                num++;
            }
            return arrangeSize;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.DisplayMode == AutoHideMode.Inline)
            {
                return base.MeasureOverride(constraint);
            }
            Size size = new Size();
            UIElementCollection internalChildren = base.InternalChildren;
            int num = 0;
            int count = internalChildren.Count;
            while (num < count)
            {
                UIElement element = internalChildren[num];
                if (element != null)
                {
                    element.Measure(constraint);
                    size.Width = Math.Max(size.Width, element.DesiredSize.Width);
                    size.Height = Math.Max(size.Height, element.DesiredSize.Height);
                }
                num++;
            }
            return size;
        }

        public AutoHideMode DisplayMode
        {
            get => 
                (AutoHideMode) base.GetValue(DisplayModeProperty);
            set => 
                base.SetValue(DisplayModeProperty, value);
        }
    }
}

