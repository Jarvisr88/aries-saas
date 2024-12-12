namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringVMoveToOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 4;

        public PdfType1CharstringVMoveToOperator() : base(4)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double dy = interpreter.CharstringStack.PopDouble();
            interpreter.RelativeMoveTo(0.0, dy);
            interpreter.CharstringStack.Clear();
        }
    }
}

