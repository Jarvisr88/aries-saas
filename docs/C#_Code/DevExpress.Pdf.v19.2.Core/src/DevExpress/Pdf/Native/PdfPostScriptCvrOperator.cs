namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptCvrOperator : PdfPostScriptOperator
    {
        public const string Token = "cvr";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            stack.Push(PdfDocumentReader.ConvertToDouble(stack.Pop(true)));
        }
    }
}

