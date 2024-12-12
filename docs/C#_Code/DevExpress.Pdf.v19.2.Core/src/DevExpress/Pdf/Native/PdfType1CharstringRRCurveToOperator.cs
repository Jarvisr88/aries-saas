namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringRRCurveToOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 8;

        public PdfType1CharstringRRCurveToOperator() : base(8)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double num6 = interpreter.CharstringStack.PopDouble();
            interpreter.RelativeCurveTo(num6, interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble());
            interpreter.CharstringStack.Clear();
        }
    }
}

