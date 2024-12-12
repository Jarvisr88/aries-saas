namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal class ColorChannels24PixelConverter : PixelConverter
    {
        public void ExtractPixel(byte[] line, ref int j, byte[] tempBuffer, int i)
        {
            int index = j;
            j = index + 1;
            line[index] = tempBuffer[i + 2];
            index = j;
            j = index + 1;
            line[index] = tempBuffer[i + 1];
            index = j;
            j = index + 1;
            line[index] = tempBuffer[i];
        }

        public int TargetBytesPerPixel =>
            3;

        public int SourceBytesPerPixel =>
            3;
    }
}

