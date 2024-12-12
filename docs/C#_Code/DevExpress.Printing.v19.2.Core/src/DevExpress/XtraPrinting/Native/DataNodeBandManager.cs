namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.DataNodes;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public abstract class DataNodeBandManager : IBandManager
    {
        protected IRootDataNode rootNode;
        protected RootDocumentBand rootBand;
        private bool endOfDataWasReached;
        private Dictionary<DocumentBand, IDataNode> nodes;

        public DataNodeBandManager(IRootDataNode rootNode);
        protected abstract DocumentBand CreateDetailBand(IDataNode node);
        protected abstract DocumentBand CreateGroupFooterBand(IGroupNode node);
        protected abstract DocumentBand CreateGroupHeaderBand(IGroupNode node);
        bool IBandManager.TryCollectFriends(DocumentBand docBand, out DocumentBand result);
        public void EnsureGroupFooter(DocumentBand documentBand);
        public void EnsureReportFooterBand(DocumentBand documentBand);
        public DocumentBand GetBand(DocumentBand parentBand, PageBuildInfo pageBuildInfo);
        private static int GetDetailLevel(IDataNode dataNode, int level);
        private DocumentBand GetNodeBand(DocumentBand parentBand, IDataNode parentNode, int bandIndex);
        public DocumentBand GetPageFooterBand(DocumentBand band);
        private DocumentBand GetRootNodeBand(DocumentBand parentBand, IDataNode parentNode, int index);
        public virtual void Initialize(RootDocumentBand rootBand);
        private void InsertDetailBand(DocumentBand parentBand, IDataNode parentNode, int bandIndex, int nodeIndex);
        private void InsertDocumentBandContainer(DocumentBand parentBand, IDataNode parentNode, int bandIndex, int nodeIndex);
        protected virtual void OnEndOfData();
        private DocumentBand SafeGetBand(IListWrapper<DocumentBand> bands, int index);

        PrintingSystemBase IBandManager.PrintingSystem { get; }

        bool IBandManager.IsCompleted { get; }
    }
}

