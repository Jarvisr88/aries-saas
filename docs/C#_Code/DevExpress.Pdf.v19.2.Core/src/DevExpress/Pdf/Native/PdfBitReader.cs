namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class PdfBitReader
    {
        private const int highBitMask = 0x80;
        private readonly Stream stream;
        private byte currentByte;
        private int currentBitMask;

        public PdfBitReader(Stream stream)
        {
            this.stream = stream;
        }

        public int GetBit()
        {
            if ((this.currentBitMask == 0) && !this.GoToNextByte())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int num = ((this.currentByte & this.currentBitMask) == 0) ? 0 : 1;
            this.currentBitMask = this.currentBitMask >> 1;
            return num;
        }

        public int GetInteger(int bitCount)
        {
            int num = 0;
            for (int i = 0; i < bitCount; i++)
            {
                num = (num << 1) | this.GetBit();
            }
            return num;
        }

        internal uint GetUnsignedInteger(int bitCount) => 
            (uint) this.GetInteger(bitCount);

        protected virtual bool GoToNextByte()
        {
            int num = this.stream.ReadByte();
            if (num < 0)
            {
                return false;
            }
            this.currentByte = (byte) num;
            this.currentBitMask = 0x80;
            return true;
        }

        public bool IgnoreExtendedBits() => 
            (this.currentBitMask == 0x80) ? (this.stream.Position < this.stream.Length) : this.GoToNextByte();

        public byte[] ReadBytes(int value)
        {
            byte[] buffer = new byte[value];
            for (int i = 0; i < value; i++)
            {
                buffer[i] = (byte) this.stream.ReadByte();
            }
            this.GoToNextByte();
            return buffer;
        }

        protected byte CurrentByte =>
            this.currentByte;

        protected int CurrentBitMask
        {
            get => 
                this.currentBitMask;
            set => 
                this.currentBitMask = value;
        }
    }
}

