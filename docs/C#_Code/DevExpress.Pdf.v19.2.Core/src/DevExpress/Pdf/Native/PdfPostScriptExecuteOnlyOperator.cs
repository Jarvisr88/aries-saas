namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptExecuteOnlyOperator : PdfPostScriptOperator
    {
        public const string Token = "executeonly";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            if (!(interpreter.Stack.Peek() is IList<object>))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }
    }
}

