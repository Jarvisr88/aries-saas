namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct ExportOptionContext
    {
        private ExportFormat currentFormat;
        private Action<ExportFormat> currentBeginExportAction;
        private Action<ExportFormat> currentExportAction;
        private Action currentEndExportAction;
        public ExportFormat Format =>
            this.currentFormat;
        public Action<ExportFormat> BeginExportAction =>
            this.currentBeginExportAction;
        public Action<ExportFormat> ExportAction =>
            this.currentExportAction;
        public Action EndExportAction =>
            this.currentEndExportAction;
        public ExportOptionContext(ExportFormat format, Action<ExportFormat> beginExportAction, Action<ExportFormat> exportAction, Action endExportAction)
        {
            this.currentFormat = format;
            this.currentBeginExportAction = beginExportAction;
            this.currentExportAction = exportAction;
            this.currentEndExportAction = endExportAction;
        }
    }
}

