namespace DevExpress.Xpf.Core.Internal
{
    using System;
    using System.Windows;
    using System.Windows.Documents;

    public class BadgeAdornerWorker : BadgeWorkerBase
    {
        private AdornerLayer adornerLayer;
        private BadgeAdorner adorner;

        public BadgeAdornerWorker(FrameworkElement target) : base(target)
        {
            this.UpdateAdorner(true);
        }

        protected override bool CalculateIsVisibleOverride() => 
            this.adornerLayer != null;

        protected override void Destroy()
        {
            PresentationSource.RemoveSourceChangedHandler(base.FrameworkElement, new SourceChangedEventHandler(this.PresentationSourceChanged));
            base.Destroy();
        }

        protected override void HideOverride()
        {
            try
            {
                if ((this.adornerLayer != null) && (this.adorner != null))
                {
                    this.adornerLayer.Remove(this.adorner);
                    this.adorner.Destroy();
                    this.adorner = null;
                }
            }
            finally
            {
                if (base.BadgeControl != null)
                {
                    base.BadgeControl = null;
                }
            }
        }

        private void PresentationSourceChanged(object sender, SourceChangedEventArgs e)
        {
            this.UpdateAdorner(false);
        }

        protected override void ShowOverride()
        {
            if (base.BadgeControl == null)
            {
                base.BadgeControl = base.Badge.CreateControl();
                this.adorner = new BadgeAdorner(base.FrameworkElement, base.BadgeControl);
                this.adornerLayer.Add(this.adorner);
            }
        }

        private void UpdateAdorner(bool addSourceChanged)
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(base.FrameworkElement);
            if (!ReferenceEquals(this.adornerLayer, adornerLayer) || (this.adornerLayer == null))
            {
                this.Hide();
                this.adornerLayer = adornerLayer;
                if (ReferenceEquals(this.adornerLayer, null) & addSourceChanged)
                {
                    PresentationSource.AddSourceChangedHandler(base.FrameworkElement, new SourceChangedEventHandler(this.PresentationSourceChanged));
                }
                else
                {
                    this.Show();
                }
            }
        }
    }
}

