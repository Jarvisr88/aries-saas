namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public enum PdfDeviceColorSpaceKind
    {
        [PdfFieldName("DeviceGray")]
        Gray = 0,
        [PdfFieldName("DeviceRGB")]
        RGB = 1,
        [PdfFieldName("DeviceCMYK")]
        CMYK = 2
    }
}

