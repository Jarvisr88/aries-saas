namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfType1GlyphSubpath
    {
        private readonly IList<PdfType1GlyphPathSegment> segments = new List<PdfType1GlyphPathSegment>();

        public void Add(PdfType1GlyphPathSegment segment)
        {
            this.segments.Add(segment);
        }

        public PdfPoint MovePoint(PdfPoint point)
        {
            foreach (PdfType1GlyphPathSegment segment in this.segments)
            {
                point = segment.MovePoint(point);
            }
            return point;
        }

        public void Write(PdfType2CharstringBinaryWriter stream, IDictionary<string, PdfType1GlyphDescription> fontGlyphDescriptions)
        {
            foreach (PdfType1GlyphPathSegment segment in this.segments)
            {
                segment.Write(stream, fontGlyphDescriptions);
            }
        }

        public bool IsEmpty =>
            this.segments.Count == 0;
    }
}

