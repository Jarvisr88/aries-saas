namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfHatchPatternCache
    {
        private readonly IDictionary<Tuple<int, int, DXHatchStyle, PdfRectangle>, PdfPattern> cache = new Dictionary<Tuple<int, int, DXHatchStyle, PdfRectangle>, PdfPattern>();
        private readonly PdfDocumentCatalog documentCatalog;

        public PdfHatchPatternCache(PdfDocumentCatalog documentCatalog)
        {
            this.documentCatalog = documentCatalog;
        }

        public PdfPattern GetPattern(DXHatchBrush brush, PdfRectangle bBox)
        {
            PdfPattern pattern;
            Tuple<int, int, DXHatchStyle, PdfRectangle> key = new Tuple<int, int, DXHatchStyle, PdfRectangle>(brush.BackgroundColor.ToArgb(), brush.ForegroundColor.ToArgb(), brush.HatchStyle, bBox);
            if (!this.cache.TryGetValue(key, out pattern))
            {
                pattern = PdfHatchPatternConstructor.Create(brush).CreatePattern(bBox, this.documentCatalog);
                this.cache.Add(key, pattern);
            }
            return pattern;
        }
    }
}

