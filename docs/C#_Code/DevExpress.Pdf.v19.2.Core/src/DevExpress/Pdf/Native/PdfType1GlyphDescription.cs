namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfType1GlyphDescription
    {
        private readonly IList<PdfType1GlyphSubpath> subpaths = new List<PdfType1GlyphSubpath>();
        private PdfType1GlyphSubpath currentSubpath = new PdfType1GlyphSubpath();
        private IList<PdfPoint> flexOperators = new List<PdfPoint>();
        private PdfPoint flexPoint;
        private bool flexDetectionStarted;

        public PdfType1GlyphDescription()
        {
            this.subpaths.Add(this.currentSubpath);
        }

        public void AddCallothersubrMark(int otherSubrIndex, IList<object> otherSubrParams)
        {
            if (this.flexDetectionStarted)
            {
                if ((otherSubrIndex == 0) && (this.flexOperators.Count == 7))
                {
                    this.currentSubpath.Add(PdfType1GlyphPathSegment.CreateFlexSegment(this.flexOperators));
                    this.flexDetectionStarted = false;
                    this.flexOperators.Clear();
                }
                else if (otherSubrIndex != 2)
                {
                    this.ClearFlexDetection();
                }
                else
                {
                    this.flexOperators.Add(this.flexPoint);
                    this.flexPoint = new PdfPoint();
                }
            }
            if (otherSubrIndex == 1)
            {
                this.flexDetectionStarted = true;
            }
        }

        public void AddLineTo(double dx, double dy)
        {
            if (this.flexDetectionStarted)
            {
                this.ClearFlexDetection();
            }
            this.currentSubpath.Add(PdfType1GlyphPathSegment.CreateLineTo(dx, dy));
        }

        public void AddMoveTo(double dx, double dy)
        {
            if (this.flexDetectionStarted)
            {
                this.flexPoint = PdfPoint.Add(this.flexPoint, new PdfPoint(dx, dy));
            }
            else
            {
                this.currentSubpath.Add(PdfType1GlyphPathSegment.CreateMoveTo(dx, dy));
            }
        }

        public void AddRRCurveTo(double dx1, double dy1, double dx2, double dy2, double dx3, double dy3)
        {
            if (this.flexDetectionStarted)
            {
                this.ClearFlexDetection();
            }
            this.currentSubpath.Add(PdfType1GlyphPathSegment.CreateCurveTo(dx1, dy1, dx2, dy2, dx3, dy3));
        }

        private PdfPoint AppendSubpaths(PdfType2CharstringBinaryWriter charstringStream, IDictionary<string, PdfType1GlyphDescription> fontGlyphDescriptions, PdfPoint currentPoint)
        {
            foreach (PdfType1GlyphSubpath subpath in this.subpaths)
            {
                subpath.Write(charstringStream, fontGlyphDescriptions);
                currentPoint = subpath.MovePoint(currentPoint);
            }
            return currentPoint;
        }

        private void ClearFlexDetection()
        {
            foreach (PdfPoint point in this.flexOperators)
            {
                this.currentSubpath.Add(PdfType1GlyphPathSegment.CreateMoveTo(point.X, point.Y));
            }
            if ((this.flexPoint.X != 0.0) || (this.flexPoint.Y != 0.0))
            {
                this.currentSubpath.Add(PdfType1GlyphPathSegment.CreateMoveTo(this.flexPoint.X, this.flexPoint.Y));
            }
            this.flexOperators.Clear();
            this.flexDetectionStarted = false;
        }

        public byte[] ConvertToType2Charstring(IDictionary<string, PdfType1GlyphDescription> fontGlyphDescriptions)
        {
            using (PdfType2CharstringBinaryWriter writer = new PdfType2CharstringBinaryWriter())
            {
                writer.WriteDouble(this.Width);
                PdfPoint sidebearing = this.Sidebearing;
                PdfType1GlyphPathSegment.CreateMoveTo(sidebearing.X, sidebearing.Y).Write(writer, fontGlyphDescriptions);
                sidebearing = this.AppendSubpaths(writer, fontGlyphDescriptions, sidebearing);
                PdfType1StandardAccentedGlyphInfo seacInfo = this.SeacInfo;
                if (seacInfo != null)
                {
                    PdfType1GlyphDescription standardEncodingGlyph = this.GetStandardEncodingGlyph(seacInfo.BaseGlyph, fontGlyphDescriptions);
                    PdfType1GlyphDescription description2 = this.GetStandardEncodingGlyph(seacInfo.AccentGlyph, fontGlyphDescriptions);
                    if (standardEncodingGlyph != null)
                    {
                        sidebearing = standardEncodingGlyph.AppendSubpaths(writer, fontGlyphDescriptions, sidebearing);
                    }
                    if (description2 != null)
                    {
                        PdfPoint point2 = PdfPoint.Add(PdfPoint.Add(new PdfPoint(-sidebearing.X, -sidebearing.Y), this.Sidebearing), seacInfo.AccentDelta);
                        PdfType1GlyphPathSegment.CreateMoveTo(point2.X, point2.Y).Write(writer, fontGlyphDescriptions);
                        PdfPoint currentPoint = new PdfPoint();
                        description2.AppendSubpaths(writer, fontGlyphDescriptions, currentPoint);
                    }
                }
                writer.WriteByte(14);
                return writer.Data;
            }
        }

        public void CreateNewSubpath()
        {
            this.ClearFlexDetection();
            this.currentSubpath = new PdfType1GlyphSubpath();
            this.subpaths.Add(this.currentSubpath);
        }

        private PdfType1GlyphDescription GetStandardEncodingGlyph(int code, IDictionary<string, PdfType1GlyphDescription> fontGlyphDescriptions)
        {
            string str;
            PdfType1GlyphDescription description;
            return ((!PdfType1FontPredefinedEncoding.StandardEncoding.TryGetValue((byte) code, out str) || !fontGlyphDescriptions.TryGetValue(str, out description)) ? null : description);
        }

        public double Width { get; set; }

        public double Height { get; set; }

        public PdfPoint Sidebearing { get; set; }

        public PdfType1StandardAccentedGlyphInfo SeacInfo { get; set; }
    }
}

