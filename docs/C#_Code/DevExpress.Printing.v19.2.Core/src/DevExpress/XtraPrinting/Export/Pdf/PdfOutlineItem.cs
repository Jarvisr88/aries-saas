namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.IO;

    public class PdfOutlineItem : PdfDocumentDictionaryObject
    {
        private PdfOutlineItem parent;
        private PdfOutlineEntryCollection entries;

        public PdfOutlineItem(PdfOutlineItem parent, bool compressed) : base(compressed)
        {
            this.entries = new PdfOutlineEntryCollection();
            this.parent = parent;
        }

        public override void FillUp()
        {
            if (this.Entries.Count > 0)
            {
                base.Dictionary.Add("Count", this.Entries.Count);
                base.Dictionary.Add("First", this.First.InnerObject);
                base.Dictionary.Add("Last", this.Last.InnerObject);
            }
            foreach (PdfOutlineEntry entry in this.entries)
            {
                entry.FillUp();
            }
            base.FillUp();
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            base.RegisterContent(xRef);
            foreach (PdfOutlineEntry entry in this.entries)
            {
                entry.Register(xRef);
            }
        }

        protected override void WriteContent(StreamWriter writer)
        {
            for (int i = 0; i < this.Entries.Count; i++)
            {
                this.Entries[i].Write(writer);
            }
        }

        public PdfOutlineItem Parent =>
            this.parent;

        public PdfOutlineEntryCollection Entries =>
            this.entries;

        public PdfOutlineEntry First =>
            (this.entries.Count > 0) ? this.entries[0] : null;

        public PdfOutlineEntry Last =>
            (this.entries.Count > 0) ? this.entries[this.entries.Count - 1] : null;
    }
}

