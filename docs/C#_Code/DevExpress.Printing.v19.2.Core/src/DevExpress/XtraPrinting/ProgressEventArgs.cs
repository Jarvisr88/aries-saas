namespace DevExpress.XtraPrinting
{
    using System;

    public class ProgressEventArgs : EventArgs
    {
        private int position;
        private int maximum;

        public ProgressEventArgs(int position, int maximum)
        {
            this.position = position;
            this.maximum = maximum;
        }

        public int Position =>
            this.position;

        public int Maximum =>
            this.maximum;
    }
}

