namespace DevExpress.Text.Fonts
{
    using DevExpress.Text.Fonts.DirectWrite;
    using System;
    using System.Collections.Generic;

    public abstract class DXFontEngine : IDisposable
    {
        protected DXFontEngine()
        {
        }

        public static DXFontEngine CreateEngine(DXEngineType type)
        {
            if (type != DXEngineType.DirectWrite)
            {
                throw new Exception("todo");
            }
            return new DirectWriteFontEngine();
        }

        public static DXFontEngine CreatePlatformEngine()
        {
            PlatformID platform = Environment.OSVersion.Platform;
            return ((platform == PlatformID.Win32NT) ? CreateEngine(DXEngineType.DirectWrite) : ((platform == PlatformID.Unix) ? CreateEngine(DXEngineType.CrossPlatform) : null));
        }

        public abstract IDXPrivateFontCollection CreatePrivateFontCollection(IEnumerable<byte[]> fontFiles);
        public IDXPrivateFontCollection CreatePrivateFontCollection(params byte[][] fontFiles) => 
            this.CreatePrivateFontCollection((IEnumerable<byte[]>) fontFiles);

        public abstract void Dispose();

        public abstract IDXSystemFontCollection SystemFontCollection { get; }
    }
}

