namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringVstemOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 3;

        public PdfType1CharstringVstemOperator() : base(3)
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

