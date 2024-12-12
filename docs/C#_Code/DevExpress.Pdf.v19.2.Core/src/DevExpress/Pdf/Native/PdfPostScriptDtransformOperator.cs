namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptDtransformOperator : PdfPostScriptOperator
    {
        public const string Token = "dtransform";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            stack.Push(stack.PopDouble());
            stack.Push(stack.PopDouble());
        }
    }
}

