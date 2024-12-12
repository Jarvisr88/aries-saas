namespace DevExpress.Xpf.Core
{
    using System.Windows;

    public interface IDropTargetFactoryEx
    {
        IDropTarget CreateDropTarget(UIElement dropTargetElement, UIElement sourceElement);
    }
}

