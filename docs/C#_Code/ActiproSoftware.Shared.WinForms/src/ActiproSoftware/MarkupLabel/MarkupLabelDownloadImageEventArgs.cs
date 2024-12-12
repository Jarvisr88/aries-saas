namespace ActiproSoftware.MarkupLabel
{
    using System;
    using System.Drawing;

    public class MarkupLabelDownloadImageEventArgs : EventArgs
    {
        private MarkupLabelImageElement #irb;
        private System.Drawing.Image #M0d;

        public MarkupLabelDownloadImageEventArgs(MarkupLabelImageElement element)
        {
            this.#irb = element;
        }

        public MarkupLabelImageElement Element =>
            this.#irb;

        public System.Drawing.Image Image
        {
            get => 
                this.#M0d;
            set => 
                this.#M0d = value;
        }
    }
}

