namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using System;

    public interface IDirectWriteFontFileStreamProvider
    {
        IDWriteFontFileStream GetFontFileStream(int index);
    }
}

