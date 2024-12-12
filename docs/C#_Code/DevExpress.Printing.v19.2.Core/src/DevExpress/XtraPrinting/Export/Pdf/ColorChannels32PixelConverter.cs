namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class ColorChannels32PixelConverter : PixelConverter
    {
        private bool hasTransparentPixels;

        public void ExtractPixel(byte[] line, ref int j, byte[] tempBuffer, int i)
        {
            int num2;
            byte num = tempBuffer[i + 3];
            if (num == 0xff)
            {
                num2 = j;
                j = num2 + 1;
                line[num2] = tempBuffer[i + 2];
                num2 = j;
                j = num2 + 1;
                line[num2] = tempBuffer[i + 1];
                num2 = j;
                j = num2 + 1;
                line[num2] = tempBuffer[i];
            }
            else
            {
                this.hasTransparentPixels |= true;
                if (num == 0)
                {
                    num2 = j;
                    j = num2 + 1;
                    line[num2] = 0;
                    num2 = j;
                    j = num2 + 1;
                    line[num2] = 0;
                    num2 = j;
                    j = num2 + 1;
                    line[num2] = 0;
                }
                else
                {
                    float num3 = ((float) num) / 255f;
                    num2 = j;
                    j = num2 + 1;
                    line[num2] = (byte) (num3 * tempBuffer[i + 2]);
                    num2 = j;
                    j = num2 + 1;
                    line[num2] = (byte) (num3 * tempBuffer[i + 1]);
                    num2 = j;
                    j = num2 + 1;
                    line[num2] = (byte) (num3 * tempBuffer[i]);
                }
            }
        }

        public bool HasTransparentPixels =>
            this.hasTransparentPixels;

        public int TargetBytesPerPixel =>
            3;

        public int SourceBytesPerPixel =>
            4;
    }
}

