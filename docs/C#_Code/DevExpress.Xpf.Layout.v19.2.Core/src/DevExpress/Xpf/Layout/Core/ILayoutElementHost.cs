namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public interface ILayoutElementHost : IBaseObject, IDisposable
    {
        Point ClientToScreen(Point clientPoint);
        void EnsureLayoutRoot();
        ILayoutElement GetDragItem(ILayoutElement element);
        ILayoutElement GetElement(object key);
        ILayoutElementBehavior GetElementBehavior(ILayoutElement element);
        ILayoutElementFactory GetLayoutElementFactory();
        void Invalidate();
        void ReleaseCapture();
        Point ScreenToClient(Point screenPoint);
        void SetCapture();

        HostType Type { get; }

        ILayoutElement LayoutRoot { get; }

        object RootKey { get; }

        bool IsActiveAndCanProcessEvent { get; }
    }
}

