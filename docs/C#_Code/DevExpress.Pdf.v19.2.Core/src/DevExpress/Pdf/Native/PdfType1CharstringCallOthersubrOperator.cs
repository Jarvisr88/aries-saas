namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringCallOthersubrOperator : PdfType1CharstringTwoByteOperator
    {
        public const byte Code = 0x10;

        public PdfType1CharstringCallOthersubrOperator() : base(0x10)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            int index = interpreter.CharstringStack.PopInt();
            interpreter.CallOtherSubr(index, interpreter.CharstringStack.PopInt());
        }
    }
}

