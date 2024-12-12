namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptBeginOperator : PdfPostScriptOperator
    {
        public const string Token = "begin";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfPostScriptDictionary item = interpreter.Stack.Pop(true) as PdfPostScriptDictionary;
            if (item == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            interpreter.DictionaryStack.Push(item);
        }
    }
}

