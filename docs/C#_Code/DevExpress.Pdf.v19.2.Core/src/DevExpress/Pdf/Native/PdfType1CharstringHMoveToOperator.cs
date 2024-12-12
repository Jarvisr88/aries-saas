namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringHMoveToOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 0x16;

        public PdfType1CharstringHMoveToOperator() : base(0x16)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double dx = interpreter.CharstringStack.PopDouble();
            interpreter.RelativeMoveTo(dx, 0.0);
            interpreter.CharstringStack.Clear();
        }
    }
}

