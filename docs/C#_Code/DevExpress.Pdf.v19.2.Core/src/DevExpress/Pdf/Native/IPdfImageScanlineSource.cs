namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfImageScanlineSource : IDisposable
    {
        void FillNextScanline(byte[] scanlineData);

        int ComponentsCount { get; }

        bool HasAlpha { get; }
    }
}

