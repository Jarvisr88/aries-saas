namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptGtOperator : PdfPostScriptOperator
    {
        public const string Token = "gt";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            stack.Push(PdfDocumentReader.ConvertToDouble(stack.Pop(true)) > PdfDocumentReader.ConvertToDouble(stack.Pop(true)));
        }
    }
}

