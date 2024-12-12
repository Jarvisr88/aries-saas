namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringHstemOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 1;

        public PdfType1CharstringHstemOperator() : base(1)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            interpreter.CharstringStack.Pop(true);
            interpreter.CharstringStack.Pop(true);
            interpreter.CharstringStack.Clear();
        }
    }
}

