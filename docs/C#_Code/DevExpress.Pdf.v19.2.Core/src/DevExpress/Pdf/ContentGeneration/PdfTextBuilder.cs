namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfTextBuilder
    {
        private readonly PdfExportFont font;
        private readonly double pointToPdfFontUnitsFactor;
        private readonly Dictionary<int, PdfExportFontGlyphInfo> glyphMapping;
        private readonly PdfTextWriter writer;
        private double currentPosition;
        private double currentActualPosition;
        private double previousVerticalOffset;
        private double additionalHorizontalOffset;

        public PdfTextBuilder(PdfCommandConstructor commandConstructor, PdfExportFontInfo fontInfo)
        {
            this.<CommandConstructor>k__BackingField = commandConstructor;
            this.font = fontInfo.Font;
            this.pointToPdfFontUnitsFactor = 1000f / fontInfo.FontSize;
            this.glyphMapping = new Dictionary<int, PdfExportFontGlyphInfo>();
            this.writer = commandConstructor.CreateTextWriter(this.font.UseTwoByteCodePoints);
        }

        private void Append(DXCluster cluster)
        {
            IList<DXGlyph> glyphs = cluster.Glyphs;
            int count = glyphs.Count;
            string text = null;
            for (int i = 0; i < count; i++)
            {
                float width;
                DXGlyph glyph = glyphs[i];
                ushort index = glyph.Index;
                if (index == 0)
                {
                    width = 0f;
                }
                else
                {
                    PdfExportFontGlyphInfo info;
                    if (this.glyphMapping.TryGetValue(index, out info))
                    {
                        width = info.Width;
                    }
                    else
                    {
                        string str2;
                        if ((count == 1) && (cluster.Text.Length == 1))
                        {
                            str2 = cluster.Text.GetText();
                        }
                        else
                        {
                            text ??= PdfTextUtils.NormalizeAndDecompose(cluster.Text.GetText());
                            str2 = GetUnicode(i, glyphs.Count, text, cluster);
                        }
                        width = this.font.GetGlyphWidth(index);
                        this.glyphMapping.Add(index, new PdfExportFontGlyphInfo(str2, width));
                    }
                }
                this.AppendGlyph(glyph, width);
            }
        }

        public void Append(IList<DXCluster> line)
        {
            foreach (DXCluster cluster in line)
            {
                this.Append(cluster);
            }
            this.writer.EndText();
            this.ResetPositions();
        }

        private void AppendGlyph(DXGlyph glyph, float glyphWidth)
        {
            ushort index = glyph.Index;
            if (index != 0)
            {
                DXGlyphOffset offset = glyph.Offset;
                float verticalOffset = offset.VerticalOffset;
                if (verticalOffset != this.previousVerticalOffset)
                {
                    this.writer.EndText();
                    double xOffset = this.currentActualPosition / this.pointToPdfFontUnitsFactor;
                    this.additionalHorizontalOffset += xOffset;
                    this.StartNextLineCore(xOffset, verticalOffset - this.previousVerticalOffset);
                    this.ResetPositions();
                    this.previousVerticalOffset = verticalOffset;
                }
                double num3 = this.currentActualPosition + (offset.HorizontalOffset * this.pointToPdfFontUnitsFactor);
                if (this.currentPosition != num3)
                {
                    double num5 = this.currentPosition - num3;
                    this.writer.AppendOffset((float) num5);
                    this.currentPosition -= num5;
                }
                this.writer.AppendGlyph(index);
            }
            this.currentActualPosition += glyph.Advance * this.pointToPdfFontUnitsFactor;
            this.currentPosition += glyphWidth;
        }

        private static string GetUnicode(int index, int glyphCount, string text, DXCluster cluster)
        {
            if (text.Length != glyphCount)
            {
                return ((index != 0) ? "" : text);
            }
            if ((cluster.BidiLevel % 2) != 0)
            {
                index = (text.Length - index) - 1;
            }
            return new string(text[index], 1);
        }

        private void ResetPositions()
        {
            this.currentPosition = 0.0;
            this.currentActualPosition = 0.0;
        }

        public void StartNextLine(double xOffset, double yOffset)
        {
            this.StartNextLineCore(xOffset - this.additionalHorizontalOffset, yOffset - this.previousVerticalOffset);
            this.additionalHorizontalOffset = 0.0;
            this.previousVerticalOffset = 0.0;
        }

        protected virtual void StartNextLineCore(double xOffset, double yOffset)
        {
            this.CommandConstructor.StartTextLineWithOffsets(xOffset, yOffset);
        }

        protected PdfCommandConstructor CommandConstructor { get; }

        public IDictionary<int, PdfExportFontGlyphInfo> Subset =>
            this.glyphMapping;
    }
}

