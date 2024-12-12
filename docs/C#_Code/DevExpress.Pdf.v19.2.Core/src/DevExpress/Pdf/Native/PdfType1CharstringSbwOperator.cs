namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringSbwOperator : PdfType1CharstringTwoByteOperator
    {
        public const byte Code = 7;

        public PdfType1CharstringSbwOperator() : base(7)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double sbx = interpreter.CharstringStack.PopDouble();
            interpreter.SetSidebearing(sbx, interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble());
            interpreter.CharstringStack.Clear();
        }
    }
}

