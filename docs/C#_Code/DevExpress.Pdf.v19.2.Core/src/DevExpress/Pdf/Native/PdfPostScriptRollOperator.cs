namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfPostScriptRollOperator : PdfPostScriptOperator
    {
        public const string Token = "roll";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            object obj2 = stack.Pop(true);
            object obj3 = stack.Pop(true);
            if (!(obj2 is int) || !(obj3 is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int capacity = (int) obj3;
            int num2 = (int) obj2;
            List<object> list = new List<object>(capacity);
            for (int i = 0; i < capacity; i++)
            {
                list.Add(stack.Pop(true));
            }
            int num3 = capacity - 1;
            num2 = (num2 - ((num2 / capacity) * capacity)) + num3;
            if (num2 >= capacity)
            {
                num2 -= capacity;
            }
            for (int j = 0; j < capacity; j++)
            {
                stack.Push(list[num2]);
                if (--num2 < 0)
                {
                    num2 = num3;
                }
            }
        }
    }
}

