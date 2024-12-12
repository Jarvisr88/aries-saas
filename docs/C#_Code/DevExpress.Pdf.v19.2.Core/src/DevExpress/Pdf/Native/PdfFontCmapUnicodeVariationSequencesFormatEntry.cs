namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontCmapUnicodeVariationSequencesFormatEntry : PdfFontCmapFormatEntry
    {
        private const int headerLength = 10;
        private const int variationSelectorRecordSize = 11;
        private readonly PdfFontCmapUnicodeVariationSelectorRecord[] variationSelectorRecords;

        public PdfFontCmapUnicodeVariationSequencesFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId)
        {
            long num = stream.Position - 2L;
            stream.ReadInt();
            int num2 = stream.ReadInt();
            this.variationSelectorRecords = new PdfFontCmapUnicodeVariationSelectorRecord[num2];
            for (int i = 0; i < num2; i++)
            {
                PdfFontCmapUnicodeVariationSelectorDefaultTable[] tableArray;
                PdfFontCmapUnicodeVariationSelectorNonDefaultTable[] tableArray2;
                int varSelector = stream.Get24BitInt();
                int num5 = stream.ReadInt();
                int num6 = stream.ReadInt();
                long position = stream.Position;
                if (num5 == 0)
                {
                    tableArray = null;
                }
                else
                {
                    stream.Position = num + num5;
                    int num8 = stream.ReadInt();
                    tableArray = new PdfFontCmapUnicodeVariationSelectorDefaultTable[num8];
                    for (int j = 0; j < num8; j++)
                    {
                        tableArray[j] = new PdfFontCmapUnicodeVariationSelectorDefaultTable(stream.Get24BitInt(), stream.ReadByte());
                    }
                }
                if (num6 == 0)
                {
                    tableArray2 = null;
                }
                else
                {
                    stream.Position = num + num6;
                    int num10 = stream.ReadInt();
                    tableArray2 = new PdfFontCmapUnicodeVariationSelectorNonDefaultTable[num10];
                    for (int j = 0; j < num10; j++)
                    {
                        tableArray2[j] = new PdfFontCmapUnicodeVariationSelectorNonDefaultTable(stream.Get24BitInt(), stream.ReadShort());
                    }
                }
                stream.Position = position;
                this.variationSelectorRecords[i] = new PdfFontCmapUnicodeVariationSelectorRecord(varSelector, tableArray, tableArray2);
            }
            stream.Position = num + this.Length;
        }

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            tableStream.WriteInt(this.Length);
            tableStream.WriteInt(this.variationSelectorRecords.Length);
            int offset = 10 + (this.variationSelectorRecords.Length * 11);
            foreach (PdfFontCmapUnicodeVariationSelectorRecord record in this.variationSelectorRecords)
            {
                offset += record.Write(tableStream, offset);
            }
            foreach (PdfFontCmapUnicodeVariationSelectorRecord record2 in this.variationSelectorRecords)
            {
                PdfFontCmapUnicodeVariationSelectorDefaultTable[] defaultTables = record2.DefaultTables;
                if (defaultTables != null)
                {
                    tableStream.WriteInt(defaultTables.Length);
                    foreach (PdfFontCmapUnicodeVariationSelectorDefaultTable table in defaultTables)
                    {
                        table.Write(tableStream);
                    }
                }
                PdfFontCmapUnicodeVariationSelectorNonDefaultTable[] nonDefaultTables = record2.NonDefaultTables;
                if (nonDefaultTables != null)
                {
                    tableStream.WriteInt(nonDefaultTables.Length);
                    foreach (PdfFontCmapUnicodeVariationSelectorNonDefaultTable table2 in nonDefaultTables)
                    {
                        table2.Write(tableStream);
                    }
                }
            }
        }

        public PdfFontCmapUnicodeVariationSelectorRecord[] VariationSelectorRecords =>
            this.variationSelectorRecords;

        public override int Length
        {
            get
            {
                int num = 10 + (this.variationSelectorRecords.Length * 11);
                foreach (PdfFontCmapUnicodeVariationSelectorRecord record in this.variationSelectorRecords)
                {
                    PdfFontCmapUnicodeVariationSelectorDefaultTable[] defaultTables = record.DefaultTables;
                    if (defaultTables != null)
                    {
                        num += (defaultTables.Length * 4) + 4;
                    }
                    PdfFontCmapUnicodeVariationSelectorNonDefaultTable[] nonDefaultTables = record.NonDefaultTables;
                    if (nonDefaultTables != null)
                    {
                        num += (nonDefaultTables.Length * 5) + 4;
                    }
                }
                return num;
            }
        }

        protected override PdfFontCmapFormatID Format =>
            PdfFontCmapFormatID.UnicodeVariationSequences;
    }
}

