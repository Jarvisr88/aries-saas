namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class TTFCMap : TTFTable
    {
        private const string unicodeMissingError = "The unicode CMap doesn't exist in the font file";
        private const string tableFormatError = "Invalid CMap format";
        private ushort versionNumber;
        private ushort unicodeVersion;
        private int segCount;
        private ushort[] ends;
        private ushort[] starts;
        private ushort[] deltas;
        private ushort[] rangeOffsets;
        private ushort[] map;
        private ushort eID;
        private ushort format;
        private int startPosition;
        private Encoding fontEncoding;

        public TTFCMap(TTFFile ttfFile) : base(ttfFile)
        {
            this.map = new ushort[0x10000];
        }

        private void CreateFormat2Map(ushort[] subHeaderKeys, Subheader[] subHeaders, ushort[] glyphIndexArray)
        {
            for (int i = 0; i < 0x100; i++)
            {
                if ((subHeaderKeys[i] > 0) || (i == 0))
                {
                    int index = subHeaderKeys[i];
                    for (int j = 0; j < subHeaders[index].EntryCount; j++)
                    {
                        ushort num4 = glyphIndexArray[subHeaders[index].GlyphOffset + j];
                        if (num4 != 0)
                        {
                            num4 = (ushort) (num4 + ((ushort) subHeaders[index].IdDelta));
                            this.map[(i << 8) | (subHeaders[index].FirstCode + j)] = num4;
                        }
                    }
                }
            }
        }

        private void CreateFormat4Map(TTFStream ttfStream)
        {
            int position = ttfStream.Position;
            int index = 0;
            while (index < this.ends.Length)
            {
                int num3 = this.starts[index];
                while (true)
                {
                    if (num3 > this.ends[index])
                    {
                        index++;
                        break;
                    }
                    ushort num4 = 0;
                    if ((this.rangeOffsets[index] == 0) || (num3 == 0xffff))
                    {
                        num4 = (ushort) ((num3 + this.deltas[index]) & 0xffff);
                    }
                    else
                    {
                        int offset = (((((this.rangeOffsets[index] / 2) + num3) - this.starts[index]) + index) - this.rangeOffsets.Length) * 2;
                        ttfStream.Seek(position);
                        ttfStream.Move(offset);
                        num4 = ttfStream.ReadUShort();
                        if (num4 != 0)
                        {
                            num4 = (ushort) ((num4 + this.deltas[index]) & 0xffff);
                        }
                    }
                    this.map[num3] = num4;
                    num3++;
                }
            }
        }

        private void CreateUnicodeTable(TTFInitializeParam param)
        {
            this.segCount = 0;
            for (int i = 0; i < (param.Chars.Count - 1); i++)
            {
                if (param.Chars[i + 1] != (param.Chars[i] + '\x0001'))
                {
                    this.segCount++;
                }
            }
            this.segCount += 2;
            this.ends = new ushort[this.segCount];
            this.starts = new ushort[this.segCount];
            this.deltas = new ushort[this.segCount];
            this.rangeOffsets = new ushort[this.segCount];
            int index = 0;
            if (param.Chars.Count > 0)
            {
                this.starts[0] = param.Chars[0];
                int num3 = 0;
                while (true)
                {
                    if (num3 >= (param.Chars.Count - 1))
                    {
                        this.ends[index] = param.Chars[param.Chars.Count - 1];
                        this.starts[index + 1] = this.ends[index + 1] = 0xffff;
                        break;
                    }
                    if (param.Chars[num3 + 1] != (param.Chars[num3] + '\x0001'))
                    {
                        this.ends[index] = param.Chars[num3];
                        this.starts[index + 1] = param.Chars[num3 + 1];
                        index++;
                    }
                    num3++;
                }
            }
            this.CreateUnicodeTable2();
        }

        private void CreateUnicodeTable2()
        {
            int num = 0;
            int index = 0;
            while (index < this.segCount)
            {
                bool flag = true;
                int num3 = this.starts[index];
                while (true)
                {
                    if (num3 < this.ends[index])
                    {
                        if (this.map[num3 + 1] == (this.map[num3] + 1))
                        {
                            num3++;
                            continue;
                        }
                        flag = false;
                    }
                    if (!flag)
                    {
                        this.rangeOffsets[index] = (ushort) (num + ((this.rangeOffsets.Length - index) * 2));
                        num += ((this.ends[index] - this.starts[index]) + 1) * 2;
                        this.deltas[index] = 0;
                    }
                    else
                    {
                        this.rangeOffsets[index] = 0;
                        int num4 = this.map[this.starts[index]] - this.starts[index];
                        if (num4 < 0)
                        {
                            num4 += 0x10000;
                        }
                        this.deltas[index] = (ushort) num4;
                    }
                    index++;
                    break;
                }
            }
        }

        private Encoding GetEncoding()
        {
            try
            {
                return DXEncoding.GetEncoding(base.Owner.FontCodePage);
            }
            catch
            {
                return null;
            }
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            TTFCMap map = pattern as TTFCMap;
            this.versionNumber = map.versionNumber;
            this.unicodeVersion = map.unicodeVersion;
            for (int i = 0; i < param.Chars.Count; i++)
            {
                ushort index = param.Chars[i];
                this.map[index] = map[index];
            }
            this.CreateUnicodeTable(param);
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            int position = ttfStream.Position;
            this.versionNumber = ttfStream.ReadUShort();
            int num2 = Convert.ToInt32(ttfStream.ReadUShort());
            int offset = -1;
            int num4 = 0;
            while (true)
            {
                if (num4 < num2)
                {
                    ushort num5 = ttfStream.ReadUShort();
                    this.eID = ttfStream.ReadUShort();
                    if ((((num5 != 3) || (this.eID != 1)) && ((num5 != 3) || (this.eID != 0))) && ((num5 != 3) || (this.eID != 3)))
                    {
                        ttfStream.Move(4);
                        num4++;
                        continue;
                    }
                    offset = Convert.ToInt32(ttfStream.ReadULong());
                }
                if (offset == -1)
                {
                    throw new TTFFileException("The unicode CMap doesn't exist in the font file");
                }
                ttfStream.Seek(position);
                ttfStream.Move(offset);
                this.ReadUnicodeTable(ttfStream);
                return;
            }
        }

        private void ReadUnicodeTable(TTFStream ttfStream)
        {
            this.startPosition = ttfStream.Position;
            this.format = ttfStream.ReadUShort();
            ushort format = this.format;
            if (format == 2)
            {
                this.ReadUnicodeTable2(ttfStream);
            }
            else
            {
                if (format != 4)
                {
                    throw new TTFFileException("Invalid CMap format");
                }
                this.ReadUnicodeTable4(ttfStream);
            }
        }

        private void ReadUnicodeTable2(TTFStream ttfStream)
        {
            ushort num = ttfStream.ReadUShort();
            this.unicodeVersion = ttfStream.ReadUShort();
            ushort[] subHeaderKeys = new ushort[0x100];
            List<ushort> list = new List<ushort>();
            for (int i = 0; i < subHeaderKeys.Length; i++)
            {
                subHeaderKeys[i] = (ushort) (ttfStream.ReadUShort() / 8);
                if (!list.Contains(subHeaderKeys[i]))
                {
                    list.Add(subHeaderKeys[i]);
                }
            }
            Subheader[] subHeaders = new Subheader[list.Count];
            int num2 = ttfStream.Position + (list.Count * 8);
            for (int j = 0; j < list.Count; j++)
            {
                ushort firstCode = ttfStream.ReadUShort();
                ushort entryCount = ttfStream.ReadUShort();
                short idDelta = ttfStream.ReadShort();
                int position = ttfStream.Position;
                ushort num10 = ttfStream.ReadUShort();
                subHeaders[j] = new Subheader(firstCode, entryCount, idDelta, (num10 - (num2 - position)) / 2);
            }
            ushort[] glyphIndexArray = new ushort[(num - (ttfStream.Position - this.startPosition)) / 2];
            for (int k = 0; k < glyphIndexArray.Length; k++)
            {
                glyphIndexArray[k] = ttfStream.ReadUShort();
            }
            this.fontEncoding = this.GetEncoding();
            this.CreateFormat2Map(subHeaderKeys, subHeaders, glyphIndexArray);
        }

        private void ReadUnicodeTable4(TTFStream ttfStream)
        {
            ushort num = ttfStream.ReadUShort();
            this.unicodeVersion = ttfStream.ReadUShort();
            this.segCount = Convert.ToInt32((int) (ttfStream.ReadUShort() / 2));
            this.ends = new ushort[this.segCount];
            this.starts = new ushort[this.segCount];
            this.deltas = new ushort[this.segCount];
            this.rangeOffsets = new ushort[this.segCount];
            ttfStream.Move(6);
            for (int i = 0; i < this.segCount; i++)
            {
                this.ends[i] = ttfStream.ReadUShort();
            }
            ttfStream.Move(2);
            for (int j = 0; j < this.segCount; j++)
            {
                this.starts[j] = ttfStream.ReadUShort();
            }
            for (int k = 0; k < this.segCount; k++)
            {
                this.deltas[k] = ttfStream.ReadUShort();
            }
            for (int m = 0; m < this.segCount; m++)
            {
                this.rangeOffsets[m] = ttfStream.ReadUShort();
            }
            this.CreateFormat4Map(ttfStream);
        }

        private void WriteGlyphIdArray(TTFStream ttfStream)
        {
            for (int i = 0; i < this.rangeOffsets.Length; i++)
            {
                if (this.rangeOffsets[i] != 0)
                {
                    for (int j = this.starts[i]; j <= this.ends[i]; j++)
                    {
                        ttfStream.WriteUShort(this.map[j]);
                    }
                }
            }
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            int position = ttfStream.Position;
            ttfStream.WriteUShort(this.versionNumber);
            ttfStream.WriteUShort(1);
            ttfStream.WriteUShort(3);
            ttfStream.WriteUShort(1);
            ttfStream.WriteULong((uint) ((ttfStream.Position - position) + 4));
            this.WriteUnicodeTable(ttfStream);
        }

        private void WriteUnicodeTable(TTFStream ttfStream)
        {
            int position = ttfStream.Position;
            ttfStream.WriteUShort(4);
            ttfStream.WriteUShort(0);
            ttfStream.WriteUShort(this.unicodeVersion);
            ttfStream.WriteUShort((ushort) (this.segCount * 2));
            double y = Math.Floor(Math.Log((double) this.segCount, 2.0));
            double num3 = Math.Pow(2.0, y) * 2.0;
            ttfStream.WriteUShort((ushort) num3);
            ttfStream.WriteUShort((ushort) y);
            ttfStream.WriteUShort((ushort) ((this.segCount * 2) - num3));
            for (int i = 0; i < this.ends.Length; i++)
            {
                ttfStream.WriteUShort(this.ends[i]);
            }
            ttfStream.WriteUShort(0);
            for (int j = 0; j < this.starts.Length; j++)
            {
                ttfStream.WriteUShort(this.starts[j]);
            }
            for (int k = 0; k < this.deltas.Length; k++)
            {
                ttfStream.WriteUShort(this.deltas[k]);
            }
            for (int m = 0; m < this.rangeOffsets.Length; m++)
            {
                ttfStream.WriteUShort(this.rangeOffsets[m]);
            }
            this.WriteGlyphIdArray(ttfStream);
            int offset = ttfStream.Position - position;
            ttfStream.Seek(position);
            ttfStream.Move(2);
            ttfStream.WriteUShort((ushort) offset);
            ttfStream.Seek(position);
            ttfStream.Move(offset);
        }

        protected internal override string Tag =>
            "cmap";

        public int Count =>
            this.map.Length;

        public ushort this[ushort code]
        {
            get
            {
                ushort index = 0;
                if (this.format == 4)
                {
                    index = (this.eID != 0) ? code : ((this.map[code] == 0) ? ((ushort) (code + 0xf000)) : code);
                }
                if ((this.format == 2) && (this.fontEncoding != null))
                {
                    char[] chars = new char[] { (char) code };
                    byte[] buffer = Encoding.Convert(Encoding.Unicode, this.fontEncoding, Encoding.Unicode.GetBytes(chars));
                    index = (buffer.Length > 1) ? ((ushort) ((buffer[0] << 8) | buffer[1])) : buffer[0];
                }
                return this.map[index];
            }
        }

        public override int Length
        {
            get
            {
                int num = ((14 + (2 * this.segCount)) + 2) + ((2 * this.segCount) * 3);
                for (int i = 0; i < this.segCount; i++)
                {
                    if (this.rangeOffsets[i] != 0)
                    {
                        num += ((this.ends[i] - this.starts[i]) + 1) * 2;
                    }
                }
                return (12 + num);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Subheader
        {
            private int glyphOffset;
            private ushort firstCode;
            private ushort entryCount;
            private short idDelta;
            public Subheader(ushort firstCode, ushort entryCount, short idDelta, int glyphOffset)
            {
                this.firstCode = firstCode;
                this.entryCount = entryCount;
                this.idDelta = idDelta;
                this.glyphOffset = glyphOffset;
            }

            public ushort FirstCode =>
                this.firstCode;
            public ushort EntryCount =>
                this.entryCount;
            public short IdDelta =>
                this.idDelta;
            public int GlyphOffset
            {
                get => 
                    this.glyphOffset;
                set => 
                    this.glyphOffset = value;
            }
        }
    }
}

