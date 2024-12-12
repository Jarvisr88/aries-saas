namespace DevExpress.Xpf.Core
{
    using System.Windows;

    public interface IDropTargetFactory
    {
        IDropTarget CreateDropTarget(UIElement dropTargetElement);
    }
}

