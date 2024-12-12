namespace DevExpress.Xpf.Printing.DataNodes
{
    using DevExpress.XtraPrinting.DataNodes;
    using System;

    internal abstract class VisualNodeBase : DataNodeBase
    {
        protected VisualNodeBase(IDataNode parent, int index) : base(parent, index)
        {
        }

        protected VisualRootNode Root =>
            (VisualRootNode) base.Root;
    }
}

