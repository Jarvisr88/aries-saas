namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfFontHheaTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        internal const string EntryTag = "hhea";
        private readonly int version;
        private readonly short lineGap;
        private readonly short advanceWidthMax;
        private readonly short minLeftSideBearing;
        private readonly short minRightSideBearing;
        private readonly short xMaxExtent;
        private readonly short caretSlopeRise;
        private readonly short caretSlopeRun;
        private readonly short metricDataFormat;
        private readonly int numberOfHMetrics;
        private short ascender;
        private short descender;
        private bool shouldWrite;

        public PdfFontHheaTableDirectoryEntry(byte[] tableData) : base("hhea", tableData)
        {
            PdfBinaryStream tableStream = base.TableStream;
            this.version = tableStream.ReadInt();
            this.ascender = tableStream.ReadShort();
            this.descender = tableStream.ReadShort();
            this.lineGap = tableStream.ReadShort();
            this.advanceWidthMax = tableStream.ReadShort();
            this.minLeftSideBearing = tableStream.ReadShort();
            this.minRightSideBearing = tableStream.ReadShort();
            this.xMaxExtent = tableStream.ReadShort();
            this.caretSlopeRise = tableStream.ReadShort();
            this.caretSlopeRun = tableStream.ReadShort();
            tableStream.ReadShort();
            tableStream.ReadShort();
            tableStream.ReadShort();
            tableStream.ReadShort();
            tableStream.ReadShort();
            this.metricDataFormat = tableStream.ReadShort();
            this.numberOfHMetrics = tableStream.ReadUshort();
        }

        public PdfFontHheaTableDirectoryEntry(PdfFont font, int glyphCount, short ascent, short descent) : base("hhea")
        {
            this.version = 0x10000;
            PdfFontDescriptor fontDescriptor = font.FontDescriptor;
            this.ascender = ascent;
            this.descender = descent;
            double num = (fontDescriptor != null) ? Math.Abs(fontDescriptor.ItalicAngle) : 0.0;
            short num2 = (short) (this.ascender - this.descender);
            this.lineGap = (short) (1.2 * num2);
            double maxValue = double.MaxValue;
            double num4 = 0.0;
            foreach (double num5 in font.GlyphWidths)
            {
                if (num5 != 0.0)
                {
                    maxValue = Math.Min(num5, maxValue);
                    num4 = Math.Max(num5, num4);
                }
            }
            this.advanceWidthMax = (short) num4;
            this.minLeftSideBearing = 0;
            this.minRightSideBearing = 0;
            this.xMaxExtent = (short) maxValue;
            this.caretSlopeRise = (num == 0.0) ? ((short) 1) : Convert.ToInt16((double) (num2 * Math.Sin((90.0 - num) * 0.017453292519943295)));
            this.caretSlopeRun = Convert.ToInt16((double) (num2 * Math.Sin(num * 0.017453292519943295)));
            this.metricDataFormat = 0;
            this.numberOfHMetrics = glyphCount;
            this.shouldWrite = true;
        }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            if (this.shouldWrite)
            {
                PdfBinaryStream stream = base.CreateNewStream();
                stream.WriteInt(this.version);
                stream.WriteShort(this.ascender);
                stream.WriteShort(this.descender);
                stream.WriteShort(this.lineGap);
                stream.WriteShort(this.advanceWidthMax);
                stream.WriteShort(this.minLeftSideBearing);
                stream.WriteShort(this.minRightSideBearing);
                stream.WriteShort(this.xMaxExtent);
                stream.WriteShort(this.caretSlopeRise);
                stream.WriteShort(this.caretSlopeRun);
                stream.WriteShort(0);
                stream.WriteShort(0);
                stream.WriteShort(0);
                stream.WriteShort(0);
                stream.WriteShort(0);
                stream.WriteShort(this.metricDataFormat);
                stream.WriteShort((short) this.numberOfHMetrics);
            }
        }

        public void Validate()
        {
            if ((this.ascender == 0) && (this.descender == 0))
            {
                this.ascender = 1;
                this.descender = -1;
                this.shouldWrite = true;
            }
        }

        public int Version =>
            this.version;

        public short LineGap =>
            this.lineGap;

        public short AdvanceWidthMax =>
            this.advanceWidthMax;

        public short MinLeftSideBearing =>
            this.minLeftSideBearing;

        public short MinRightSideBearing =>
            this.minRightSideBearing;

        public short XMaxExtent =>
            this.xMaxExtent;

        public short CaretSlopeRise =>
            this.caretSlopeRise;

        public short CaretSlopeRun =>
            this.caretSlopeRun;

        public short MetricDataFormat =>
            this.metricDataFormat;

        public int NumberOfHMetrics =>
            this.numberOfHMetrics;

        public short Ascender =>
            this.ascender;

        public short Descender =>
            this.descender;
    }
}

