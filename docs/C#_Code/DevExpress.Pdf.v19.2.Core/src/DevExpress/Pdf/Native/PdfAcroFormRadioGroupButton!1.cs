namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfAcroFormRadioGroupButton<TRect>
    {
        private readonly string name;
        private readonly TRect rect;
        public string Name =>
            this.name;
        public TRect Rect =>
            this.rect;
        public PdfAcroFormRadioGroupButton(string name, TRect rect)
        {
            this.name = name;
            this.rect = rect;
        }
    }
}

