namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Text;

    public class PdfPostScriptDefineFontOperator : PdfPostScriptOperator
    {
        public const string Token = "definefont";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            string str;
            PdfStack stack = interpreter.Stack;
            PdfPostScriptDictionary dictionary = stack.Pop(true) as PdfPostScriptDictionary;
            if (dictionary == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            object obj2 = stack.Pop(true);
            PdfName name = obj2 as PdfName;
            if (name != null)
            {
                str = name.Name;
            }
            else
            {
                byte[] bytes = obj2 as byte[];
                if (bytes == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                str = Encoding.UTF8.GetString(bytes);
            }
            PdfType1FontClassicFontProgram program = new PdfType1FontClassicFontProgram(dictionary);
            interpreter.FontDirectory.Add(str, program);
            stack.Push(program);
        }
    }
}

