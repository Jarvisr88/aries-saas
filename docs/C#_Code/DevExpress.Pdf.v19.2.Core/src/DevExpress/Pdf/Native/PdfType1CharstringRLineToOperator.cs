namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringRLineToOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 5;

        public PdfType1CharstringRLineToOperator() : base(5)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double dx = interpreter.CharstringStack.PopDouble();
            interpreter.RelativeLineTo(dx, interpreter.CharstringStack.PopDouble());
            interpreter.CharstringStack.Clear();
        }
    }
}

