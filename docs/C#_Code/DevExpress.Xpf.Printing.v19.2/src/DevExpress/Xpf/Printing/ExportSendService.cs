namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Exports;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Windows;

    internal class ExportSendService : IExportSendService
    {
        public void Export(PrintingSystemBase ps, Window ownerWindow, ExportOptionsBase options, IDialogService dialogService)
        {
            new ExportFileHelper(ps, new EmailSender(), ownerWindow, dialogService).ExecExport(options, null);
        }

        public void SendFileByEmail(PrintingSystemBase ps, EmailSenderBase emailSender, Window ownerWindow, ExportOptionsBase options, IDialogService dialogService)
        {
            new ExportFileHelper(ps, emailSender, ownerWindow, dialogService).SendFileByEmail(options, null);
        }
    }
}

