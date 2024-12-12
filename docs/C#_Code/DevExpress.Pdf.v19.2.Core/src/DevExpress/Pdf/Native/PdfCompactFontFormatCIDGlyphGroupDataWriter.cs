namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatCIDGlyphGroupDataWriter
    {
        private readonly PdfCompactFontFormatStringIndex stringIndex;
        private readonly List<PdfCompactFontFormatDictIndexOperator> operators = new List<PdfCompactFontFormatDictIndexOperator>();
        private PdfCompactFontFormatDictIndexPrivateOperator privateOperator;

        public PdfCompactFontFormatCIDGlyphGroupDataWriter(PdfType1FontCIDGlyphGroupData data, PdfCompactFontFormatStringIndex stringIndex)
        {
            this.stringIndex = stringIndex;
            double underlinePosition = data.UnderlinePosition;
            if (underlinePosition != -100.0)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexUnderlinePositionOperator(underlinePosition));
            }
            underlinePosition = data.UnderlineThickness;
            if (underlinePosition != 50.0)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexUnderlineThicknessOperator(underlinePosition));
            }
            PdfRectangle fontBBox = data.FontBBox;
            if (!fontBBox.Equals(PdfType1FontCompactFontProgram.DefaultFontBBox))
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexFontBBoxOperator(fontBBox));
            }
            PdfType1FontType fontType = data.FontType;
            if (fontType != PdfType1FontType.Type2)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexCharstringTypeOperator(fontType));
            }
            PdfTransformationMatrix fontMatrix = data.FontMatrix;
            if (!fontMatrix.Equals(PdfType1FontCompactFontProgram.DefaultFontMatrix))
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexFontMatrixOperator(fontMatrix));
            }
            int uniqueID = data.UniqueID;
            if (uniqueID != 0)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexUniqueIDOperator(uniqueID));
            }
            PdfType1FontPrivateData @private = data.Private;
            if (@private != null)
            {
                this.privateOperator = new PdfCompactFontFormatDictIndexPrivateOperator(@private);
                this.operators.Add(this.privateOperator);
            }
            uniqueID = data.CIDCount;
            if (uniqueID != 0x2210)
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexCIDCountOperator(uniqueID));
            }
            string fontName = data.FontName;
            if (!string.IsNullOrEmpty(fontName))
            {
                this.operators.Add(new PdfCompactFontFormatDictIndexFontNameOperator(fontName));
            }
        }

        public void Write(PdfBinaryStream stream)
        {
            foreach (PdfCompactFontFormatDictIndexOperator @operator in this.operators)
            {
                @operator.Write(this.stringIndex, stream);
            }
        }

        public PdfCompactFontFormatDictIndexPrivateOperator PrivateOperator =>
            this.privateOperator;

        public int DataSize
        {
            get
            {
                int num = 0;
                foreach (PdfCompactFontFormatDictIndexOperator @operator in this.operators)
                {
                    num += @operator.GetSize(this.stringIndex);
                }
                return num;
            }
        }
    }
}

