namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class PdfOutlineEntryCollection : CollectionBase
    {
        public int Add(PdfOutlineEntry item) => 
            base.List.Contains(item) ? this.IndexOf(item) : base.List.Add(item);

        public int IndexOf(PdfOutlineEntry item) => 
            base.InnerList.IndexOf(item);

        public PdfOutlineEntry this[int index] =>
            (PdfOutlineEntry) base.List[index];
    }
}

