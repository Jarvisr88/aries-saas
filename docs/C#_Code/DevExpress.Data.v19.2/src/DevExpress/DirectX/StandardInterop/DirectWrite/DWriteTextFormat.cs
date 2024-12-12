namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.StandardInterop;
    using System;

    public class DWriteTextFormat : ComObject<IDWriteTextFormat>
    {
        protected internal DWriteTextFormat(IDWriteTextFormat nativeObject) : base(nativeObject)
        {
        }
    }
}

