namespace DevExpress.Mvvm.Native
{
    using System;

    public class StrongReferenceActionInvokerFactory : IActionInvokerFactory
    {
        IActionInvoker IActionInvokerFactory.CreateActionInvoker<TMessage>(object recipient, Action<TMessage> action) => 
            new StrongReferenceActionInvoker<TMessage>(recipient, action);
    }
}

