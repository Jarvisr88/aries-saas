namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfIdentityEncoding : PdfCompositeFontEncoding
    {
        internal const string HorizontalIdentityName = "Identity-H";
        internal const string VerticalIdentityName = "Identity-V";
        private static PdfIdentityEncoding horizontalIdentity = new PdfIdentityEncoding(false);
        private static PdfIdentityEncoding verticalIdentity = new PdfIdentityEncoding(true);
        private readonly bool isVertical;

        private PdfIdentityEncoding(bool isVertical)
        {
            this.isVertical = isVertical;
        }

        internal override short GetCID(byte[] code) => 
            (short) ((code[0] << 8) + code[1]);

        protected internal override PdfStringCommandData GetStringData(byte[] bytes, double[] glyphOffsets)
        {
            int num = bytes.Length / 2;
            short[] str = new short[num];
            byte[][] charCodes = new byte[num][];
            double[] offsets = new double[num + 1];
            int index = 0;
            int num3 = 0;
            while (index < num)
            {
                if (glyphOffsets != null)
                {
                    offsets[index] = glyphOffsets[num3];
                }
                byte num4 = bytes[num3++];
                byte num5 = bytes[num3++];
                str[index] = (short) ((num4 << 8) + num5);
                charCodes[index] = new byte[] { num4, num5 };
                index++;
            }
            return new PdfStringCommandData(charCodes, str, offsets);
        }

        protected internal override object Write(PdfObjectCollection objects) => 
            new PdfName(ReferenceEquals(this, VerticalIdentity) ? "Identity-V" : "Identity-H");

        public static PdfIdentityEncoding HorizontalIdentity =>
            horizontalIdentity;

        public static PdfIdentityEncoding VerticalIdentity =>
            verticalIdentity;

        public override bool IsVertical =>
            this.isVertical;
    }
}

