namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;

    internal interface PixelConverter
    {
        void ExtractPixel(byte[] line, ref int j, byte[] tempBuffer, int i);

        int TargetBytesPerPixel { get; }

        int SourceBytesPerPixel { get; }
    }
}

