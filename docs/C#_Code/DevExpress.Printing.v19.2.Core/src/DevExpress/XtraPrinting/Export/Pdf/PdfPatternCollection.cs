namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PdfPatternCollection : PdfObjectCollection<PdfPattern>
    {
        private bool compressed;

        public PdfPatternCollection(bool compressed)
        {
            this.compressed = compressed;
        }

        public PdfPattern CreateAddUnique(Color foreColor, Color backColor)
        {
            PdfPattern pattern3;
            using (IEnumerator<PdfPattern> enumerator = base.List.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfPattern current = enumerator.Current;
                        if (!(current.ForegroundColor == foreColor) || !(current.BackgroundColor == backColor))
                        {
                            continue;
                        }
                        pattern3 = current;
                    }
                    else
                    {
                        PdfPattern pdfDocumentObject = new PdfPattern(this.compressed, foreColor, backColor, base.List.Count);
                        base.Add(pdfDocumentObject);
                        return pdfDocumentObject;
                    }
                    break;
                }
            }
            return pattern3;
        }
    }
}

