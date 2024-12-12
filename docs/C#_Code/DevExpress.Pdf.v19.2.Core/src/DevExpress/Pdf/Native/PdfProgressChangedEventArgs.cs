namespace DevExpress.Pdf.Native
{
    using System;

    public class PdfProgressChangedEventArgs : EventArgs
    {
        private readonly int progressValue;

        public PdfProgressChangedEventArgs(int progressValue)
        {
            this.progressValue = progressValue;
        }

        public int ProgressValue =>
            this.progressValue;
    }
}

