namespace DevExpress.XtraExport
{
    using System;

    public class ProviderProgressEventArgs : EventArgs
    {
        private int position;

        public ProviderProgressEventArgs(int position)
        {
            this.position = position;
        }

        public int Position =>
            this.position;
    }
}

