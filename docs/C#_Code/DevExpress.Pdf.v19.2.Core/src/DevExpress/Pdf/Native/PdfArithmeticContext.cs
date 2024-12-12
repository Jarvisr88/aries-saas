namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfArithmeticContext
    {
        private readonly PdfArithmeticState state;
        private readonly byte[] context;

        public PdfArithmeticContext(PdfArithmeticState state, int length)
        {
            this.state = state;
            this.context = new byte[1 << (length & 0x1f)];
        }

        public PdfArithmeticContext(PdfArithmeticState state, byte[] initialContext)
        {
            this.state = state;
            this.context = initialContext;
        }

        public int DecodeBit(int cx) => 
            this.state.Decode(this.context, cx);
    }
}

