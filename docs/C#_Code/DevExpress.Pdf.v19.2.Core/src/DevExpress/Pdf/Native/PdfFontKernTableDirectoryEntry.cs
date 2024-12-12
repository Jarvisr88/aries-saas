namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfFontKernTableDirectoryEntry : PdfFontTableDirectoryEntry
    {
        public const string EntryTag = "kern";
        private Dictionary<int, short> kerning;

        public PdfFontKernTableDirectoryEntry(byte[] tableData) : base("kern", tableData)
        {
            this.kerning = new Dictionary<int, short>();
            int length = tableData.Length;
            PdfBinaryStream tableStream = base.TableStream;
            tableStream.ReadUshort();
            int num2 = tableStream.ReadUshort();
            int num3 = 0;
            int num4 = 6;
            while ((num3 < num2) && (num4 < length))
            {
                tableStream.Position = num4;
                num4 += tableStream.ReadUshort();
                if ((tableStream.ReadUshort() & 0xfff7) == 1)
                {
                    int num5 = tableStream.ReadUshort();
                    tableStream.ReadArray(6);
                    for (int i = 0; i < num5; i++)
                    {
                        int key = tableStream.ReadInt();
                        short num8 = tableStream.ReadShort();
                        if (!this.kerning.ContainsKey(key))
                        {
                            this.kerning[key] = num8;
                        }
                    }
                }
                num3++;
            }
        }

        public short GetKerning(int glyphIndex1, int glyphIndex2)
        {
            short num;
            return (this.kerning.TryGetValue((glyphIndex1 << 0x10) + glyphIndex2, out num) ? num : 0);
        }
    }
}

