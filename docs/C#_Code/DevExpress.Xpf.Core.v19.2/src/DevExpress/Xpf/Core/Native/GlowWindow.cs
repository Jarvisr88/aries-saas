namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Security;
    using System.Windows.Controls;
    using System.Windows.Media;

    [SecuritySafeCritical]
    internal class GlowWindow : DXHwndWrapper
    {
        private const string GlowWindowClassName = "ThemedWindowGlowWindow";
        private const int GlowDepth = 9;
        private readonly ThemedWindow targetWindow;
        private readonly Dock orientation;
        private readonly GlowBitmap[] activeGlowBitmaps;
        private readonly GlowBitmap[] inactiveGlowBitmaps;
        private static ushort sharedWindowClassAtom;
        private static DevExpress.Xpf.Core.Native.NativeWindowMethods.WndProc sharedWndProc;
        private static long createdGlowWindows;
        private static long disposedGlowWindows;
        private int left;
        private int top;
        private int width;
        private int height;
        private bool isVisible;
        private bool isActive;
        private Color activeGlowColor;
        private Color inactiveGlowColor;
        private GlowWindow.FieldInvalidationTypes invalidatedValues;
        private bool pendingDelayRender;

        public GlowWindow(ThemedWindow owner, Dock orientation);
        private void BeginDelayedRender();
        private void CancelDelayedRender();
        public void ChangeOwner(IntPtr newOwner);
        private void ClearCache(GlowBitmap[] cache);
        public void CommitChanges();
        private void CommitDelayedRender(object sender, EventArgs e);
        protected override ushort CreateWindowClassCore();
        [SecuritySafeCritical]
        protected override IntPtr CreateWindowCore();
        protected override void DestroyWindowClassCore();
        protected override void DisposeManagedResources();
        protected override void DisposeNativeResources();
        [SecuritySafeCritical]
        private void DrawBottom(GlowDrawingContext drawingContext);
        [SecuritySafeCritical]
        private void DrawLeft(GlowDrawingContext drawingContext);
        [SecuritySafeCritical]
        private void DrawRight(GlowDrawingContext drawingContext);
        [SecuritySafeCritical]
        private void DrawTop(GlowDrawingContext drawingContext);
        private GlowBitmap GetOrCreateBitmap(GlowDrawingContext drawingContext, GlowBitmapPart bitmapPart);
        [SecuritySafeCritical]
        private static void GetSharedWindowClassAtom();
        private void InvalidateCachedBitmaps();
        private bool InvalidatedValuesHasFlag(GlowWindow.FieldInvalidationTypes flag);
        [SecuritySafeCritical]
        private void RenderLayeredWindow();
        private void UpdateLayeredWindowCore();
        private void UpdateProperty<T>(ref T field, T value, GlowWindow.FieldInvalidationTypes invalidatedValues) where T: struct;
        [SecuritySafeCritical]
        public void UpdateWindowPos();
        private void UpdateWindowPosCore();
        [SecuritySafeCritical]
        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        private bool IsDeferringChanges { get; }

        private static ushort SharedWindowClassAtom { get; }

        public bool IsVisible { get; set; }

        public int Left { get; set; }

        public int Top { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsActive { get; set; }

        public Color ActiveGlowColor { get; set; }

        public Color InactiveGlowColor { get; set; }

        internal IntPtr TargetWindowHandle { get; }

        protected override bool IsWindowSubclassed { get; }

        private bool IsPositionValid { get; }

        [Flags]
        private enum FieldInvalidationTypes
        {
            public const GlowWindow.FieldInvalidationTypes None = GlowWindow.FieldInvalidationTypes.None;,
            public const GlowWindow.FieldInvalidationTypes Location = GlowWindow.FieldInvalidationTypes.Location;,
            public const GlowWindow.FieldInvalidationTypes Size = GlowWindow.FieldInvalidationTypes.Size;,
            public const GlowWindow.FieldInvalidationTypes ActiveColor = GlowWindow.FieldInvalidationTypes.ActiveColor;,
            public const GlowWindow.FieldInvalidationTypes InactiveColor = GlowWindow.FieldInvalidationTypes.InactiveColor;,
            public const GlowWindow.FieldInvalidationTypes Render = GlowWindow.FieldInvalidationTypes.Render;,
            public const GlowWindow.FieldInvalidationTypes Visibility = GlowWindow.FieldInvalidationTypes.Visibility;
        }
    }
}

