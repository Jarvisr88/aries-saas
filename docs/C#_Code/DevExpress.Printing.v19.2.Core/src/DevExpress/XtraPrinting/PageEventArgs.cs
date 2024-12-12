namespace DevExpress.XtraPrinting
{
    using System;
    using System.Collections.Generic;

    public class PageEventArgs : EventArgs
    {
        private DevExpress.XtraPrinting.Page page;
        private IList<DocumentBand> documentBands;

        internal PageEventArgs(DevExpress.XtraPrinting.Page page) : this(page, null)
        {
        }

        internal PageEventArgs(List<DocumentBand> documentBands) : this(null, documentBands)
        {
        }

        internal PageEventArgs(DevExpress.XtraPrinting.Page page, IList<DocumentBand> documentBands)
        {
            this.page = page;
            this.documentBands = documentBands;
        }

        public DevExpress.XtraPrinting.Page Page =>
            this.page;

        public IList<DocumentBand> DocumentBands =>
            this.documentBands;
    }
}

