namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringHstem3Operator : PdfType1CharstringTwoByteOperator
    {
        public const byte Code = 2;

        public PdfType1CharstringHstem3Operator() : base(2)
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

