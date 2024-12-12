namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public interface ILayoutElement : IBaseObject, IDisposable, ISupportHierarchy<ILayoutElement>, ISupportVisitor<ILayoutElement>
    {
        LayoutElementHitInfo CalcHitInfo(Point pt);
        bool EnsureBounds();
        IEnumerator<ILayoutElement> GetEnumerator();
        DevExpress.Xpf.Layout.Core.State GetState(object hitResult);
        bool HitTest(Point pt);
        void Invalidate();
        void ResetState();
        void SetState(object hitResult, DevExpress.Xpf.Layout.Core.State state);

        ILayoutContainer Container { get; }

        System.Windows.Size Size { get; }

        Point Location { get; }

        bool IsActive { get; }

        bool IsEnabled { get; }

        bool IsReady { get; }

        bool HitTestingEnabled { get; }

        bool IsTabHeader { get; }
    }
}

