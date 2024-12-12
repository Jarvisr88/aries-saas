namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfFlateLZWDecodeFilterPredictorDataSource : IPdfFlateDataSource, IDisposable
    {
        private readonly IPdfFlateDataSource source;
        private readonly int bytesPerPixel;
        private readonly int rowLength;
        private readonly byte[] previousPixel;
        private readonly int rowInitialOffset;
        private readonly int bitsPerComponent;
        private byte[] currentRow;
        private int rowOffset;

        protected PdfFlateLZWDecodeFilterPredictorDataSource(PdfFlateLZWDecodeFilter filter, IPdfFlateDataSource source, int rowInitialOffset)
        {
            this.source = source;
            this.rowInitialOffset = rowInitialOffset;
            this.bitsPerComponent = filter.BitsPerComponent;
            int colors = filter.Colors;
            this.bytesPerPixel = (this.bitsPerComponent * colors) / 8;
            this.bytesPerPixel ??= 1;
            if (this.bitsPerComponent == 0x10)
            {
                this.rowLength = (filter.Columns * colors) * 2;
            }
            else
            {
                int num2 = 8 / this.bitsPerComponent;
                int num3 = filter.Columns * colors;
                this.rowLength = num3 / num2;
                if ((num3 % num2) != 0)
                {
                    this.rowLength++;
                }
            }
            this.previousPixel = new byte[this.bytesPerPixel];
            this.currentRow = new byte[this.rowLength + rowInitialOffset];
        }

        public void Dispose()
        {
            this.source.Dispose();
        }

        public void FillBuffer(byte[] buffer)
        {
            int length = buffer.Length;
            if (this.rowOffset == 0)
            {
                this.StartNextRow();
            }
            else
            {
                int count = (this.rowLength - this.rowOffset) + this.rowInitialOffset;
                if (count >= length)
                {
                    Buffer.BlockCopy(this.currentRow, this.rowOffset, buffer, 0, length);
                    this.rowOffset += length;
                    return;
                }
                length -= count;
                Buffer.BlockCopy(this.currentRow, this.rowOffset, buffer, 0, count);
                this.rowOffset = 0;
                this.StartNextRow();
            }
            while (length != 0)
            {
                this.ProcessRow();
                if (length <= this.rowLength)
                {
                    Buffer.BlockCopy(this.currentRow, this.rowOffset, buffer, buffer.Length - length, length);
                    this.rowOffset += length;
                    if (length == this.rowLength)
                    {
                        this.rowOffset = 0;
                    }
                    return;
                }
                Buffer.BlockCopy(this.currentRow, this.rowOffset, buffer, buffer.Length - length, this.rowLength);
                this.rowOffset = 0;
                length -= this.rowLength;
                this.StartNextRow();
            }
        }

        protected abstract void ProcessRow();
        protected virtual void StartNextRow()
        {
            Array.Clear(this.previousPixel, 0, this.bytesPerPixel);
            this.source.FillBuffer(this.currentRow);
            this.rowOffset = this.rowInitialOffset;
        }

        protected int BytesPerPixel =>
            this.bytesPerPixel;

        protected int RowLength =>
            this.rowLength + this.rowInitialOffset;

        protected byte[] PreviousPixel =>
            this.previousPixel;

        protected int BitsPerComponent =>
            this.bitsPerComponent;

        protected byte[] CurrentRow
        {
            get => 
                this.currentRow;
            set => 
                this.currentRow = value;
        }
    }
}

