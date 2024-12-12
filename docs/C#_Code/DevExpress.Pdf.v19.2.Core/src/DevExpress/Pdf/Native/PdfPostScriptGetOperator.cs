namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfPostScriptGetOperator : PdfPostScriptOperator
    {
        public const string Token = "get";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            PdfName name = stack.Pop(true) as PdfName;
            if (name == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            string key = name.Name;
            object obj2 = stack.Pop(true);
            PdfPostScriptDictionary dictionary = obj2 as PdfPostScriptDictionary;
            if (dictionary != null)
            {
                if (!dictionary.ContainsKey(key))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                stack.Push(dictionary[key]);
            }
            else
            {
                PdfType1FontClassicFontProgram program = obj2 as PdfType1FontClassicFontProgram;
                if ((program == null) || (key != "CharStrings"))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                stack.Push(program.CharStrings);
            }
        }
    }
}

