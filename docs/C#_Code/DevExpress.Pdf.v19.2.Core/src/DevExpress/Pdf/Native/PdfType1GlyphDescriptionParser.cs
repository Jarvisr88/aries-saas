namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfType1GlyphDescriptionParser : PdfType1CharstringInterpreter
    {
        private readonly PdfType1GlyphDescription glyphDescription;

        private PdfType1GlyphDescriptionParser(IList<PdfType1CharstringSubroutine> fontSubroutines) : base(fontSubroutines)
        {
            this.glyphDescription = new PdfType1GlyphDescription();
        }

        protected override void CallOtherSubr(int index, IList<object> parameters)
        {
            this.glyphDescription.AddCallothersubrMark(index, parameters);
        }

        public override void ClosePath()
        {
            this.glyphDescription.CreateNewSubpath();
        }

        public static PdfType1GlyphDescription ParseDescription(PdfType1FontClassicFontProgram program, byte[] charstring)
        {
            PdfType1GlyphDescriptionParser parser = new PdfType1GlyphDescriptionParser(program.GetSubroutineArray());
            parser.Execute(PdfType1CharstringParser.Parse(charstring));
            return parser.GlyphDescription;
        }

        public override void RelativeCurveTo(double dx1, double dy1, double dx2, double dy2, double dx3, double dy3)
        {
            this.glyphDescription.AddRRCurveTo(dx1, dy1, dx2, dy2, dx3, dy3);
        }

        public override void RelativeLineTo(double dx, double dy)
        {
            this.glyphDescription.AddLineTo(dx, dy);
        }

        public override void RelativeMoveTo(double dx, double dy)
        {
            this.glyphDescription.AddMoveTo(dx, dy);
        }

        public override void Seac(double asb, double adx, double ady, int bchar, int achar)
        {
            this.glyphDescription.SeacInfo = new PdfType1StandardAccentedGlyphInfo(bchar, achar, new PdfPoint(adx, ady), asb);
        }

        public override void SetSidebearing(double sbx, double sby, double wx, double wy)
        {
            this.glyphDescription.Sidebearing = new PdfPoint(sbx, sby);
            this.glyphDescription.Width = wx;
            this.glyphDescription.Height = wy;
        }

        public PdfType1GlyphDescription GlyphDescription =>
            this.glyphDescription;
    }
}

