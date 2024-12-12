namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptDefOperator : PdfPostScriptOperator
    {
        public const string Token = "def";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            Stack<PdfPostScriptDictionary> dictionaryStack = interpreter.DictionaryStack;
            if (dictionaryStack.Count < 1)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfStack stack = interpreter.Stack;
            object obj2 = stack.Pop(true);
            PdfName name = stack.Pop(true) as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            dictionaryStack.Peek().Add(name.Name, obj2);
        }
    }
}

