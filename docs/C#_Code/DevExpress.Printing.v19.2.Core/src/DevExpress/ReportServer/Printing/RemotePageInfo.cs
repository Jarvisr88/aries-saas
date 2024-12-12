namespace DevExpress.ReportServer.Printing
{
    using DevExpress.XtraPrinting;
    using System;

    internal class RemotePageInfo : IDisposable
    {
        private PrintingSystemBase ps;
        private DevExpress.XtraPrinting.Page page;

        public RemotePageInfo(DevExpress.XtraPrinting.Page fakedPage)
        {
            this.page = fakedPage;
        }

        public RemotePageInfo(DevExpress.XtraPrinting.Page realPage, PrintingSystemBase ps)
        {
            this.page = realPage;
            this.ps = ps;
        }

        public void Dispose()
        {
            if ((this.ps != null) && !this.ps.IsDisposed)
            {
                this.ps.Dispose();
            }
            this.ps = null;
        }

        public DevExpress.XtraPrinting.Page Page
        {
            get => 
                this.page;
            set
            {
                if (value != null)
                {
                    this.page = value;
                }
            }
        }
    }
}

