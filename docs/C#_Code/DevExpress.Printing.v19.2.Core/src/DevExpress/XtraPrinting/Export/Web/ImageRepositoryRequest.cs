namespace DevExpress.XtraPrinting.Export.Web
{
    using System;
    using System.Drawing;

    public abstract class ImageRepositoryRequest
    {
        private ImageEventHandler onRequestImageSource;

        public virtual event ImageEventHandler RequestImageSource
        {
            add
            {
                this.onRequestImageSource = (this.onRequestImageSource + value) as ImageEventHandler;
            }
            remove
            {
                this.onRequestImageSource = (this.onRequestImageSource - value) as ImageEventHandler;
            }
        }

        protected ImageRepositoryRequest()
        {
        }

        protected void RaiseRequestImageSource(Image image)
        {
            if (this.onRequestImageSource != null)
            {
                this.onRequestImageSource(this, new ImageEventArgs(image));
            }
        }
    }
}

