namespace DevExpress.XtraEditors.DXErrorProvider
{
    using System;
    using System.ComponentModel;

    public class ErrorInfo
    {
        private string errorText;
        private DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType;

        public ErrorInfo();
        public ErrorInfo(string errorText, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType);

        [Description("Gets or sets the error text associated with the current property name.")]
        public string ErrorText { get; set; }

        [Description("Gets or sets the type of error associated with the current property name.")]
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorType { get; set; }
    }
}

