namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Grid.Native;
    using System.Windows.Controls;

    public class FixedLeftColumnHeaderDropTargetFactory : ColumnHeaderDropTargetFactory
    {
        protected override IDropTarget CreateDropTargetCore(Panel panel) => 
            new FixedLeftDropTarget(panel);
    }
}

