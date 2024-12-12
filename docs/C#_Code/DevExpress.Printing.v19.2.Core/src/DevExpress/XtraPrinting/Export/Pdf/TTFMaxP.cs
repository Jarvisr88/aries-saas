namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFMaxP : TTFTable
    {
        private byte[] tableVersion;
        private ushort numGlyphs;
        private ushort maxPoints;
        private ushort maxContours;
        private ushort maxCompositePoints;
        private ushort maxCompositeContours;
        private ushort maxZones;
        private ushort maxTwilightPoints;
        private ushort maxStorage;
        private ushort maxFunctionDefs;
        private ushort maxInstructionDefs;
        private ushort maxStackElements;
        private ushort maxSizeOfInstructions;
        private ushort maxComponentElements;
        private ushort maxComponentDepth;

        public TTFMaxP(TTFFile ttfFile) : base(ttfFile)
        {
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            TTFMaxP xp = pattern as TTFMaxP;
            this.tableVersion = new byte[xp.tableVersion.Length];
            xp.tableVersion.CopyTo(this.tableVersion, 0);
            this.numGlyphs = xp.numGlyphs;
            this.maxPoints = xp.maxPoints;
            this.maxContours = xp.maxContours;
            this.maxCompositePoints = xp.maxCompositePoints;
            this.maxCompositeContours = xp.maxCompositeContours;
            this.maxZones = xp.maxZones;
            this.maxTwilightPoints = xp.maxTwilightPoints;
            this.maxStorage = xp.maxStorage;
            this.maxFunctionDefs = xp.maxFunctionDefs;
            this.maxInstructionDefs = xp.maxInstructionDefs;
            this.maxStackElements = xp.maxStackElements;
            this.maxSizeOfInstructions = xp.maxSizeOfInstructions;
            this.maxComponentElements = xp.maxComponentElements;
            this.maxComponentDepth = xp.maxComponentDepth;
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            this.tableVersion = ttfStream.ReadBytes(4);
            this.numGlyphs = ttfStream.ReadUShort();
            this.maxPoints = ttfStream.ReadUShort();
            this.maxContours = ttfStream.ReadUShort();
            this.maxCompositePoints = ttfStream.ReadUShort();
            this.maxCompositeContours = ttfStream.ReadUShort();
            this.maxZones = ttfStream.ReadUShort();
            this.maxTwilightPoints = ttfStream.ReadUShort();
            this.maxStorage = ttfStream.ReadUShort();
            this.maxFunctionDefs = ttfStream.ReadUShort();
            this.maxInstructionDefs = ttfStream.ReadUShort();
            this.maxStackElements = ttfStream.ReadUShort();
            this.maxSizeOfInstructions = ttfStream.ReadUShort();
            this.maxComponentElements = ttfStream.ReadUShort();
            this.maxComponentDepth = ttfStream.ReadUShort();
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            ttfStream.WriteBytes(this.tableVersion);
            ttfStream.WriteUShort(this.numGlyphs);
            ttfStream.WriteUShort(this.maxPoints);
            ttfStream.WriteUShort(this.maxContours);
            ttfStream.WriteUShort(this.maxCompositePoints);
            ttfStream.WriteUShort(this.maxCompositeContours);
            ttfStream.WriteUShort(this.maxZones);
            ttfStream.WriteUShort(this.maxTwilightPoints);
            ttfStream.WriteUShort(this.maxStorage);
            ttfStream.WriteUShort(this.maxFunctionDefs);
            ttfStream.WriteUShort(this.maxInstructionDefs);
            ttfStream.WriteUShort(this.maxStackElements);
            ttfStream.WriteUShort(this.maxSizeOfInstructions);
            ttfStream.WriteUShort(this.maxComponentElements);
            ttfStream.WriteUShort(this.maxComponentDepth);
        }

        public int NumGlyphs =>
            Convert.ToInt32(this.numGlyphs);

        public override int Length =>
            SizeOf;

        public static int SizeOf =>
            0x20;

        protected internal override string Tag =>
            "maxp";
    }
}

