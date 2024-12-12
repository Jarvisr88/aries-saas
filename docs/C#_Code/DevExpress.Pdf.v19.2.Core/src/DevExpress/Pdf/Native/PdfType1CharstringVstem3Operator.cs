namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringVstem3Operator : PdfType1CharstringTwoByteOperator
    {
        public const byte Code = 1;

        public PdfType1CharstringVstem3Operator() : base(1)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            interpreter.CharstringStack.Pop(true);
            interpreter.CharstringStack.Pop(true);
            interpreter.CharstringStack.Pop(true);
            interpreter.CharstringStack.Pop(true);
            interpreter.CharstringStack.Pop(true);
            interpreter.CharstringStack.Pop(true);
            interpreter.CharstringStack.Clear();
        }
    }
}

