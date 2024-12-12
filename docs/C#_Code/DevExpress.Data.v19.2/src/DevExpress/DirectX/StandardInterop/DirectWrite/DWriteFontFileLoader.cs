namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.StandardInterop;
    using System;

    public class DWriteFontFileLoader : ComObject<IDWriteFontFileLoader>
    {
        internal DWriteFontFileLoader(IDWriteFontFileLoader nativeObject) : base(nativeObject)
        {
        }

        public DWriteFontFileStream CreateStreamFromKey(IntPtr fontFileReferenceKey, int fontFileReferenceKeySize)
        {
            IDWriteFontFileStream stream;
            InteropHelpers.CheckHResult(base.WrappedObject.CreateStreamFromKey(fontFileReferenceKey, fontFileReferenceKeySize, out stream));
            return new DWriteFontFileStream(stream);
        }
    }
}

