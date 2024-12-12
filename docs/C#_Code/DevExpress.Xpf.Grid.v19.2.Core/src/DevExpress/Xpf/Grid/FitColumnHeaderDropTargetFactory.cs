namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System.Windows;

    public class FitColumnHeaderDropTargetFactory : GridDropTargetFactoryBase
    {
        protected sealed override IDropTarget CreateDropTarget(UIElement dropTargetElement) => 
            new FitColumnHeaderDropTarget(dropTargetElement as GridColumnHeaderBase);
    }
}

