namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RenderHistory
    {
        private Dictionary<int, Pair<int, int>> rowIndices;
        private HashSet<int> renderedDocumentBands;
        private bool reportHeaderRendered;
        private bool reportFooterRendered;
        private bool pageHeaderRendered;
        private bool pageIsUpdated;

        public RenderHistory();
        public RenderHistory(RenderHistory source);
        private void CollectRenderedDocumentBands(DocumentBand docBand);
        public Pair<int, int> GetDetailRowIndexes(DocumentBand docBand);
        public int GetLastDetailRowIndex(DocumentBand docBand);
        public bool IsDocumentBandRendered(DocumentBand docBand);
        public void Reset();
        public void UpdateDetailBandInfo(DocumentBand docBand);
        public void UpdateDocumentBandsInfo(DocumentBand docBand);
        public void UpdateRenderInfo(DocumentBand docBand, ProcessState processState);

        public bool PageIsUpdated { get; set; }

        public bool ReportHeaderRendered { get; set; }

        public bool ReportFooterRendered { get; set; }

        public bool PageHeaderRendered { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RenderHistory.<>c <>9;
            public static Func<DocumentBand, bool> <>9__25_0;

            static <>c();
            internal bool <UpdateDetailBandInfo>b__25_0(DocumentBand item);
        }
    }
}

