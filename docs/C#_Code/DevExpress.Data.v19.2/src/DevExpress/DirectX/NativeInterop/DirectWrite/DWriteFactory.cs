namespace DevExpress.DirectX.NativeInterop.DirectWrite
{
    using DevExpress.DirectX.Common;
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.NativeInterop;
    using DevExpress.DirectX.NativeInterop.CCW;
    using System;
    using System.Runtime.CompilerServices;

    public class DWriteFactory : ComObject
    {
        public DWriteFactory(IntPtr nativeObject) : base(nativeObject)
        {
        }

        public void CreateCustomFontCollection()
        {
            throw new NotImplementedException();
        }

        public DWriteFontFile CreateCustomFontFileReference(IntPtr fontFileReferenceKey, int fontFileReferenceKeySize, IComCallableWrapper<IDWriteFontFileLoaderCCW> fontFileLoader)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, fontFileReferenceKey, fontFileReferenceKeySize, fontFileLoader.NativeObject, out ptr, 8));
            return new DWriteFontFile(ptr);
        }

        public void CreateCustomRenderingParams()
        {
            throw new NotImplementedException();
        }

        public void CreateEllipsisTrimmingSign()
        {
            throw new NotImplementedException();
        }

        public DWriteFontFace CreateFontFace(DWRITE_FONT_FACE_TYPE fontFaceType, DWriteFontFile fontFile)
        {
            DWriteFontFile[] fontFiles = new DWriteFontFile[] { fontFile };
            return this.CreateFontFace(fontFaceType, 1, fontFiles, 0, DWRITE_FONT_SIMULATIONS.None);
        }

        public DWriteFontFace CreateFontFace(DWRITE_FONT_FACE_TYPE fontFaceType, int numberOfFiles, DWriteFontFile[] fontFiles, int faceIndex, DWRITE_FONT_SIMULATIONS fontFaceSimulationFlags)
        {
            IntPtr ptr;
            Converter<DWriteFontFile, IntPtr> converter = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Converter<DWriteFontFile, IntPtr> local1 = <>c.<>9__7_0;
                converter = <>c.<>9__7_0 = fontFile => fontFile.ToNativeObject();
            }
            using (ArrayMarshaler marshaler = new ArrayMarshaler(Array.ConvertAll<DWriteFontFile, IntPtr>(fontFiles, converter)))
            {
                InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, (int) fontFaceType, numberOfFiles, marshaler.Pointer, faceIndex, (int) fontFaceSimulationFlags, out ptr, 9));
            }
            return new DWriteFontFace(ptr);
        }

        public void CreateFontFileReference()
        {
            throw new NotImplementedException();
        }

        public void CreateGdiCompatibleTextLayout()
        {
            throw new NotImplementedException();
        }

        public void CreateGlyphRunAnalysis()
        {
        }

        public void CreateMonitorRenderingParams()
        {
            throw new NotImplementedException();
        }

        public void CreateNumberSubstitution()
        {
        }

        public void CreateRenderingParams()
        {
            throw new NotImplementedException();
        }

        public void CreateTextAnalyzer()
        {
            throw new NotImplementedException();
        }

        public void CreateTextFormat()
        {
            throw new NotImplementedException();
        }

        public void CreateTextLayout()
        {
            throw new NotImplementedException();
        }

        public void CreateTypography()
        {
            throw new NotImplementedException();
        }

        public void GetGdiInterop()
        {
        }

        public DWriteFontCollection GetSystemFontCollection(bool checkForUpdates)
        {
            IntPtr ptr;
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, out ptr, MarshalBool(checkForUpdates), 3));
            return new DWriteFontCollection(ptr);
        }

        public void RegisterFontCollectionLoader()
        {
            throw new NotImplementedException();
        }

        public void RegisterFontFileLoader(IComCallableWrapper<IDWriteFontFileLoaderCCW> fontFileLoader)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, fontFileLoader.NativeObject, 13));
        }

        public void UnregisterFontCollectionLoader()
        {
            throw new NotImplementedException();
        }

        public void UnregisterFontFileLoader(IComCallableWrapper<IDWriteFontFileLoaderCCW> fontFileLoader)
        {
            InteropHelpers.CheckHResult(ComObject.InvokeHelper.CalliInt(base.NativeObject, fontFileLoader.NativeObject, 14));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DWriteFactory.<>c <>9 = new DWriteFactory.<>c();
            public static Converter<DWriteFontFile, IntPtr> <>9__7_0;

            internal IntPtr <CreateFontFace>b__7_0(DWriteFontFile fontFile) => 
                fontFile.ToNativeObject();
        }
    }
}

