namespace DevExpress.XtraPrinting
{
    using System;
    using System.Drawing.Printing;

    public class PrintProgressEventArgs : EventArgs
    {
        private QueryPageSettingsEventArgs source;
        private int pageIndex;

        internal PrintProgressEventArgs(QueryPageSettingsEventArgs source, int pageIndex)
        {
            this.source = source;
            this.pageIndex = pageIndex;
        }

        public int PageIndex =>
            this.pageIndex;

        public System.Drawing.Printing.PageSettings PageSettings
        {
            get => 
                this.source.PageSettings;
            set => 
                this.source.PageSettings = value;
        }

        public System.Drawing.Printing.PrintAction PrintAction =>
            this.source.PrintAction;
    }
}

