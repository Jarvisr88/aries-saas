namespace DevExpress.XtraPrinting.Export.Pdf.Compression
{
    using DevExpress.Utils.Zip;
    using System;
    using System.IO;
    using System.IO.Compression;

    public class Deflater
    {
        private BitBuffer bitBuffer = new BitBuffer();
        private Adler32 adler32 = new Adler32();
        private static TableItem[] lengthTable = new TableItem[0x1d];
        private static TableItem[] offsetTable = new TableItem[30];
        private static CodeTableItem[] lengthBaseCodeTable = new CodeTableItem[4];
        private const int lz77WindowSizeExponent = 13;

        static Deflater()
        {
            FillLengthTable();
            FillOffsetTable();
            FillLengthBaseCodeTable();
        }

        public void Deflate(MemoryStream source, MemoryStream dest)
        {
            if (source.Length != 0)
            {
                this.WriteZLibHeader();
                this.bitBuffer.ToStream(dest);
                System.IO.Compression.DeflateStream stream = new System.IO.Compression.DeflateStream(dest, CompressionMode.Compress, true);
                source.WriteTo(stream);
                stream.Dispose();
                this.adler32.Add(source.GetBuffer(), 0, (int) source.Length);
                this.adler32.Write(dest);
            }
        }

        public static MemoryStream DeflateStream(MemoryStream source)
        {
            MemoryStream dest = new MemoryStream();
            source.Seek(0L, SeekOrigin.Begin);
            new Deflater().Deflate(source, dest);
            return dest;
        }

        private void EncodeLength(int length)
        {
            TableItem item = null;
            int index = 0;
            while (true)
            {
                if (index < lengthTable.Length)
                {
                    if (!lengthTable[index].Contains(length))
                    {
                        index++;
                        continue;
                    }
                    item = lengthTable[index];
                }
                if (item == null)
                {
                    throw new CompressException("Invalid length");
                }
                this.EncodeLengthBase(item.Base);
                item.EncodeExtraBits(length, this.bitBuffer);
                return;
            }
        }

        private void EncodeLengthBase(int lengthBase)
        {
            CodeTableItem item = null;
            int index = 0;
            while (true)
            {
                if (index < lengthBaseCodeTable.Length)
                {
                    if (!lengthBaseCodeTable[index].Contains(lengthBase))
                    {
                        index++;
                        continue;
                    }
                    item = lengthBaseCodeTable[index];
                }
                if (item == null)
                {
                    throw new CompressException("Invalid base of the length");
                }
                item.Encode(lengthBase, this.bitBuffer);
                return;
            }
        }

        private void EncodeLiteral(byte literal)
        {
            this.EncodeLengthBase(literal);
        }

        private void EncodeOffset(int offset)
        {
            TableItem item = null;
            int index = 0;
            while (true)
            {
                if (index < offsetTable.Length)
                {
                    if (!offsetTable[index].Contains(offset))
                    {
                        index++;
                        continue;
                    }
                    item = offsetTable[index];
                }
                if (item == null)
                {
                    throw new CompressException("Invalid offset");
                }
                this.EncodeOffsetBase(item.Base);
                item.EncodeExtraBits(offset, this.bitBuffer);
                return;
            }
        }

        private void EncodeOffsetBase(int offsetBase)
        {
            int b = Utils.BitReverse(offsetBase, 5);
            this.bitBuffer.WriteBits(b, 5);
        }

        private static void FillLengthBaseCodeTable()
        {
            lengthBaseCodeTable[0] = new CodeTableItem(0, 0x8f, 8, 0x30);
            lengthBaseCodeTable[1] = new CodeTableItem(0x90, 0xff, 9, 400);
            lengthBaseCodeTable[2] = new CodeTableItem(0x100, 0x117, 7, 0);
            lengthBaseCodeTable[3] = new CodeTableItem(280, 0x11f, 8, 0xc0);
        }

        private static void FillLengthTable()
        {
            lengthTable[0] = new TableItem(3, 3, 0x101, 0);
            lengthTable[1] = new TableItem(4, 4, 0x102, 0);
            lengthTable[2] = new TableItem(5, 5, 0x103, 0);
            lengthTable[3] = new TableItem(6, 6, 260, 0);
            lengthTable[4] = new TableItem(7, 7, 0x105, 0);
            lengthTable[5] = new TableItem(8, 8, 0x106, 0);
            lengthTable[6] = new TableItem(9, 9, 0x107, 0);
            lengthTable[7] = new TableItem(10, 10, 0x108, 0);
            lengthTable[8] = new TableItem(11, 12, 0x109, 1);
            lengthTable[9] = new TableItem(13, 14, 0x10a, 1);
            lengthTable[10] = new TableItem(15, 0x10, 0x10b, 1);
            lengthTable[11] = new TableItem(0x11, 0x12, 0x10c, 1);
            lengthTable[12] = new TableItem(0x13, 0x16, 0x10d, 2);
            lengthTable[13] = new TableItem(0x17, 0x1a, 270, 2);
            lengthTable[14] = new TableItem(0x1b, 30, 0x10f, 2);
            lengthTable[15] = new TableItem(0x1f, 0x22, 0x110, 2);
            lengthTable[0x10] = new TableItem(0x23, 0x2a, 0x111, 3);
            lengthTable[0x11] = new TableItem(0x2b, 50, 0x112, 3);
            lengthTable[0x12] = new TableItem(0x33, 0x3a, 0x113, 3);
            lengthTable[0x13] = new TableItem(0x3b, 0x42, 0x114, 3);
            lengthTable[20] = new TableItem(0x43, 0x52, 0x115, 4);
            lengthTable[0x15] = new TableItem(0x53, 0x62, 0x116, 4);
            lengthTable[0x16] = new TableItem(0x63, 0x72, 0x117, 4);
            lengthTable[0x17] = new TableItem(0x73, 130, 280, 4);
            lengthTable[0x18] = new TableItem(0x83, 0xa2, 0x119, 5);
            lengthTable[0x19] = new TableItem(0xa3, 0xc2, 0x11a, 5);
            lengthTable[0x1a] = new TableItem(0xc3, 0xe2, 0x11b, 5);
            lengthTable[0x1b] = new TableItem(0xe3, 0x101, 0x11c, 5);
            lengthTable[0x1c] = new TableItem(0x102, 0x102, 0x11d, 0);
        }

        private static void FillOffsetTable()
        {
            offsetTable[0] = new TableItem(1, 1, 0, 0);
            offsetTable[1] = new TableItem(2, 2, 1, 0);
            offsetTable[2] = new TableItem(3, 3, 2, 0);
            offsetTable[3] = new TableItem(4, 4, 3, 0);
            offsetTable[4] = new TableItem(5, 6, 4, 1);
            offsetTable[5] = new TableItem(7, 8, 5, 1);
            offsetTable[6] = new TableItem(9, 12, 6, 2);
            offsetTable[7] = new TableItem(13, 0x10, 7, 2);
            offsetTable[8] = new TableItem(0x11, 0x18, 8, 3);
            offsetTable[9] = new TableItem(0x19, 0x20, 9, 3);
            offsetTable[10] = new TableItem(0x21, 0x30, 10, 4);
            offsetTable[11] = new TableItem(0x31, 0x40, 11, 4);
            offsetTable[12] = new TableItem(0x41, 0x60, 12, 5);
            offsetTable[13] = new TableItem(0x61, 0x80, 13, 5);
            offsetTable[14] = new TableItem(0x81, 0xc0, 14, 6);
            offsetTable[15] = new TableItem(0xc1, 0x100, 15, 6);
            offsetTable[0x10] = new TableItem(0x101, 0x180, 0x10, 7);
            offsetTable[0x11] = new TableItem(0x181, 0x200, 0x11, 7);
            offsetTable[0x12] = new TableItem(0x201, 0x300, 0x12, 8);
            offsetTable[0x13] = new TableItem(0x301, 0x400, 0x13, 8);
            offsetTable[20] = new TableItem(0x401, 0x600, 20, 9);
            offsetTable[0x15] = new TableItem(0x601, 0x800, 0x15, 9);
            offsetTable[0x16] = new TableItem(0x801, 0xc00, 0x16, 10);
            offsetTable[0x17] = new TableItem(0xc01, 0x1000, 0x17, 10);
            offsetTable[0x18] = new TableItem(0x1001, 0x1800, 0x18, 11);
            offsetTable[0x19] = new TableItem(0x1801, 0x2000, 0x19, 11);
            offsetTable[0x1a] = new TableItem(0x2001, 0x3000, 0x1a, 12);
            offsetTable[0x1b] = new TableItem(0x3001, 0x4000, 0x1b, 12);
            offsetTable[0x1c] = new TableItem(0x4001, 0x6000, 0x1c, 13);
            offsetTable[0x1d] = new TableItem(0x6001, 0x8000, 0x1d, 13);
        }

        private bool ProcessLZ77ResultValue(LZ77ResultValue resultValue)
        {
            if (resultValue == null)
            {
                return false;
            }
            if (resultValue.IsLiteral)
            {
                this.EncodeLiteral(resultValue.Literal);
            }
            else
            {
                this.EncodeLength(resultValue.Length);
                this.EncodeOffset(resultValue.Offset);
            }
            return true;
        }

        private void WriteBlockHeader()
        {
            this.bitBuffer.WriteBits(1, 1);
            this.bitBuffer.WriteBits(1, 2);
        }

        private void WriteZLibHeader()
        {
            int s = 0x5800 | 0x80;
            s += 0x1f - (s % 0x1f);
            this.bitBuffer.WriteShortMSB(s);
        }
    }
}

