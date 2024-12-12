namespace DevExpress.Data.WizardFramework
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IWizardView
    {
        event EventHandler Cancel;

        event EventHandler Finish;

        event EventHandler Next;

        event EventHandler Previous;

        void EnableFinish(bool enable);
        void EnableNext(bool enable);
        void EnablePrevious(bool enable);
        void SetPageContent(object content);
        void ShowError(string error);
    }
}

