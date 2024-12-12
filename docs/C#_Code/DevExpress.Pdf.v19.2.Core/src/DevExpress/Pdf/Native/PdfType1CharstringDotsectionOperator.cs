namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringDotsectionOperator : PdfType1CharstringTwoByteOperator
    {
        public const byte Code = 0;

        public PdfType1CharstringDotsectionOperator() : base(0)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            interpreter.CharstringStack.Clear();
        }
    }
}

