namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    public class DWriteFactory : ComObject<IDWriteFactory>
    {
        protected internal DWriteFactory(IDWriteFactory nativeObject) : base(nativeObject)
        {
        }

        [SecuritySafeCritical]
        public DWriteFontCollection CreateCustomFontCollection(IDWriteFontCollectionLoader loader, IntPtr collectionKey)
        {
            DWriteFontCollection fonts;
            GCHandle handle = GCHandle.Alloc(collectionKey, GCHandleType.Pinned);
            try
            {
                fonts = new DWriteFontCollection(base.WrappedObject.CreateCustomFontCollection(loader, handle.AddrOfPinnedObject(), IntPtr.Size));
            }
            finally
            {
                handle.Free();
            }
            return fonts;
        }

        public DWriteFontFile CreateCustomFontFileReference(IntPtr fontFileReferenceKey, int fontFileReferenceKeySize, IDWriteFontFileLoader fontFileLoader) => 
            new DWriteFontFile(base.WrappedObject.CreateCustomFontFileReference(fontFileReferenceKey, fontFileReferenceKeySize, fontFileLoader));

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
            Converter<DWriteFontFile, IDWriteFontFile> converter = <>c.<>9__5_0;
            if (<>c.<>9__5_0 == null)
            {
                Converter<DWriteFontFile, IDWriteFontFile> local1 = <>c.<>9__5_0;
                converter = <>c.<>9__5_0 = fontFile => fontFile.FontFile;
            }
            IDWriteFontFile[] fileArray = Array.ConvertAll<DWriteFontFile, IDWriteFontFile>(fontFiles, converter);
            return new DWriteFontFace(base.WrappedObject.CreateFontFace(fontFaceType, numberOfFiles, fileArray, faceIndex, fontFaceSimulationFlags));
        }

        public void CreateFontFileReference(string filePath, IntPtr lastWriteTime, out DWriteFontFile fontFile)
        {
            throw new NotImplementedException();
        }

        public void CreateGdiCompatibleTextLayout()
        {
            throw new NotImplementedException();
        }

        public void CreateGlyphRunAnalysis()
        {
            throw new NotImplementedException();
        }

        public void CreateMonitorRenderingParams()
        {
            throw new NotImplementedException();
        }

        public void CreateNumberSubstitution()
        {
            throw new NotImplementedException();
        }

        public void CreateRenderingParams()
        {
            throw new NotImplementedException();
        }

        public DWriteTextAnalyzer CreateTextAnalyzer() => 
            new DWriteTextAnalyzer(base.WrappedObject.CreateTextAnalyzer());

        public DWriteTextFormat CreateTextFormat(string fontFamilyName, DWriteFontCollection fontCollection, DWRITE_FONT_WEIGHT weight, DWRITE_FONT_STYLE style, DWRITE_FONT_STRETCH fontStretch, float fontSize, string localeName) => 
            new DWriteTextFormat(base.WrappedObject.CreateTextFormat(fontFamilyName, fontCollection?.WrappedObject, weight, style, fontStretch, fontSize, localeName));

        public DWriteTextLayout CreateTextLayout(string str, DWriteTextFormat format, float maxWidth, float maxHeight) => 
            new DWriteTextLayout(base.WrappedObject.CreateTextLayout(str, str.Length, format?.WrappedObject, maxWidth, maxHeight));

        public void CreateTypography()
        {
            throw new NotImplementedException();
        }

        public DWriteGdiInterop GetGdiInterop() => 
            new DWriteGdiInterop(base.WrappedObject.GetGdiInterop());

        public DWriteFontCollection GetSystemFontCollection(bool checkForUpdates)
        {
            IDWriteFontCollection fonts;
            base.WrappedObject.GetSystemFontCollection(out fonts, checkForUpdates);
            return new DWriteFontCollection(fonts);
        }

        public void RegisterFontCollectionLoader(IDWriteFontCollectionLoader loader)
        {
            base.WrappedObject.RegisterFontCollectionLoader(loader);
        }

        public void RegisterFontFileLoader(IDWriteFontFileLoader fontFileLoader)
        {
            base.WrappedObject.RegisterFontFileLoader(fontFileLoader);
        }

        public void UnregisterFontCollectionLoader(IDWriteFontCollectionLoader loader)
        {
            base.WrappedObject.UnregisterFontCollectionLoader(loader);
        }

        public void UnregisterFontFileLoader(IDWriteFontFileLoader fontFileLoader)
        {
            base.WrappedObject.UnregisterFontFileLoader(fontFileLoader);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DWriteFactory.<>c <>9 = new DWriteFactory.<>c();
            public static Converter<DWriteFontFile, IDWriteFontFile> <>9__5_0;

            internal IDWriteFontFile <CreateFontFace>b__5_0(DWriteFontFile fontFile) => 
                fontFile.FontFile;
        }
    }
}

