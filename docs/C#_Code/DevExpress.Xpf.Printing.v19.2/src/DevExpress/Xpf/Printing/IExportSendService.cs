namespace DevExpress.Xpf.Printing
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Export;
    using System;
    using System.Windows;

    public interface IExportSendService
    {
        void Export(PrintingSystemBase ps, Window ownerWindow, ExportOptionsBase options, IDialogService dialogService);
        void SendFileByEmail(PrintingSystemBase ps, EmailSenderBase emailSender, Window ownerWindow, ExportOptionsBase options, IDialogService dialogService);
    }
}

