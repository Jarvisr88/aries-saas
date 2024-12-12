namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public interface IPanel : IControl
    {
        UIElement ChildAt(Point p, bool ignoreInternalElements, bool ignoreTempChildren, bool useBounds);
        FrameworkElements GetChildren(bool includeInternalElements, bool includeTempChildren, bool visibleOnly);
        FrameworkElements GetLogicalChildren(bool visibleOnly);

        Size ActualDesiredSize { get; }

        UIElementCollection Children { get; }

        Rect ChildrenBounds { get; }

        Rect ClientBounds { get; }

        Rect ContentBounds { get; }
    }
}

