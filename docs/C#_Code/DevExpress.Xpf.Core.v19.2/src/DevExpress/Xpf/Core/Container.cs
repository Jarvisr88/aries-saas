namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System.Windows;

    [DXToolboxBrowsable(false)]
    public class Container : PanelBase
    {
        protected override Size OnArrange(Rect bounds)
        {
            foreach (FrameworkElement element in base.GetLogicalChildren(false))
            {
                element.Arrange(bounds);
            }
            return base.OnArrange(bounds);
        }

        protected override Size OnMeasure(Size availableSize)
        {
            Size maxSize = base.OnMeasure(availableSize);
            foreach (FrameworkElement element in base.GetLogicalChildren(false))
            {
                element.Measure(availableSize);
                SizeHelper.UpdateMaxSize(ref maxSize, element.GetDesiredSize());
            }
            return maxSize;
        }
    }
}

