namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;

    public interface ISupportWizardBackCommand
    {
        void OnGoBack(CancelEventArgs e);

        bool CanGoBack { get; }
    }
}

