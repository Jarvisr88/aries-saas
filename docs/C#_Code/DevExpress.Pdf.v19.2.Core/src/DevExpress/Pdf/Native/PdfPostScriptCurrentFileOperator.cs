namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptCurrentFileOperator : PdfPostScriptOperator
    {
        public const string Token = "currentfile";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfPostScriptFileParser parser = interpreter.Parser;
            if (parser != null)
            {
                interpreter.Stack.Push(parser);
            }
        }
    }
}

