namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfFontMetrics
    {
        private readonly double ascent;
        private readonly double descent;
        private readonly double lineSpacing;
        private readonly PdfRectangle emBBox;

        public PdfFontMetrics(PdfFontFile fontFile)
        {
            double num;
            PdfFontHeadTableDirectoryEntry head = fontFile.Head;
            if (head == null)
            {
                num = 2048.0;
                this.emBBox = new PdfRectangle(0.0, 0.0, 0.0, 0.0);
            }
            else
            {
                double num2 = 1000.0 / ((double) head.UnitsPerEm);
                this.emBBox = new PdfRectangle(Math.Round((double) (head.XMin * num2)), Math.Round((double) (head.YMin * num2)), Math.Round((double) (head.XMax * num2)), Math.Round((double) (head.YMax * num2)));
            }
            PdfFontOS2TableDirectoryEntry entry2 = fontFile.OS2;
            PdfFontHheaTableDirectoryEntry hhea = fontFile.Hhea;
            if (entry2 == null)
            {
                if (hhea != null)
                {
                    this.ascent = ((double) hhea.Ascender) / num;
                    this.descent = ((double) -hhea.Descender) / num;
                    this.lineSpacing = (this.ascent + this.descent) + (((double) hhea.LineGap) / num);
                }
            }
            else if (entry2.UseTypoMetrics)
            {
                this.ascent = ((double) entry2.TypoAscender) / num;
                this.descent = ((double) -entry2.TypoDescender) / num;
                this.lineSpacing = (this.ascent + this.descent) + (((double) entry2.TypoLineGap) / num);
            }
            else
            {
                this.ascent = ((double) entry2.WinAscent) / num;
                this.descent = ((double) entry2.WinDescent) / num;
                this.lineSpacing = this.ascent + this.descent;
                if (hhea != null)
                {
                    this.lineSpacing += PdfMathUtils.Max(0.0, ((double) (hhea.LineGap - ((entry2.WinAscent + entry2.WinDescent) - (hhea.Ascender - hhea.Descender)))) / num);
                }
            }
        }

        public PdfFontMetrics(PdfFontMetrics metrics, double lineSpacing)
        {
            this.ascent = metrics.ascent;
            this.descent = metrics.descent;
            this.lineSpacing = lineSpacing;
            this.emBBox = new PdfRectangle(0.0, 0.0, 0.0, 0.0);
        }

        public PdfFontMetrics(double ascent, double descent, double lineSpacing, double unitsPerEm)
        {
            this.ascent = ascent / unitsPerEm;
            this.descent = descent / unitsPerEm;
            this.lineSpacing = lineSpacing / unitsPerEm;
            this.emBBox = new PdfRectangle(0.0, 0.0, 0.0, 0.0);
        }

        public double GetAscent(double fontSize) => 
            this.ascent * fontSize;

        public double GetDescent(double fontSize) => 
            this.descent * fontSize;

        public double GetLineSpacing(double fontSize) => 
            this.lineSpacing * fontSize;

        public double EmAscent =>
            this.ascent * 1000.0;

        public double EmDescent =>
            this.descent * 1000.0;

        public PdfRectangle EmBBox =>
            this.emBBox;
    }
}

