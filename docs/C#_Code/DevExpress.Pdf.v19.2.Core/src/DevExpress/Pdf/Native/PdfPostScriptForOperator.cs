namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptForOperator : PdfPostScriptOperator
    {
        public const string Token = "for";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            IList<object> list = stack.Pop(true) as IList<object>;
            if (list == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            object obj2 = stack.Pop(true);
            object obj3 = stack.Pop(true);
            object obj4 = stack.Pop(true);
            if ((obj4 is double) || ((obj3 is double) || (obj2 is double)))
            {
                double num = PdfDocumentReader.ConvertToDouble(obj4);
                double num2 = PdfDocumentReader.ConvertToDouble(obj3);
                double num3 = PdfDocumentReader.ConvertToDouble(obj2);
                if (num2 == 0.0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                else if (num2 > 0.0)
                {
                    for (double i = num; i <= num3; i += num2)
                    {
                        stack.Push(i);
                        interpreter.Execute((IEnumerable<object>) list);
                    }
                }
                else
                {
                    for (double i = num; i >= num3; i += num2)
                    {
                        stack.Push(i);
                        interpreter.Execute((IEnumerable<object>) list);
                    }
                }
            }
            else
            {
                if (!(obj4 is int) || (!(obj3 is int) || !(obj2 is int)))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                int num6 = (int) obj4;
                int num7 = (int) obj3;
                int num8 = (int) obj2;
                if (num7 == 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                else if (num7 > 0)
                {
                    for (int i = num6; i <= num8; i += num7)
                    {
                        stack.Push(i);
                        interpreter.Execute((IEnumerable<object>) list);
                    }
                }
                else
                {
                    for (int i = num6; i >= num8; i += num7)
                    {
                        stack.Push(i);
                        interpreter.Execute((IEnumerable<object>) list);
                    }
                }
            }
        }
    }
}

