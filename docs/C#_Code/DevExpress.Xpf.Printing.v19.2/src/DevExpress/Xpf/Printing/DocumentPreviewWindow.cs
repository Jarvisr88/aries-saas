namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Core;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;

    public class DocumentPreviewWindow : ThemedWindow, IComponentConnector
    {
        internal DocumentPreviewControl previewControl;
        private bool _contentLoaded;

        public DocumentPreviewWindow()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/documentpreviewwindow.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                this.previewControl = (DocumentPreviewControl) target;
            }
            else
            {
                this._contentLoaded = true;
            }
        }

        public DocumentPreviewControl PreviewControl =>
            this.previewControl;

        [Obsolete("The DocumentPreviewWindow.Model property is no longer available. More information: https://www.devexpress.com/Support/WhatsNew/DXperience/files/16.1.2.bc.xml#BC3444", true)]
        public IDocumentPreviewModel Model { get; set; }
    }
}

