namespace DevExpress.Xpf.Core
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class DialogControl : UserControl, IComponentConnector
    {
        public static readonly DependencyProperty DialogContentProperty = DependencyProperty.Register("DialogContent", typeof(FrameworkElement), typeof(DialogControl), new UIPropertyMetadata(null));
        public static readonly DependencyProperty UseContentIndentsProperty = DependencyProperty.Register("UseContentIndents", typeof(bool), typeof(DialogControl), new PropertyMetadata(false));
        public static readonly DependencyProperty ShowApplyButtonProperty = DependencyProperty.Register("ShowApplyButton", typeof(bool), typeof(DialogControl), new PropertyMetadata(false));
        internal DialogControl root;
        public Button OkButton;
        public Button CancelButton;
        public Button ApplyButton;
        private bool _contentLoaded;

        public DialogControl()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void applyButton_Click(object sender, RoutedEventArgs e)
        {
            this.IDialogContent.OnApply();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            FloatingContainer.CloseDialog(this, false);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Core.v19.2;component/core/frameworkelements/dialogcontrol.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.IDialogContent != null)
            {
                if (!this.IDialogContent.CanCloseWithOKResult())
                {
                    return;
                }
                this.IDialogContent.OnOk();
            }
            FloatingContainer.CloseDialog(this, true);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.root = (DialogControl) target;
                    return;

                case 2:
                    this.OkButton = (Button) target;
                    this.OkButton.Click += new RoutedEventHandler(this.okButton_Click);
                    return;

                case 3:
                    this.CancelButton = (Button) target;
                    this.CancelButton.Click += new RoutedEventHandler(this.cancelButton_Click);
                    return;

                case 4:
                    this.ApplyButton = (Button) target;
                    this.ApplyButton.Click += new RoutedEventHandler(this.applyButton_Click);
                    return;
            }
            this._contentLoaded = true;
        }

        public FrameworkElement DialogContent
        {
            get => 
                (FrameworkElement) base.GetValue(DialogContentProperty);
            set => 
                base.SetValue(DialogContentProperty, value);
        }

        public bool ShowApplyButton
        {
            get => 
                (bool) base.GetValue(ShowApplyButtonProperty);
            set => 
                base.SetValue(ShowApplyButtonProperty, value);
        }

        public bool UseContentIndents
        {
            get => 
                (bool) base.GetValue(UseContentIndentsProperty);
            set => 
                base.SetValue(UseContentIndentsProperty, value);
        }

        private DevExpress.Xpf.Core.IDialogContent IDialogContent =>
            this.DialogContent as DevExpress.Xpf.Core.IDialogContent;
    }
}

