namespace DevExpress.Xpf.Printing
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DocumentPreviewBarManagerController : TemplatedBarManagerController
    {
        public static readonly DependencyProperty DocumentViewerProperty = DependencyPropertyManager.Register("DocumentViewer", typeof(DevExpress.Xpf.Printing.DocumentViewer), typeof(DocumentPreviewBarManagerController), new UIPropertyMetadata(null, new PropertyChangedCallback(DocumentPreviewBarManagerController.OnDocumentViewerPropertyChanged)));

        static DocumentPreviewBarManagerController()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DocumentPreviewBarManagerController), new FrameworkPropertyMetadata(typeof(DocumentPreviewBarManagerController)));
        }

        private void OnDocumentViewerChanged(DevExpress.Xpf.Printing.DocumentViewer previewControl)
        {
            BarManager local1 = previewControl.With<DevExpress.Xpf.Printing.DocumentViewer, BarManager>(new Func<DevExpress.Xpf.Printing.DocumentViewer, BarManager>(BarManager.GetBarManager));
            BarManager manager2 = local1;
            if (local1 == null)
            {
                BarManager local2 = local1;
                BarManager barManager = BarManager.GetBarManager(this);
                manager2 = barManager;
                if (barManager == null)
                {
                    BarManager local3 = barManager;
                    manager2 = this.AssociatedObject.With<DependencyObject, BarManager>(new Func<DependencyObject, BarManager>(BarManager.GetBarManager));
                }
            }
            BarManager manager = manager2;
            if (manager != null)
            {
                manager.SetValue(DevExpress.Xpf.Printing.DocumentViewer.DocumentViewerProperty, previewControl);
            }
        }

        private static void OnDocumentViewerPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((DocumentPreviewBarManagerController) sender).OnDocumentViewerChanged((DevExpress.Xpf.Printing.DocumentViewer) e.NewValue);
        }

        public DevExpress.Xpf.Printing.DocumentViewer DocumentViewer
        {
            get => 
                (DevExpress.Xpf.Printing.DocumentViewer) base.GetValue(DocumentViewerProperty);
            set => 
                base.SetValue(DocumentViewerProperty, value);
        }

        public string ToolBarContainerName { get; set; }
    }
}

