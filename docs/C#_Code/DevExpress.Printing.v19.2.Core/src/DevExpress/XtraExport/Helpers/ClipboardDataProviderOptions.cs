namespace DevExpress.XtraExport.Helpers
{
    using System;
    using System.Runtime.InteropServices;

    public abstract class ClipboardDataProviderOptions
    {
        private bool copyColumnHeaders = true;
        private bool copyCollapsedData = true;

        public ClipboardDataProviderOptions(bool copyColumnHeaders = true, bool copyCollapsedData = true)
        {
            this.copyColumnHeaders = copyColumnHeaders;
            this.copyCollapsedData = copyCollapsedData;
        }

        public bool CopyColumnHeaders
        {
            get => 
                this.copyColumnHeaders;
            set
            {
                if (this.copyColumnHeaders != value)
                {
                    this.copyColumnHeaders = value;
                }
            }
        }

        public bool CopyCollapsedData
        {
            get => 
                this.copyCollapsedData;
            set
            {
                if (this.copyCollapsedData != value)
                {
                    this.copyCollapsedData = value;
                }
            }
        }
    }
}

