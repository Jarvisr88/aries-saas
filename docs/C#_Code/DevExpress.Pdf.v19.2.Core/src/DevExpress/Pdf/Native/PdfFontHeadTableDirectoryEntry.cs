namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfFontHeadTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        internal const string EntryTag = "head";
        private static readonly DateTime minDateTime = new DateTime(0x770, 1, 1);
        private readonly int version;
        private readonly int fontRevision;
        private readonly int checkSumAdjustment;
        private readonly int magicNumber;
        private readonly PdfFontHeadTableDirectoryEntryFlags flags;
        private readonly short unitsPerEm;
        private readonly long created;
        private readonly long modified;
        private readonly PdfFontHeadTableDirectoryEntryMacStyle macStyle;
        private readonly short lowestRecPPEM;
        private readonly PdfFontDirectionHint fontDirectionHint;
        private readonly PdfIndexToLocFormat indexToLocFormat;
        private readonly short glyphDataFormat;
        private short xMin;
        private short yMin;
        private short xMax;
        private short yMax;
        private bool shouldWrite;

        public PdfFontHeadTableDirectoryEntry(byte[] tableData) : base("head", tableData)
        {
            PdfBinaryStream tableStream = base.TableStream;
            this.version = tableStream.ReadInt();
            this.fontRevision = tableStream.ReadInt();
            this.checkSumAdjustment = tableStream.ReadInt();
            this.magicNumber = tableStream.ReadInt();
            this.flags = (PdfFontHeadTableDirectoryEntryFlags) tableStream.ReadShort();
            this.unitsPerEm = tableStream.ReadShort();
            this.created = tableStream.ReadLong();
            this.modified = tableStream.ReadLong();
            this.xMin = tableStream.ReadShort();
            this.yMin = tableStream.ReadShort();
            this.xMax = tableStream.ReadShort();
            this.yMax = tableStream.ReadShort();
            this.macStyle = (PdfFontHeadTableDirectoryEntryMacStyle) tableStream.ReadShort();
            this.lowestRecPPEM = tableStream.ReadShort();
            this.fontDirectionHint = (PdfFontDirectionHint) tableStream.ReadShort();
            this.indexToLocFormat = (PdfIndexToLocFormat) tableStream.ReadShort();
            this.glyphDataFormat = tableStream.ReadShort();
        }

        public PdfFontHeadTableDirectoryEntry(PdfRectangle fontBBox) : base("head")
        {
            this.version = 0x10000;
            this.fontRevision = 0;
            this.checkSumAdjustment = 0;
            this.magicNumber = 0x5f0f3cf5;
            this.flags = PdfFontHeadTableDirectoryEntryFlags.Empty;
            this.unitsPerEm = 0x3e8;
            this.created = (DateTime.Now - minDateTime).Seconds;
            this.modified = this.created;
            if (fontBBox != null)
            {
                this.xMin = Convert.ToInt16(Math.Max(-32768.0, fontBBox.Left));
                this.yMin = Convert.ToInt16(Math.Max(-32768.0, fontBBox.Bottom));
                this.xMax = Convert.ToInt16(Math.Min(32767.0, fontBBox.Right));
                this.yMax = Convert.ToInt16(Math.Min(32767.0, fontBBox.Top));
            }
            this.lowestRecPPEM = 6;
            this.shouldWrite = true;
        }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            if (this.shouldWrite)
            {
                PdfBinaryStream stream = base.CreateNewStream();
                stream.WriteInt(this.version);
                stream.WriteInt(this.fontRevision);
                stream.WriteInt(this.checkSumAdjustment);
                stream.WriteInt(this.magicNumber);
                stream.WriteShort((short) this.flags);
                stream.WriteShort(this.unitsPerEm);
                stream.WriteLong(this.created);
                stream.WriteLong(this.modified);
                stream.WriteShort(this.xMin);
                stream.WriteShort(this.yMin);
                stream.WriteShort(this.xMax);
                stream.WriteShort(this.yMax);
                stream.WriteShort((short) this.macStyle);
                stream.WriteShort(this.lowestRecPPEM);
                stream.WriteShort((short) this.fontDirectionHint);
                stream.WriteShort((short) this.indexToLocFormat);
                stream.WriteShort(this.glyphDataFormat);
            }
        }

        private static short Max(short? first, short second) => 
            (first != null) ? Math.Max(first.Value, second) : second;

        private static short Min(short? first, short second) => 
            (first != null) ? Math.Min(first.Value, second) : second;

        public void Validate(IEnumerable<PdfGlyphDescription> glyphs)
        {
            short xMin = this.xMin;
            short yMin = this.yMin;
            short xMax = this.xMax;
            short yMax = this.yMax;
            foreach (PdfGlyphDescription description in glyphs)
            {
                this.xMin = Min(description.XMin, this.xMin);
                this.yMin = Min(description.YMin, this.yMin);
                this.xMax = Max(description.XMax, this.xMax);
                this.yMax = Max(description.YMax, this.yMax);
            }
            this.shouldWrite = ((xMin != this.xMin) || ((yMin != this.yMin) || (xMax != this.xMax))) || (yMax != this.yMax);
        }

        public int Version =>
            this.version;

        public int FontRevision =>
            this.fontRevision;

        public int CheckSumAdjustment =>
            this.checkSumAdjustment;

        public int MagicNumber =>
            this.magicNumber;

        public PdfFontHeadTableDirectoryEntryFlags Flags =>
            this.flags;

        public short UnitsPerEm =>
            this.unitsPerEm;

        public long Created =>
            this.created;

        public long Modified =>
            this.modified;

        public short XMin =>
            this.xMin;

        public short YMin =>
            this.yMin;

        public short XMax =>
            this.xMax;

        public short YMax =>
            this.yMax;

        public PdfFontHeadTableDirectoryEntryMacStyle MacStyle =>
            this.macStyle;

        public short LowestRecPPEM =>
            this.lowestRecPPEM;

        public PdfFontDirectionHint FontDirectionHint =>
            this.fontDirectionHint;

        public PdfIndexToLocFormat IndexToLocFormat =>
            this.indexToLocFormat;

        public short GlyphDataFormat =>
            this.glyphDataFormat;
    }
}

