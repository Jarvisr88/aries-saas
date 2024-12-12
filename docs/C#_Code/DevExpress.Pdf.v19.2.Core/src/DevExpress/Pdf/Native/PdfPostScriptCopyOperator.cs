namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfPostScriptCopyOperator : PdfPostScriptOperator
    {
        public const string Token = "copy";

        public override void Execute(PdfPostScriptInterpreter interpreter)
        {
            PdfStack stack = interpreter.Stack;
            int? nullable = stack.Pop(true) as int?;
            if (nullable != null)
            {
                object[] objArray = new object[nullable.Value];
                int index = 0;
                while (true)
                {
                    int? nullable2 = nullable;
                    if (!((index < nullable2.GetValueOrDefault()) ? (nullable2 != null) : false))
                    {
                        int num2 = 0;
                        while (num2 < 2)
                        {
                            int num3 = nullable.Value - 1;
                            while (true)
                            {
                                if (num3 < 0)
                                {
                                    num2++;
                                    break;
                                }
                                stack.Push(objArray[num3]);
                                num3--;
                            }
                        }
                        break;
                    }
                    objArray[index] = stack.Pop(true);
                    index++;
                }
            }
        }
    }
}

