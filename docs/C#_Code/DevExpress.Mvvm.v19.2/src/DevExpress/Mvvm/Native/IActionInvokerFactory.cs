namespace DevExpress.Mvvm.Native
{
    using System;

    public interface IActionInvokerFactory
    {
        IActionInvoker CreateActionInvoker<TMessage>(object recipient, Action<TMessage> action);
    }
}

