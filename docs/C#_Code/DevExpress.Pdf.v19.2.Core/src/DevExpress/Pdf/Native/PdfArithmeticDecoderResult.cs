namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class PdfArithmeticDecoderResult
    {
        public PdfArithmeticDecoderResult(int result, bool code)
        {
            this.Result = result;
            this.Code = code;
        }

        public int Result { get; private set; }

        public bool Code { get; private set; }
    }
}

