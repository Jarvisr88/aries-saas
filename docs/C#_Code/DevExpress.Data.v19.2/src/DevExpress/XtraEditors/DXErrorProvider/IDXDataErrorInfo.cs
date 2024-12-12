namespace DevExpress.XtraEditors.DXErrorProvider
{
    using System;

    public interface IDXDataErrorInfo
    {
        void GetError(ErrorInfo info);
        void GetPropertyError(string propertyName, ErrorInfo info);
    }
}

