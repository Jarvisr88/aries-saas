namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Validation;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class ScaleWindow : DXDialog, IComponentConnector
    {
        internal Grid LayoutRoot;
        internal ComboBoxEdit cmbBxScaleFactor;
        internal ComboBoxEdit cmbBxPagesToFit;
        private bool _contentLoaded;

        public ScaleWindow(float scaleFactor, int pagesToFit)
        {
            this.InitializeComponent();
            ScaleWindowViewModel model = new ScaleWindowViewModel(scaleFactor, pagesToFit);
            this.ViewModel = model;
            base.Loaded += new RoutedEventHandler(this.ScaleWindow_Loaded);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(System.Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void cmbBxPagesToFit_Validate(object sender, ValidationEventArgs e)
        {
            FinishValidation(e, this.ViewModel.ValidatePagesToFit(e.Value));
        }

        private void cmbBxScaleFactor_Validate(object sender, ValidationEventArgs e)
        {
            FinishValidation(e, this.ViewModel.ValidateScaleFactor(e.Value));
        }

        private static void FinishValidation(ValidationEventArgs e, DevExpress.Xpf.Printing.Native.ValidationResult validationResult)
        {
            e.IsValid = validationResult.IsValid;
            if (!e.IsValid)
            {
                e.ErrorContent = validationResult.ErrorMessage;
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/native/scalewindow.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void ScaleWindow_Loaded(object sender, RoutedEventArgs e)
        {
            base.OkButton.Command = this.ViewModel.ApplyCommand;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 2:
                    this.cmbBxScaleFactor = (ComboBoxEdit) target;
                    this.cmbBxScaleFactor.Validate += new ValidateEventHandler(this.cmbBxScaleFactor_Validate);
                    return;

                case 3:
                    this.cmbBxPagesToFit = (ComboBoxEdit) target;
                    this.cmbBxPagesToFit.Validate += new ValidateEventHandler(this.cmbBxPagesToFit_Validate);
                    return;
            }
            this._contentLoaded = true;
        }

        public ScaleWindowViewModel ViewModel
        {
            get => 
                this.LayoutRoot.DataContext as ScaleWindowViewModel;
            set => 
                this.LayoutRoot.DataContext = value;
        }
    }
}

