namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System.Windows;

    public class RTL_MouseLocationStrategy : BaseLocationStrategy
    {
        public override Point GetMousePosition(IndependentMouseEventArgs e, UIElement relativeTo);
    }
}

