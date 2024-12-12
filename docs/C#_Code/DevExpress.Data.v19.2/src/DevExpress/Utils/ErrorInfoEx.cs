namespace DevExpress.Utils
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Reflection;

    public class ErrorInfoEx : DevExpress.Utils.ErrorInfo
    {
        private DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default;

        public virtual DevExpress.XtraEditors.DXErrorProvider.ErrorType GetErrorType(object obj)
        {
            object obj2;
            base.Hash.TryGetValue(obj, out obj2);
            ErrorItem item = obj2 as ErrorItem;
            return ((item != null) ? item.ErrorType : this.errorType);
        }

        public virtual void SetError(object obj, string errorText, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType)
        {
            if (obj == null)
            {
                this.ErrorText = errorText;
                this.ErrorType = errorType;
            }
            else
            {
                object obj2;
                base.Hash.TryGetValue(obj, out obj2);
                ErrorItem item = obj2 as ErrorItem;
                if ((errorText != null) && (errorText.Length == 0))
                {
                    errorText = null;
                }
                if (((errorText != null) || (item != null)) && ((item == null) || !string.Equals(item.ErrorText, errorText)))
                {
                    if ((errorText != null) && (errorType != DevExpress.XtraEditors.DXErrorProvider.ErrorType.None))
                    {
                        base.Hash[obj] = new ErrorItem(errorText, errorType);
                    }
                    else
                    {
                        base.Hash.Remove(obj);
                    }
                    this.OnChanged();
                }
            }
        }

        public virtual void SetErrorType(object obj, DevExpress.XtraEditors.DXErrorProvider.ErrorType newErrorType)
        {
            object obj2;
            base.Hash.TryGetValue(obj, out obj2);
            ErrorItem item = obj2 as ErrorItem;
            if (item == null)
            {
                this.errorType = newErrorType;
            }
            else if (item.ErrorType != newErrorType)
            {
                item.ErrorType = newErrorType;
                this.OnChanged();
            }
        }

        public virtual DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorType
        {
            get => 
                this.errorType;
            set
            {
                if (this.ErrorType != value)
                {
                    this.errorType = value;
                    this.OnChanged();
                }
            }
        }

        public override string this[object obj]
        {
            get
            {
                object obj2;
                if (obj == null)
                {
                    return ((this.ErrorType != DevExpress.XtraEditors.DXErrorProvider.ErrorType.None) ? this.ErrorText : null);
                }
                base.Hash.TryGetValue(obj, out obj2);
                ErrorItem item = obj2 as ErrorItem;
                return (((item == null) || (item.ErrorType == DevExpress.XtraEditors.DXErrorProvider.ErrorType.None)) ? null : item.ErrorText);
            }
            set => 
                this.SetError(obj, value, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Default);
        }

        private class ErrorItem
        {
            private string errorText;
            private DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType;

            public ErrorItem(string errorText, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType)
            {
                this.errorText = errorText;
                this.errorType = errorType;
            }

            public string ErrorText
            {
                get => 
                    this.errorText;
                set => 
                    this.errorText = value;
            }

            public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorType
            {
                get => 
                    this.errorType;
                set => 
                    this.errorType = value;
            }
        }
    }
}

