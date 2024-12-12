namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringPopOperator : PdfType1CharstringTwoByteOperator
    {
        public const byte Code = 0x11;

        public PdfType1CharstringPopOperator() : base(0x11)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            if (interpreter.Stack.Count != 0)
            {
                interpreter.CharstringStack.Push(interpreter.Stack.Pop(true));
            }
            else
            {
                interpreter.CharstringStack.Push(3);
            }
        }
    }
}

