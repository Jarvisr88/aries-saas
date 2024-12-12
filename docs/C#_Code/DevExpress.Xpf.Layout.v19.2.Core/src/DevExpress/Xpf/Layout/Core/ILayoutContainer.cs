namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public interface ILayoutContainer : ILayoutElement, IBaseObject, IDisposable, ISupportHierarchy<ILayoutElement>, ISupportVisitor<ILayoutElement>
    {
        LayoutElementCollection Items { get; }
    }
}

