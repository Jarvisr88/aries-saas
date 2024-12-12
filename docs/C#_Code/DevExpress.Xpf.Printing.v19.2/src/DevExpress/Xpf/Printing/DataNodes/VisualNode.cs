namespace DevExpress.Xpf.Printing.DataNodes
{
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting.DataNodes;
    using System;

    internal class VisualNode : VisualNodeBase, IVisualDetailNode, IDataNode
    {
        private readonly object data;

        public VisualNode(IDataNode parent, int index, object data) : base(parent, index)
        {
            this.data = data;
        }

        public override bool CanGetChild(int index) => 
            false;

        protected override IDataNode CreateChildNode(int index)
        {
            throw new NotSupportedException();
        }

        public RowViewInfo GetDetail(bool allowContentReuse) => 
            (base.Root.ItemTemplate != null) ? new RowViewInfo(base.Root.ItemTemplate, this.data) : null;
    }
}

