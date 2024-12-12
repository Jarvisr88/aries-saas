namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.ComponentModel;

    internal class RowDataErrorValidator : DataErrorValidator
    {
        protected internal override MultiErrorInfo CreateFromDataErrorInfo(IDataErrorInfo obj) => 
            MultiErrorInfo.Create(obj.Error, ErrorType.Default);

        protected internal override MultiErrorInfo CreateFromDXDataErrorInfo(IDXDataErrorInfo obj)
        {
            ErrorInfo info = new ErrorInfo();
            obj.GetError(info);
            return MultiErrorInfo.Create(info.ErrorText, info.ErrorType);
        }

        protected internal override MultiErrorInfo CreateFromNotifyDataErrorInfo(INotifyDataErrorInfo obj)
        {
            MultiErrorInfo multiErrorInfo = this.ValidateProperty(obj, null);
            if (!multiErrorInfo.HasErrors())
            {
                multiErrorInfo = this.ValidateProperty(obj, string.Empty);
            }
            return multiErrorInfo;
        }

        private MultiErrorInfo ValidateProperty(INotifyDataErrorInfo obj, string propertyName) => 
            new PropertyDataErrorValidator(propertyName).CreateFromNotifyDataErrorInfo(obj);
    }
}

