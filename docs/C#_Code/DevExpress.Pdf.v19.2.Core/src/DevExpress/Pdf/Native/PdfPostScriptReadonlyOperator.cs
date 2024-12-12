namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptReadonlyOperator : PdfPostScriptOperator
    {
        public const string Token = "readonly";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            if (interpreter.Stack.Count <= 0)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }
    }
}

