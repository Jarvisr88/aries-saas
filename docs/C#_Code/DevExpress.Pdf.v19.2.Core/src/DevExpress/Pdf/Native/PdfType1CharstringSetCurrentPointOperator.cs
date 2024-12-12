namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringSetCurrentPointOperator : PdfType1CharstringTwoByteOperator
    {
        public const byte Code = 0x21;

        public PdfType1CharstringSetCurrentPointOperator() : base(0x21)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double num = interpreter.CharstringStack.PopDouble();
            double num2 = interpreter.CharstringStack.PopDouble();
        }
    }
}

