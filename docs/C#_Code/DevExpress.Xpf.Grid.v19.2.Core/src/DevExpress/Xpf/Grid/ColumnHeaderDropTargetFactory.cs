namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System.Windows;
    using System.Windows.Controls;

    public class ColumnHeaderDropTargetFactory : GridDropTargetFactoryBase
    {
        protected sealed override IDropTarget CreateDropTarget(UIElement dropTargetElement) => 
            this.CreateDropTargetCore((Panel) dropTargetElement);

        protected virtual IDropTarget CreateDropTargetCore(Panel panel) => 
            new DropTarget(panel);
    }
}

