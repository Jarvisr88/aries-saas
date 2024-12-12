namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing.Printing;

    public class PrintDocumentEventArgs : EventArgs
    {
        private System.Drawing.Printing.PrintDocument printDocument;

        internal PrintDocumentEventArgs(System.Drawing.Printing.PrintDocument printDocument)
        {
            this.printDocument = printDocument;
        }

        public System.Drawing.Printing.PrintDocument PrintDocument =>
            this.printDocument;
    }
}

