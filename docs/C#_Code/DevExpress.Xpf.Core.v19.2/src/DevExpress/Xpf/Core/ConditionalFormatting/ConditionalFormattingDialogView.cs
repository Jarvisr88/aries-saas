namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class ConditionalFormattingDialogView : UserControl, IComponentConnector
    {
        internal Label descriptionLabel;
        internal ContentPresenter editValuePresenter;
        internal Label connectorLabel;
        internal ComboBoxEdit formatsComboBox;
        internal DialogService customFormatService;
        internal CheckEdit applyFormatToWholeRowCheckEdit;
        private bool _contentLoaded;

        public ConditionalFormattingDialogView()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/core/conditionalformatting/dialogs/views/conditionalformattingdialogview.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.descriptionLabel = (Label) target;
                    return;

                case 2:
                    this.editValuePresenter = (ContentPresenter) target;
                    return;

                case 3:
                    this.connectorLabel = (Label) target;
                    return;

                case 4:
                    this.formatsComboBox = (ComboBoxEdit) target;
                    return;

                case 5:
                    this.customFormatService = (DialogService) target;
                    return;

                case 6:
                    this.applyFormatToWholeRowCheckEdit = (CheckEdit) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

