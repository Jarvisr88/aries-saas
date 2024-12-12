namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class PropertyDataErrorValidator : DataErrorValidator
    {
        public PropertyDataErrorValidator(string propertyName)
        {
            this.<PropertyName>k__BackingField = propertyName;
        }

        protected internal override MultiErrorInfo CreateFromDataErrorInfo(IDataErrorInfo obj) => 
            MultiErrorInfo.Create(obj[this.PropertyName], ErrorType.Default);

        protected internal override MultiErrorInfo CreateFromDXDataErrorInfo(IDXDataErrorInfo obj)
        {
            ErrorInfo info = new ErrorInfo();
            obj.GetPropertyError(this.PropertyName, info);
            return MultiErrorInfo.Create(info.ErrorText, info.ErrorType);
        }

        protected internal override MultiErrorInfo CreateFromNotifyDataErrorInfo(INotifyDataErrorInfo obj)
        {
            object[] localArray1;
            if (!obj.HasErrors)
            {
                return MultiErrorInfo.Create();
            }
            IEnumerable errors = obj.GetErrors(this.PropertyName);
            if (errors != null)
            {
                localArray1 = errors.Cast<object>().ToArray<object>();
            }
            else
            {
                IEnumerable local1 = errors;
                localArray1 = null;
            }
            return ValidationMultiErrorReader.Read<object>(localArray1);
        }

        public string PropertyName { get; }
    }
}

