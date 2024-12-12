namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptFontDirectoryOperator : PdfPostScriptOperator
    {
        public const string Token = "FontDirectory";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            interpreter.Stack.Push(interpreter.FontDirectory);
        }
    }
}

