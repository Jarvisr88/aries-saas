namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;

    [Obsolete("The DocumentPreview and corresponding xxxPreviewModel classes and interfaces are now obsolete. Use the DocumentPreviewControl class to display a print preview. More information: https://www.devexpress.com/Support/WhatsNew/DXperience/files/16.1.2.bc.xml#BC3444"), DXToolboxBrowsable(false)]
    public class DocumentPreview : DocumentPreviewBase
    {
        public static readonly DependencyProperty ModelProperty = DependencyPropertyManager.Register("Model", typeof(IDocumentPreviewModel), typeof(DocumentPreview), new PropertyMetadata(null));
        private static readonly DependencyPropertyKey DocumentViewerPropertyKey = DependencyPropertyManager.RegisterReadOnly("DocumentViewer", typeof(DevExpress.Xpf.Printing.DocumentViewer), typeof(DocumentPreview), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty DocumentViewerProperty = DocumentViewerPropertyKey.DependencyProperty;

        static DocumentPreview()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DocumentPreview), new FrameworkPropertyMetadata(typeof(DocumentPreview)));
            DevExpress.Xpf.Bars.BarManager.CheckBarItemNames = false;
        }

        public override void OnApplyTemplate()
        {
            DocumentPreviewBarManagerController controller;
            base.OnApplyTemplate();
            this.DocumentViewer = base.GetTemplateChild("DocumentViewer") as DevExpress.Xpf.Printing.DocumentViewer;
            if ((this.BarManager != null) && ((controller = this.BarManager.Controllers.OfType<DocumentPreviewBarManagerController>().FirstOrDefault<DocumentPreviewBarManagerController>()) != null))
            {
                Binding binding = new Binding(DocumentViewerProperty.GetName());
                binding.Source = this;
                binding.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(controller, DocumentPreviewBarManagerController.DocumentViewerProperty, binding);
                if (BarManagerCustomization.GetTemplate(this) != null)
                {
                    BarManagerCustomization.ApplyTemplate(this);
                }
            }
        }

        [Description("Specifies the model for the Document Preview.")]
        public override IDocumentPreviewModel Model
        {
            get => 
                (IDocumentPreviewModel) base.GetValue(ModelProperty);
            set => 
                base.SetValue(ModelProperty, value);
        }

        [Description("Provides access to the associated Document Viewer visual control. This is a dependency property.")]
        public override DevExpress.Xpf.Printing.DocumentViewer DocumentViewer
        {
            get => 
                (DevExpress.Xpf.Printing.DocumentViewer) base.GetValue(DocumentViewerProperty);
            protected set => 
                base.SetValue(DocumentViewerPropertyKey, value);
        }

        [Description("Provides access to the bar manager of the RibbonDocumentPreview.")]
        public DevExpress.Xpf.Bars.BarManager BarManager =>
            (DevExpress.Xpf.Bars.BarManager) base.GetTemplateChild("barManager");
    }
}

