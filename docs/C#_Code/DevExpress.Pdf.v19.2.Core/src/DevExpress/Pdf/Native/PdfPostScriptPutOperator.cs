namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptPutOperator : PdfPostScriptOperator
    {
        public const string Token = "put";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            object obj2 = stack.Pop(true);
            object obj3 = stack.Pop(true);
            object obj4 = stack.Pop(true);
            IList<object> list = obj4 as IList<object>;
            if (list == null)
            {
                PdfPostScriptDictionary dictionary = obj4 as PdfPostScriptDictionary;
                PdfName name = obj3 as PdfName;
                if ((dictionary == null) || (name == null))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                dictionary.Add(name.Name, obj2);
            }
            else
            {
                if (!(obj3 is int))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                int num = (int) obj3;
                if ((num >= 0) && (num < list.Count))
                {
                    list[num] = obj2;
                }
            }
        }
    }
}

