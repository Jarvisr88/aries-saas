namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.DataNodes;
    using DevExpress.XtraPrinting.Native;
    using System;

    internal class VisualDataNodeBandManager : DataNodeBandManager
    {
        private readonly DocumentBandInitializer bandInitializer;
        public readonly int totalDetailCount;

        public VisualDataNodeBandManager(IRootDataNode rootNode, DocumentBandInitializer bandInitializer) : base(rootNode)
        {
            Guard.ArgumentNotNull(bandInitializer, "bandInitializer");
            this.bandInitializer = bandInitializer;
            this.totalDetailCount = rootNode.GetTotalDetailCount();
        }

        protected override DocumentBand CreateDetailBand(IDataNode node)
        {
            DetailDocumentBand band = new DetailDocumentBand();
            IVisualDetailNode node1 = (IVisualDetailNode) node;
            this.bandInitializer.Initialize(band, new Func<bool, RowViewInfo>(node1.GetDetail), new int?(node.Index));
            band.Scale((double) base.rootBand.PrintingSystem.Document.ScaleFactor, null);
            if (this.totalDetailCount != -1)
            {
                ProgressReflector progressReflector = base.rootBand.PrintingSystem.ProgressReflector;
                progressReflector.RangeValue++;
            }
            return band;
        }

        protected override DocumentBand CreateGroupFooterBand(IGroupNode node)
        {
            DocumentBand band = new DocumentBand(DocumentBandKind.Footer);
            IVisualGroupNode node1 = (IVisualGroupNode) node;
            this.bandInitializer.Initialize(band, new Func<bool, RowViewInfo>(node1.GetFooter), new int?(node.Index));
            if (!(node is IVisualGroupNodeFixedFooter))
            {
                band.Scale((double) base.rootBand.PrintingSystem.Document.ScaleFactor, null);
                return band;
            }
            DocumentBand band2 = new DocumentBand(DocumentBandKind.Footer);
            band2.AddBand(band);
            DocumentBand band3 = new DocumentBand(DocumentBandKind.Footer);
            IVisualGroupNodeFixedFooter footer1 = (IVisualGroupNodeFixedFooter) node;
            this.bandInitializer.Initialize(band3, new Func<bool, RowViewInfo>(footer1.GetFixedFooter), new int?(node.Index));
            band2.AddBand(band3);
            band2.Scale((double) base.rootBand.PrintingSystem.Document.ScaleFactor, null);
            return band2;
        }

        protected override DocumentBand CreateGroupHeaderBand(IGroupNode node)
        {
            DocumentBand band = new DocumentBand(DocumentBandKind.Header);
            IVisualGroupNode node1 = (IVisualGroupNode) node;
            this.bandInitializer.Initialize(band, new Func<bool, RowViewInfo>(node1.GetHeader), new int?(node.Index));
            band.Scale((double) base.rootBand.PrintingSystem.Document.ScaleFactor, null);
            return band;
        }

        public override void Initialize(RootDocumentBand rootBand)
        {
            if (rootBand == null)
            {
                throw new ArgumentNullException("rootBand");
            }
            base.Initialize(rootBand);
            if (this.totalDetailCount != -1)
            {
                rootBand.PrintingSystem.ProgressReflector.InitializeRange(this.totalDetailCount);
            }
        }

        protected override void OnEndOfData()
        {
            base.OnEndOfData();
            base.rootBand.PrintingSystem.ProgressReflector.MaximizeRange();
        }
    }
}

