namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration.Extensions;
    using DevExpress.Pdf.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfExportFontDescriptorBuilder : IPdfFontDescriptorBuilder
    {
        public PdfExportFontDescriptorBuilder(IPdfExportPlatformFont platformFont)
        {
            PdfFontMetrics metrics = platformFont.Metrics;
            this.<BBox>k__BackingField = metrics.EmBBox;
            this.<Ascent>k__BackingField = metrics.EmAscent;
            this.<Descent>k__BackingField = -metrics.EmDescent;
            this.<Flags>k__BackingField = platformFont.Descriptor.IsSymbolFont() ? PdfFontFlags.Symbolic : PdfFontFlags.Nonsymbolic;
            this.<Bold>k__BackingField = platformFont.Descriptor.IsBold();
            PdfFontFile fontFile = platformFont.FontFile;
            if (fontFile != null)
            {
                PdfPanose panose1;
                PdfFontPostTableDirectoryEntry post = fontFile.Post;
                this.<ItalicAngle>k__BackingField = (post == null) ? ((double) 0f) : ((double) post.ItalicAngle);
                PdfFontOS2TableDirectoryEntry entry2 = fontFile.OS2;
                if (entry2 != null)
                {
                    panose1 = entry2.Panose;
                }
                else
                {
                    panose1 = new PdfPanose();
                }
                PdfPanose panose = panose1;
                if (panose.Proportion == PdfPanoseProportion.Monospaced)
                {
                    this.<Flags>k__BackingField = this.Flags | PdfFontFlags.FixedPitch;
                }
                PdfPanoseSerifStyle serifStyle = panose.SerifStyle;
                if ((serifStyle != PdfPanoseSerifStyle.NormalSans) && ((serifStyle != PdfPanoseSerifStyle.ObtuseSans) && (serifStyle != PdfPanoseSerifStyle.PerpendicularSans)))
                {
                    this.<Flags>k__BackingField = this.Flags | PdfFontFlags.Serif;
                }
                if (panose.FamilyKind == PdfPanoseFamilyKind.LatinHandWritten)
                {
                    this.<Flags>k__BackingField = this.Flags | PdfFontFlags.Script;
                }
                if (platformFont.Descriptor.IsItalic())
                {
                    this.<Flags>k__BackingField = this.Flags | PdfFontFlags.Italic;
                }
                PdfFontMaxpTableDirectoryEntry maxp = fontFile.Maxp;
                if (maxp != null)
                {
                    this.<NumGlyphs>k__BackingField = maxp.NumGlyphs;
                }
                else
                {
                    PdfTrueTypeLocaTableDirectoryEntry loca = fontFile.Loca;
                    this.<NumGlyphs>k__BackingField = (loca != null) ? Math.Max(0, loca.GlyphOffsets.Length - 1) : 0;
                }
            }
        }

        public string FontFamily { get; }

        public double Ascent { get; }

        public double Descent { get; }

        public bool Bold { get; }

        public PdfRectangle BBox { get; }

        public int NumGlyphs { get; }

        public double XHeight { get; }

        public double StemV { get; }

        public double StemH { get; }

        public PdfFontFlags Flags { get; } = PdfFontFlags.Nonsymbolic

        public double ItalicAngle { get; }

        public double CapHeight { get; } = 500.0
    }
}

