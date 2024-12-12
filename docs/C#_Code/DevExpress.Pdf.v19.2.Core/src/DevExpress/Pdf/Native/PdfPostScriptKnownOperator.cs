namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfPostScriptKnownOperator : PdfPostScriptOperator
    {
        public const string Token = "known";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            PdfName name = stack.Pop(true) as PdfName;
            PdfPostScriptDictionary dictionary = stack.Pop(true) as PdfPostScriptDictionary;
            if ((name == null) || (dictionary == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            stack.Push(dictionary.ContainsKey(name.Name));
        }
    }
}

