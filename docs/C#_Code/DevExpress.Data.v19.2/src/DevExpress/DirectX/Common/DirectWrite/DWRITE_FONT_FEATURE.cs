namespace DevExpress.DirectX.Common.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_FONT_FEATURE
    {
        private readonly DWRITE_FONT_FEATURE_TAG nameTag;
        private readonly int parameter;
        public DWRITE_FONT_FEATURE_TAG NameTag =>
            this.nameTag;
        public int Parameter =>
            this.parameter;
        public DWRITE_FONT_FEATURE(DWRITE_FONT_FEATURE_TAG nameTag, int parameter)
        {
            this.nameTag = nameTag;
            this.parameter = parameter;
        }
    }
}

