namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System.Windows;

    public class IndicatorColumnHeaderDropTargetFactory : GridDropTargetFactoryBase
    {
        protected sealed override IDropTarget CreateDropTarget(UIElement dropTargetElement) => 
            new IndicatorColumnHeaderDropTarget(dropTargetElement as GridColumnHeaderBase);
    }
}

