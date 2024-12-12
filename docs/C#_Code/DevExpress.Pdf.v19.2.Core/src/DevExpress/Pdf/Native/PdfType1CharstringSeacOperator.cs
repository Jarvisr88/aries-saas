namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringSeacOperator : PdfType1CharstringTwoByteOperator
    {
        public const byte Code = 6;

        public PdfType1CharstringSeacOperator() : base(6)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double asb = interpreter.CharstringStack.PopDouble();
            interpreter.Seac(asb, interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopInt(), interpreter.CharstringStack.PopInt());
            interpreter.CharstringStack.Clear();
        }
    }
}

