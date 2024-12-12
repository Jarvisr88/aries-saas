namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;

    public class PdfTransparencyGSCollection : PdfObjectCollection<PdfTransparencyGS>
    {
        private bool compressed;

        public PdfTransparencyGSCollection(bool compressed)
        {
            this.compressed = compressed;
        }

        public PdfTransparencyGS CreateAddUnique(int transparency)
        {
            PdfTransparencyGS ygs3;
            using (IEnumerator<PdfTransparencyGS> enumerator = base.List.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfTransparencyGS current = enumerator.Current;
                        if (current.Transparency != transparency)
                        {
                            continue;
                        }
                        ygs3 = current;
                    }
                    else
                    {
                        PdfTransparencyGS pdfDocumentObject = new PdfTransparencyGS(transparency, this.compressed);
                        base.Add(pdfDocumentObject);
                        return pdfDocumentObject;
                    }
                    break;
                }
            }
            return ygs3;
        }
    }
}

