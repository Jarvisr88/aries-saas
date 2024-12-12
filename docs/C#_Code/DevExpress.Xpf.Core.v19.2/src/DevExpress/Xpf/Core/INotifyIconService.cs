namespace DevExpress.Xpf.Core
{
    using System;

    public interface INotifyIconService
    {
        void ResetStatusIcon();
        void SetStatusIcon(object icon);
    }
}

