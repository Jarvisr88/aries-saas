namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;

    public class PdfSecurityOptionsControl : Control, INotifyPropertyChanged
    {
        private bool openPasswordEnabled;
        private bool requireOpenPassword;
        private bool restrictPermissions;
        private bool enableScreenReaders;
        private DevExpress.XtraPrinting.PrintingPermissions printingPermissions;
        private DevExpress.XtraPrinting.ChangingPermissions changingPermissions;
        private PdfEncryptionLevel pdfEncryptionLevel;
        private IEnumerable<KeyValuePair<DevExpress.XtraPrinting.ChangingPermissions, string>> changingPermissionsValues;
        private IEnumerable<KeyValuePair<DevExpress.XtraPrinting.PrintingPermissions, string>> printingPermissionsValues;
        private IEnumerable<KeyValuePair<PdfEncryptionLevel, string>> pdfEncryptionLevelValues;
        private string openPassword;
        private string permissionsPassword;
        private bool isPermissionsEnabled;
        private bool enableCopying;

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<RepeatPasswordCompleteEventArgs> RepeatPasswordComplete;

        public event EventHandler RequireOpenPasswordChanged;

        public event EventHandler RestrictPermissionsChanged;

        static PdfSecurityOptionsControl()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PdfSecurityOptionsControl), new FrameworkPropertyMetadata(typeof(PdfSecurityOptionsControl)));
        }

        public PdfSecurityOptionsControl()
        {
            base.DataContext = this;
        }

        public void EnableControl_OpenPassword(bool enable)
        {
            this.OpenPasswordEnabled = enable;
        }

        public void EnableControlGroup_Permissions(bool enable)
        {
            this.IsPermissionsEnabled = enable;
        }

        public void InitializeChangingPermissions(IEnumerable<KeyValuePair<DevExpress.XtraPrinting.ChangingPermissions, string>> availablePermissions)
        {
            this.ChangingPermissionsValues = availablePermissions;
        }

        public void InitializePdfEncryptionLevel(IEnumerable<KeyValuePair<PdfEncryptionLevel, string>> availablePermissions)
        {
            this.PdfEncryptionLevelValues = availablePermissions;
        }

        public void InitializePrintingPermissions(IEnumerable<KeyValuePair<DevExpress.XtraPrinting.PrintingPermissions, string>> availablePermissions)
        {
            this.PrintingPermissionsValues = availablePermissions;
        }

        public void PasswordDoesNotMatch()
        {
            MessageBox.Show(PrintingLocalizer.GetString(PrintingStringId.RepeatPassword_ConfirmationPasswordDoesNotMatch), PrintingLocalizer.GetString(PrintingStringId.PdfPasswordSecurityOptions_Title), MessageBoxButton.OK);
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            this.RaisePropertyChanged<T>(this.PropertyChanged, property);
        }

        public void RepeatOpenPassword()
        {
            this.RepeatPassword(PrintingLocalizer.GetString(PrintingStringId.RepeatPassword_OpenPassword_Title), PrintingLocalizer.GetString(PrintingStringId.RepeatPassword_OpenPassword_Note), PrintingLocalizer.GetString(PrintingStringId.RepeatPassword_OpenPassword));
        }

        public void RepeatPassword(string caption, string note, string passwordName)
        {
            RepeatPasswordWindow window = new RepeatPasswordWindow(caption, note, passwordName) {
                Owner = Window.GetWindow(this),
                FlowDirection = base.FlowDirection,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            string repeatedPassword = null;
            bool? nullable = window.ShowDialog();
            bool flag = true;
            if ((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false)
            {
                repeatedPassword = window.password.Password;
            }
            if (this.RepeatPasswordComplete != null)
            {
                this.RepeatPasswordComplete(this, new RepeatPasswordCompleteEventArgs(repeatedPassword));
            }
        }

        public void RepeatPermissionsPassword()
        {
            this.RepeatPassword(PrintingLocalizer.GetString(PrintingStringId.RepeatPassword_PermissionsPassword_Title), PrintingLocalizer.GetString(PrintingStringId.RepeatPassword_PermissionsPassword_Note), PrintingLocalizer.GetString(PrintingStringId.RepeatPassword_PermissionsPassword));
        }

        public bool RequireOpenPassword
        {
            get => 
                this.requireOpenPassword;
            set
            {
                if (this.requireOpenPassword != value)
                {
                    this.requireOpenPassword = value;
                    if (this.RequireOpenPasswordChanged != null)
                    {
                        this.RequireOpenPasswordChanged(this, EventArgs.Empty);
                    }
                    this.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_RequireOpenPassword)), new ParameterExpression[0]));
                }
            }
        }

        public string OpenPassword
        {
            get => 
                this.openPassword;
            set
            {
                if (this.openPassword != value)
                {
                    this.openPassword = value;
                    this.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_OpenPassword)), new ParameterExpression[0]));
                }
            }
        }

        public bool RestrictPermissions
        {
            get => 
                this.restrictPermissions;
            set
            {
                if (this.restrictPermissions != value)
                {
                    this.restrictPermissions = value;
                    if (!value)
                    {
                        this.PermissionsPassword = string.Empty;
                    }
                    if (this.RestrictPermissionsChanged != null)
                    {
                        this.RestrictPermissionsChanged(this, EventArgs.Empty);
                    }
                    this.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_RestrictPermissions)), new ParameterExpression[0]));
                }
            }
        }

        public string PermissionsPassword
        {
            get => 
                this.permissionsPassword;
            set
            {
                if (this.permissionsPassword != value)
                {
                    this.permissionsPassword = value;
                    if (this.permissionsPassword == string.Empty)
                    {
                        this.PrintingPermissions = DevExpress.XtraPrinting.PrintingPermissions.None;
                        this.ChangingPermissions = DevExpress.XtraPrinting.ChangingPermissions.None;
                        this.EnableCopying = false;
                        this.EnableScreenReaders = false;
                    }
                    this.RaisePropertyChanged<string>(System.Linq.Expressions.Expression.Lambda<Func<string>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_PermissionsPassword)), new ParameterExpression[0]));
                }
            }
        }

        public DevExpress.XtraPrinting.PrintingPermissions PrintingPermissions
        {
            get => 
                this.printingPermissions;
            set
            {
                if (this.printingPermissions != value)
                {
                    this.printingPermissions = value;
                    this.RaisePropertyChanged<DevExpress.XtraPrinting.PrintingPermissions>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.XtraPrinting.PrintingPermissions>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_PrintingPermissions)), new ParameterExpression[0]));
                }
            }
        }

        public DevExpress.XtraPrinting.ChangingPermissions ChangingPermissions
        {
            get => 
                this.changingPermissions;
            set
            {
                if (this.changingPermissions != value)
                {
                    this.changingPermissions = value;
                    this.RaisePropertyChanged<DevExpress.XtraPrinting.ChangingPermissions>(System.Linq.Expressions.Expression.Lambda<Func<DevExpress.XtraPrinting.ChangingPermissions>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_ChangingPermissions)), new ParameterExpression[0]));
                }
            }
        }

        public PdfEncryptionLevel EncryptionLevel
        {
            get => 
                this.pdfEncryptionLevel;
            set
            {
                if (this.pdfEncryptionLevel != value)
                {
                    this.pdfEncryptionLevel = value;
                    this.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_OpenPasswordEnabled)), new ParameterExpression[0]));
                }
            }
        }

        public IEnumerable<KeyValuePair<DevExpress.XtraPrinting.ChangingPermissions, string>> ChangingPermissionsValues
        {
            get => 
                this.changingPermissionsValues;
            set
            {
                if (!ReferenceEquals(this.changingPermissionsValues, value))
                {
                    this.changingPermissionsValues = value;
                    this.RaisePropertyChanged<IEnumerable<KeyValuePair<DevExpress.XtraPrinting.ChangingPermissions, string>>>(System.Linq.Expressions.Expression.Lambda<Func<IEnumerable<KeyValuePair<DevExpress.XtraPrinting.ChangingPermissions, string>>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_ChangingPermissionsValues)), new ParameterExpression[0]));
                }
            }
        }

        public IEnumerable<KeyValuePair<PdfEncryptionLevel, string>> PdfEncryptionLevelValues
        {
            get => 
                this.pdfEncryptionLevelValues;
            set
            {
                if (!ReferenceEquals(this.pdfEncryptionLevelValues, value))
                {
                    this.pdfEncryptionLevelValues = value;
                    this.RaisePropertyChanged<IEnumerable<KeyValuePair<PdfEncryptionLevel, string>>>(System.Linq.Expressions.Expression.Lambda<Func<IEnumerable<KeyValuePair<PdfEncryptionLevel, string>>>>(System.Linq.Expressions.Expression.Field(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), fieldof(PdfSecurityOptionsControl.pdfEncryptionLevelValues)), new ParameterExpression[0]));
                }
            }
        }

        public IEnumerable<KeyValuePair<DevExpress.XtraPrinting.PrintingPermissions, string>> PrintingPermissionsValues
        {
            get => 
                this.printingPermissionsValues;
            set
            {
                if (!ReferenceEquals(this.printingPermissionsValues, value))
                {
                    this.printingPermissionsValues = value;
                    this.RaisePropertyChanged<IEnumerable<KeyValuePair<DevExpress.XtraPrinting.PrintingPermissions, string>>>(System.Linq.Expressions.Expression.Lambda<Func<IEnumerable<KeyValuePair<DevExpress.XtraPrinting.PrintingPermissions, string>>>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_PrintingPermissionsValues)), new ParameterExpression[0]));
                }
            }
        }

        public bool EnableCopying
        {
            get => 
                this.enableCopying;
            set
            {
                if (this.enableCopying != value)
                {
                    this.enableCopying = value;
                    this.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_EnableCopying)), new ParameterExpression[0]));
                }
            }
        }

        public bool EnableScreenReaders
        {
            get => 
                this.enableScreenReaders;
            set
            {
                if (this.enableScreenReaders != value)
                {
                    this.enableScreenReaders = value;
                    this.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_EnableScreenReaders)), new ParameterExpression[0]));
                }
            }
        }

        public bool OpenPasswordEnabled
        {
            get => 
                this.openPasswordEnabled;
            set
            {
                if (this.openPasswordEnabled != value)
                {
                    this.openPasswordEnabled = value;
                    this.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_OpenPasswordEnabled)), new ParameterExpression[0]));
                }
            }
        }

        public bool IsPermissionsEnabled
        {
            get => 
                this.isPermissionsEnabled;
            set
            {
                if (this.isPermissionsEnabled != value)
                {
                    this.isPermissionsEnabled = value;
                    this.RaisePropertyChanged<bool>(System.Linq.Expressions.Expression.Lambda<Func<bool>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(PdfSecurityOptionsControl)), (MethodInfo) methodof(PdfSecurityOptionsControl.get_IsPermissionsEnabled)), new ParameterExpression[0]));
                }
            }
        }
    }
}

