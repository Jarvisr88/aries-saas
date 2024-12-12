namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("5810cd44-0ca0-4701-b3fa-bec5182ae4f6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteTextAnalysisSink
    {
        void SetScriptAnalysis([In] int textPosition, [In] int textLength, [In] ref DWRITE_SCRIPT_ANALYSIS scriptAnalysis);
        void SetLineBreakpoints([In] int textPosition, [In] int textLength, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] DWRITE_LINE_BREAKPOINT[] lineBreakpoints);
        void SetBidiLevel([In] int textPosition, [In] int textLength, [In] byte explicitLevel, [In] byte resolvedLevel);
        void SetNumberSubstitution([In] int textPosition, [In] int textLength, [In] IDWriteNumberSubstitution numberSubstitution);
    }
}

