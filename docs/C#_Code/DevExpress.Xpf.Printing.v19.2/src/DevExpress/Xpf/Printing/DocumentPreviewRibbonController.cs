namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Ribbon;
    using System;
    using System.Windows;

    public class DocumentPreviewRibbonController : TemplatedRibbonController
    {
        public static readonly DependencyProperty DocumentViewerProperty = DependencyPropertyManager.Register("DocumentViewer", typeof(DevExpress.Xpf.Printing.DocumentViewer), typeof(DocumentPreviewRibbonController), new UIPropertyMetadata(null, new PropertyChangedCallback(DocumentPreviewRibbonController.OnDocumentViewerPropertyChanged)));

        static DocumentPreviewRibbonController()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DocumentPreviewRibbonController), new FrameworkPropertyMetadata(typeof(DocumentPreviewRibbonController)));
        }

        public DocumentPreviewRibbonController()
        {
            base.ApplyTemplate();
        }

        private void OnDocumentViewerChanged(object previewControl)
        {
            RibbonControl ribbon = RibbonControl.GetRibbon(this);
            if (ribbon != null)
            {
                ribbon.SetValue(DevExpress.Xpf.Printing.DocumentViewer.DocumentViewerProperty, previewControl);
            }
        }

        private static void OnDocumentViewerPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((DocumentPreviewRibbonController) sender).OnDocumentViewerChanged(e.NewValue);
        }

        public DevExpress.Xpf.Printing.DocumentViewer DocumentViewer
        {
            get => 
                (DevExpress.Xpf.Printing.DocumentViewer) base.GetValue(DocumentViewerProperty);
            set => 
                base.SetValue(DocumentViewerProperty, value);
        }
    }
}

