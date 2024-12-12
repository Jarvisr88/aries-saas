namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexFontMatrixOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 7;
        private readonly PdfTransformationMatrix fontMatrix;

        public PdfCompactFontFormatDictIndexFontMatrixOperator(PdfTransformationMatrix fontMatrix)
        {
            this.fontMatrix = fontMatrix;
        }

        public PdfCompactFontFormatDictIndexFontMatrixOperator(IList<object> operands) : this(new PdfTransformationMatrix(operands))
        {
        }

        public override void Execute(PdfType1FontCIDGlyphGroupData glyphGroupData, PdfBinaryStream stream)
        {
            glyphGroupData.FontMatrix = this.fontMatrix;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            fontProgram.FontMatrix = this.fontMatrix;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            (((((base.GetSize(stringIndex) + CalcDoubleSize(this.fontMatrix.A)) + CalcDoubleSize(this.fontMatrix.B)) + CalcDoubleSize(this.fontMatrix.C)) + CalcDoubleSize(this.fontMatrix.D)) + CalcDoubleSize(this.fontMatrix.E)) + CalcDoubleSize(this.fontMatrix.F);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteDouble(stream, this.fontMatrix.A);
            WriteDouble(stream, this.fontMatrix.B);
            WriteDouble(stream, this.fontMatrix.C);
            WriteDouble(stream, this.fontMatrix.D);
            WriteDouble(stream, this.fontMatrix.E);
            WriteDouble(stream, this.fontMatrix.F);
            stream.WriteByte(12);
            stream.WriteByte(7);
        }
    }
}

