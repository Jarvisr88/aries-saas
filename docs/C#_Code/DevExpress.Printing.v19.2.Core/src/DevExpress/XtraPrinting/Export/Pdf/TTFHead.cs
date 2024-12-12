namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFHead : TTFTable
    {
        private byte[] tableVersion;
        private byte[] fontRevision;
        private uint checkSumAdjustment;
        private uint magicNumber;
        private ushort flags;
        private ushort unitsPerEm;
        private byte[] created;
        private byte[] modified;
        private short xMin;
        private short yMin;
        private short xMax;
        private short yMax;
        private ushort macStyle;
        private ushort lowestRecPPEM;
        private short fontDirectionHint;
        private short indexToLocFormat;
        private short glyphDataFormat;

        public TTFHead(TTFFile ttfFile) : base(ttfFile)
        {
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            TTFHead head = pattern as TTFHead;
            this.tableVersion = new byte[head.tableVersion.Length];
            head.tableVersion.CopyTo(this.tableVersion, 0);
            this.fontRevision = new byte[head.fontRevision.Length];
            head.fontRevision.CopyTo(this.fontRevision, 0);
            this.checkSumAdjustment = 0;
            this.magicNumber = head.magicNumber;
            this.flags = head.flags;
            this.unitsPerEm = head.UnitsPerEm;
            this.created = new byte[head.created.Length];
            head.created.CopyTo(this.created, 0);
            this.modified = new byte[head.modified.Length];
            head.modified.CopyTo(this.modified, 0);
            this.xMin = head.XMin;
            this.yMin = head.YMin;
            this.xMax = head.XMax;
            this.yMax = head.YMax;
            this.macStyle = head.macStyle;
            this.lowestRecPPEM = head.lowestRecPPEM;
            this.fontDirectionHint = head.fontDirectionHint;
            this.indexToLocFormat = head.indexToLocFormat;
            this.glyphDataFormat = head.glyphDataFormat;
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            this.tableVersion = ttfStream.ReadBytes(4);
            this.fontRevision = ttfStream.ReadBytes(4);
            this.checkSumAdjustment = ttfStream.ReadULong();
            this.magicNumber = ttfStream.ReadULong();
            this.flags = ttfStream.ReadUShort();
            this.unitsPerEm = ttfStream.ReadUShort();
            this.created = ttfStream.ReadBytes(8);
            this.modified = ttfStream.ReadBytes(8);
            this.xMin = ttfStream.ReadFWord();
            this.yMin = ttfStream.ReadFWord();
            this.xMax = ttfStream.ReadFWord();
            this.yMax = ttfStream.ReadFWord();
            this.macStyle = ttfStream.ReadUShort();
            this.lowestRecPPEM = ttfStream.ReadUShort();
            this.fontDirectionHint = ttfStream.ReadShort();
            this.indexToLocFormat = ttfStream.ReadShort();
            this.glyphDataFormat = ttfStream.ReadShort();
        }

        public void WriteCheckSumAdjustment(TTFStream ttfStream)
        {
            if (base.Entry != null)
            {
                ttfStream.Seek(base.Entry.Offset);
                ttfStream.Move(8);
                if (this.checkSumAdjustment == 0)
                {
                    this.checkSumAdjustment = TTFUtils.CalculateCheckSum(ttfStream, 0, ttfStream.Length);
                    this.checkSumAdjustment = 0xb1b0afba - this.checkSumAdjustment;
                }
                ttfStream.WriteULong(this.checkSumAdjustment);
            }
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            ttfStream.WriteBytes(this.tableVersion);
            ttfStream.WriteBytes(this.fontRevision);
            ttfStream.WriteULong(this.checkSumAdjustment);
            ttfStream.WriteULong(this.magicNumber);
            ttfStream.WriteUShort(this.flags);
            ttfStream.WriteUShort(this.unitsPerEm);
            ttfStream.WriteBytes(this.created);
            ttfStream.WriteBytes(this.modified);
            ttfStream.WriteFWord(this.xMin);
            ttfStream.WriteFWord(this.yMin);
            ttfStream.WriteFWord(this.xMax);
            ttfStream.WriteFWord(this.yMax);
            ttfStream.WriteUShort(this.macStyle);
            ttfStream.WriteUShort(this.lowestRecPPEM);
            ttfStream.WriteShort(this.fontDirectionHint);
            ttfStream.WriteShort(this.indexToLocFormat);
            ttfStream.WriteShort(this.glyphDataFormat);
        }

        public ushort Flags =>
            this.flags;

        public ushort UnitsPerEm =>
            this.unitsPerEm;

        public short XMin =>
            this.xMin;

        public short YMin =>
            this.yMin;

        public short XMax =>
            this.xMax;

        public short YMax =>
            this.yMax;

        public short IndexToLocFormat =>
            this.indexToLocFormat;

        public override int Length =>
            SizeOf;

        public static int SizeOf =>
            0x36;

        protected internal override string Tag =>
            "head";
    }
}

