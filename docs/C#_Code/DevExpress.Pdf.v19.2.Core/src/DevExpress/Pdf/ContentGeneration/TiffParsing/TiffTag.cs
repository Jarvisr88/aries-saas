namespace DevExpress.Pdf.ContentGeneration.TiffParsing
{
    using System;

    public enum TiffTag : short
    {
        PhotometricInterpretation = 0x106,
        Compression = 0x103,
        ImageLength = 0x101,
        ImageWidth = 0x100,
        FillOrder = 0x10a,
        ResolutionUnit = 0x128,
        XResolution = 0x11a,
        YResolution = 0x11b,
        RowsPerStrip = 0x116,
        StripOffsets = 0x111,
        StripByteCounts = 0x117,
        T4Options = 0x124,
        T6Options = 0x125,
        Predictor = 0x13d,
        BitsPerSample = 0x102,
        ColorMap = 320,
        SamplesPerPixel = 0x115
    }
}

