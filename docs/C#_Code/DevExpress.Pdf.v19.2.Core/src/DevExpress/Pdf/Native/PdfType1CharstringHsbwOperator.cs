namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringHsbwOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 13;

        public PdfType1CharstringHsbwOperator() : base(13)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            double sbx = interpreter.CharstringStack.PopDouble();
            interpreter.SetSidebearing(sbx, interpreter.CharstringStack.PopDouble());
            interpreter.CharstringStack.Clear();
        }
    }
}

