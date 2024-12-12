namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class TTFStreamAsByteArray : TTFStream
    {
        private byte[] data;
        private int position;

        public TTFStreamAsByteArray(byte[] data)
        {
            this.data = data;
        }

        protected override byte _read()
        {
            int position = this.position;
            this.position = position + 1;
            return this.data[position];
        }

        protected override void _seek(int newPosition)
        {
            this.position = newPosition;
        }

        protected override void _write(byte value)
        {
            int position = this.position;
            this.position = position + 1;
            this.data[position] = value;
        }

        public override int Position =>
            this.position;

        public override int Length =>
            this.data.Length;
    }
}

