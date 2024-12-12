namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PdfBehaviorProvider : BehaviorProvider
    {
        internal double GetMaxZoomFactor() => 
            ((IEnumerable<double>) this.GetZoomFactors()).Max();

        internal double GetMinZoomFactor() => 
            ((IEnumerable<double>) this.GetZoomFactors()).Min();

        public int CurrentPageNumber =>
            base.PageIndex + 1;
    }
}

