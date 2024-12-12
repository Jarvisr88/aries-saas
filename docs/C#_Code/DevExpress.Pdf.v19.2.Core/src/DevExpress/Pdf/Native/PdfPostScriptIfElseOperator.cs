namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptIfElseOperator : PdfPostScriptOperator
    {
        public const string Token = "ifelse";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            IList<object> list = stack.Pop(true) as IList<object>;
            IList<object> list2 = stack.Pop(true) as IList<object>;
            object obj2 = stack.Pop(true);
            if (!(obj2 as bool) || ((list2 == null) || (list == null)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            interpreter.Execute(((bool) obj2) ? ((IEnumerable<object>) list2) : ((IEnumerable<object>) list));
        }
    }
}

