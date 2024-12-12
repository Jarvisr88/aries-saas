namespace DevExpress.DirectX.Common.DirectWrite
{
    using DevExpress.DirectX.Common;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [StructLayout(LayoutKind.Sequential)]
    public struct DWRITE_TYPOGRAPHIC_FEATURES : IDisposable
    {
        private IntPtr features;
        private readonly int featureCount;
        [SecuritySafeCritical]
        public DWRITE_TYPOGRAPHIC_FEATURES(DWRITE_FONT_FEATURE[] fontFeatures)
        {
            this.features = InteropHelpers.StructArrayToPtr<DWRITE_FONT_FEATURE>(fontFeatures);
            this.featureCount = fontFeatures.Length;
        }

        public void Dispose()
        {
            if (this.features != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.features);
                this.features = IntPtr.Zero;
            }
        }
    }
}

