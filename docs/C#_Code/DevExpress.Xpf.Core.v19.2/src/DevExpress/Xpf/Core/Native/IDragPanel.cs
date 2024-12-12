namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public interface IDragPanel
    {
        event EventHandler ChildrenChanged;

        bool CanStartDrag(FrameworkElement child);
        DragWidgetWindow CreateDragWidget(FrameworkElement child);
        void DropOnEmptySpace(FrameworkElement child);
        FrameworkElement Insert(FrameworkElement child, int index);
        FrameworkElement Move(FrameworkElement child, int index);
        void OnDragFinished();
        void Remove(FrameworkElement child);
        void SetVisibility(FrameworkElement child, Visibility visibility);

        IDragPanelVisual VisualPanel { get; }

        string Region { get; }

        DragControllerBase Controller { get; }

        IEnumerable<FrameworkElement> Children { get; }

        System.Windows.Controls.Orientation Orientation { get; }
    }
}

