namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.Xpf.Printing.Parameters;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    [Obsolete("The DocumentPreview and corresponding xxxPreviewModel classes and interfaces are now obsolete. Use the DocumentPreviewControl class to display a print preview. More information: https://www.devexpress.com/Support/WhatsNew/DXperience/files/16.1.2.bc.xml#BC3444"), DXToolboxBrowsable(false), LicenseProvider(typeof(DX_WPF_ControlRequiredForReports_LicenseProvider))]
    public class DocumentViewer : Control
    {
        private IDialogService originalDialogService;
        public static readonly DependencyProperty ModelProperty = DependencyProperty.Register("Model", typeof(IPreviewModel), typeof(DevExpress.Xpf.Printing.DocumentViewer), new PropertyMetadata(null, new PropertyChangedCallback(DevExpress.Xpf.Printing.DocumentViewer.ModelChangedCallback)));
        public static readonly DependencyProperty DocumentViewerProperty = DependencyProperty.RegisterAttached("DocumentViewer", typeof(DevExpress.Xpf.Printing.DocumentViewer), typeof(DevExpress.Xpf.Printing.DocumentViewer), new PropertyMetadata(null));

        static DocumentViewer()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DevExpress.Xpf.Printing.DocumentViewer), new FrameworkPropertyMetadata(typeof(DevExpress.Xpf.Printing.DocumentViewer)));
            About.CheckLicenseShowNagScreen(typeof(DevExpress.Xpf.Printing.DocumentViewer));
        }

        public DocumentViewer()
        {
            base.FocusVisualStyle = null;
        }

        public static DevExpress.Xpf.Printing.DocumentViewer GetDocumentViewer(DependencyObject obj) => 
            (DevExpress.Xpf.Printing.DocumentViewer) obj.GetValue(DocumentViewerProperty);

        private static void ModelChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DevExpress.Xpf.Printing.DocumentViewer) d).OnModelChanged((IPreviewModel) e.OldValue, (IPreviewModel) e.NewValue);
        }

        private void OnModelChanged(IPreviewModel oldModel, IPreviewModel newModel)
        {
            if (oldModel != null)
            {
                oldModel.DialogService = this.originalDialogService;
                this.originalDialogService = null;
            }
            if (newModel != null)
            {
                this.originalDialogService = this.Model.DialogService;
                this.Model.DialogService = new DevExpress.Xpf.Printing.DialogService(this);
                if (this.CursorContainer != null)
                {
                    this.Model.CursorService = new CursorService(this.CursorContainer);
                }
            }
        }

        public static void SetDocumentViewer(DependencyObject obj, DocumentPreview value)
        {
            obj.SetValue(DocumentViewerProperty, value);
        }

        [Description("Specifies the model for the Document Preview. This is a dependency property.")]
        public IPreviewModel Model
        {
            get => 
                (IPreviewModel) base.GetValue(ModelProperty);
            set => 
                base.SetValue(ModelProperty, value);
        }

        private DevExpress.Xpf.Printing.Native.CursorContainer CursorContainer =>
            (DevExpress.Xpf.Printing.Native.CursorContainer) base.GetTemplateChild("previewCursorContainer");

        private DevExpress.Xpf.Docking.DockLayoutManager DockLayoutManager =>
            (DevExpress.Xpf.Docking.DockLayoutManager) base.GetTemplateChild("documentPreviewDockLayoutManager");

        public DevExpress.Xpf.Printing.Parameters.ParametersPanel ParametersPanel =>
            base.GetTemplateChild("parametersPanel") as DevExpress.Xpf.Printing.Parameters.ParametersPanel;
    }
}

