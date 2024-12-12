namespace DevExpress.Utils.Internal
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.CompilerServices;

    public class TTFontInfo
    {
        private static readonly int ErrorGlyphWidth = 0x7d0;
        private TTFHeader header;
        private CMAPTable cmap;
        internal ushort[] widths;
        internal KernTable kernTable = new KernTable();

        public TTFontInfo(Stream stream)
        {
            BigEndianStreamReader reader = new BigEndianStreamReader(stream);
            this.header = new TTFHeader();
            this.header.Read(reader);
            if (!this.header.IsInternal)
            {
                this.ReadCMAPTable(reader);
                this.ReadHMetrics(reader);
                this.ReadKerning(reader);
            }
            else
            {
                this.ReadCMAPTableInternal(reader);
                if (this.header.Version == -1f)
                {
                    this.ReadHMetricsInternalV1(reader);
                }
                else
                {
                    this.ReadHMetricsInternalV2(reader);
                }
                if (this.header.IsSupportKerningInternal)
                {
                    this.ReadKerningInternal(reader);
                }
            }
        }

        private int AlternativeMeasureChar(char c, double fontSize) => 
            ErrorGlyphWidth;

        public int FUnitsToFontSizeUnits(int valueInFUnits, double fontSizeInUnits) => 
            (int) Math.Round((double) ((valueInFUnits * fontSizeInUnits) / ((double) this.header.UnitsPerEm)));

        public int GetAscent(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.Ascent, fontSizeInUnits);

        public Size[] GetCharSegments() => 
            this.cmap.GetCharSegments();

        public int GetDescent(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.Descent, fontSizeInUnits);

        public int GetGlyphIndex(char chr) => 
            this.cmap.GetGlyphIndex(chr);

        public int GetLineGap(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.LineGap, fontSizeInUnits);

        private string GetNextWord(string str, int position)
        {
            int length = str.Length;
            if (position >= str.Length)
            {
                return null;
            }
            int startIndex = position;
            while ((position < length) && this.IsSpaceOrLineSeparator(str[position]))
            {
                position++;
            }
            if (position >= length)
            {
                return str.Substring(startIndex);
            }
            while ((position < length) && !this.IsSpaceOrLineSeparator(str[position]))
            {
                position++;
            }
            if (position >= length)
            {
                return str.Substring(startIndex);
            }
            while ((position < length) && this.IsSpaceOrLineSeparator(str[position]))
            {
                position++;
            }
            return ((position < length) ? str.Substring(startIndex, position - startIndex) : str.Substring(startIndex));
        }

        public int GetStrikeOutPosition(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.StrikeOutPosition, fontSizeInUnits);

        public int GetStrikeOutSize(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.StrikeOutSize, fontSizeInUnits);

        public int GetSubscriptXOffset(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.SubscriptXOffset, fontSizeInUnits);

        public int GetSubscriptXSize(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.SubscriptXSize, fontSizeInUnits);

        public int GetSubscriptYOffset(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.SubscriptYOffset, fontSizeInUnits);

        public int GetSubscriptYSize(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.SubscriptYSize, fontSizeInUnits);

        public int GetSuperscriptXOffset(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.SuperscriptXOffset, fontSizeInUnits);

        public int GetSuperscriptXSize(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.SuperscriptXSize, fontSizeInUnits);

        public int GetSuperscriptYOffset(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.SuperscriptYOffset, fontSizeInUnits);

        public int GetSuperscriptYSize(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.SuperscriptYSize, fontSizeInUnits);

        public int GetUnderlinePosition(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.UnderlinePosition, fontSizeInUnits);

        public int GetUnderlineSize(double fontSizeInUnits) => 
            this.FUnitsToFontSizeUnits(this.header.UnderlineSize, fontSizeInUnits);

        private bool IsSpaceOrLineSeparator(char ch) => 
            char.IsWhiteSpace(ch);

        public Rectangle[] MeasureCharacterBounds(string str, double fontSizeInUnits)
        {
            Rectangle[] rectangleArray = new Rectangle[str.Length];
            if (!string.IsNullOrEmpty(str))
            {
                int height = this.FUnitsToFontSizeUnits((this.header.Ascent + this.header.Descent) + this.header.LineGap, fontSizeInUnits);
                int glyphIndex = this.GetGlyphIndex(str[0]);
                int x = 0;
                for (int i = 0; i < str.Length; i++)
                {
                    int index = glyphIndex;
                    int valueInFUnits = ((this.widths.Length <= index) || (index < 0)) ? ErrorGlyphWidth : this.widths[index];
                    if (i != (str.Length - 1))
                    {
                        glyphIndex = this.GetGlyphIndex(str[i + 1]);
                        valueInFUnits += this.kernTable[index, glyphIndex];
                    }
                    valueInFUnits = this.FUnitsToFontSizeUnits(valueInFUnits, fontSizeInUnits);
                    rectangleArray[i] = new Rectangle(x, 0, valueInFUnits, height);
                    x += valueInFUnits;
                }
            }
            return rectangleArray;
        }

        public SizeF MeasureMultilineText(string str, double availableWidth, double fontSize)
        {
            string[] strArray;
            if (string.IsNullOrEmpty(str))
            {
                return new SizeF(0f, (float) ((((this.header.Ascent + this.header.Descent) + this.header.LineGap) * fontSize) / ((double) this.header.UnitsPerEm)));
            }
            availableWidth *= ((double) this.header.UnitsPerEm) / fontSize;
            bool flag = (str.IndexOf('\r') >= 0) || (str.IndexOf('\n') >= 0);
            if (flag)
            {
                str = str.Replace("\r\n", "\n").Replace('\r', '\n');
            }
            SizeF ef = new SizeF();
            int num = 1;
            if (flag)
            {
                char[] separator = new char[] { '\n' };
                strArray = str.Split(separator);
            }
            else
            {
                strArray = new string[] { str };
            }
            float num2 = 0f;
            int length = strArray.Length;
            bool flag2 = false;
            int index = 0;
            while (index < length)
            {
                int position = 0;
                num2 = 0f;
                flag2 = false;
                if (index > 0)
                {
                    ef.Width = Math.Max(ef.Width, num2);
                    num++;
                }
                while (true)
                {
                    string nextWord = this.GetNextWord(strArray[index], position);
                    if (nextWord == null)
                    {
                        index++;
                        break;
                    }
                    flag2 = false;
                    float num6 = this.MeasureTextWidth(nextWord, fontSize);
                    if ((num2 + num6) <= availableWidth)
                    {
                        num2 += num6;
                    }
                    else if (num2 == 0f)
                    {
                        ef.Width = Math.Max(ef.Width, num2 + this.MeasureTextWidth(nextWord.Trim(), fontSize));
                        num++;
                        num2 = 0f;
                        flag2 = true;
                    }
                    else
                    {
                        num6 = this.MeasureTextWidth(nextWord.Trim(), fontSize);
                        if ((num2 + num6) > availableWidth)
                        {
                            ef.Width = Math.Max(ef.Width, num2);
                            num++;
                            num2 = num6;
                        }
                        else
                        {
                            ef.Width = Math.Max(ef.Width, num2 + num6);
                            num++;
                            num2 = 0f;
                            flag2 = true;
                        }
                    }
                    position += nextWord.Length;
                }
            }
            if (flag2)
            {
                num = Math.Max(1, num - 1);
            }
            ef.Width = Math.Max(ef.Width, num2);
            ef.Height = num * ((this.header.Ascent + this.header.Descent) + this.header.LineGap);
            ef.Width = (float) ((ef.Width * fontSize) / ((double) this.header.UnitsPerEm));
            ef.Height = (float) ((ef.Height * fontSize) / ((double) this.header.UnitsPerEm));
            return ef;
        }

        public SizeF MeasureText(string str, double fontSize)
        {
            if (string.IsNullOrEmpty(str))
            {
                return SizeF.Empty;
            }
            SizeF ef = new SizeF(this.MeasureTextWidth(str, fontSize), (float) ((this.header.Ascent + this.header.Descent) + this.header.LineGap));
            ef.Width = (float) ((ef.Width * fontSize) / ((double) this.header.UnitsPerEm));
            ef.Height = (float) ((ef.Height * fontSize) / ((double) this.header.UnitsPerEm));
            return ef;
        }

        private float MeasureTextWidth(string str, double fontSize)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0f;
            }
            float num = 0f;
            int glyphIndex = this.GetGlyphIndex(str[0]);
            for (int i = 0; i < str.Length; i++)
            {
                int index = glyphIndex;
                num = ((this.widths.Length <= index) || (index < 0)) ? (num + this.AlternativeMeasureChar(str[i], fontSize)) : (num + this.widths[index]);
                if (i != (str.Length - 1))
                {
                    glyphIndex = this.GetGlyphIndex(str[i + 1]);
                    num += this.kernTable[index, glyphIndex];
                }
            }
            return num;
        }

        private void ReadCMAPTable(BigEndianStreamReader reader)
        {
            reader.Stream.Position = this.header.Tables["cmap"].Offset;
            this.cmap = new CMAPTable();
            this.cmap.Read(reader);
        }

        private void ReadCMAPTableInternal(BigEndianStreamReader reader)
        {
            this.cmap = new CMAPTable();
            this.cmap.ReadInternal(reader);
        }

        private void ReadHMetrics(BigEndianStreamReader reader)
        {
            reader.Stream.Position = this.header.Tables["hmtx"].Offset;
            int hMetricsCount = this.header.HMetricsCount;
            int glyphsCount = this.header.GlyphsCount;
            this.widths = new ushort[glyphsCount];
            for (int i = 0; i < glyphsCount; i++)
            {
                if (i >= hMetricsCount)
                {
                    this.widths[i] = this.widths[0];
                }
                else
                {
                    this.widths[i] = reader.ReadUShort();
                    reader.Stream.Seek(2L, SeekOrigin.Current);
                }
            }
        }

        private void ReadHMetricsInternalCompressedFormat(BigEndianStreamReader reader)
        {
            int glyphsCount = this.header.GlyphsCount;
            this.widths = new ushort[glyphsCount];
            int num2 = 0;
            while (num2 < glyphsCount)
            {
                int num3 = reader.ReadUShort();
                ushort num4 = reader.ReadUShort();
                int num5 = num2 + num3;
                int index = num2;
                while (true)
                {
                    if (index >= num5)
                    {
                        num2 = num5;
                        break;
                    }
                    this.widths[index] = num4;
                    index++;
                }
            }
        }

        private void ReadHMetricsInternalDefaultFormat(BigEndianStreamReader reader)
        {
            int glyphsCount = this.header.GlyphsCount;
            this.widths = new ushort[glyphsCount];
            for (int i = 0; i < glyphsCount; i++)
            {
                this.widths[i] = reader.ReadUShort();
            }
        }

        private void ReadHMetricsInternalV1(BigEndianStreamReader reader)
        {
            if (reader.Stream.ReadByte() > 0)
            {
                this.ReadHMetricsInternalCompressedFormat(reader);
            }
            else
            {
                this.ReadHMetricsInternalDefaultFormat(reader);
            }
        }

        private void ReadHMetricsInternalV2(BigEndianStreamReader reader)
        {
            int glyphsCount = this.header.GlyphsCount;
            ArrayStreamHelper<ushort> helper = new ArrayStreamHelper<ushort>();
            Func<BigEndianStreamReader, ushort> readItemAction = <>c.<>9__17_0;
            if (<>c.<>9__17_0 == null)
            {
                Func<BigEndianStreamReader, ushort> local1 = <>c.<>9__17_0;
                readItemAction = <>c.<>9__17_0 = r => r.ReadUShort();
            }
            this.widths = helper.ReadArray(reader, glyphsCount, readItemAction);
        }

        private void ReadKerning(BigEndianStreamReader reader)
        {
            if (this.header.Tables.ContainsKey("kern"))
            {
                reader.Stream.Position = this.header.Tables["kern"].Offset;
                if (reader.ReadUShort() != 0)
                {
                    return;
                }
                if (reader.ReadUShort() > 0)
                {
                    this.kernTable = new KernTable(reader);
                    return;
                }
            }
            this.kernTable = new KernTable();
        }

        private void ReadKerningInternal(BigEndianStreamReader reader)
        {
            this.kernTable = new KernTable();
            this.kernTable.ReadInternal(reader);
        }

        protected void SetAscent(int value)
        {
            this.header.Ascent = value;
        }

        protected void SetDescent(int value)
        {
            this.header.Descent = value;
        }

        protected void SetLineGap(int value)
        {
            this.header.LineGap = value;
        }

        private void WriteCMAPTableInternal(BigEndianStreamWriter writer)
        {
            this.cmap.WriteInternal(writer);
        }

        private void WriteHMetricsInternal(BigEndianStreamWriter writer)
        {
            Action<BigEndianStreamWriter, ushort> writeItemAction = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                Action<BigEndianStreamWriter, ushort> local1 = <>c.<>9__21_0;
                writeItemAction = <>c.<>9__21_0 = (w, value) => w.WriteUShort(value);
            }
            new ArrayStreamHelper<ushort>().WriteArray(writer, this.widths, writeItemAction);
        }

        public void WriteInternal(Stream stream)
        {
            BigEndianStreamWriter writer = new BigEndianStreamWriter(stream);
            this.header.WriteInternal(writer);
            this.WriteCMAPTableInternal(writer);
            this.WriteHMetricsInternal(writer);
            this.WriteKerningInternal(writer);
        }

        private void WriteKerningInternal(BigEndianStreamWriter writer)
        {
            this.kernTable.WriteInternal(writer);
        }

        public TTFWeightClass WeightClass =>
            this.header.WeightClass;

        public byte[] Panose =>
            this.header.Panose;

        [CLSCompliant(false)]
        public ushort[] Widths =>
            this.widths;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TTFontInfo.<>c <>9 = new TTFontInfo.<>c();
            public static Func<BigEndianStreamReader, ushort> <>9__17_0;
            public static Action<BigEndianStreamWriter, ushort> <>9__21_0;

            internal ushort <ReadHMetricsInternalV2>b__17_0(BigEndianStreamReader r) => 
                r.ReadUShort();

            internal void <WriteHMetricsInternal>b__21_0(BigEndianStreamWriter w, ushort value)
            {
                w.WriteUShort(value);
            }
        }
    }
}

