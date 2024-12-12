namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("8339FDE3-106F-47ab-8373-1C6295EB10B3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteInlineObject
    {
        void Draw();
        void GetMetrics();
        void GetOverhangMetrics();
        void GetBreakConditions();
    }
}

