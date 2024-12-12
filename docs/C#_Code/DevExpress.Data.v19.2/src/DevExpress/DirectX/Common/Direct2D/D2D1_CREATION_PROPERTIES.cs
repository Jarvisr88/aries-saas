namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_CREATION_PROPERTIES
    {
        private readonly D2D1_THREADING_MODE threadingMode;
        private readonly D2D1_DEBUG_LEVEL debugLevel;
        private readonly D2D1_DEVICE_CONTEXT_OPTIONS options;
        public D2D1_CREATION_PROPERTIES(D2D1_DEBUG_LEVEL debugLevel, D2D1_THREADING_MODE threadingMode, D2D1_DEVICE_CONTEXT_OPTIONS options)
        {
            this.debugLevel = debugLevel;
            this.threadingMode = threadingMode;
            this.options = options;
        }
    }
}

