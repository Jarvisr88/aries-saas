namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCompactFontFormatDictIndexROSOperator : PdfCompactFontFormatDictIndexTwoByteOperator
    {
        public const byte Code = 30;
        private readonly string registry;
        private readonly string ordering;
        private readonly double supplement;

        public PdfCompactFontFormatDictIndexROSOperator(PdfCompactFontFormatStringIndex stringIndex, IList<object> operands)
        {
            if (operands.Count != 3)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.registry = stringIndex[PdfDocumentReader.ConvertToInteger(operands[0])];
            this.ordering = stringIndex[PdfDocumentReader.ConvertToInteger(operands[1])];
            this.supplement = PdfDocumentReader.ConvertToDouble(operands[2]);
        }

        public PdfCompactFontFormatDictIndexROSOperator(string registry, string ordering, double supplement)
        {
            this.registry = registry;
            this.ordering = ordering;
            this.supplement = supplement;
        }

        public override void Execute(PdfType1FontCompactFontProgram fontProgram, PdfBinaryStream stream)
        {
            PdfType1FontCompactCIDFontProgram program = ToCIDFontProgram(fontProgram);
            program.Registry = this.registry;
            program.Ordering = this.ordering;
            program.Supplement = this.supplement;
        }

        public override int GetSize(PdfCompactFontFormatStringIndex stringIndex) => 
            ((base.GetSize(stringIndex) + CalcIntegerSize(stringIndex.GetSID(this.registry))) + CalcIntegerSize(stringIndex.GetSID(this.ordering))) + CalcDoubleSize(this.supplement);

        public override void Write(PdfCompactFontFormatStringIndex stringIndex, PdfBinaryStream stream)
        {
            WriteInteger(stream, stringIndex.GetSID(this.registry));
            WriteInteger(stream, stringIndex.GetSID(this.ordering));
            WriteDouble(stream, this.supplement);
            stream.WriteByte(12);
            stream.WriteByte(30);
        }
    }
}

