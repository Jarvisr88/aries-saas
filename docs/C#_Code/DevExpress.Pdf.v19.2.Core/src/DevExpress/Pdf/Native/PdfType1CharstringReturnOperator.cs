namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringReturnOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 11;

        public PdfType1CharstringReturnOperator() : base(11)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            interpreter.Return();
        }
    }
}

