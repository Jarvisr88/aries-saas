namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptDivOperator : PdfPostScriptOperator
    {
        public const string Token = "div";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            stack.Push(stack.PopDouble() / stack.PopDouble());
        }
    }
}

