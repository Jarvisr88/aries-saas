namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptEndOperator : PdfPostScriptOperator
    {
        public const string Token = "end";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            Stack<PdfPostScriptDictionary> dictionaryStack = interpreter.DictionaryStack;
            if (dictionaryStack.Count <= 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            dictionaryStack.Pop();
        }
    }
}

