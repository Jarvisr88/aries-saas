namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringHVCurveToOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 0x1f;

        public PdfType1CharstringHVCurveToOperator() : base(0x1f)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double num4 = interpreter.CharstringStack.PopDouble();
            interpreter.RelativeCurveTo(num4, 0.0, interpreter.CharstringStack.PopDouble(), interpreter.CharstringStack.PopDouble(), 0.0, interpreter.CharstringStack.PopDouble());
            interpreter.CharstringStack.Clear();
        }
    }
}

