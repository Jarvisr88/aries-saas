namespace DevExpress.Xpf.Editors.Validation
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class BaseValidationError : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
            }
            remove
            {
            }
        }

        public BaseValidationError() : this(null)
        {
        }

        public BaseValidationError(object errorContent) : this(errorContent, null)
        {
        }

        public BaseValidationError(object errorContent, System.Exception exception) : this(errorContent, exception, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical)
        {
        }

        public BaseValidationError(object errorContent, System.Exception exception, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType)
        {
            this.ErrorContent = errorContent;
            this.Exception = exception;
            this.ErrorType = errorType;
        }

        internal bool IsHidden { get; set; }

        [Description("Gets the error description.")]
        public object ErrorContent { get; private set; }

        [Description("Gets the exception.")]
        public System.Exception Exception { get; private set; }

        [Description("Gets the error type.")]
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorType { get; private set; }
    }
}

