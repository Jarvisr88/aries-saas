namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.InteropServices;

    internal class TTFName : TTFTable
    {
        private const int familyNameID = 1;
        private const int postScriptNameID = 6;
        private const int MacintoshPlatformID = 1;
        private const int MicrosoftPlatformID = 3;
        public const int EnglishLanguageID = 0x409;
        private int startPosition;
        private ushort numberOfRecords;
        private ushort offsetOfStringStorage;
        private Record[] records;
        private string familyName;
        private string macFamilyName;
        private string postScriptName;

        public TTFName(TTFFile ttfFile) : base(ttfFile)
        {
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            throw new TTFFileException("Not supported");
        }

        private void ReadRecords(TTFStream ttfStream)
        {
            this.records = new Record[this.numberOfRecords];
            for (int i = 0; i < this.numberOfRecords; i++)
            {
                this.records[i].PlatformID = ttfStream.ReadUShort();
                this.records[i].EncodingID = ttfStream.ReadUShort();
                this.records[i].LanguageID = ttfStream.ReadUShort();
                this.records[i].NameID = ttfStream.ReadUShort();
                this.records[i].StringLength = ttfStream.ReadUShort();
                this.records[i].StringOffset = ttfStream.ReadUShort();
            }
        }

        private string ReadString(TTFStream ttfStream, int i)
        {
            ttfStream.Seek(this.startPosition);
            ttfStream.Move(this.offsetOfStringStorage + this.records[i].StringOffset);
            byte[] bytes = ttfStream.ReadBytes(this.records[i].StringLength);
            return DXEncoding.ASCII.GetString(bytes, 0, bytes.Length);
        }

        private void ReadStringStorage(TTFStream ttfStream)
        {
            for (int i = 0; i < this.records.Length; i++)
            {
                if ((this.records[i].PlatformID == 3) && ((this.records[i].EncodingID == 1) && (this.records[i].LanguageID == 0x409)))
                {
                    if (this.records[i].NameID == 1)
                    {
                        this.familyName = this.ReadUnicodeString(ttfStream, i);
                    }
                }
                else if ((this.records[i].PlatformID == 1) && ((this.records[i].EncodingID == 0) && (this.records[i].LanguageID == 0)))
                {
                    ushort nameID = this.records[i].NameID;
                    if (nameID == 1)
                    {
                        this.macFamilyName = this.ReadString(ttfStream, i);
                    }
                    else if (nameID == 6)
                    {
                        this.postScriptName = this.ReadString(ttfStream, i);
                    }
                }
            }
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            this.startPosition = ttfStream.Position;
            ttfStream.Move(2);
            this.numberOfRecords = ttfStream.ReadUShort();
            this.offsetOfStringStorage = ttfStream.ReadUShort();
            this.ReadRecords(ttfStream);
            this.ReadStringStorage(ttfStream);
        }

        private string ReadUnicodeString(TTFStream ttfStream, int i)
        {
            ttfStream.Seek(this.startPosition);
            ttfStream.Move(this.offsetOfStringStorage + this.records[i].StringOffset);
            return ttfStream.ReadUnicodeString(this.records[i].StringLength);
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            throw new TTFFileException("Not supported");
        }

        public override int Length
        {
            get
            {
                int num = (0 + (this.familyName.Length * 2)) + (this.macFamilyName.Length + this.postScriptName.Length);
                return ((6 + (Record.SizeOf * this.records.Length)) + num);
            }
        }

        public string FamilyName =>
            this.familyName;

        public string MacintoshFamilyName =>
            this.macFamilyName;

        public string PostScriptName =>
            this.postScriptName;

        protected internal override string Tag =>
            "name";

        [StructLayout(LayoutKind.Sequential)]
        private struct Record
        {
            public ushort PlatformID;
            public ushort EncodingID;
            public ushort LanguageID;
            public ushort NameID;
            public ushort StringLength;
            public ushort StringOffset;
            public static int SizeOf =>
                12;
        }
    }
}

