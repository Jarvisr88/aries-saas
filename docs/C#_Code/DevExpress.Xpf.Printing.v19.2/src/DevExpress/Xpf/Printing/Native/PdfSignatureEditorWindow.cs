namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Markup;

    public class PdfSignatureEditorWindow : DXDialog, IPdfSignatureEditorView, IComponentConnector
    {
        internal ListBoxEdit listBoxEdit;
        internal TextEdit teReason;
        internal TextEdit teLocation;
        internal TextEdit teContactInformation;
        private bool _contentLoaded;

        public event EventHandler SelectedCertificateItemChanged;

        public event EventHandler Submit;

        public PdfSignatureEditorWindow()
        {
            this.InitializeComponent();
            this.listBoxEdit.SelectedIndexChanged += delegate (object o, RoutedEventArgs e) {
                if (this.SelectedCertificateItemChanged != null)
                {
                    this.SelectedCertificateItemChanged(this, EventArgs.Empty);
                }
            };
        }

        public void EnableTextEditors(bool enable)
        {
            bool flag2;
            this.teContactInformation.IsEnabled = flag2 = enable;
            this.teReason.IsEnabled = this.teLocation.IsEnabled = flag2;
        }

        public void FillCertificateItems(IEnumerable<ICertificateItem> items)
        {
            this.listBoxEdit.ItemsSource = items;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/native/pdfsignatureeditorwindow.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            base.OkButton.Click += delegate (object o, RoutedEventArgs e) {
                if (this.Submit != null)
                {
                    this.Submit(this, EventArgs.Empty);
                }
            };
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.listBoxEdit = (ListBoxEdit) target;
                    return;

                case 2:
                    this.teReason = (TextEdit) target;
                    return;

                case 3:
                    this.teLocation = (TextEdit) target;
                    return;

                case 4:
                    this.teContactInformation = (TextEdit) target;
                    return;
            }
            this._contentLoaded = true;
        }

        public string Reason
        {
            get => 
                this.teReason.Text;
            set => 
                this.teReason.Text = value;
        }

        public string Location
        {
            get => 
                this.teLocation.Text;
            set => 
                this.teLocation.Text = value;
        }

        public string ContactInfo
        {
            get => 
                this.teContactInformation.Text;
            set => 
                this.teContactInformation.Text = value;
        }

        public ICertificateItem SelectedCertificateItem
        {
            get => 
                this.listBoxEdit.EditValue as ICertificateItem;
            set => 
                this.listBoxEdit.EditValue = value;
        }
    }
}

