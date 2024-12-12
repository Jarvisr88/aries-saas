namespace DevExpress.Text.Fonts.DirectWrite.CCW
{
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts.DirectWrite;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DWritePrivateFontCollectionFontFileLoader : IDWriteFontFileLoader
    {
        private readonly IDirectWriteFontFileStreamProvider streamProvider;

        public DWritePrivateFontCollectionFontFileLoader(IDirectWriteFontFileStreamProvider streamProvider)
        {
            this.streamProvider = streamProvider;
        }

        [SecuritySafeCritical]
        public int CreateStreamFromKey(IntPtr fontFileReferenceKey, int fontFileReferenceKeySize, out IDWriteFontFileStream fontFileStream)
        {
            if (fontFileReferenceKeySize != 4)
            {
                throw new ArgumentException("fontFileReferenceKeySize");
            }
            fontFileStream = this.streamProvider.GetFontFileStream(Marshal.ReadInt32(fontFileReferenceKey));
            return 0;
        }
    }
}

