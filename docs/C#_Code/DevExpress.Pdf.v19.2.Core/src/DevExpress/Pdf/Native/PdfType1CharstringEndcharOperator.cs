namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringEndcharOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 14;

        public PdfType1CharstringEndcharOperator() : base(14)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            interpreter.EndCharacter();
            interpreter.CharstringStack.Clear();
        }
    }
}

