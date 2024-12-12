namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    public class GroupNode : NodeBase, IGroupNode, INode
    {
        public static readonly DependencyProperty NodeTypeProperty;
        private ObservableCollection<INode> subNodes = new ObservableCollection<INode>();

        static GroupNode()
        {
            Type ownerType = typeof(GroupNode);
            NodeTypeProperty = DependencyPropertyManager.Register("NodeType", typeof(GroupType), ownerType, new PropertyMetadata(GroupType.And));
        }

        protected override object Accept(INodeVisitor visitor) => 
            visitor.Visit(this);

        public override void SetOwner(FilterControl owner, GroupNode parentNode)
        {
            foreach (NodeBase base2 in this.SubNodes)
            {
                base2.SetOwner(owner, this);
            }
            base.SetOwner(owner, parentNode);
        }

        public GroupType NodeType
        {
            get => 
                (GroupType) base.GetValue(NodeTypeProperty);
            set => 
                base.SetValue(NodeTypeProperty, value);
        }

        public IList<INode> SubNodes =>
            this.subNodes;
    }
}

