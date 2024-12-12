namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatTopDictIndexWriter
    {
        private readonly byte majorVersion;
        private readonly byte minorVersion;
        private readonly string name;
        private readonly PdfCompactFontFormatStringIndex stringIndex;
        private readonly PdfCompactFontFormatBinaryIndex globalSubrs;
        private readonly List<PdfCompactFontFormatDictIndexOperator> operators;
        private readonly List<ICompactFontFormatDictIndexOffsetOperator> offsetOperators;

        private PdfCompactFontFormatTopDictIndexWriter(PdfType1FontCompactFontProgram fontProgram)
        {
            double italicAngle;
            int uniqueID;
            this.operators = new List<PdfCompactFontFormatDictIndexOperator>();
            this.offsetOperators = new List<ICompactFontFormatDictIndexOffsetOperator>();
            this.majorVersion = fontProgram.MajorVersion;
            this.minorVersion = fontProgram.MinorVersion;
            this.name = fontProgram.FontName;
            this.stringIndex = fontProgram.StringIndex;
            this.globalSubrs = new PdfCompactFontFormatBinaryIndex(fontProgram.GlobalSubrs);
            PdfType1FontCompactCIDFontProgram program = fontProgram as PdfType1FontCompactCIDFontProgram;
            if (program != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexROSOperator(program.Registry, program.Ordering, program.Supplement));
                italicAngle = program.CIDFontVersion;
                if (italicAngle != 0.0)
                {
                    this.operators.Add(new PdfCompactFontFormatDictIndexCIDFontVersionOperator(italicAngle));
                }
                uniqueID = program.CIDCount;
                if (uniqueID != 0x2210)
                {
                    this.operators.Add(new PdfCompactFontFormatDictIndexCIDCountOperator(uniqueID));
                }
                int? uIDBase = program.UIDBase;
                if (uIDBase != null)
                {
                    this.operators.Add(new PdfCompactFontFormatDictIndexUIDBaseOperator(uIDBase.Value));
                }
                if (program.GlyphGroupSelector != null)
                {
                    PdfCompactFontFormatDictIndexFDSelectOperator item = new PdfCompactFontFormatDictIndexFDSelectOperator(program.GlyphGroupSelector);
                    this.operators.Add(item);
                    this.offsetOperators.Add(item);
                }
                if (program.GlyphGroupData != null)
                {
                    PdfCompactFontFormatDictIndexFDArrayOperator item = new PdfCompactFontFormatDictIndexFDArrayOperator(program.GlyphGroupData, this.stringIndex);
                    this.operators.Add(item);
                    this.offsetOperators.Add(item);
                    this.offsetOperators.AddRange(item.OffsetOperators);
                }
            }
            PdfType1FontInfo fontInfo = fontProgram.FontInfo;
            string version = fontInfo.Version;
            if (version != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexVersionOperator(version));
            }
            version = fontInfo.Notice;
            if (version != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexNoticeOperator(version));
            }
            version = fontInfo.Copyright;
            if (version != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexCopyrightOperator(version));
            }
            version = fontInfo.FullName;
            if (version != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexFullNameOperator(version));
            }
            version = fontInfo.FamilyName;
            if (version != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexFamilyNameOperator(version));
            }
            version = fontInfo.Weight;
            if (version != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexWeightOperator(version));
            }
            if (fontInfo.IsFixedPitch)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexIsFixedPitchOperator(true));
            }
            italicAngle = fontInfo.ItalicAngle;
            if (italicAngle != 0.0)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexItalicAngleOperator(italicAngle));
            }
            italicAngle = fontInfo.UnderlinePosition;
            if (italicAngle != -100.0)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexUnderlinePositionOperator(italicAngle));
            }
            italicAngle = fontInfo.UnderlineThickness;
            if (italicAngle != 50.0)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexUnderlineThicknessOperator(italicAngle));
            }
            PdfType1FontType fontType = fontProgram.FontType;
            if (fontType != PdfType1FontType.Type2)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexCharstringTypeOperator(fontType));
            }
            PdfTransformationMatrix fontMatrix = fontProgram.FontMatrix;
            if (!fontMatrix.Equals(PdfType1FontCompactFontProgram.DefaultFontMatrix))
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexFontMatrixOperator(fontMatrix));
            }
            uniqueID = fontProgram.UniqueID;
            if (uniqueID != 0)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexUniqueIDOperator(uniqueID));
            }
            if (fontProgram.PaintType != PdfType1FontPaintType.Filled)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexPaintTypeOperator(fontProgram.PaintType));
            }
            PdfRectangle fontBBox = fontProgram.FontBBox;
            if (!fontBBox.Equals(PdfType1FontCompactFontProgram.DefaultFontBBox))
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexFontBBoxOperator(fontBBox));
            }
            italicAngle = fontProgram.StrokeWidth;
            if (italicAngle != 0.0)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexStrokeWidthOperator(italicAngle));
            }
            int[] xUID = fontProgram.XUID;
            if (xUID != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexXUIDOperator(xUID));
            }
            PdfType1FontEncoding encoding = fontProgram.Encoding;
            if (!encoding.IsDefault)
            {
                PdfCompactFontFormatDictIndexEncodingOperator item = new PdfCompactFontFormatDictIndexEncodingOperator(encoding);
                this.operators.Add(item);
                if (encoding.Offset == 0)
                {
                    this.offsetOperators.Add(item);
                }
            }
            PdfType1FontCharset charset = fontProgram.Charset;
            if (!charset.IsDefault)
            {
                PdfCompactFontFormatDictIndexCharsetOperator item = new PdfCompactFontFormatDictIndexCharsetOperator(charset);
                this.operators.Add(item);
                if (charset.Offset == 0)
                {
                    this.offsetOperators.Add(item);
                }
            }
            IList<byte[]> charStrings = fontProgram.CharStrings;
            if (charStrings != null)
            {
                PdfCompactFontFormatDictIndexCharStringsOperator item = new PdfCompactFontFormatDictIndexCharStringsOperator(charStrings);
                this.operators.Add(item);
                this.offsetOperators.Add(item);
            }
            PdfType1FontPrivateData @private = fontProgram.Private;
            if (@private != null)
            {
                PdfCompactFontFormatDictIndexPrivateOperator item = new PdfCompactFontFormatDictIndexPrivateOperator(@private);
                this.operators.Add(item);
                this.offsetOperators.Add(item);
            }
            version = fontProgram.PostScript;
            if (version != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexPostScriptOperator(version));
            }
            version = fontInfo.BaseFontName;
            if (version != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexBaseFontNameOperator(version));
            }
            double[] baseFontBlend = fontProgram.BaseFontBlend;
            if (baseFontBlend != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexBaseFontBlendOperator(baseFontBlend));
            }
            version = fontProgram.CIDFontName;
            if (version != null)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexFontNameOperator(version));
            }
            bool flag = true;
            while (flag)
            {
                flag = false;
                int num3 = ((this.name.Length + this.stringIndex.DataLength) + this.globalSubrs.DataLength) + 0x1a;
                foreach (PdfCompactFontFormatDictIndexOperator operator7 in this.operators)
                {
                    num3 += operator7.GetSize(this.stringIndex);
                }
                foreach (ICompactFontFormatDictIndexOffsetOperator operator8 in this.offsetOperators)
                {
                    int size = operator8.GetSize(this.stringIndex);
                    operator8.Offset = num3;
                    if (operator8.GetSize(this.stringIndex) != size)
                    {
                        flag = true;
                        break;
                    }
                    num3 += operator8.Length;
                }
            }
        }

        private void Write(PdfBinaryStream stream)
        {
            stream.WriteByte(this.majorVersion);
            stream.WriteByte(this.minorVersion);
            stream.WriteByte(4);
            stream.WriteByte(1);
            string[] strings = new string[] { this.name };
            new PdfCompactFontFormatNameIndex(strings).Write(stream);
            using (PdfBinaryStream stream2 = new PdfBinaryStream())
            {
                foreach (PdfCompactFontFormatDictIndexOperator @operator in this.operators)
                {
                    @operator.Write(this.stringIndex, stream2);
                }
                new PdfCompactFontFormatTopDictIndex(stream2.Data).Write(stream);
            }
            this.stringIndex.Write(stream);
            this.globalSubrs.Write(stream);
            foreach (ICompactFontFormatDictIndexOffsetOperator operator2 in this.offsetOperators)
            {
                operator2.WriteData(stream);
            }
        }

        public static byte[] Write(PdfType1FontCompactFontProgram fontProgram)
        {
            PdfCompactFontFormatTopDictIndexWriter writer = new PdfCompactFontFormatTopDictIndexWriter(fontProgram);
            using (PdfBinaryStream stream = new PdfBinaryStream())
            {
                writer.Write(stream);
                return stream.Data;
            }
        }
    }
}

