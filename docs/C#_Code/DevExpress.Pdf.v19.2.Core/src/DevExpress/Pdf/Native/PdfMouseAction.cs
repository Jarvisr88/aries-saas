namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfMouseAction
    {
        private readonly PdfDocumentPosition documentPosition;
        private readonly PdfMouseButton button;
        private readonly PdfModifierKeys modifierKeys;
        private readonly int clicks;
        private readonly bool isOutsideOfView;
        public PdfDocumentPosition DocumentPosition =>
            this.documentPosition;
        public PdfMouseButton Button =>
            this.button;
        public PdfModifierKeys ModifierKeys =>
            this.modifierKeys;
        public int Clicks =>
            this.clicks;
        public bool IsOutsideOfView =>
            this.isOutsideOfView;
        public PdfMouseAction(PdfDocumentPosition documentPosition, PdfMouseButton button, PdfModifierKeys modifierKeys, int clicks, bool isOutsideOfView)
        {
            this.documentPosition = documentPosition;
            this.button = button;
            this.modifierKeys = modifierKeys;
            this.clicks = clicks;
            this.isOutsideOfView = isOutsideOfView;
        }

        public PdfMouseAction(PdfDocumentPosition documentPosition, PdfMouseButton button, PdfModifierKeys modifierKeys, int clicks) : this(documentPosition, button, modifierKeys, clicks, false)
        {
        }
    }
}

