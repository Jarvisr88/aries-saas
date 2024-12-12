namespace DevExpress.Printing.Native.PrintEditor
{
    using System;
    using System.Drawing.Printing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct PrinterSettingsStub
    {
        private short copies;
        private bool collate;
        private System.Drawing.Printing.Duplex duplex;
        private System.Drawing.Printing.PaperSource paperSource;
        private bool canDuplex;
        public short Copies =>
            this.copies;
        public bool Collate =>
            this.collate;
        public System.Drawing.Printing.Duplex Duplex =>
            this.duplex;
        public System.Drawing.Printing.PaperSource PaperSource =>
            this.paperSource;
        public bool CanDuplex =>
            this.canDuplex;
        public PrinterSettingsStub(short copies, bool collate, System.Drawing.Printing.PaperSource paperSource) : this(copies, collate, paperSource, false, System.Drawing.Printing.Duplex.Default)
        {
        }

        public PrinterSettingsStub(short copies, bool collate, System.Drawing.Printing.PaperSource paperSource, System.Drawing.Printing.Duplex duplex) : this(copies, collate, paperSource, true, duplex)
        {
        }

        private PrinterSettingsStub(short copies, bool collate, System.Drawing.Printing.PaperSource paperSource, bool canDuplex, System.Drawing.Printing.Duplex duplex)
        {
            this.copies = copies;
            this.collate = collate;
            this.paperSource = paperSource;
            this.canDuplex = canDuplex;
            this.duplex = duplex;
        }
    }
}

