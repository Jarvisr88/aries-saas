namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.XtraPrinting;
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

    public class PdfPasswordSecurityOptionsView : DXDialog, IPdfPasswordSecurityOptionsView, IComponentConnector
    {
        internal PdfSecurityOptionsControl SecurityOptionsControl;
        private bool _contentLoaded;

        public event EventHandler Dismiss;

        public event EventHandler<RepeatPasswordCompleteEventArgs> RepeatPasswordComplete;

        public event EventHandler RequireOpenPasswordChanged;

        public event EventHandler RestrictPermissionsChanged;

        public event EventHandler Submit;

        public PdfPasswordSecurityOptionsView()
        {
            this.InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.PdfPasswordSecurityOptionsView_Loaded);
        }

        void IPdfPasswordSecurityOptionsView.Close()
        {
        }

        public void EnableControl_OpenPassword(bool enable)
        {
            this.SecurityOptionsControl.EnableControl_OpenPassword(enable);
        }

        public void EnableControlGroup_Permissions(bool enable)
        {
            this.SecurityOptionsControl.EnableControlGroup_Permissions(enable);
        }

        public void InitializeChangingPermissions(IEnumerable<KeyValuePair<DevExpress.XtraPrinting.ChangingPermissions, string>> availablePermissions)
        {
            this.SecurityOptionsControl.InitializeChangingPermissions(availablePermissions);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Xpf.Printing.v19.2;component/native/pdfpasswordsecurityoptionsview.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void InitializePdfEncryptionLevel(IEnumerable<KeyValuePair<PdfEncryptionLevel, string>> availablePermissions)
        {
            this.SecurityOptionsControl.InitializePdfEncryptionLevel(availablePermissions);
        }

        public void InitializePrintingPermissions(IEnumerable<KeyValuePair<DevExpress.XtraPrinting.PrintingPermissions, string>> availablePermissions)
        {
            this.SecurityOptionsControl.InitializePrintingPermissions(availablePermissions);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Subscribe();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            bool? dialogResult = base.DialogResult;
            bool flag = true;
            if ((dialogResult.GetValueOrDefault() == flag) ? (dialogResult != null) : false)
            {
                if (this.Submit == null)
                {
                    EventHandler submit = this.Submit;
                }
                else
                {
                    this.Submit(this, EventArgs.Empty);
                }
            }
            else if (this.Dismiss == null)
            {
                EventHandler dismiss = this.Dismiss;
            }
            else
            {
                this.Dismiss(this, EventArgs.Empty);
            }
        }

        public void PasswordDoesNotMatch()
        {
            this.SecurityOptionsControl.PasswordDoesNotMatch();
        }

        private void PdfPasswordSecurityOptionsView_Loaded(object sender, RoutedEventArgs e)
        {
            this.SubscribeSecurityOptionsControl();
        }

        public void RepeatOpenPassword()
        {
            this.SecurityOptionsControl.RepeatOpenPassword();
        }

        public void RepeatPassword(string caption, string note, string passwordName)
        {
            this.SecurityOptionsControl.RepeatPassword(caption, note, passwordName);
        }

        public void RepeatPermissionsPassword()
        {
            this.SecurityOptionsControl.RepeatPermissionsPassword();
        }

        private void SecurityOptionsControl_RepeatPasswordComplete(object sender, RepeatPasswordCompleteEventArgs e)
        {
            if (this.RepeatPasswordComplete != null)
            {
                this.RepeatPasswordComplete(this, e);
            }
        }

        private void SecurityOptionsControl_RequireOpenPasswordChanged(object sender, EventArgs e)
        {
            if (this.RequireOpenPasswordChanged != null)
            {
                this.RequireOpenPasswordChanged(this, EventArgs.Empty);
            }
        }

        private void SecurityOptionsControl_RestrictPermissionsChanged(object sender, EventArgs e)
        {
            if (this.RestrictPermissionsChanged != null)
            {
                this.RestrictPermissionsChanged(this, EventArgs.Empty);
            }
        }

        private void Subscribe()
        {
            base.Closing += new CancelEventHandler(this.OnClosing);
        }

        private void SubscribeSecurityOptionsControl()
        {
            this.SecurityOptionsControl.RequireOpenPasswordChanged += new EventHandler(this.SecurityOptionsControl_RequireOpenPasswordChanged);
            this.SecurityOptionsControl.RestrictPermissionsChanged += new EventHandler(this.SecurityOptionsControl_RestrictPermissionsChanged);
            this.SecurityOptionsControl.RepeatPasswordComplete += new EventHandler<RepeatPasswordCompleteEventArgs>(this.SecurityOptionsControl_RepeatPasswordComplete);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                this.SecurityOptionsControl = (PdfSecurityOptionsControl) target;
            }
            else
            {
                this._contentLoaded = true;
            }
        }

        public bool RequireOpenPassword
        {
            get => 
                this.SecurityOptionsControl.RequireOpenPassword;
            set => 
                this.SecurityOptionsControl.RequireOpenPassword = value;
        }

        public string OpenPassword
        {
            get => 
                this.SecurityOptionsControl.OpenPassword;
            set => 
                this.SecurityOptionsControl.OpenPassword = value;
        }

        public bool RestrictPermissions
        {
            get => 
                this.SecurityOptionsControl.RestrictPermissions;
            set => 
                this.SecurityOptionsControl.RestrictPermissions = value;
        }

        public string PermissionsPassword
        {
            get => 
                this.SecurityOptionsControl.PermissionsPassword;
            set => 
                this.SecurityOptionsControl.PermissionsPassword = value;
        }

        public DevExpress.XtraPrinting.PrintingPermissions PrintingPermissions
        {
            get => 
                this.SecurityOptionsControl.PrintingPermissions;
            set => 
                this.SecurityOptionsControl.PrintingPermissions = value;
        }

        public DevExpress.XtraPrinting.ChangingPermissions ChangingPermissions
        {
            get => 
                this.SecurityOptionsControl.ChangingPermissions;
            set => 
                this.SecurityOptionsControl.ChangingPermissions = value;
        }

        public bool EnableCopying
        {
            get => 
                this.SecurityOptionsControl.EnableCopying;
            set => 
                this.SecurityOptionsControl.EnableCopying = value;
        }

        public bool EnableScreenReaders
        {
            get => 
                this.SecurityOptionsControl.EnableScreenReaders;
            set => 
                this.SecurityOptionsControl.EnableScreenReaders = value;
        }

        public PdfEncryptionLevel EncryptionLevel
        {
            get => 
                this.SecurityOptionsControl.EncryptionLevel;
            set => 
                this.SecurityOptionsControl.EncryptionLevel = value;
        }
    }
}

