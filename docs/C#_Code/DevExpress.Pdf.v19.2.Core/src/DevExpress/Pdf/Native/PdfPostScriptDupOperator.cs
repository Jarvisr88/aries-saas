namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptDupOperator : PdfPostScriptOperator
    {
        public const string Token = "dup";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            stack.Push(stack.Peek());
        }
    }
}

