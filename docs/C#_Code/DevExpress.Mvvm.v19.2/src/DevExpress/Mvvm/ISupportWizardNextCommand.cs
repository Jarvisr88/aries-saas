namespace DevExpress.Mvvm
{
    using System;
    using System.ComponentModel;

    public interface ISupportWizardNextCommand
    {
        void OnGoForward(CancelEventArgs e);

        bool CanGoForward { get; }
    }
}

