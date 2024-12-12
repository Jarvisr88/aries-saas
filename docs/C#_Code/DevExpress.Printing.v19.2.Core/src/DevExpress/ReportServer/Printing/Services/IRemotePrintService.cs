namespace DevExpress.ReportServer.Printing.Services
{
    using System;

    public interface IRemotePrintService
    {
        void Print(Action<PrintDocument> printAction, Func<PrintDocument> runDialogAction);
        void PrintDirect(int fromIndex, int toIndex, Action<string> printDirectAction);
    }
}

