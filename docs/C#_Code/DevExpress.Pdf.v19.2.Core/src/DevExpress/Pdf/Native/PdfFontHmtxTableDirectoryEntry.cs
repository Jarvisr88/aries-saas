namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontHmtxTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        public const string EntryTag = "hmtx";
        private short[] advanceWidths;
        private bool shouldWrite;
        private int expectedTableSize;

        public PdfFontHmtxTableDirectoryEntry(byte[] tableData) : base("hmtx", tableData)
        {
        }

        public PdfFontHmtxTableDirectoryEntry(int glyphCount) : base("hmtx")
        {
            PdfBinaryStream tableStream = base.TableStream;
            int num = glyphCount * 4;
            for (int i = 0; i < num; i++)
            {
                tableStream.WriteByte(0);
            }
        }

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            if (this.shouldWrite)
            {
                byte[] tableData = base.TableData;
                PdfBinaryStream stream = base.CreateNewStream();
                stream.WriteArray(tableData);
                stream.WriteArray(new byte[this.expectedTableSize - tableData.Length]);
            }
        }

        public short[] FillAdvanceWidths(int hMetricsCount, int glyphsCount)
        {
            PdfBinaryStream tableStream = base.TableStream;
            tableStream.Position = 0L;
            int num = Math.Max(hMetricsCount, glyphsCount);
            this.advanceWidths = new short[num];
            if (tableStream.Length >= (hMetricsCount * 4))
            {
                int index = 0;
                while (true)
                {
                    if (index >= hMetricsCount)
                    {
                        short num2 = this.advanceWidths[hMetricsCount - 1];
                        for (int i = hMetricsCount; i < num; i++)
                        {
                            this.advanceWidths[i] = num2;
                        }
                        break;
                    }
                    this.advanceWidths[index] = tableStream.ReadShort();
                    tableStream.ReadShort();
                    index++;
                }
            }
            return this.advanceWidths;
        }

        public void Validate(PdfFontFile fontFile)
        {
            PdfFontMaxpTableDirectoryEntry maxp = fontFile.Maxp;
            PdfFontHheaTableDirectoryEntry hhea = fontFile.Hhea;
            this.expectedTableSize = Math.Max((hhea == null) ? 0 : hhea.NumberOfHMetrics, (maxp == null) ? 0 : maxp.NumGlyphs) * 4;
            if (base.TableStream.Length < this.expectedTableSize)
            {
                this.shouldWrite = true;
            }
        }

        public short[] AdvanceWidths =>
            this.advanceWidths;
    }
}

