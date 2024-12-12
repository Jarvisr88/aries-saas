namespace DevExpress.DataAccess.Wizard.Services
{
    using System;

    public interface IWaitFormActivator
    {
        void CloseWaitForm();
        void CloseWaitForm(bool throwException, int delay, bool waitForClose);
        void EnableCancelButton(bool enable);
        void EnableWaitFormDescription(bool show);
        void SetWaitFormCaption(string caption);
        void SetWaitFormDescription(string message);
        void SetWaitFormObject(ISupportCancel dataSourceLoader);
        void ShowWaitForm(bool fadeIn, bool fadeOut, bool useDelay);
    }
}

