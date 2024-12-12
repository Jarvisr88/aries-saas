namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptSystemDictOperator : PdfPostScriptOperator
    {
        public const string Token = "systemdict";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            interpreter.Stack.Push(interpreter.SystemDictionary);
        }
    }
}

