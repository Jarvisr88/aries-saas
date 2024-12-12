namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop;
    using System;
    using System.Runtime.InteropServices;

    public class DWriteFontFile : ComObject<IDWriteFontFile>
    {
        internal DWriteFontFile(IDWriteFontFile nativeObject) : base(nativeObject)
        {
        }

        public void Analyze(out bool isSupportedFontType, out DWRITE_FONT_FILE_TYPE fontFileType, out DWRITE_FONT_FACE_TYPE fontFaceType, out int numberOfFaces)
        {
            throw new NotImplementedException();
        }

        public byte[] GetData()
        {
            IntPtr ptr;
            int num;
            byte[] buffer;
            base.WrappedObject.GetReferenceKey(out ptr, out num);
            using (DWriteFontFileLoader loader = new DWriteFontFileLoader(base.WrappedObject.GetLoader()))
            {
                using (DWriteFontFileStream stream = loader.CreateStreamFromKey(ptr, num))
                {
                    buffer = stream.ReadAllData();
                }
            }
            return buffer;
        }

        public void GetLoader()
        {
            throw new NotImplementedException();
        }

        public void GetReferenceKey()
        {
        }

        internal IDWriteFontFile FontFile =>
            base.WrappedObject;
    }
}

