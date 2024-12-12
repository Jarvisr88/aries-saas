namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Validation.Native;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ValidationEventArgs : RoutedEventArgs
    {
        public ValidationEventArgs(RoutedEvent routedEvent, object source, object value, CultureInfo culture) : this(routedEvent, source, value, culture, UpdateEditorSource.DoValidate)
        {
        }

        public ValidationEventArgs(RoutedEvent routedEvent, object source, object value, CultureInfo culture, UpdateEditorSource updateSource) : base(routedEvent, source)
        {
            this.Value = value;
            this.Culture = culture;
            this.UpdateSource = updateSource;
            this.SetError(null);
        }

        public void SetError(object errorContent)
        {
            this.SetErrorCore(errorContent);
            this.ErrorType = this.IsValid ? DevExpress.XtraEditors.DXErrorProvider.ErrorType.None : DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default;
        }

        public void SetError(object errorContent, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType)
        {
            this.SetErrorCore(errorContent);
            this.ErrorType = errorType;
        }

        private void SetErrorCore(object errorContent)
        {
            this.ErrorContent = errorContent;
            this.IsValid = (this.ErrorContent == null) || string.IsNullOrEmpty(errorContent as string);
        }

        internal System.Exception Exception { get; set; }

        public object ErrorContent { get; set; }

        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorType { get; set; }

        public bool IsValid { get; set; }

        public object Value { get; private set; }

        public CultureInfo Culture { get; private set; }

        public UpdateEditorSource UpdateSource { get; private set; }
    }
}

