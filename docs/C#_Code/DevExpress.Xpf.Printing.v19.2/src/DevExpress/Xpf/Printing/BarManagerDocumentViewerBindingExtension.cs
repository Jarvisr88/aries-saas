namespace DevExpress.Xpf.Printing
{
    using System.Windows;

    public class BarManagerDocumentViewerBindingExtension : BarManagerPreviewControlBindingExtensionBase
    {
        protected override DependencyProperty ControlProperty =>
            DocumentViewer.DocumentViewerProperty;
    }
}

