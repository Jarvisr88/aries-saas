namespace DevExpress.Utils.Internal
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class TTFHeader
    {
        public const float InternalFormatVersion = -1f;
        public const float InternalFormatVersion2 = -2f;
        private float version;
        private Dictionary<string, TTFTableRecord> tables;
        private int glyphsCount;
        private int unitsPerEm;
        private int hMetricsCount;
        private int lineGap;
        private int ascent;
        private int descent;
        private int strikeOutSize;
        private int strikeOutPosition;
        private int underlineSize;
        private int underlinePosition;
        private TTFIndexToLocFormat indexToLocFormat;
        private short glyphDataFormat;
        private short caretOffset;
        private bool isNonLinearScaling;
        private int subscriptXSize;
        private int subscriptXOffset;
        private int subscriptYSize;
        private int subscriptYOffset;
        private int superscriptXSize;
        private int superscriptXOffset;
        private int superscriptYSize;
        private int superscriptYOffset;
        private TTFWeightClass weightClass;
        private TTFWidthClass widthClass;
        private byte[] panose = new byte[10];

        private void ParseFlags(ushort flags)
        {
            this.isNonLinearScaling = (flags & 0x10) == 0x10;
        }

        public void Read(BigEndianStreamReader reader)
        {
            this.version = reader.ReadFixed32();
            if (this.version == 1f)
            {
                this.ReadV10(reader);
            }
            else
            {
                if ((this.version != -1f) && (this.version != -2f))
                {
                    throw new NotSupportedException($"version={this.version}, expected versions: {1.0}, {-1f}, {-2f}");
                }
                this.ReadInternal(reader);
            }
        }

        private void ReadHEADTable(BigEndianStreamReader reader)
        {
            this.SeekToTable(reader, "head");
            if (reader.ReadFixed32() != 1.0)
            {
                throw new NotSupportedException("head table version");
            }
            reader.Stream.Seek((long) 12, SeekOrigin.Current);
            ushort flags = reader.ReadUShort();
            this.ParseFlags(flags);
            this.unitsPerEm = reader.ReadUShort();
            reader.Stream.Seek((long) 30, SeekOrigin.Current);
            this.indexToLocFormat = (TTFIndexToLocFormat) reader.ReadShort();
            this.glyphDataFormat = reader.ReadShort();
        }

        private void ReadHHEATable(BigEndianStreamReader reader)
        {
            this.SeekToTable(reader, "hhea");
            if (reader.ReadFixed32() != 1.0)
            {
                throw new NotSupportedException("hhea table version");
            }
            this.ascent = reader.ReadShort();
            this.descent = -reader.ReadShort();
            this.lineGap = reader.ReadShort();
            reader.Stream.Seek((long) 12, SeekOrigin.Current);
            this.caretOffset = reader.ReadShort();
            if (reader.ReadUInt32() != 0)
            {
                throw new ArgumentException("corrupted hhea table: no zero padding");
            }
            if (reader.ReadUInt32() != 0)
            {
                throw new ArgumentException("corrupted hhea table: no zero padding");
            }
            if (reader.ReadShort() != 0)
            {
                throw new NotSupportedException("metric data format");
            }
            this.hMetricsCount = reader.ReadUShort();
        }

        private void ReadInternal(BigEndianStreamReader reader)
        {
            this.glyphsCount = reader.ReadUShort();
            this.unitsPerEm = reader.ReadUShort();
            this.ascent = reader.ReadShort();
            this.descent = reader.ReadShort();
            this.lineGap = reader.ReadShort();
            this.caretOffset = reader.ReadShort();
            this.subscriptXSize = reader.ReadShort();
            this.subscriptXOffset = reader.ReadShort();
            this.subscriptYSize = reader.ReadShort();
            this.subscriptYOffset = reader.ReadShort();
            this.superscriptXSize = reader.ReadShort();
            this.superscriptXOffset = reader.ReadShort();
            this.superscriptYSize = reader.ReadShort();
            this.superscriptYOffset = reader.ReadShort();
            this.strikeOutPosition = reader.ReadShort();
            this.strikeOutSize = reader.ReadShort();
            this.underlinePosition = reader.ReadShort();
            this.underlineSize = reader.ReadShort();
            reader.Stream.Read(this.panose, 0, 10);
        }

        private void ReadMAXPTable(BigEndianStreamReader reader)
        {
            this.SeekToTable(reader, "maxp");
            if (reader.ReadFixed32() != 1.0)
            {
                throw new NotSupportedException("maxp table version");
            }
            this.glyphsCount = reader.ReadUShort();
        }

        private void ReadOS2Table(BigEndianStreamReader reader)
        {
            this.SeekToTable(reader, "OS/2");
            reader.ReadUShort();
            reader.Stream.Seek(2L, SeekOrigin.Current);
            this.weightClass = (TTFWeightClass) reader.ReadUShort();
            this.widthClass = (TTFWidthClass) reader.ReadUShort();
            reader.Stream.Seek(2L, SeekOrigin.Current);
            this.subscriptXSize = reader.ReadShort();
            this.subscriptYSize = reader.ReadShort();
            this.subscriptXOffset = reader.ReadShort();
            this.subscriptYOffset = reader.ReadShort();
            this.superscriptXSize = reader.ReadShort();
            this.superscriptYSize = reader.ReadShort();
            this.superscriptXOffset = reader.ReadShort();
            this.superscriptYOffset = -reader.ReadShort();
            this.strikeOutSize = reader.ReadShort();
            this.strikeOutPosition = reader.ReadShort();
            reader.ReadShort();
            reader.Stream.Read(this.panose, 0, 10);
        }

        private void ReadPOSTTable(BigEndianStreamReader reader)
        {
            if (this.SeekToTable(reader, "post"))
            {
                reader.Stream.Seek(8L, SeekOrigin.Current);
                this.underlinePosition = -reader.ReadShort();
                this.underlineSize = reader.ReadShort();
            }
        }

        private void ReadTableRecords(BigEndianStreamReader reader, int numTables)
        {
            this.tables = new Dictionary<string, TTFTableRecord>(numTables);
            for (int i = 0; i < numTables; i++)
            {
                TTFTableRecord record = new TTFTableRecord();
                record.Read(reader);
                this.Tables.Add(record.Tag, record);
            }
        }

        private void ReadV10(BigEndianStreamReader reader)
        {
            int numTables = reader.ReadUShort();
            reader.Stream.Seek(6L, SeekOrigin.Current);
            this.ReadTableRecords(reader, numTables);
            this.ReadMAXPTable(reader);
            this.ReadHEADTable(reader);
            this.ReadHHEATable(reader);
            this.ReadOS2Table(reader);
            this.ReadPOSTTable(reader);
        }

        public bool SeekToTable(BigEndianStreamReader reader, string name)
        {
            if (!this.Tables.ContainsKey(name))
            {
                return false;
            }
            reader.Stream.Seek((long) this.Tables[name].Offset, SeekOrigin.Begin);
            return true;
        }

        public void WriteInternal(BigEndianStreamWriter writer)
        {
            writer.WriteFixed32(-2f);
            writer.WriteUShort((ushort) this.GlyphsCount);
            writer.WriteUShort((ushort) this.UnitsPerEm);
            writer.WriteShort((short) this.Ascent);
            writer.WriteShort((short) this.Descent);
            writer.WriteShort((short) this.LineGap);
            writer.WriteShort(this.CaretOffset);
            writer.WriteShort((short) this.SubscriptXSize);
            writer.WriteShort((short) this.SubscriptXOffset);
            writer.WriteShort((short) this.SubscriptYSize);
            writer.WriteShort((short) this.SubscriptYOffset);
            writer.WriteShort((short) this.SuperscriptXSize);
            writer.WriteShort((short) this.SuperscriptXOffset);
            writer.WriteShort((short) this.SuperscriptYSize);
            writer.WriteShort((short) this.SuperscriptYOffset);
            writer.WriteShort((short) this.StrikeOutPosition);
            writer.WriteShort((short) this.StrikeOutSize);
            writer.WriteShort((short) this.UnderlinePosition);
            writer.WriteShort((short) this.UnderlineSize);
            writer.Stream.Write(this.Panose, 0, this.Panose.Length);
        }

        public bool IsInternal =>
            (this.Version == -1f) || (this.Version == -2f);

        public bool IsSupportKerningInternal =>
            this.Version == -2f;

        public float Version =>
            this.version;

        public int GlyphsCount =>
            this.glyphsCount;

        public int UnitsPerEm =>
            this.unitsPerEm;

        public short GlyphDataFormat =>
            this.glyphDataFormat;

        public TTFIndexToLocFormat IndexToLocTableFormat =>
            this.indexToLocFormat;

        public Dictionary<string, TTFTableRecord> Tables =>
            this.tables;

        public bool IsNonLinearScaling =>
            this.isNonLinearScaling;

        public int Ascent
        {
            get => 
                this.ascent;
            internal set => 
                this.ascent = value;
        }

        public int Descent
        {
            get => 
                this.descent;
            internal set => 
                this.descent = value;
        }

        public int LineGap
        {
            get => 
                this.lineGap;
            internal set => 
                this.lineGap = value;
        }

        public short CaretOffset =>
            this.caretOffset;

        public int HMetricsCount =>
            this.hMetricsCount;

        public int SubscriptXSize =>
            this.subscriptXSize;

        public int SubscriptXOffset =>
            this.subscriptXOffset;

        public int SubscriptYSize =>
            this.subscriptYSize;

        public int SubscriptYOffset =>
            this.subscriptYOffset;

        public int SuperscriptXSize =>
            this.superscriptXSize;

        public int SuperscriptXOffset =>
            this.superscriptXOffset;

        public int SuperscriptYSize =>
            this.superscriptYSize;

        public int SuperscriptYOffset =>
            this.superscriptYOffset;

        public TTFWeightClass WeightClass =>
            this.weightClass;

        public TTFWidthClass WidthClass =>
            this.widthClass;

        public int StrikeOutSize =>
            this.strikeOutSize;

        public int StrikeOutPosition =>
            this.strikeOutPosition;

        public int UnderlineSize =>
            this.underlineSize;

        public int UnderlinePosition =>
            this.underlinePosition;

        public byte[] Panose =>
            this.panose;
    }
}

