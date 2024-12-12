namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PdfPostScriptDictionaryEntry
    {
        private readonly string key;
        private readonly object value;
        public string Key =>
            this.key;
        public object Value =>
            this.value;
        public PdfPostScriptDictionaryEntry(string key, object value)
        {
            this.key = key;
            this.value = value;
        }
    }
}

