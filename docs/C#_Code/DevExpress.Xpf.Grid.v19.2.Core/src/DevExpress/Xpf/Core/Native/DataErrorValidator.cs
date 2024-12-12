namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.ComponentModel;

    internal abstract class DataErrorValidator
    {
        protected DataErrorValidator()
        {
        }

        protected internal abstract MultiErrorInfo CreateFromDataErrorInfo(IDataErrorInfo obj);
        protected internal abstract MultiErrorInfo CreateFromDXDataErrorInfo(IDXDataErrorInfo obj);
        protected internal abstract MultiErrorInfo CreateFromNotifyDataErrorInfo(INotifyDataErrorInfo obj);
        public MultiErrorInfo Validate(RowValidationObjectAccessor accessor, bool validatesOnNotifyDataErrors)
        {
            if (accessor == null)
            {
                return MultiErrorInfo.Create();
            }
            if (validatesOnNotifyDataErrors)
            {
                INotifyDataErrorInfo notifyDataErrorInfo = accessor.NotifyDataErrorInfo;
                if (notifyDataErrorInfo != null)
                {
                    return this.CreateFromNotifyDataErrorInfo(notifyDataErrorInfo);
                }
            }
            IDXDataErrorInfo dXErrorInfo = accessor.DXErrorInfo;
            if (dXErrorInfo != null)
            {
                return this.CreateFromDXDataErrorInfo(dXErrorInfo);
            }
            IDataErrorInfo errorInfo = accessor.ErrorInfo;
            return ((errorInfo == null) ? MultiErrorInfo.Create() : this.CreateFromDataErrorInfo(errorInfo));
        }
    }
}

