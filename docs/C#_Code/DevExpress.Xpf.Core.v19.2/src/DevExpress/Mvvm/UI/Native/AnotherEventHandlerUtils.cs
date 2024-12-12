namespace DevExpress.Mvvm.UI.Native
{
    using System;

    public static class AnotherEventHandlerUtils
    {
        public static EventHandler<E> MakeWeak<E>(EventHandler<E> eventHandler, UnregisterCallback<E> unregister) where E: EventArgs
        {
            if (eventHandler == null)
            {
                throw new ArgumentNullException("eventHandler");
            }
            if (eventHandler.Method.IsStatic || (eventHandler.Target == null))
            {
                throw new ArgumentException("Only instance methods are supported.", "eventHandler");
            }
            Type[] typeArguments = new Type[] { eventHandler.Method.DeclaringType, typeof(E) };
            Type[] types = new Type[] { typeof(EventHandler<E>), typeof(UnregisterCallback<E>) };
            object[] parameters = new object[] { eventHandler, unregister };
            return ((IAnotherWeakEventHandler<E>) typeof(AnotherWeakEventHandler<,>).MakeGenericType(typeArguments).GetConstructor(types).Invoke(parameters)).Handler;
        }
    }
}

