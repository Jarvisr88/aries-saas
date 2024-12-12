namespace DevExpress.Pdf.ContentGeneration
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfFontParameters
    {
        private readonly string name;
        private readonly int weight;
        private readonly bool isItalic;
        public string Name =>
            this.name;
        public int Weight =>
            this.weight;
        public bool IsItalic =>
            this.isItalic;
        public PdfFontParameters(string name, int weight, bool isItalic)
        {
            this.name = name;
            this.weight = weight;
            this.isItalic = isItalic;
        }
    }
}

