namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptNoAccessOperator : PdfPostScriptOperator
    {
        public const string Token = "noaccess";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            object obj2 = interpreter.Stack.Peek();
            if (!(obj2 is IList<object>) && (!(obj2 is byte[]) && !(obj2 is PdfPostScriptDictionary)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }
    }
}

