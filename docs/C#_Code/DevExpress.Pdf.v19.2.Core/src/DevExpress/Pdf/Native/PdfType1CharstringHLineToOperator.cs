namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringHLineToOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 6;

        public PdfType1CharstringHLineToOperator() : base(6)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double dx = interpreter.CharstringStack.PopDouble();
            interpreter.RelativeLineTo(dx, 0.0);
            interpreter.CharstringStack.Clear();
        }
    }
}

