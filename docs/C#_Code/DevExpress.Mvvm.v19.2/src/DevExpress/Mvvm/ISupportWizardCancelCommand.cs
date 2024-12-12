namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;

    public interface ISupportWizardCancelCommand
    {
        void OnCancel(CancelEventArgs e);

        bool CanCancel { get; }
    }
}

