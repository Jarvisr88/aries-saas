namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.DataNodes;
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CollectionViewLink : TemplatedLink
    {
        private GroupInfoCollection groupInfos;

        public CollectionViewLink() : base(string.Empty)
        {
        }

        protected override void BuildCore()
        {
            if (this.CollectionView == null)
            {
                throw new InvalidOperationException("CollectionView is null");
            }
            base.BuildCore();
        }

        protected override IRootDataNode CreateRootNode() => 
            new VisualRootNode(this.CollectionView, this.DetailTemplate, this.GroupInfos);

        protected override bool IsDocumentLayoutRightToLeft() => 
            this.RightToLeftLayout;

        public bool RightToLeftLayout { get; set; }

        [Description("Provides access to a collection of objects, which store information about grouping.")]
        public GroupInfoCollection GroupInfos
        {
            get
            {
                this.groupInfos ??= new GroupInfoCollection();
                return this.groupInfos;
            }
        }

        [Description("Gets or sets an object, which should be printed by the CollectionViewLink.")]
        public ICollectionView CollectionView { get; set; }

        [Description("Specifies the template for the document's detail area.")]
        public DataTemplate DetailTemplate { get; set; }
    }
}

