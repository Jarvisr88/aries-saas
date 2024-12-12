namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.XtraPrinting;

    internal interface IPageOwnerProvider
    {
        PageList PageOwner { get; }
    }
}

