namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Text.Fonts;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfExportFontInfo
    {
        public PdfExportFontInfo(PdfExportFont font, float fontSize) : this(font, fontSize, DXFontDecorations.None)
        {
        }

        public PdfExportFontInfo(PdfExportFont font, float fontSize, DXFontDecorations decorations)
        {
            this.<Font>k__BackingField = font;
            this.<FontSize>k__BackingField = fontSize;
            this.<Decorations>k__BackingField = decorations;
        }

        public PdfExportFont Font { get; }

        public float FontSize { get; }

        public DXFontDecorations Decorations { get; }

        public double FontLineSize =>
            ((double) this.FontSize) / 14.0;

        public bool ShouldSetStrokingColor =>
            (this.Decorations != DXFontDecorations.None) || this.Font.Simulations.HasFlag(DXFontSimulations.Bold);
    }
}

