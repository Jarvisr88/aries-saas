namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Security;

    [SecuritySafeCritical]
    internal sealed class GlowDrawingContext : DisposableObject
    {
        public BLENDFUNCTION Blend;
        private readonly IntPtr hdcScreen;
        private readonly IntPtr hdcWindow;
        private readonly GlowBitmap windowBitmap;
        private readonly IntPtr hdcBackground;

        [SecuritySafeCritical]
        public GlowDrawingContext(int width, int height);
        protected override void DisposeManagedResources();
        [SecuritySafeCritical]
        protected override void DisposeNativeResources();

        public bool IsInitialized { get; }

        public IntPtr ScreenDC { get; }

        public IntPtr WindowDC { get; }

        public IntPtr BackgroundDC { get; }

        public int Width { get; }

        public int Height { get; }
    }
}

