namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;

    public interface ISupportWizardFinishCommand
    {
        void OnFinish(CancelEventArgs e);

        bool CanFinish { get; }
    }
}

