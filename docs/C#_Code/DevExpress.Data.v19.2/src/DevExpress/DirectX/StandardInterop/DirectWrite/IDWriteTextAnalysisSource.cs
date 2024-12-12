namespace DevExpress.DirectX.StandardInterop.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, Guid("688e1a58-5094-47c8-adc8-fbcea60ae92b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDWriteTextAnalysisSource
    {
        void GetTextAtPosition([In] int textPosition, [MarshalAs(UnmanagedType.LPWStr)] out string textString, out int textLength);
        void GetTextBeforePosition([In] int textPosition, [MarshalAs(UnmanagedType.LPWStr)] out string textString, out int textLength);
        [PreserveSig]
        DWRITE_READING_DIRECTION GetParagraphReadingDirection();
        void GetLocaleName([In] int textPosition, out int textLength, [MarshalAs(UnmanagedType.LPWStr)] out string localeName);
        void GetNumberSubstitution([In] int textPosition, out int textLength, out IDWriteNumberSubstitution numberSubstitution);
    }
}

