namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting.DataNodes;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public class SimpleLink : TemplatedLink
    {
        private DataTemplate detailTemplate;
        private int detailCount;

        public event EventHandler<CreateAreaEventArgs> CreateDetail;

        public SimpleLink() : this(string.Empty)
        {
        }

        public SimpleLink(string documentName) : base(documentName)
        {
        }

        public SimpleLink(DataTemplate detail, int detailCount) : this(detail, detailCount, string.Empty)
        {
        }

        public SimpleLink(DataTemplate detailTemplate, int detailCount, string documentName) : base(documentName)
        {
            this.DetailTemplate = detailTemplate;
            this.DetailCount = detailCount;
        }

        protected override IRootDataNode CreateRootNode() => 
            new PlainDataRootNode(this, this.DetailCount);

        protected override bool IsDocumentLayoutRightToLeft() => 
            this.RightToLeftLayout;

        private void RaiseCreateDetail(CreateAreaEventArgs e)
        {
            if (this.CreateDetail != null)
            {
                this.CreateDetail(this, e);
            }
        }

        public bool RightToLeftLayout { get; set; }

        [Description("Specifies the template for the document's detail area.")]
        public DataTemplate DetailTemplate
        {
            get => 
                this.detailTemplate;
            set => 
                this.detailTemplate = value;
        }

        [Description("Gets or sets the number of times the SimpleLink.CreateDetail event is raised to create the detail area.")]
        public int DetailCount
        {
            get => 
                this.detailCount;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                this.detailCount = value;
            }
        }

        private class PlainDataDetailNode : IVisualDetailNode, IDataNode
        {
            private readonly SimpleLink.PlainDataRootNode root;

            public PlainDataDetailNode(SimpleLink.PlainDataRootNode root, int index)
            {
                this.root = root;
                this.Index = index;
            }

            public bool CanGetChild(int index) => 
                false;

            public IDataNode GetChild(int index)
            {
                throw new NotSupportedException();
            }

            public RowViewInfo GetDetail(bool allowContentReuse) => 
                this.root.GetDetail(this.Index);

            public int Index { get; private set; }

            public bool IsDetailContainer =>
                false;

            public IDataNode Parent =>
                this.root;

            public bool PageBreakBefore =>
                false;

            public bool PageBreakAfter =>
                false;
        }

        private class PlainDataRootNode : IRootDataNode, IDataNode
        {
            private readonly int detailCount;
            private readonly Dictionary<int, Pair<DataTemplate, object>> details;

            public PlainDataRootNode(SimpleLink link, int detailCount)
            {
                this.Link = link;
                this.detailCount = detailCount;
                this.details = new Dictionary<int, Pair<DataTemplate, object>>(detailCount);
            }

            public bool CanGetChild(int index)
            {
                CreateAreaEventArgs e = new CreateAreaEventArgs(index);
                if (index >= this.detailCount)
                {
                    return false;
                }
                this.Link.RaiseCreateDetail(e);
                this.details[index] = new Pair<DataTemplate, object>(this.Link.DetailTemplate, e.Data);
                return true;
            }

            public IDataNode GetChild(int index) => 
                new SimpleLink.PlainDataDetailNode(this, index);

            public RowViewInfo GetDetail(int index) => 
                (this.Link.DetailTemplate != null) ? new RowViewInfo(this.Link.DetailTemplate, this.details[index].Second) : null;

            public int GetTotalDetailCount() => 
                this.detailCount;

            public SimpleLink Link { get; private set; }

            public int Index
            {
                get
                {
                    throw new NotSupportedException();
                }
            }

            public bool IsDetailContainer =>
                true;

            public IDataNode Parent =>
                null;

            public bool PageBreakBefore =>
                false;

            public bool PageBreakAfter =>
                false;
        }
    }
}

