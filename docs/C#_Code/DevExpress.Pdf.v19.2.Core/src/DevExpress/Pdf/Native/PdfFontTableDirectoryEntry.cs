namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfFontTableDirectoryEntry : PdfDisposableObject
    {
        protected const double Ratio = 0.017453292519943295;
        protected const double TypoLineGapRatio = 1.2;
        private readonly string tag;
        private PdfBinaryStream tableStream;

        public PdfFontTableDirectoryEntry(string tag)
        {
            this.tag = tag;
            this.tableStream = new PdfBinaryStream();
        }

        public PdfFontTableDirectoryEntry(string tag, byte[] tableData)
        {
            this.tag = tag;
            this.tableStream = new PdfBinaryStream();
            this.tableStream.WriteArray(tableData);
            this.tableStream.Position = 0L;
        }

        protected virtual void ApplyChanges()
        {
        }

        public static PdfFontTableDirectoryEntry Create(string tag, byte[] array)
        {
            if (array.Length != 0)
            {
                uint num = <PrivateImplementationDetails>.ComputeStringHash(tag);
                if (num <= 0x8d39bde6)
                {
                    if (num <= 0x5b1e7839)
                    {
                        if (num == 0x32694bc3)
                        {
                            if (tag == "head")
                            {
                                return new PdfFontHeadTableDirectoryEntry(array);
                            }
                        }
                        else if (num != 0x3610086e)
                        {
                            if ((num == 0x5b1e7839) && (tag == "glyf"))
                            {
                                return new PdfTrueTypeGlyfTableDirectoryEntry(array);
                            }
                        }
                        else if (tag == "OS/2")
                        {
                            return new PdfFontOS2TableDirectoryEntry(array);
                        }
                    }
                    else if (num == 0x682f424a)
                    {
                        if (tag == "CFF ")
                        {
                            return new PdfOpenTypeCFFTableDirectoryEntry(array);
                        }
                    }
                    else if (num != 0x89e7fa47)
                    {
                        if ((num == 0x8d39bde6) && (tag == "name"))
                        {
                            return new PdfFontNameTableDirectoryEntry(array);
                        }
                    }
                    else if (tag == "post")
                    {
                        return new PdfFontPostTableDirectoryEntry(array);
                    }
                }
                else if (num <= 0xc511f4bb)
                {
                    if (num == 0x971c0602)
                    {
                        if (tag == "hmtx")
                        {
                            return new PdfFontHmtxTableDirectoryEntry(array);
                        }
                    }
                    else if (num != 0xb56821bc)
                    {
                        if ((num == 0xc511f4bb) && (tag == "hhea"))
                        {
                            return new PdfFontHheaTableDirectoryEntry(array);
                        }
                    }
                    else if (tag == "cmap")
                    {
                        return new PdfFontCmapTableDirectoryEntry(array);
                    }
                }
                else if (num == 0xde6bfe4b)
                {
                    if (tag == "maxp")
                    {
                        return new PdfFontMaxpTableDirectoryEntry(array);
                    }
                }
                else if (num != 0xf50d81b4)
                {
                    if ((num == 0xfe65e729) && (tag == "kern"))
                    {
                        return new PdfFontKernTableDirectoryEntry(array);
                    }
                }
                else if (tag == "loca")
                {
                    return new PdfTrueTypeLocaTableDirectoryEntry(array);
                }
            }
            return new PdfFontTableDirectoryEntry(tag, array);
        }

        protected PdfBinaryStream CreateNewStream()
        {
            this.tableStream.Dispose();
            this.tableStream = new PdfBinaryStream();
            return this.tableStream;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.tableStream.Dispose();
            }
        }

        public int Write(PdfBinaryStream stream, int offset)
        {
            int num;
            this.ApplyChanges();
            if (this.Length == 0)
            {
                this.tableStream.WriteInt(0);
                num = 4;
            }
            stream.WriteString(this.tag);
            int num2 = 0;
            byte[] alignedTableData = this.AlignedTableData;
            int num3 = alignedTableData.Length / 4;
            int num4 = 0;
            int num5 = 0;
            while (num4 < num3)
            {
                int num6 = (((alignedTableData[num5++] << 0x18) + (alignedTableData[num5++] << 0x10)) + (alignedTableData[num5++] << 8)) + alignedTableData[num5++];
                num2 += num6;
                num4++;
            }
            stream.WriteInt(num2);
            stream.WriteInt(offset);
            stream.WriteInt(num);
            return alignedTableData.Length;
        }

        protected PdfBinaryStream TableStream =>
            this.tableStream;

        public string Tag =>
            this.tag;

        public int Length =>
            (int) this.tableStream.Length;

        public byte[] TableData =>
            this.tableStream.Data;

        public byte[] AlignedTableData =>
            this.tableStream.ToAlignedArray();
    }
}

