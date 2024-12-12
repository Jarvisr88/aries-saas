namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Reflection;

    public class PdfPages : PdfPageTreeItem
    {
        private ArrayList pages;

        public PdfPages(PdfPageTreeItem parent, bool compressed) : base(parent, compressed)
        {
            this.pages = new ArrayList();
        }

        public void Clear()
        {
            this.pages.Clear();
        }

        public PdfPage CreatePage()
        {
            PdfPage page = new PdfPage(this, base.Compressed);
            this.pages.Add(page);
            return page;
        }

        private PdfArray CreatePdfArray()
        {
            PdfArray array = new PdfArray();
            for (int i = 0; i < this.Count; i++)
            {
                array.Add(this[i].Dictionary);
            }
            return array;
        }

        public override void FillUp()
        {
            base.Dictionary.Add("Type", new PdfName("Pages"));
            base.Dictionary.Add("Kids", this.CreatePdfArray());
            base.Dictionary.Add("Count", new PdfNumber(this.LeafCount));
            base.FillUp();
            for (int i = 0; i < this.Count; i++)
            {
                this[i].FillUp();
            }
        }

        public PdfPage GetPage(ref int index)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i] is PdfPage)
                {
                    index--;
                    if (index < 0)
                    {
                        return (this[i] as PdfPage);
                    }
                }
                else
                {
                    PdfPage page = ((PdfPages) this[i]).GetPage(ref index);
                    if (page != null)
                    {
                        return page;
                    }
                }
            }
            return null;
        }

        protected override void RegisterContent(PdfXRef xRef)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Register(xRef);
            }
        }

        protected override void WriteContent(StreamWriter writer)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Write(writer);
            }
        }

        public PdfPageTreeItem this[int index] =>
            this.pages[index] as PdfPageTreeItem;

        public int Count =>
            this.pages.Count;

        public int LeafCount
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    num = !(this[i] is PdfPages) ? (num + 1) : (num + ((PdfPages) this[i]).LeafCount);
                }
                return num;
            }
        }
    }
}

