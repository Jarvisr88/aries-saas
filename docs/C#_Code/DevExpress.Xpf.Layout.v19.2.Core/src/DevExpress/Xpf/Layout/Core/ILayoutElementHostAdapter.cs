namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public interface ILayoutElementHostAdapter : IDisposable
    {
        LayoutElementHitInfo CalcHitInfo(ILayoutElementHost host, Point pt);
        bool HitTest(ILayoutElementHost host, Point pt);
    }
}

