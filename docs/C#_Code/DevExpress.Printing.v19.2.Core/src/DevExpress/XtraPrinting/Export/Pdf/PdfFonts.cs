namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.IO;
    using System.Reflection;

    public class PdfFonts
    {
        private ArrayList list = new ArrayList();

        private PdfFont Add(PdfFont pdfFont)
        {
            this.list.Add(pdfFont);
            pdfFont.SetName("F" + Convert.ToString(this.Count));
            return pdfFont;
        }

        public void AddUnique(PdfFont pdfFont)
        {
            if (!this.list.Contains(pdfFont))
            {
                this.Add(pdfFont);
            }
        }

        internal static PdfFont CreatePdfFont(Font font, bool compressed) => 
            (!SecurityHelper.IsUnmanagedCodeGrantedAndHasZeroHwnd || AzureCompatibility.Enable) ? new PartialTrustPdfFont(font, compressed) : new PdfFont(font, compressed);

        public void DisposeAndClear()
        {
            foreach (PdfFont font in this.list)
            {
                font.Dispose();
            }
            this.list.Clear();
        }

        public void FillUp()
        {
            foreach (PdfFont font in this.list)
            {
                font.FillUp();
            }
        }

        public PdfFont FindFont(Font font)
        {
            PdfFont font3;
            using (IEnumerator enumerator = this.list.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfFont current = (PdfFont) enumerator.Current;
                        if (!current.Font.Equals(font) && !PdfFontUtils.FontEquals(current.Font, font))
                        {
                            continue;
                        }
                        font3 = current;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return font3;
        }

        public void Register(PdfXRef xRef)
        {
            foreach (PdfFont font in this.list)
            {
                font.Register(xRef);
            }
        }

        public PdfFont RegisterFont(Font font, bool compressed)
        {
            PdfFont font2 = this.FindFont(font);
            return ((font2 == null) ? this.Add(CreatePdfFont(font, compressed)) : font2);
        }

        public void Write(StreamWriter writer)
        {
            foreach (PdfFont font in this.list)
            {
                font.Write(writer);
            }
        }

        public int Count =>
            this.list.Count;

        public PdfFont this[int index] =>
            this.list[index] as PdfFont;
    }
}

