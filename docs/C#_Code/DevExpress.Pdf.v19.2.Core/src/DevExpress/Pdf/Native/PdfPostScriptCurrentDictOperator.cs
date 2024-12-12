namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptCurrentDictOperator : PdfPostScriptOperator
    {
        public const string Token = "currentdict";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            Stack<PdfPostScriptDictionary> dictionaryStack = interpreter.DictionaryStack;
            if (dictionaryStack.Count <= 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            interpreter.Stack.Push(dictionaryStack.Peek());
        }
    }
}

