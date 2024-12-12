namespace DevExpress.Office
{
    using System;

    public abstract class DpiSupport
    {
        private readonly float screenDpiX;
        private readonly float screenDpiY;
        private readonly float screenDpi;

        protected DpiSupport() : this(DocumentModelDpi.DpiX, DocumentModelDpi.DpiY, DocumentModelDpi.Dpi)
        {
        }

        protected DpiSupport(float screenDpiX, float screenDpiY) : this(screenDpiX, screenDpiY, screenDpiX)
        {
        }

        protected DpiSupport(float screenDpiX, float screenDpiY, float screenDpi)
        {
            this.screenDpiX = screenDpiX;
            this.screenDpiY = screenDpiY;
            this.screenDpi = screenDpi;
        }

        public float ScreenDpiX =>
            this.screenDpiX;

        public float ScreenDpiY =>
            this.screenDpiY;

        public float ScreenDpi =>
            this.screenDpi;
    }
}

