namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Documents;

    public class PdfViewerSplashScreenService : ViewServiceBase, ISplashScreenService
    {
        private PositionedAdornerContainer adornerContainer;
        private AdornerLayer adornerLayer;
        private LoadingProgressControl loadingProgressControl;

        void ISplashScreenService.HideSplashScreen()
        {
            if (this.IsSplashScreenActive)
            {
                this.adornerLayer.Do<AdornerLayer>(x => x.Remove(this.adornerContainer));
                this.IsSplashScreenActive = false;
            }
        }

        void ISplashScreenService.SetSplashScreenProgress(double progress, double maxProgress)
        {
            this.loadingProgressControl.TotalProgress = maxProgress;
            this.loadingProgressControl.Progress = progress;
        }

        void ISplashScreenService.SetSplashScreenState(object state)
        {
            bool isSplashScreenActive = this.IsSplashScreenActive;
        }

        void ISplashScreenService.ShowSplashScreen(string documentType)
        {
            if (!this.IsSplashScreenActive)
            {
                this.adornerLayer = AdornerLayer.GetAdornerLayer(base.AssociatedObject);
                this.adornerLayer.Do<AdornerLayer>(x => x.Add(this.adornerContainer));
                this.UpdateAdornerContainerPosition();
                this.IsSplashScreenActive = true;
            }
        }

        private void OnAssociatedObjectSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateAdornerContainerPosition();
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            LoadingProgressControl control1 = new LoadingProgressControl();
            control1.Width = 200.0;
            this.loadingProgressControl = control1;
            this.loadingProgressControl.SizeChanged += new SizeChangedEventHandler(this.OnLoadingProgressControlSizeChanged);
            this.adornerContainer = new PositionedAdornerContainer(base.AssociatedObject, this.loadingProgressControl);
            base.AssociatedObject.SizeChanged += new SizeChangedEventHandler(this.OnAssociatedObjectSizeChanged);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (this.IsSplashScreenActive)
            {
                this.adornerLayer.Do<AdornerLayer>(x => x.Remove(this.adornerContainer));
            }
            this.loadingProgressControl.SizeChanged -= new SizeChangedEventHandler(this.OnLoadingProgressControlSizeChanged);
            base.AssociatedObject.SizeChanged -= new SizeChangedEventHandler(this.OnAssociatedObjectSizeChanged);
            this.adornerLayer = null;
        }

        private void OnLoadingProgressControlSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateAdornerContainerPosition();
        }

        private void UpdateAdornerContainerPosition()
        {
            if ((this.adornerContainer != null) && (this.adornerLayer != null))
            {
                this.adornerContainer.UpdateLocation(new Point((base.AssociatedObject.ActualWidth / 2.0) - (this.adornerContainer.DesiredSize.Width / 2.0), (base.AssociatedObject.ActualHeight / 2.0) - (this.adornerContainer.DesiredSize.Height / 2.0)));
            }
        }

        public bool IsSplashScreenActive { get; private set; }
    }
}

