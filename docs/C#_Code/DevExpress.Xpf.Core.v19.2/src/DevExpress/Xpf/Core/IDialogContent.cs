namespace DevExpress.Xpf.Core
{
    using System;

    public interface IDialogContent
    {
        bool CanCloseWithOKResult();
        void OnApply();
        void OnOk();
    }
}

