namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System.Windows;

    public class BandChooserDropTargetFactory : GridDropTargetFactoryBase
    {
        protected sealed override IDropTarget CreateDropTarget(UIElement dropTargetElement) => 
            new BandChooserDropTarget();
    }
}

