namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public interface IDockLayoutContainer : IDockLayoutElement, ILayoutElement, IBaseObject, IDisposable, ISupportHierarchy<ILayoutElement>, ISupportVisitor<ILayoutElement>, IDragSource, IDropTarget, ILayoutContainer
    {
        Rect GetHeadersPanelBounds();
        Rect GetSelectedPageBounds();

        bool HasHeadersPanel { get; }

        bool IsTabContainer { get; }

        Dock TabHeaderLocation { get; }

        bool IsHorizontalHeaders { get; }
    }
}

