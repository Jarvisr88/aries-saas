namespace DevExpress.Mvvm.Native
{
    using System;

    public class WeakReferenceActionInvokerFactory : IActionInvokerFactory
    {
        IActionInvoker IActionInvokerFactory.CreateActionInvoker<TMessage>(object recipient, Action<TMessage> action) => 
            !action.Method.IsStatic ? ((IActionInvoker) new WeakReferenceActionInvoker<TMessage>(recipient, action)) : ((IActionInvoker) new StrongReferenceActionInvoker<TMessage>(recipient, action));
    }
}

