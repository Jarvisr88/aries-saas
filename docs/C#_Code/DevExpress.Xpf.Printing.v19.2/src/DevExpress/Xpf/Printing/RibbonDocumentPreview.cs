namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf;
    using DevExpress.Xpf.Bars;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Ribbon;
    using DevExpress.Xpf.Utils;
    using DevExpress.Xpf.Utils.About;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Data;

    [Obsolete("The RibbonDocumentPreview and corresponding xxxPreviewModel classes and interfaces are now obsolete. Use the DocumentPreviewControl class to display a print preview. More information: https://www.devexpress.com/Support/WhatsNew/DXperience/files/16.1.2.bc.xml#BC3444"), DXToolboxBrowsable(false), LicenseProvider(typeof(DX_WPF_ControlRequiredForReports_LicenseProvider))]
    public class RibbonDocumentPreview : DocumentPreviewBase
    {
        public static readonly DependencyProperty ModelProperty = DependencyPropertyManager.Register("Model", typeof(IDocumentPreviewModel), typeof(RibbonDocumentPreview), new PropertyMetadata(null));
        private static readonly DependencyPropertyKey DocumentViewerPropertyKey = DependencyPropertyManager.RegisterReadOnly("DocumentViewer", typeof(DevExpress.Xpf.Printing.DocumentViewer), typeof(RibbonDocumentPreview), new FrameworkPropertyMetadata(null));
        public static readonly DependencyProperty DocumentViewerProperty = DocumentViewerPropertyKey.DependencyProperty;

        static RibbonDocumentPreview()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(RibbonDocumentPreview), new FrameworkPropertyMetadata(typeof(RibbonDocumentPreview)));
            About.CheckLicenseShowNagScreen(typeof(RibbonDocumentPreview));
            DevExpress.Xpf.Bars.BarManager.CheckBarItemNames = false;
        }

        public RibbonDocumentPreview()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        public override void OnApplyTemplate()
        {
            DocumentPreviewRibbonController controller;
            base.OnApplyTemplate();
            this.DocumentViewer = base.GetTemplateChild("DocumentViewer") as DevExpress.Xpf.Printing.DocumentViewer;
            if ((this.Ribbon != null) && ((controller = (DocumentPreviewRibbonController) this.Ribbon.Controllers[0]) != null))
            {
                Binding binding = new Binding(DocumentViewerProperty.GetName());
                binding.Source = this;
                binding.Mode = BindingMode.OneWay;
                BindingOperations.SetBinding(controller, DocumentPreviewRibbonController.DocumentViewerProperty, binding);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (RibbonCustomization.GetTemplate(this) != null)
            {
                RibbonCustomization.ApplyTemplate(this);
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

        [Description("Provides access to the RibbonControl object associated with the RibbonDocumentPreview.")]
        public RibbonControl Ribbon =>
            (RibbonControl) base.GetTemplateChild("ribbonControl");

        [Description("Provides access to the RibbonStatusBarControl object associated with the RibbonDocumentPreview.")]
        public RibbonStatusBarControl RibbonStatusBar =>
            (RibbonStatusBarControl) base.GetTemplateChild("previewStatusBar");

        [Description("Provides access to the bar manager of the RibbonDocumentPreview.")]
        public DevExpress.Xpf.Bars.BarManager BarManager =>
            (DevExpress.Xpf.Bars.BarManager) base.GetTemplateChild("barManager");
    }
}

