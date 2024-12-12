namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptUserDictOperator : PdfPostScriptOperator
    {
        public const string Token = "userdict";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            interpreter.Stack.Push(interpreter.UserDictionary);
        }
    }
}

