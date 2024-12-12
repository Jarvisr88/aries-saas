namespace DevExpress.DataAccess.Wizard
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.DataAccess.Wizard.Services;
    using System;
    using System.Drawing;

    public interface IWizardRunnerContext : IUIRunnerContext
    {
        bool Confirm(string message);
        IWizardView CreateWizardView(string wizardTitle, Size wizardSize);
        bool Run<TModel>(Wizard<TModel> wizard) where TModel: IWizardModel;
        void ShowMessage(string message);
        void ShowMessage(string message, string caption);

        IWaitFormActivator WaitFormActivator { get; }
    }
}

