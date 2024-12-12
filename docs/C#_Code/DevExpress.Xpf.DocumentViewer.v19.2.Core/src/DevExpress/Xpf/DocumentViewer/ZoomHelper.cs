namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ZoomHelper
    {
        private double CalcViewPageVisibleWidth() => 
            (this.Viewport.IsEmpty || this.PageVisibleSize.IsEmpty) ? 1.0 : (this.Viewport.Width / this.PageVisibleSize.Width);

        protected virtual double CalcViewPageWidth() => 
            (this.Viewport.IsEmpty || this.PageSize.IsEmpty) ? 1.0 : (this.Viewport.Width / this.PageSize.Width);

        protected virtual double CalcViewWholePage() => 
            (this.Viewport.IsEmpty || this.PageSize.IsEmpty) ? 1.0 : Math.Min((double) (this.Viewport.Width / this.PageSize.Width), (double) (this.Viewport.Height / this.PageSize.Height));

        public double CalcZoomFactor(ZoomMode zoomMode) => 
            this.CalcZoomFactor(zoomMode, 1.0);

        public double CalcZoomFactor(ZoomMode zoomMode, double currentZoomFactor)
        {
            switch (zoomMode)
            {
                case ZoomMode.Custom:
                    return currentZoomFactor;

                case ZoomMode.ActualSize:
                    return 1.0;

                case ZoomMode.FitToWidth:
                    return this.CalcViewPageWidth();

                case ZoomMode.FitToVisible:
                    return this.CalcViewPageVisibleWidth();

                case ZoomMode.PageLevel:
                    return this.CalcViewWholePage();
            }
            throw new ArgumentException("zoomMode");
        }

        public Size Viewport { get; set; }

        public Size PageSize { get; set; }

        public Size PageVisibleSize { get; set; }
    }
}

