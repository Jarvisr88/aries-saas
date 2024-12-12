namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    public class PdfOutlineEntry : PdfOutlineItem
    {
        private string title;
        private DestinationInfo destInfo;
        private PdfDestination dest;

        public PdfOutlineEntry(PdfOutlineItem parent, string title, DestinationInfo destInfo, bool compressed) : base(parent, compressed)
        {
            this.title = string.Empty;
            this.destInfo = destInfo;
            this.title = title;
        }

        public override void FillUp()
        {
            if (this.dest != null)
            {
                base.Dictionary.Add("Title", new PdfTextUnicode(this.title));
                base.Dictionary.Add("Dest", this.dest);
                if (this.Prev != null)
                {
                    base.Dictionary.Add("Prev", this.Prev.InnerObject);
                }
                if (this.Next != null)
                {
                    base.Dictionary.Add("Next", this.Next.InnerObject);
                }
                if (base.Parent != null)
                {
                    base.Dictionary.Add("Parent", base.Parent.InnerObject);
                }
            }
            base.FillUp();
        }

        private PdfOutlineEntry GetOutlineEntry(int index) => 
            ((base.Parent == null) || ((index < 0) || (index >= base.Parent.Entries.Count))) ? null : base.Parent.Entries[index];

        public void SetDestination(PdfDestination dest)
        {
            this.dest = dest;
        }

        public int DestPageIndex =>
            (this.destInfo != null) ? this.destInfo.DestPageIndex : -1;

        public float DestTop =>
            (this.destInfo != null) ? this.destInfo.DestTop : 0f;

        public int Index =>
            (base.Parent != null) ? base.Parent.Entries.IndexOf(this) : -1;

        public PdfOutlineEntry Next =>
            this.GetOutlineEntry(this.Index + 1);

        public PdfOutlineEntry Prev =>
            this.GetOutlineEntry(this.Index - 1);
    }
}

