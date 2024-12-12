namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfShadingCache
    {
        private static readonly PdfRange defaultRange = new PdfRange(0.0, 1.0);
        private readonly IDictionary<ulong, PdfAxialShading> cache = new Dictionary<ulong, PdfAxialShading>();
        private readonly PdfDocumentCatalog documentCatalog;

        public PdfShadingCache(PdfDocumentCatalog documentCatalog)
        {
            this.documentCatalog = documentCatalog;
        }

        public PdfAxialShading GetShading(ARGBColor startColor, ARGBColor endColor)
        {
            PdfAxialShading shading;
            ulong key = (ulong) ((startColor.ToArgb() << 0x20) + endColor.ToArgb());
            if (!this.cache.TryGetValue(key, out shading))
            {
                PdfObjectList<PdfCustomFunction> blendFunctions = new PdfObjectList<PdfCustomFunction>(this.documentCatalog.Objects);
                PdfRange[] domain = new PdfRange[] { defaultRange };
                PdfRange[] range = new PdfRange[] { defaultRange, defaultRange, defaultRange };
                blendFunctions.Add(new PdfExponentialInterpolationFunction(PdfGraphicsConverter.ConvertColor(startColor).Components, PdfGraphicsConverter.ConvertColor(endColor).Components, 1.0, domain, range));
                shading = new PdfAxialShading(new PdfPoint(0.0, 0.0), new PdfPoint(1.0, 0.0), blendFunctions);
                this.cache.Add(key, shading);
            }
            return shading;
        }
    }
}

