namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Drawing;

    public class Pdf32BppBitmapToXObjectConverter : PdfBitmapToXObjectConverter
    {
        private byte[] sMask;
        private bool hasMask;
        private PdfImageStreamFlateEncoder smaskEncoder;

        public Pdf32BppBitmapToXObjectConverter(Bitmap bitmap, bool extractSMask) : base(bitmap)
        {
            if (extractSMask && this.hasMask)
            {
                this.sMask = this.smaskEncoder.GetEncodedData();
            }
            this.smaskEncoder.Dispose();
        }

        protected override void ReadNextImageRow(byte[] rowBuffer, PdfImageStreamFlateEncoder dataEncoder)
        {
            this.smaskEncoder ??= new PdfImageStreamFlateEncoder(base.Width);
            int num = 0;
            int num2 = 0;
            while (num < base.Width)
            {
                byte num3 = rowBuffer[num2++];
                byte num4 = rowBuffer[num2++];
                byte num5 = rowBuffer[num2++];
                dataEncoder.Add(num5);
                dataEncoder.Add(num4);
                dataEncoder.Add(num3);
                byte num6 = rowBuffer[num2++];
                this.hasMask |= num6 != 0xff;
                this.smaskEncoder.Add(num6);
                num++;
            }
            this.smaskEncoder.EndRow();
        }

        protected override byte[] SMask =>
            this.sMask;

        protected override int ComponentsCount =>
            4;
    }
}

