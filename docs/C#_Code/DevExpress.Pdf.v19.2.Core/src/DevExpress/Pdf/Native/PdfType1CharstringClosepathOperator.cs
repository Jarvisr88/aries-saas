namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringClosepathOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 9;

        public PdfType1CharstringClosepathOperator() : base(9)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            interpreter.ClosePath();
            interpreter.CharstringStack.Clear();
        }
    }
}

