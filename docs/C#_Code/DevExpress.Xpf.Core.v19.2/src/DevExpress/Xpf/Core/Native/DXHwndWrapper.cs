namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical]
    public abstract class DXHwndWrapper : DisposableObject
    {
        private static long failedDestroyWindows;
        private static int lastDestroyWindowError;
        private static DXHwndWrapper.FaultEventCallback callback;
        private bool isHandleCreationAllowed;
        private IntPtr handle;
        private ushort wndClassAtom;
        private GCHandle gcHandle;
        private readonly DXHwndWrapper.WndProcWrapper wndProcWrapper;

        static DXHwndWrapper();
        public DXHwndWrapper();
        [CLSCompliant(false)]
        protected virtual ushort CreateWindowClassCore();
        protected abstract IntPtr CreateWindowCore();
        protected virtual void DestroyWindowClassCore();
        [SecuritySafeCritical]
        protected virtual void DestroyWindowCore();
        [SecuritySafeCritical]
        protected override void DisposeNativeResources();
        public void EnsureHandle();
        [CLSCompliant(false)]
        protected ushort RegisterClass(string className);
        [SecuritySafeCritical]
        private void SubclassWndProc();
        [SecuritySafeCritical]
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

        [CLSCompliant(false)]
        protected ushort WindowClassAtom { get; }

        public IntPtr Handle { get; }

        protected virtual bool IsWindowSubclassed { get; }

        private class FaultEventCallback
        {
            public int ExecuteCallback();
        }

        private class WndProcWrapper
        {
            private GCHandle gcHandle;
            private WeakReference target;
            public DevExpress.Xpf.Core.Native.NativeWindowMethods.WndProc PublicWndProc;

            public WndProcWrapper(DXHwndWrapper owner);
            private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

            public DXHwndWrapper TargetWrapper { get; }
        }
    }
}

