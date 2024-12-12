namespace DevExpress.Xpf.PdfViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Media.Imaging;

    public class PdfSelectionContent
    {
        internal PdfSelectionContent(PdfSelectionContentType contentType, BitmapSource imageSource, string text)
        {
            this.ContentType = contentType;
            this.Image = imageSource;
            this.Text = text;
        }

        public PdfSelectionContentType ContentType { get; private set; }

        public BitmapSource Image { get; private set; }

        public string Text { get; private set; }
    }
}

