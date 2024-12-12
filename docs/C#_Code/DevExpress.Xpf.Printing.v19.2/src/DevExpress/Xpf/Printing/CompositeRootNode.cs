namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Printing.BrickCollection;
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class CompositeRootNode : IRootDataNode, IDataNode, IDisposable
    {
        private readonly Dictionary<IRootDataNode, IVisualTreeWalker> rootNodeTreeWalkerDictionary = new Dictionary<IRootDataNode, IVisualTreeWalker>();
        private readonly int detailCount;

        public CompositeRootNode(CompositeLink link)
        {
            this.Link = link;
            this.detailCount = link.Links.Count<TemplatedLink>();
        }

        public bool CanGetChild(int index) => 
            (index >= 0) && (index < this.detailCount);

        public IDataNode GetChild(int index)
        {
            TemplatedLink link = this.Link.Links.ElementAt<TemplatedLink>(index);
            link.CreateDocument(false);
            PageBreakInfo pageBreaksInfo = this.Link.PageBreaks.FirstOrDefault<PageBreakInfo>(x => ReferenceEquals(x.Link, link));
            IDataNode node = new CompositeLinkGroupContainer(this, link.RootNode, index, link.GetReportHeaderRowViewInfo(true), link.GetReportFooterRowViewInfo(true), pageBreaksInfo);
            (link as PrintableControlLink).Do<PrintableControlLink>(delegate (PrintableControlLink x) {
                this.rootNodeTreeWalkerDictionary[link.RootNode] = x.PrintableControl.GetCustomVisualTreeWalker();
            });
            return node;
        }

        public int GetTotalDetailCount() => 
            this.detailCount;

        void IDisposable.Dispose()
        {
            this.rootNodeTreeWalkerDictionary.Clear();
        }

        internal IVisualTreeWalker TryGetWalker(IDataNode node)
        {
            Func<IDataNode, IEnumerable<IDataNode>> getItems = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<IDataNode, IEnumerable<IDataNode>> local1 = <>c.<>9__10_0;
                getItems = <>c.<>9__10_0 = x => ((x is IRootDataNode) || (x.Parent == null)) ? Enumerable.Empty<IDataNode>() : x.Parent.Yield<IDataNode>();
            }
            IRootDataNode key = node.Yield<IDataNode>().Flatten<IDataNode>(getItems).OfType<IRootDataNode>().SingleOrDefault<IRootDataNode>();
            IVisualTreeWalker walker = null;
            this.rootNodeTreeWalkerDictionary.TryGetValue(key, out walker);
            return walker;
        }

        public CompositeLink Link { get; private set; }

        public int Index
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool IsDetailContainer =>
            false;

        public bool PageBreakAfter =>
            false;

        public bool PageBreakBefore =>
            false;

        public IDataNode Parent =>
            null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CompositeRootNode.<>c <>9 = new CompositeRootNode.<>c();
            public static Func<IDataNode, IEnumerable<IDataNode>> <>9__10_0;

            internal IEnumerable<IDataNode> <TryGetWalker>b__10_0(IDataNode x) => 
                ((x is IRootDataNode) || (x.Parent == null)) ? Enumerable.Empty<IDataNode>() : x.Parent.Yield<IDataNode>();
        }
    }
}

