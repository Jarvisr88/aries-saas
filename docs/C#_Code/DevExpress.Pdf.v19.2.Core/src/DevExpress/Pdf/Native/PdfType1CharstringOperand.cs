namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfType1CharstringOperand : IPdfType1CharstringToken
    {
        private readonly int value;

        public PdfType1CharstringOperand(int value)
        {
            this.value = value;
        }

        public void Execute(PdfType1CharstringInterpreter interpreter)
        {
            interpreter.CharstringStack.Push(this.value);
        }

        public int Value =>
            this.value;
    }
}

