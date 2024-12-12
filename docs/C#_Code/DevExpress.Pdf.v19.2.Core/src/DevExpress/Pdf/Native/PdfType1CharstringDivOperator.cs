namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringDivOperator : PdfType1CharstringTwoByteOperator
    {
        public const byte Code = 12;

        public PdfType1CharstringDivOperator() : base(12)
        {
        }

        public override void Execute(PdfType1CharstringInterpreter interpreter)
        {
            PdfStack charstringStack = interpreter.CharstringStack;
            object obj2 = charstringStack.Pop(true);
            charstringStack.Push(PdfDocumentReader.ConvertToDouble(charstringStack.Pop(true)) / PdfDocumentReader.ConvertToDouble(obj2));
        }
    }
}

