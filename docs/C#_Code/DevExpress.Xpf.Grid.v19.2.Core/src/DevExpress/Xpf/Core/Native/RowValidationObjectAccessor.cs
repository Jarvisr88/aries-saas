namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal class RowValidationObjectAccessor
    {
        private readonly Func<INotifyDataErrorInfo> getNotifyDataErrorInfo;
        private readonly Func<IDXDataErrorInfo> getDXErrorInfo;
        private readonly Func<IDataErrorInfo> getErrorInfo;
        private RowValueBox<INotifyDataErrorInfo> notifyDataErrorInfoCore;
        private RowValueBox<IDXDataErrorInfo> dXErrorInfoCore;
        private RowValueBox<IDataErrorInfo> errorInfoCore;

        public RowValidationObjectAccessor(Func<INotifyDataErrorInfo> getNotifyDataErrorInfo, Func<IDXDataErrorInfo> getDXErrorInfo, Func<IDataErrorInfo> getErrorInfo)
        {
            this.getNotifyDataErrorInfo = getNotifyDataErrorInfo;
            this.getDXErrorInfo = getDXErrorInfo;
            this.getErrorInfo = getErrorInfo;
        }

        public INotifyDataErrorInfo NotifyDataErrorInfo
        {
            get
            {
                this.notifyDataErrorInfoCore ??= new RowValueBox<INotifyDataErrorInfo>(this.getNotifyDataErrorInfo());
                return this.notifyDataErrorInfoCore.Value;
            }
        }

        public IDXDataErrorInfo DXErrorInfo
        {
            get
            {
                this.dXErrorInfoCore ??= new RowValueBox<IDXDataErrorInfo>(this.getDXErrorInfo());
                return this.dXErrorInfoCore.Value;
            }
        }

        public IDataErrorInfo ErrorInfo
        {
            get
            {
                this.errorInfoCore ??= new RowValueBox<IDataErrorInfo>(this.getErrorInfo());
                return this.errorInfoCore.Value;
            }
        }

        private class RowValueBox<T>
        {
            public RowValueBox(T val)
            {
                this.Value = val;
            }

            public T Value { get; private set; }
        }
    }
}

