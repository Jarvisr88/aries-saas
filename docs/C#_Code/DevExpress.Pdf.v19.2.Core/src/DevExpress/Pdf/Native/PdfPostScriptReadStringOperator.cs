namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptReadStringOperator : PdfPostScriptOperator
    {
        public const string Token = "readstring";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            bool flag;
            PdfStack stack = interpreter.Stack;
            byte[] str = stack.Pop(true) as byte[];
            PdfPostScriptFileParser parser = stack.Pop(true) as PdfPostScriptFileParser;
            if ((str == null) || (parser == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int newSize = parser.ReadString(str);
            if (newSize == str.Length)
            {
                flag = true;
            }
            else
            {
                flag = false;
                Array.Resize<byte>(ref str, newSize);
            }
            stack.Push(str);
            stack.Push(flag);
        }
    }
}

