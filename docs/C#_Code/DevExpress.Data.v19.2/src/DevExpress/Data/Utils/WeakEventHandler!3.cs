namespace DevExpress.Data.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public class WeakEventHandler<TOwner, TEventArgs, THandler> : IWeakEventHandler<THandler> where TOwner: class
    {
        private WeakReference ownerReference;
        private Action<WeakEventHandler<TOwner, TEventArgs, THandler>, object> onDetachAction;
        private Action<TOwner, object, TEventArgs> onEventAction;

        public WeakEventHandler(TOwner owner, Action<TOwner, object, TEventArgs> onEventAction, Action<WeakEventHandler<TOwner, TEventArgs, THandler>, object> onDetachAction, Func<WeakEventHandler<TOwner, TEventArgs, THandler>, THandler> createHandlerFunction);
        public void OnEvent(object source, TEventArgs eventArgs);

        public THandler Handler { get; private set; }
    }
}

