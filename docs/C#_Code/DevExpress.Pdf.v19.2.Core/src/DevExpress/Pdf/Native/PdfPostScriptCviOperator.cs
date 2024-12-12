namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptCviOperator : PdfPostScriptOperator
    {
        public const string Token = "cvi";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            stack.Push(PdfDocumentReader.ConvertToInteger(stack.Pop(true)));
        }
    }
}

