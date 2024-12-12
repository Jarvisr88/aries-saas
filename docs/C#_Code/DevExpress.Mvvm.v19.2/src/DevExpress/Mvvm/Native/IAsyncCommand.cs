namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Threading;
    using System.Windows.Input;

    public interface IAsyncCommand : IDelegateCommand, ICommand
    {
        void Wait(TimeSpan timeout);

        bool IsExecuting { get; }

        [Obsolete("Use the IsCancellationRequested property instead.")]
        bool ShouldCancel { get; }

        System.Threading.CancellationTokenSource CancellationTokenSource { get; }

        bool IsCancellationRequested { get; }

        ICommand CancelCommand { get; }
    }
}

