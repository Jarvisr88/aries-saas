namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatPrivateDictIndexParser : PdfCompactFontFormatDictIndexParser
    {
        private PdfCompactFontFormatPrivateDictIndexParser(byte[] objectData) : base(objectData)
        {
        }

        public static PdfType1FontCompactFontPrivateData Parse(PdfBinaryStream stream, byte[] data)
        {
            PdfType1FontCompactFontPrivateData privateData = new PdfType1FontCompactFontPrivateData();
            foreach (PdfCompactFontFormatDictIndexOperator @operator in new PdfCompactFontFormatPrivateDictIndexParser(data).Parse())
            {
                if (@operator != null)
                {
                    @operator.Execute(privateData, stream);
                }
            }
            return privateData;
        }

        protected override PdfCompactFontFormatDictIndexOperator ParseOperator(byte value, IList<object> operands)
        {
            switch (value)
            {
                case 6:
                    return new PdfCompactFontFormatPrivateDictIndexBlueValuesOperator(operands);

                case 7:
                    return new PdfCompactFontFormatPrivateDictIndexOtherBluesOperator(operands);

                case 8:
                    return new PdfCompactFontFormatPrivateDictIndexFamilyBluesOperator(operands);

                case 9:
                    return new PdfCompactFontFormatPrivateDictIndexFamilyOtherBluesOperator(operands);

                case 10:
                    return new PdfCompactFontFormatPrivateDictIndexStdHWOperator(operands);

                case 11:
                    return new PdfCompactFontFormatPrivateDictIndexStdVWOperator(operands);

                case 12:
                    if (!base.HasMoreData)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    switch (base.GetNextByte())
                    {
                        case 9:
                            return new PdfCompactFontFormatPrivateDictIndexBlueScaleOperator(operands);

                        case 10:
                            return new PdfCompactFontFormatPrivateDictIndexBlueShiftOperator(operands);

                        case 11:
                            return new PdfCompactFontFormatPrivateDictIndexBlueFuzzOperator(operands);

                        case 12:
                            return new PdfCompactFontFormatPrivateDictIndexStemSnapHOperator(operands);

                        case 13:
                            return new PdfCompactFontFormatPrivateDictIndexStemSnapVOperator(operands);

                        case 14:
                            return new PdfCompactFontFormatPrivateDictIndexForceBoldOperator(operands);

                        case 15:
                            return new PdfCompactFontFormatPrivateDictIndexForceBoldThresholdOperator(operands);

                        case 0x11:
                            return new PdfCompactFontFormatPrivateDictIndexLanguageGroupOperator(operands);

                        case 0x12:
                            return new PdfCompactFontFormatPrivateDictIndexExpansionFactorOperator(operands);
                    }
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                    return null;

                case 0x13:
                    return new PdfCompactFontFormatPrivateDictIndexSubrsOperator(operands);

                case 20:
                    return new PdfCompactFontFormatPrivateDictIndexDefaultWidthXOperator(operands);

                case 0x15:
                    return new PdfCompactFontFormatPrivateDictIndexNominalWidthXOperator(operands);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }
    }
}

