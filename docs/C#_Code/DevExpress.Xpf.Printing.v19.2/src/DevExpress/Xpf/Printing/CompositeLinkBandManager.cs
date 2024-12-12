namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting.DataNodes;
    using DevExpress.XtraPrinting.Native;
    using System;

    internal class CompositeLinkBandManager : VisualDataNodeBandManager
    {
        private readonly Action<IDataNode> assignActualTreeWalker;

        public CompositeLinkBandManager(IRootDataNode rootNode, DocumentBandInitializer bandInitializer, Action<IDataNode> assignActualTreeWalker) : base(rootNode, bandInitializer)
        {
            this.assignActualTreeWalker = assignActualTreeWalker;
        }

        protected override DocumentBand CreateDetailBand(IDataNode node)
        {
            this.assignActualTreeWalker(node);
            return base.CreateDetailBand(node);
        }
    }
}

