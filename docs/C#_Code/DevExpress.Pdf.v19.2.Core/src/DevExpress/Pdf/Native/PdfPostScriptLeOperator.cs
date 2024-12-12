namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptLeOperator : PdfPostScriptOperator
    {
        public const string Token = "le";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            stack.Push(PdfDocumentReader.ConvertToDouble(stack.Pop(true)) <= PdfDocumentReader.ConvertToDouble(stack.Pop(true)));
        }
    }
}

