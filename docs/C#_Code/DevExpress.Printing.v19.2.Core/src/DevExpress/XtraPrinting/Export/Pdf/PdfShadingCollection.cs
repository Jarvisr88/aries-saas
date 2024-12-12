namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PdfShadingCollection : PdfObjectCollection<PdfShading>
    {
        private bool compressed;

        public PdfShadingCollection(bool compressed)
        {
            this.compressed = compressed;
        }

        public PdfShading CreateAddUnique(Color startColor, Color endColor)
        {
            PdfShading shading3;
            using (IEnumerator<PdfShading> enumerator = base.List.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfShading current = enumerator.Current;
                        if (!(current.StartColor == startColor) || !(current.EndColor == endColor))
                        {
                            continue;
                        }
                        shading3 = current;
                    }
                    else
                    {
                        PdfShading pdfDocumentObject = new PdfShading(this.compressed, startColor, endColor, base.List.Count);
                        base.Add(pdfDocumentObject);
                        return pdfDocumentObject;
                    }
                    break;
                }
            }
            return shading3;
        }
    }
}

