namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System.Windows;
    using System.Windows.Controls;

    public class DXDockPanel : DockPanel
    {
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            Size size = base.ArrangeOverride(arrangeSize);
            foreach (UIElement element in base.Children)
            {
                if ((element != null) && (element is INotifyPositionChanged))
                {
                    ((INotifyPositionChanged) element).OnPositionChanged(LayoutHelper.GetRelativeElementRect(element, this));
                }
            }
            return size;
        }
    }
}

