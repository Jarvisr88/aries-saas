namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class CompositeLinkGroupContainer : IVisualGroupNode, IGroupNode, IDataNode
    {
        private readonly IDataNode wrapperNode;
        private readonly int index;
        private readonly RowViewInfo header;
        private readonly RowViewInfo footer;
        private readonly PageBreakInfo pageBreaksInfo;

        public CompositeLinkGroupContainer(CompositeRootNode parent, IDataNode wrapperNode, int index, RowViewInfo headerViewInfo, RowViewInfo footerViewInfo, PageBreakInfo pageBreaksInfo = null)
        {
            this.Parent = parent;
            this.wrapperNode = wrapperNode;
            this.index = index;
            this.header = headerViewInfo;
            this.footer = footerViewInfo;
            this.pageBreaksInfo = pageBreaksInfo;
        }

        public bool CanGetChild(int index) => 
            this.wrapperNode.CanGetChild(index);

        public IDataNode GetChild(int index) => 
            this.wrapperNode.GetChild(index);

        public RowViewInfo GetFooter(bool allowContentReuse) => 
            this.footer;

        public RowViewInfo GetHeader(bool allowContentReuse) => 
            this.header;

        public bool RepeatHeaderEveryPage =>
            false;

        public GroupUnion Union =>
            GroupUnion.None;

        public int Index =>
            this.index;

        public bool IsDetailContainer =>
            this.wrapperNode.IsDetailContainer;

        public bool PageBreakAfter
        {
            get
            {
                Func<PageBreakInfo, bool> evaluator = <>c.<>9__19_0;
                if (<>c.<>9__19_0 == null)
                {
                    Func<PageBreakInfo, bool> local1 = <>c.<>9__19_0;
                    evaluator = <>c.<>9__19_0 = x => x.PageBreakAfter;
                }
                return this.pageBreaksInfo.Return<PageBreakInfo, bool>(evaluator, (<>c.<>9__19_1 ??= () => false));
            }
        }

        public bool PageBreakBefore
        {
            get
            {
                Func<PageBreakInfo, bool> evaluator = <>c.<>9__21_0;
                if (<>c.<>9__21_0 == null)
                {
                    Func<PageBreakInfo, bool> local1 = <>c.<>9__21_0;
                    evaluator = <>c.<>9__21_0 = x => x.PageBreakBefore;
                }
                return this.pageBreaksInfo.Return<PageBreakInfo, bool>(evaluator, (<>c.<>9__21_1 ??= () => false));
            }
        }

        public IDataNode Parent { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CompositeLinkGroupContainer.<>c <>9 = new CompositeLinkGroupContainer.<>c();
            public static Func<PageBreakInfo, bool> <>9__19_0;
            public static Func<bool> <>9__19_1;
            public static Func<PageBreakInfo, bool> <>9__21_0;
            public static Func<bool> <>9__21_1;

            internal bool <get_PageBreakAfter>b__19_0(PageBreakInfo x) => 
                x.PageBreakAfter;

            internal bool <get_PageBreakAfter>b__19_1() => 
                false;

            internal bool <get_PageBreakBefore>b__21_0(PageBreakInfo x) => 
                x.PageBreakBefore;

            internal bool <get_PageBreakBefore>b__21_1() => 
                false;
        }
    }
}

