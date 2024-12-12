namespace DevExpress.Pdf.Native
{
    using System;
    using System.IO;

    public class JPXBitReader : PdfBitReader
    {
        public JPXBitReader(Stream stream) : base(stream)
        {
        }

        public void AlignToByte()
        {
            if (base.CurrentByte == 0xff)
            {
                this.GoToNextByte();
            }
        }

        protected override bool GoToNextByte()
        {
            if (base.CurrentByte != 0xff)
            {
                return base.GoToNextByte();
            }
            if (!base.GoToNextByte())
            {
                return false;
            }
            base.CurrentBitMask = 0x40;
            return true;
        }
    }
}

