namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Drawing;

    public class Pdf24BppBitmapToXObjectConverter : PdfBitmapToXObjectConverter
    {
        public Pdf24BppBitmapToXObjectConverter(Bitmap bitmap) : base(bitmap)
        {
        }

        protected override void ReadNextImageRow(byte[] rowBuffer, PdfImageStreamFlateEncoder dataEncoder)
        {
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
                num++;
            }
        }

        protected override byte[] SMask =>
            null;

        protected override int ComponentsCount =>
            3;
    }
}

