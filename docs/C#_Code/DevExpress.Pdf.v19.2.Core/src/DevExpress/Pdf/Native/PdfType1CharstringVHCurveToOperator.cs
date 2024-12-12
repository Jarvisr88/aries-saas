namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringVHCurveToOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 30;

        public PdfType1CharstringVHCurveToOperator() : base(30)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double num4 = interpreter.CharstringStack.PopDouble();
            interpreter.RelativeCurveTo(0.0, num4, interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble(), 0.0);
            interpreter.CharstringStack.Clear();
        }
    }
}

