namespace DevExpress.Xpf.Core
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;

    public interface ISupportDragDrop
    {
        bool CanStartDrag(object sender, MouseButtonEventArgs e);
        IDragElement CreateDragElement(Point offset);
        IDropTarget CreateEmptyDropTarget();
        IEnumerable<UIElement> GetTopLevelDropContainers();
        bool IsCompatibleDropTargetFactory(IDropTargetFactory factory, UIElement dropTargetElement);

        FrameworkElement SourceElement { get; }
    }
}

