namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptMarkOperator : PdfPostScriptOperator
    {
        public const string Token = "mark";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfPostScriptMark mark = new PdfPostScriptMark();
            interpreter.Stack.Push(mark);
        }
    }
}

