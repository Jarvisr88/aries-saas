namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;

    public class AdornedMarkerDisplayer : IDropMarkerDisplayer
    {
        private readonly object model;
        private readonly Func<DataTemplate> getMarkerTemplate;
        private Adorner adorner;
        private AdornerLayer layer;
        private UIElement currentAdornedElement;

        public AdornedMarkerDisplayer(object model, Func<DataTemplate> getMarkerTemplate)
        {
            Guard.ArgumentNotNull(model, "model");
            Guard.ArgumentNotNull(getMarkerTemplate, "getMarkerTemplate");
            this.model = model;
            this.getMarkerTemplate = getMarkerTemplate;
        }

        public void Hide()
        {
            if ((this.adorner != null) && (this.layer != null))
            {
                this.layer.Remove(this.adorner);
            }
            this.adorner = null;
            this.layer = null;
            this.currentAdornedElement = null;
        }

        public void Show(UIElement adornedElement)
        {
            if (!ReferenceEquals(this.currentAdornedElement, adornedElement))
            {
                this.Hide();
                DataTemplate template = this.getMarkerTemplate();
                if (template != null)
                {
                    this.currentAdornedElement = adornedElement;
                    ContentControl control1 = new ContentControl();
                    control1.ContentTemplate = template;
                    control1.Content = this.Model;
                    ContentControl child = control1;
                    this.adorner = new AdornerContainer(adornedElement, child);
                    this.layer = AdornerLayer.GetAdornerLayer(adornedElement);
                    this.layer.Add(this.adorner);
                }
            }
        }

        public object Model =>
            this.model;
    }
}

