namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public interface IBackgroundWorkerWrapper
    {
        event EventHandler<DoWorkEventArgs> DoWork;

        event EventHandler<RunWorkerCompletedEventArgs> RunWorkerCompleted;

        void RunWorkerAsync();

        bool IsBusy { get; }
    }
}

