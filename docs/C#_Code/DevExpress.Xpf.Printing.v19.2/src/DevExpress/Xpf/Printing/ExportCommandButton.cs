namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.DocumentViewer;
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    internal class ExportCommandButton : CommandButton
    {
        private readonly ExportFormat format;

        public ExportCommandButton(ExportFormat format)
        {
            this.format = format;
        }

        public ExportFormat Format =>
            this.format;

        public object Tag { get; internal set; }
    }
}

