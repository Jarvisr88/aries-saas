namespace DevExpress.Xpf.Printing.DataNodes
{
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Windows.Data;

    internal class GroupVisualNode : VisualNodeBase, IVisualGroupNode, IGroupNode, IDataNode
    {
        private readonly CollectionViewGroup group;
        private int level;

        public GroupVisualNode(IDataNode parent, int index, CollectionViewGroup group) : base(parent, index)
        {
            this.level = -1;
            this.group = group;
        }

        public override bool CanGetChild(int index) => 
            (index >= 0) && (index < this.group.Items.Count);

        protected override IDataNode CreateChildNode(int index)
        {
            object data = this.group.Items[index];
            CollectionViewGroup group = data as CollectionViewGroup;
            return ((group != null) ? ((IDataNode) new GroupVisualNode(this, index, group)) : ((IDataNode) new VisualNode(this, index, data)));
        }

        public RowViewInfo GetFooter(bool allowContentReuse) => 
            ((this.GroupInfo == null) || (this.GroupInfo.FooterTemplate == null)) ? null : new RowViewInfo(this.GroupInfo.FooterTemplate, this.group);

        public RowViewInfo GetHeader(bool allowContentReuse) => 
            ((this.GroupInfo == null) || (this.GroupInfo.HeaderTemplate == null)) ? null : new RowViewInfo(this.GroupInfo.HeaderTemplate, this.group);

        private int Level
        {
            get
            {
                if (this.level == -1)
                {
                    this.level = this.GetLevel();
                }
                return this.level;
            }
        }

        public GroupUnion Union =>
            (this.GroupInfo != null) ? this.GroupInfo.Union : GroupUnion.None;

        public bool RepeatHeaderEveryPage =>
            (this.GroupInfo != null) ? this.GroupInfo.RepeatHeaderEveryPage : false;

        public override bool PageBreakAfter =>
            (this.GroupInfo != null) ? this.GroupInfo.PageBreakAfter : false;

        public override bool PageBreakBefore =>
            (this.GroupInfo != null) ? this.GroupInfo.PageBreakBefore : false;

        internal DevExpress.Xpf.Printing.GroupInfo GroupInfo =>
            (base.Root.GroupInfos.Count != 0) ? base.Root.GroupInfos[Math.Min(this.Level, base.Root.GroupInfos.Count - 1)] : null;

        public override bool IsDetailContainer =>
            !(this.group.Items[0] is CollectionViewGroup);
    }
}

