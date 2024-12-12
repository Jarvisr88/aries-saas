namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;

    public class ErrorParameters
    {
        private DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType;
        private string errorContent;

        public ErrorParameters();
        public ErrorParameters(DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType, string errorContent);
        public void SetParameters(DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType, string errorContent);

        public static ErrorParameters None { get; }

        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorType { get; private set; }

        public string ErrorContent { get; private set; }
    }
}

