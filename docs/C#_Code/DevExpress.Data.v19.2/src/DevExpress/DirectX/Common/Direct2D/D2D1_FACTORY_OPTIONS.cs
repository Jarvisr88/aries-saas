namespace DevExpress.DirectX.Common.Direct2D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct D2D1_FACTORY_OPTIONS
    {
        private readonly D2D1_DEBUG_LEVEL debugLevel;
        public D2D1_FACTORY_OPTIONS(D2D1_DEBUG_LEVEL debugLevel)
        {
            this.debugLevel = debugLevel;
        }
    }
}

