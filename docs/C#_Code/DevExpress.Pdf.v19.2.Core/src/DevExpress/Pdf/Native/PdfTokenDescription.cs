namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfTokenDescription
    {
        private readonly byte[] token;
        private readonly int length;
        private int indexToCompare;
        private byte currentComparingSymbol;

        public PdfTokenDescription(byte[] token)
        {
            this.token = token;
            this.length = token.Length;
            this.BeginCompare();
        }

        private void BeginCompare()
        {
            this.indexToCompare = 0;
            this.currentComparingSymbol = this.token[0];
        }

        public static PdfTokenDescription BeginCompare(PdfTokenDescription description) => 
            new PdfTokenDescription(description.token);

        public bool Compare(byte symbol)
        {
            if (symbol != this.currentComparingSymbol)
            {
                if (this.indexToCompare != 0)
                {
                    this.BeginCompare();
                }
            }
            else
            {
                int num = this.indexToCompare + 1;
                this.indexToCompare = num;
                if (num == this.length)
                {
                    return true;
                }
                this.currentComparingSymbol = this.token[this.indexToCompare];
            }
            return false;
        }

        public bool IsStartWithComment =>
            this.token[0] == 0x25;

        public int Length =>
            this.length;
    }
}

