namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringRMoveToOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 0x15;

        public PdfType1CharstringRMoveToOperator() : base(0x15)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double dx = interpreter.CharstringStack.PopDouble();
            interpreter.RelativeMoveTo(dx, interpreter.CharstringStack.PopDouble());
            interpreter.CharstringStack.Clear();
        }
    }
}

