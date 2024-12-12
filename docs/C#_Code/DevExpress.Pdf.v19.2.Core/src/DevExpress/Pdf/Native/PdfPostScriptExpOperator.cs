namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptExpOperator : PdfPostScriptOperator
    {
        public const string Token = "exp";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            double y = stack.PopDouble();
            stack.Push(Math.Pow(stack.PopDouble(), y));
        }
    }
}

