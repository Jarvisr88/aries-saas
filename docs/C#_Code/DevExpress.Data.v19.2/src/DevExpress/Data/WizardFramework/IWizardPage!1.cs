namespace DevExpress.Data.WizardFramework
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public interface IWizardPage<TWizardModel> where TWizardModel: IWizardModel
    {
        event EventHandler Changed;

        event EventHandler<WizardPageErrorEventArgs> Error;

        void Begin();
        void Commit();
        Type GetNextPageType();
        bool Validate(out string errorMessage);

        TWizardModel Model { get; set; }

        bool MoveNextEnabled { get; }

        bool FinishEnabled { get; }

        object PageContent { get; }
    }
}

