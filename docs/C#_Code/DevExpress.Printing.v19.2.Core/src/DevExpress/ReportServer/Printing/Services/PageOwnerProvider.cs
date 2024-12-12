namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.XtraPrinting;
    using System;

    internal class PageOwnerProvider : IPageOwnerProvider
    {
        private readonly PageList owner;

        public PageOwnerProvider(PageList owner)
        {
            this.owner = owner;
        }

        public PageList PageOwner =>
            this.owner;
    }
}

