namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfType1CharstringCallsubrOperator : PdfType1CharstringSingleByteOperator
    {
        public const byte Code = 10;

        public PdfType1CharstringCallsubrOperator() : base(10)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            int num = interpreter.CharstringStack.PopInt();
            IList<PdfType1CharstringSubroutine> fontSubroutines = interpreter.FontSubroutines;
            if ((num >= 0) && (num < fontSubroutines.Count))
            {
                interpreter.CallSubr(fontSubroutines[num].GetTokens());
            }
        }
    }
}

