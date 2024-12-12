namespace DevExpress.Mvvm.Native
{
    using System;

    public interface IActionInvoker
    {
        void ClearIfMatched(Delegate action, object recipient);
        void ExecuteIfMatched(Type messageTargetType, object parameter);

        object Target { get; }
    }
}

