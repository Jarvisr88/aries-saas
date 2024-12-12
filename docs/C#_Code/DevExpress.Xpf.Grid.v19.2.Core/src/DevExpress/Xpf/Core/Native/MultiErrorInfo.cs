namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class MultiErrorInfo
    {
        public const DevExpress.XtraEditors.DXErrorProvider.ErrorType DefaultErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default;

        private MultiErrorInfo(string errorText, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType, object[] errors)
        {
            this.<ErrorText>k__BackingField = errorText;
            this.<ErrorType>k__BackingField = errorType;
            this.<Errors>k__BackingField = errors;
        }

        public static MultiErrorInfo Create() => 
            new MultiErrorInfo(string.Empty, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default, new object[0]);

        public static MultiErrorInfo Create(string errorText, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType = 1) => 
            string.IsNullOrEmpty(errorText) ? Create() : new MultiErrorInfo(errorText, errorType, errorText.YieldToArray<string>());

        public static MultiErrorInfo Create(string errorText, object[] errors, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType = 1) => 
            new MultiErrorInfo(errorText, errorType, errors);

        public string ErrorText { get; }

        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorType { get; }

        public object[] Errors { get; }
    }
}

