namespace DevExpress.Text.Fonts.DirectWrite.CCW
{
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts.DirectWrite;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DWriteFontCollectionLoader : IDWriteFontCollectionLoader
    {
        [SecuritySafeCritical]
        public int CreateEnumeratorFromKey(IDWriteFactory factory, IntPtr collectionKey, int collectionKeySize, out IDWriteFontFileEnumerator enumerator)
        {
            if (collectionKeySize != IntPtr.Size)
            {
                throw new ArgumentException("collectionKeySize");
            }
            DirectWritePrivateFontCollection target = GCHandle.FromIntPtr(Marshal.ReadIntPtr(collectionKey)).Target as DirectWritePrivateFontCollection;
            if (target == null)
            {
                throw new ArgumentException("collection");
            }
            enumerator = new DWriteFontFileEnumerator(target.Files.GetEnumerator());
            return 0;
        }
    }
}

