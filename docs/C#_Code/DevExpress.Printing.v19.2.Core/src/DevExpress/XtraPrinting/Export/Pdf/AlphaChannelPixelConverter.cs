namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class AlphaChannelPixelConverter : PixelConverter
    {
        private bool hasTransparentPixels;

        public void ExtractPixel(byte[] line, ref int j, byte[] tempBuffer, int i)
        {
            byte num = tempBuffer[i + 3];
            this.hasTransparentPixels |= num < 0xff;
            int index = j;
            j = index + 1;
            line[index] = num;
        }

        public bool HasTransparentPixels =>
            this.hasTransparentPixels;

        public int TargetBytesPerPixel =>
            1;

        public int SourceBytesPerPixel =>
            4;
    }
}

