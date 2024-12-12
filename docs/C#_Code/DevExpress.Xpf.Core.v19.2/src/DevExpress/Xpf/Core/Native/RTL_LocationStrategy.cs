namespace DevExpress.Xpf.Core.Native
{
    using System.Windows;

    public class RTL_LocationStrategy : RTL_MouseLocationStrategy
    {
        public override Point GetPosition(FrameworkElement element, FrameworkElement relativeTo);
    }
}

