namespace DevExpress.Xpf.Printing.DataNodes
{
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Data;

    internal class VisualRootNode : VisualNodeBase, IRootDataNode, IDataNode
    {
        private readonly ICollectionView collectionView;
        private int? count;

        public VisualRootNode(ICollectionView collectionView, DataTemplate itemTemplate, IList<GroupInfo> groupInfos) : base(null, -1)
        {
            this.collectionView = collectionView;
            this.ItemTemplate = itemTemplate;
            this.GroupInfos = groupInfos;
        }

        public override bool CanGetChild(int index) => 
            (this.collectionView.Groups == null) ? ((index >= 0) && ((index < this.Count) && this.collectionView.MoveCurrentToPosition(index))) : ((index >= 0) && (index < this.collectionView.Groups.Count));

        protected override IDataNode CreateChildNode(int index) => 
            !this.IsDetailContainer ? ((IDataNode) new GroupVisualNode(this, index, (CollectionViewGroup) this.collectionView.Groups[index])) : ((IDataNode) new VisualNode(this, index, this.GetData(index)));

        private object GetData(int index)
        {
            if (!this.collectionView.MoveCurrentToPosition(index))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return this.collectionView.CurrentItem;
        }

        protected override int GetLevel()
        {
            throw new InvalidOperationException();
        }

        public int GetTotalDetailCount() => 
            !(this.collectionView.SourceCollection is ICollection) ? -1 : ((ICollection) this.collectionView.SourceCollection).Count;

        public IList<GroupInfo> GroupInfos { get; private set; }

        internal DataTemplate ItemTemplate { get; private set; }

        private int Count
        {
            get
            {
                int? nullable;
                if (this.count != null)
                {
                    return this.count.Value;
                }
                this.count = nullable = new int?(this.collectionView.Cast<object>().Count<object>());
                return nullable.Value;
            }
        }

        public override bool IsDetailContainer =>
            ReferenceEquals(this.collectionView.Groups, null);
    }
}

