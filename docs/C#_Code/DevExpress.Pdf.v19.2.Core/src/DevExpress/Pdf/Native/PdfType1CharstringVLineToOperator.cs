namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringVLineToOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 7;

        public PdfType1CharstringVLineToOperator() : base(7)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double dy = interpreter.CharstringStack.PopDouble();
            interpreter.RelativeLineTo(0.0, dy);
            interpreter.CharstringStack.Clear();
        }
    }
}

