namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptIfOperator : PdfPostScriptOperator
    {
        public const string Token = "if";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            IList<object> list = stack.Pop(true) as IList<object>;
            object obj2 = stack.Pop(true);
            if (!(obj2 as bool) || (list == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if ((bool) obj2)
            {
                interpreter.Execute((IEnumerable<object>) list);
            }
        }
    }
}

