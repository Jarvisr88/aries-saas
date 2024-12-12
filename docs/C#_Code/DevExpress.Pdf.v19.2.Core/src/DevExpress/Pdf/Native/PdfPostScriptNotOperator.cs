namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptNotOperator : PdfPostScriptOperator
    {
        public const string Token = "not";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            object obj2 = interpreter.Stack.Pop(true);
            if (obj2 as bool)
            {
                interpreter.Stack.Push(!((bool) obj2));
            }
            else if (obj2 is int)
            {
                interpreter.Stack.Push(~((int) obj2));
            }
            else
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }
    }
}

