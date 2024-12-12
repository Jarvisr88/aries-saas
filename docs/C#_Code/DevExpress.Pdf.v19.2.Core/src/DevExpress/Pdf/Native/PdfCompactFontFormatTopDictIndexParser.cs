namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatTopDictIndexParser : PdfCompactFontFormatDictIndexParser
    {
        private readonly PdfCompactFontFormatStringIndex stringIndex;

        private PdfCompactFontFormatTopDictIndexParser(PdfCompactFontFormatStringIndex stringIndex, byte[] data) : base(data)
        {
            this.stringIndex = stringIndex;
        }

        public static PdfType1FontCIDGlyphGroupData Parse(PdfBinaryStream stream, PdfCompactFontFormatStringIndex stringIndex, byte[] objectData)
        {
            PdfType1FontCIDGlyphGroupData glyphGroupData = new PdfType1FontCIDGlyphGroupData();
            foreach (PdfCompactFontFormatDictIndexOperator @operator in new PdfCompactFontFormatTopDictIndexParser(stringIndex, objectData).Parse())
            {
                @operator.Execute(glyphGroupData, stream);
            }
            return glyphGroupData;
        }

        public static PdfType1FontCompactFontProgram Parse(byte majorVersion, byte minorVersion, string fontName, PdfCompactFontFormatStringIndex stringIndex, IList<byte[]> globalSubrs, PdfBinaryStream stream, byte[] objectData)
        {
            IList<PdfCompactFontFormatDictIndexOperator> list = new PdfCompactFontFormatTopDictIndexParser(stringIndex, objectData).Parse();
            PdfCompactFontFormatDictIndexCharsetOperator @operator = null;
            PdfCompactFontFormatDictIndexFDSelectOperator operator2 = null;
            PdfType1FontCompactFontProgram fontProgram = ((list.Count <= 0) || !(list[0] is PdfCompactFontFormatDictIndexROSOperator)) ? new PdfType1FontCompactFontProgram(majorVersion, minorVersion, fontName, stringIndex, globalSubrs) : new PdfType1FontCompactCIDFontProgram(majorVersion, minorVersion, fontName, stringIndex, globalSubrs);
            foreach (PdfCompactFontFormatDictIndexOperator operator3 in list)
            {
                PdfCompactFontFormatDictIndexCharsetOperator operator4 = operator3 as PdfCompactFontFormatDictIndexCharsetOperator;
                if (operator4 != null)
                {
                    @operator = operator4;
                    continue;
                }
                PdfCompactFontFormatDictIndexFDSelectOperator operator5 = operator3 as PdfCompactFontFormatDictIndexFDSelectOperator;
                if (operator5 == null)
                {
                    operator3.Execute(fontProgram, stream);
                    continue;
                }
                operator2 = operator5;
            }
            if (@operator != null)
            {
                @operator.Execute(fontProgram, stream);
            }
            if (operator2 != null)
            {
                operator2.Execute(fontProgram, stream);
            }
            return fontProgram;
        }

        protected override PdfCompactFontFormatDictIndexOperator ParseOperator(byte value, IList<object> operands)
        {
            switch (value)
            {
                case 0:
                    return new PdfCompactFontFormatDictIndexVersionOperator(this.stringIndex, operands);

                case 1:
                    return new PdfCompactFontFormatDictIndexNoticeOperator(this.stringIndex, operands);

                case 2:
                    return new PdfCompactFontFormatDictIndexFullNameOperator(this.stringIndex, operands);

                case 3:
                    return new PdfCompactFontFormatDictIndexFamilyNameOperator(this.stringIndex, operands);

                case 4:
                    return new PdfCompactFontFormatDictIndexWeightOperator(this.stringIndex, operands);

                case 5:
                    return new PdfCompactFontFormatDictIndexFontBBoxOperator(operands);

                case 12:
                {
                    if (!base.HasMoreData)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    byte nextByte = base.GetNextByte();
                    switch (nextByte)
                    {
                        case 0:
                            return new PdfCompactFontFormatDictIndexCopyrightOperator(this.stringIndex, operands);

                        case 1:
                            return new PdfCompactFontFormatDictIndexIsFixedPitchOperator(operands);

                        case 2:
                            return new PdfCompactFontFormatDictIndexItalicAngleOperator(operands);

                        case 3:
                            return new PdfCompactFontFormatDictIndexUnderlinePositionOperator(operands);

                        case 4:
                            return new PdfCompactFontFormatDictIndexUnderlineThicknessOperator(operands);

                        case 5:
                            return new PdfCompactFontFormatDictIndexPaintTypeOperator(operands);

                        case 6:
                            return new PdfCompactFontFormatDictIndexCharstringTypeOperator(operands);

                        case 7:
                            return new PdfCompactFontFormatDictIndexFontMatrixOperator(operands);

                        case 8:
                            return new PdfCompactFontFormatDictIndexStrokeWidthOperator(operands);
                    }
                    switch (nextByte)
                    {
                        case 0x15:
                            return new PdfCompactFontFormatDictIndexPostScriptOperator(this.stringIndex, operands);

                        case 0x16:
                            return new PdfCompactFontFormatDictIndexBaseFontNameOperator(this.stringIndex, operands);

                        case 0x17:
                            return new PdfCompactFontFormatDictIndexBaseFontBlendOperator(operands);

                        case 30:
                            return new PdfCompactFontFormatDictIndexROSOperator(this.stringIndex, operands);

                        case 0x1f:
                            return new PdfCompactFontFormatDictIndexCIDFontVersionOperator(operands);

                        case 0x22:
                            return new PdfCompactFontFormatDictIndexCIDCountOperator(operands);

                        case 0x23:
                            return new PdfCompactFontFormatDictIndexUIDBaseOperator(operands);

                        case 0x24:
                            return new PdfCompactFontFormatDictIndexFDArrayOperator(operands);

                        case 0x25:
                            return new PdfCompactFontFormatDictIndexFDSelectOperator(operands);

                        case 0x26:
                            return new PdfCompactFontFormatDictIndexFontNameOperator(this.stringIndex, operands);
                    }
                    return null;
                }
                case 13:
                    return new PdfCompactFontFormatDictIndexUniqueIDOperator(operands);

                case 14:
                    return new PdfCompactFontFormatDictIndexXUIDOperator(operands);

                case 15:
                    return new PdfCompactFontFormatDictIndexCharsetOperator(operands);

                case 0x10:
                    return new PdfCompactFontFormatDictIndexEncodingOperator(operands);

                case 0x11:
                    return new PdfCompactFontFormatDictIndexCharStringsOperator(operands);

                case 0x12:
                    return new PdfCompactFontFormatDictIndexPrivateOperator(operands);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }
    }
}

