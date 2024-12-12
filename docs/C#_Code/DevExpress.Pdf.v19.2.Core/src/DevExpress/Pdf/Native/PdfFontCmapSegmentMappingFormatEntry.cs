namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PdfFontCmapSegmentMappingFormatEntry : PdfFontCmapShortFormatEntry
    {
        private const short finalCode = -1;
        private const short finalDelta = 1;
        private static readonly IDictionary<char, byte> standardEncodingUnicodeToSID;
        private readonly int segCount;
        private readonly short[] endCode;
        private readonly short[] startCode;
        private readonly short[] idDelta;
        private readonly short[] idRangeOffset;
        private readonly short[] glyphIdArray;
        private readonly int[] segmentOffsets;

        static PdfFontCmapSegmentMappingFormatEntry()
        {
            Dictionary<char, byte> dictionary1 = new Dictionary<char, byte>();
            dictionary1.Add(' ', 1);
            dictionary1.Add('!', 2);
            dictionary1.Add('"', 3);
            dictionary1.Add('#', 4);
            dictionary1.Add('$', 5);
            dictionary1.Add('%', 6);
            dictionary1.Add('&', 7);
            dictionary1.Add('’', 8);
            dictionary1.Add('(', 9);
            dictionary1.Add(')', 10);
            dictionary1.Add('*', 11);
            dictionary1.Add('+', 12);
            dictionary1.Add(',', 13);
            dictionary1.Add('-', 14);
            dictionary1.Add('.', 15);
            dictionary1.Add('/', 0x10);
            dictionary1.Add('0', 0x11);
            dictionary1.Add('1', 0x12);
            dictionary1.Add('2', 0x13);
            dictionary1.Add('3', 20);
            dictionary1.Add('4', 0x15);
            dictionary1.Add('5', 0x16);
            dictionary1.Add('6', 0x17);
            dictionary1.Add('7', 0x18);
            dictionary1.Add('8', 0x19);
            dictionary1.Add('9', 0x1a);
            dictionary1.Add(':', 0x1b);
            dictionary1.Add(';', 0x1c);
            dictionary1.Add('<', 0x1d);
            dictionary1.Add('=', 30);
            dictionary1.Add('>', 0x1f);
            dictionary1.Add('?', 0x20);
            dictionary1.Add('@', 0x21);
            dictionary1.Add('A', 0x22);
            dictionary1.Add('B', 0x23);
            dictionary1.Add('C', 0x24);
            dictionary1.Add('D', 0x25);
            dictionary1.Add('E', 0x26);
            dictionary1.Add('F', 0x27);
            dictionary1.Add('G', 40);
            dictionary1.Add('H', 0x29);
            dictionary1.Add('I', 0x2a);
            dictionary1.Add('J', 0x2b);
            dictionary1.Add('K', 0x2c);
            dictionary1.Add('L', 0x2d);
            dictionary1.Add('M', 0x2e);
            dictionary1.Add('N', 0x2f);
            dictionary1.Add('O', 0x30);
            dictionary1.Add('P', 0x31);
            dictionary1.Add('Q', 50);
            dictionary1.Add('R', 0x33);
            dictionary1.Add('S', 0x34);
            dictionary1.Add('T', 0x35);
            dictionary1.Add('U', 0x36);
            dictionary1.Add('V', 0x37);
            dictionary1.Add('W', 0x38);
            dictionary1.Add('X', 0x39);
            dictionary1.Add('Y', 0x3a);
            dictionary1.Add('Z', 0x3b);
            dictionary1.Add('[', 60);
            dictionary1.Add('\\', 0x3d);
            dictionary1.Add(']', 0x3e);
            dictionary1.Add('^', 0x3f);
            dictionary1.Add('_', 0x40);
            dictionary1.Add('‘', 0x41);
            dictionary1.Add('a', 0x42);
            dictionary1.Add('b', 0x43);
            dictionary1.Add('c', 0x44);
            dictionary1.Add('d', 0x45);
            dictionary1.Add('e', 70);
            dictionary1.Add('f', 0x47);
            dictionary1.Add('g', 0x48);
            dictionary1.Add('h', 0x49);
            dictionary1.Add('i', 0x4a);
            dictionary1.Add('j', 0x4b);
            dictionary1.Add('k', 0x4c);
            dictionary1.Add('l', 0x4d);
            dictionary1.Add('m', 0x4e);
            dictionary1.Add('n', 0x4f);
            dictionary1.Add('o', 80);
            dictionary1.Add('p', 0x51);
            dictionary1.Add('q', 0x52);
            dictionary1.Add('r', 0x53);
            dictionary1.Add('s', 0x54);
            dictionary1.Add('t', 0x55);
            dictionary1.Add('u', 0x56);
            dictionary1.Add('v', 0x57);
            dictionary1.Add('w', 0x58);
            dictionary1.Add('x', 0x59);
            dictionary1.Add('y', 90);
            dictionary1.Add('z', 0x5b);
            dictionary1.Add('{', 0x5c);
            dictionary1.Add('|', 0x5d);
            dictionary1.Add('}', 0x5e);
            dictionary1.Add('~', 0x5f);
            dictionary1.Add('\x00a1', 0x60);
            dictionary1.Add('\x00a2', 0x61);
            dictionary1.Add('\x00a3', 0x62);
            dictionary1.Add('⁄', 0x63);
            dictionary1.Add('\x00a5', 100);
            dictionary1.Add('ƒ', 0x65);
            dictionary1.Add('\x00a7', 0x66);
            dictionary1.Add('\x00a4', 0x67);
            dictionary1.Add('\'', 0x68);
            dictionary1.Add('“', 0x69);
            dictionary1.Add('\x00ab', 0x6a);
            dictionary1.Add('‹', 0x6b);
            dictionary1.Add('›', 0x6c);
            dictionary1.Add(0xfb01, 0x6d);
            dictionary1.Add(0xfb02, 110);
            dictionary1.Add('–', 0x6f);
            dictionary1.Add('†', 0x70);
            dictionary1.Add('‡', 0x71);
            dictionary1.Add('\x00b7', 0x72);
            dictionary1.Add('\x00b6', 0x73);
            dictionary1.Add('•', 0x74);
            dictionary1.Add('‚', 0x75);
            dictionary1.Add('„', 0x76);
            dictionary1.Add('”', 0x77);
            dictionary1.Add('\x00bb', 120);
            dictionary1.Add('…', 0x79);
            dictionary1.Add('‰', 0x7a);
            dictionary1.Add('\x00bf', 0x7b);
            dictionary1.Add('`', 0x7c);
            dictionary1.Add('\x00b4', 0x7d);
            dictionary1.Add('ˆ', 0x7e);
            dictionary1.Add('˜', 0x7f);
            dictionary1.Add('\x00af', 0x80);
            dictionary1.Add('˘', 0x81);
            dictionary1.Add('˙', 130);
            dictionary1.Add('\x00a8', 0x83);
            dictionary1.Add('˚', 0x84);
            dictionary1.Add('\x00b8', 0x85);
            dictionary1.Add('˝', 0x86);
            dictionary1.Add('˛', 0x87);
            dictionary1.Add('ˇ', 0x88);
            dictionary1.Add('—', 0x89);
            dictionary1.Add('\x00c6', 0x8a);
            dictionary1.Add('\x00aa', 0x8b);
            dictionary1.Add('Ł', 140);
            dictionary1.Add('\x00d8', 0x8d);
            dictionary1.Add('Œ', 0x8e);
            dictionary1.Add('\x00ba', 0x8f);
            dictionary1.Add('\x00e6', 0x90);
            dictionary1.Add('ı', 0x91);
            dictionary1.Add('ł', 0x92);
            dictionary1.Add('\x00f8', 0x93);
            dictionary1.Add('œ', 0x94);
            dictionary1.Add('\x00df', 0x95);
            standardEncodingUnicodeToSID = dictionary1;
        }

        public PdfFontCmapSegmentMappingFormatEntry(PdfFontEncodingID encodingID) : base(PdfFontPlatformID.Microsoft, encodingID, (short) 0)
        {
            this.segCount = 2;
            short[] numArray1 = new short[2];
            numArray1[1] = -1;
            this.endCode = numArray1;
            short[] numArray2 = new short[2];
            numArray2[1] = -1;
            this.startCode = numArray2;
            short[] numArray3 = new short[2];
            numArray3[1] = 1;
            this.idDelta = numArray3;
            short[] numArray4 = new short[2];
            numArray4[0] = 4;
            this.idRangeOffset = numArray4;
            this.glyphIdArray = new short[1];
        }

        public PdfFontCmapSegmentMappingFormatEntry(IDictionary<short, short> charset) : base(PdfFontPlatformID.Microsoft, PdfFontEncodingID.UGL, (short) 0)
        {
            SortedDictionary<char, short> dictionary = new SortedDictionary<char, short>();
            foreach (KeyValuePair<char, byte> pair in standardEncodingUnicodeToSID)
            {
                short num3;
                if (charset.TryGetValue(pair.Value, out num3))
                {
                    dictionary.Add(pair.Key, num3);
                }
            }
            int count = dictionary.Count;
            count ??= 1;
            this.segCount = count + 1;
            this.startCode = new short[this.segCount];
            this.endCode = new short[this.segCount];
            this.idDelta = new short[this.segCount];
            this.idRangeOffset = new short[this.segCount];
            int index = 0;
            foreach (KeyValuePair<char, short> pair2 in dictionary)
            {
                char key = pair2.Key;
                this.startCode[index] = (short) key;
                this.endCode[index] = (short) key;
                this.idDelta[index++] = pair2.Value - key;
            }
            this.startCode[count] = -1;
            this.endCode[count] = -1;
            this.idDelta[count] = 1;
            this.glyphIdArray = new short[0];
        }

        public PdfFontCmapSegmentMappingFormatEntry(PdfFontEncodingID encodingID, PdfFontCmapByteEncodingFormatEntry formatEntry) : base(PdfFontPlatformID.Microsoft, encodingID, formatEntry.Language)
        {
            byte[] glyphIdArray = formatEntry.GlyphIdArray;
            int length = glyphIdArray.Length;
            SortedDictionary<ushort, short> dictionary = new SortedDictionary<ushort, short>();
            if (encodingID == PdfFontEncodingID.Symbol)
            {
                for (ushort i = 0; i < length; i = (ushort) (i + 1))
                {
                    short num5 = glyphIdArray[i];
                    if (num5 != 0)
                    {
                        dictionary.Add((ushort) (0xf000 + i), num5);
                    }
                }
            }
            else if (formatEntry.PlatformId == PdfFontPlatformID.Macintosh)
            {
                for (short i = 0; i < length; i = (short) (i + 1))
                {
                    string str;
                    ushort num8;
                    short num7 = glyphIdArray[i];
                    if ((num7 != 0) && (PdfSimpleFontEncoding.MacRomanEncoding.TryGetValue((byte) i, out str) && PdfUnicodeConverter.GlyphCodes.TryGetValue(str, out num8)))
                    {
                        dictionary.Add(num8, num7);
                    }
                }
            }
            else
            {
                for (ushort i = 0; i < length; i = (ushort) (i + 1))
                {
                    short num10 = glyphIdArray[i];
                    if (num10 != 0)
                    {
                        dictionary.Add(i, num10);
                    }
                }
            }
            int count = dictionary.Count;
            this.segCount = count + 1;
            this.startCode = new short[this.segCount];
            this.endCode = new short[this.segCount];
            this.idDelta = new short[this.segCount];
            this.idRangeOffset = new short[this.segCount];
            int index = 0;
            foreach (KeyValuePair<ushort, short> pair in dictionary)
            {
                short key = (short) pair.Key;
                this.startCode[index] = key;
                this.endCode[index] = key;
                this.idDelta[index++] = pair.Value - key;
            }
            this.startCode[count] = -1;
            this.endCode[count] = -1;
            this.idDelta[count] = 1;
            this.glyphIdArray = new short[0];
        }

        public PdfFontCmapSegmentMappingFormatEntry(PdfFontEncodingID encodingID, PdfFontCmapSegmentMappingFormatEntry formatEntry) : base(PdfFontPlatformID.Microsoft, encodingID, formatEntry.Language)
        {
            this.segCount = formatEntry.segCount;
            this.endCode = formatEntry.endCode;
            this.startCode = formatEntry.startCode;
            this.idDelta = formatEntry.idDelta;
            this.idRangeOffset = formatEntry.idRangeOffset;
            this.glyphIdArray = formatEntry.GlyphIdArray;
        }

        public PdfFontCmapSegmentMappingFormatEntry(PdfFontEncodingID encodingID, PdfFontCmapTrimmedMappingFormatEntry formatEntry) : base(PdfFontPlatformID.Microsoft, encodingID, formatEntry.Language)
        {
            this.segCount = 2;
            short firstCode = formatEntry.FirstCode;
            short entryCount = formatEntry.EntryCount;
            if ((encodingID == PdfFontEncodingID.Symbol) && ((firstCode + entryCount) < 0x1000))
            {
                firstCode = (short) (firstCode + 0xf000);
            }
            this.endCode = new short[] { (short) ((firstCode + entryCount) - 1), -1 };
            this.startCode = new short[] { firstCode, -1 };
            short[] numArray3 = new short[2];
            numArray3[1] = 1;
            this.idDelta = numArray3;
            short[] numArray4 = new short[2];
            numArray4[0] = 4;
            this.idRangeOffset = numArray4;
            this.glyphIdArray = formatEntry.GlyphIdArray;
        }

        public PdfFontCmapSegmentMappingFormatEntry(PdfFontPlatformID platformId, PdfFontEncodingID encodingId, PdfBinaryStream stream) : base(platformId, encodingId, stream)
        {
            this.segCount = stream.ReadShort() / 2;
            stream.ReadShort();
            stream.ReadShort();
            stream.ReadShort();
            this.endCode = this.ReadSegmentsArray(stream);
            stream.ReadShort();
            this.startCode = this.ReadSegmentsArray(stream);
            this.idDelta = this.ReadSegmentsArray(stream);
            this.idRangeOffset = this.ReadSegmentsArray(stream);
            int num = (base.BodyLength - this.SegmentsLength) / 2;
            if (num < 0)
            {
                num = 0;
            }
            this.glyphIdArray = new short[num];
            for (int i = 0; i < num; i++)
            {
                this.glyphIdArray[i] = stream.ReadShort();
            }
            this.segmentOffsets = new int[this.segCount];
            int index = 0;
            for (int j = 1; index < this.segCount; j++)
            {
                this.segmentOffsets[index] = ((this.idRangeOffset[index] - (this.segCount - j)) - 2) / 2;
                index++;
            }
        }

        internal IDictionary<string, ushort> GetGlyphMapping(IList<string> glyphNames)
        {
            if (glyphNames == null)
            {
                return null;
            }
            int count = glyphNames.Count;
            int length = this.glyphIdArray.Length;
            Dictionary<string, ushort> dictionary = new Dictionary<string, ushort>();
            int index = 0;
            for (int i = this.segCount; index < this.segCount; i--)
            {
                short num5 = this.startCode[index];
                short num6 = this.endCode[index];
                if (num5 != -1)
                {
                    short num7 = this.idRangeOffset[index];
                    if (num7 <= 0)
                    {
                        int num12 = num5 + this.idDelta[index];
                        short num13 = num5;
                        while (num13 <= num6)
                        {
                            if (num12 >= count)
                            {
                                return null;
                            }
                            dictionary[glyphNames[num12]] = (ushort) num13;
                            num13 = (short) (num13 + 1);
                            num12++;
                        }
                    }
                    else
                    {
                        int num9 = num5 + (((num7 / 2) - num5) - i);
                        short num10 = num5;
                        while ((num10 <= num6) && (num9 < length))
                        {
                            short num11 = this.glyphIdArray[num9];
                            if ((num11 < 0) || (num11 >= count))
                            {
                                return null;
                            }
                            dictionary[glyphNames[num11]] = (ushort) num10;
                            num10 = (short) (num10 + 1);
                            num9++;
                        }
                    }
                }
                index++;
            }
            return dictionary;
        }

        public override int MapCode(char character)
        {
            bool isSymbolEncoding = base.IsSymbolEncoding;
            for (int i = 0; i < this.segCount; i++)
            {
                ushort num2 = (ushort) this.endCode[i];
                ushort num3 = (ushort) this.startCode[i];
                short num4 = this.idDelta[i];
                bool flag2 = (isSymbolEncoding && (num3 >= 0xf000)) && ((character < 0xf000) || (character > 0xf0ff));
                if (flag2)
                {
                    num3 = (ushort) (num3 - 0xf000);
                    num2 = (ushort) (num2 - 0xf000);
                }
                if (character <= num2)
                {
                    int num5 = character - num3;
                    if (num5 >= 0)
                    {
                        ushort num6 = (ushort) this.idRangeOffset[i];
                        if (num6 != 0)
                        {
                            int index = (((num6 / 2) + num5) + i) - this.segCount;
                            return (((index < 0) || (index >= this.glyphIdArray.Length)) ? 0 : ((ushort) (this.glyphIdArray[index] + num4)));
                        }
                        if (flag2)
                        {
                            num4 = (short) (num4 + 0xf000);
                        }
                        return ((character == '\0') ? character : ((character + num4) % 0x10000));
                    }
                }
            }
            return 0;
        }

        private short[] ReadSegmentsArray(PdfBinaryStream cmapStream)
        {
            short[] numArray = new short[this.segCount];
            for (int i = 0; i < this.segCount; i++)
            {
                numArray[i] = cmapStream.ReadShort();
            }
            return numArray;
        }

        public bool Validate()
        {
            if (this.segCount > 0)
            {
                int capacity = this.segCount - 1;
                ushort num2 = (ushort) this.endCode[0];
                int num3 = 0;
                for (int i = 1; i < capacity; i++)
                {
                    ushort num5 = (ushort) this.endCode[i];
                    if (num5 < num2)
                    {
                        List<Row> list = new List<Row>(capacity);
                        for (int j = 0; j < capacity; j++)
                        {
                            list.Add(new Row(this.endCode[j], this.startCode[j], this.idDelta[j], this.idRangeOffset[j]));
                        }
                        list.Sort();
                        for (int k = 0; k < capacity; k++)
                        {
                            Row row = list[k];
                            this.endCode[k] = row.EndCode;
                            this.startCode[k] = row.StartCode;
                            this.idDelta[k] = row.IdDelta;
                            this.idRangeOffset[k] = row.IdRangeOffset;
                        }
                        return true;
                    }
                    num2 = num5;
                    num3++;
                }
            }
            return false;
        }

        public override void Write(PdfBinaryStream tableStream)
        {
            base.Write(tableStream);
            short num = (short) (this.segCount * 2);
            tableStream.WriteShort(num);
            short num2 = Convert.ToInt16((double) (2.0 * Math.Pow(2.0, Math.Floor(Math.Log((double) this.segCount, 2.0)))));
            tableStream.WriteShort(num2);
            tableStream.WriteShort(Convert.ToInt16(Math.Log((double) (num2 / 2), 2.0)));
            tableStream.WriteShort((short) (num - num2));
            tableStream.WriteShortArray(this.endCode);
            tableStream.WriteShort(0);
            tableStream.WriteShortArray(this.startCode);
            tableStream.WriteShortArray(this.idDelta);
            tableStream.WriteShortArray(this.idRangeOffset);
            tableStream.WriteShortArray(this.glyphIdArray);
        }

        private int SegmentsLength =>
            10 + (this.segCount * 8);

        internal List<PdfFontCmapGlyphRange> GlyphRanges
        {
            get
            {
                List<PdfFontCmapGlyphRange> list = new List<PdfFontCmapGlyphRange>();
                int index = 0;
                while (true)
                {
                    if (index < this.segCount)
                    {
                        short end = this.endCode[index];
                        if (end != -1)
                        {
                            list.Add(new PdfFontCmapGlyphRange(this.startCode[index], end));
                            index++;
                            continue;
                        }
                    }
                    return list;
                }
            }
        }

        public int SegCount =>
            this.segCount;

        public short[] EndCode =>
            this.endCode;

        public short[] StartCode =>
            this.startCode;

        public short[] IdDelta =>
            this.idDelta;

        public short[] IdRangeOffset =>
            this.idRangeOffset;

        public short[] GlyphIdArray =>
            this.glyphIdArray;

        public override int Length =>
            (6 + this.SegmentsLength) + (this.glyphIdArray.Length * 2);

        protected override PdfFontCmapFormatID Format =>
            PdfFontCmapFormatID.SegmentMapping;

        [StructLayout(LayoutKind.Sequential)]
        private struct Row : IComparable<PdfFontCmapSegmentMappingFormatEntry.Row>
        {
            private readonly short endCode;
            private readonly short startCode;
            private readonly short idDelta;
            private readonly short idRangeOffset;
            public short EndCode =>
                this.endCode;
            public short StartCode =>
                this.startCode;
            public short IdDelta =>
                this.idDelta;
            public short IdRangeOffset =>
                this.idRangeOffset;
            public Row(short endCode, short startCode, short idDelta, short idRangeOffset)
            {
                this.endCode = endCode;
                this.startCode = startCode;
                this.idDelta = idDelta;
                this.idRangeOffset = idRangeOffset;
            }

            int IComparable<PdfFontCmapSegmentMappingFormatEntry.Row>.CompareTo(PdfFontCmapSegmentMappingFormatEntry.Row other)
            {
                int num = ((ushort) this.endCode) - ((ushort) other.endCode);
                if (num == 0)
                {
                    num = ((ushort) this.startCode) - ((ushort) other.startCode);
                    num ??= (((ushort) this.idDelta) - ((ushort) other.idDelta));
                }
                return num;
            }
        }
    }
}

