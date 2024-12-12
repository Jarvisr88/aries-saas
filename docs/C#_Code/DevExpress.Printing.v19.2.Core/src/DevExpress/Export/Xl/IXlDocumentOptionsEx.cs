namespace DevExpress.Export.Xl
{
    using System;

    public interface IXlDocumentOptionsEx : IXlDocumentOptions
    {
        bool UseDeviceIndependentPixels { get; set; }

        bool TruncateStringsToMaxLength { get; set; }

        bool SuppressEmptyStrings { get; set; }
    }
}

