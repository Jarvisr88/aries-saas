namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class AnotherWeakEventHandler<T, E> : IAnotherWeakEventHandler<E> where T: class where E: EventArgs
    {
        private WeakReference m_TargetRef;
        private OpenEventHandler<T, E> m_OpenHandler;
        private EventHandler<E> m_Handler;
        private UnregisterCallback<E> m_Unregister;

        public AnotherWeakEventHandler(EventHandler<E> eventHandler, UnregisterCallback<E> unregister)
        {
            this.m_TargetRef = new WeakReference(eventHandler.Target);
            this.m_OpenHandler = (OpenEventHandler<T, E>) Delegate.CreateDelegate(typeof(OpenEventHandler<T, E>), null, eventHandler.Method);
            this.m_Handler = new EventHandler<E>(this.Invoke);
            this.m_Unregister = unregister;
        }

        public void Invoke(object sender, E e)
        {
            T target = (T) this.m_TargetRef.Target;
            if (target != null)
            {
                this.m_OpenHandler(target, sender, e);
            }
            else if (this.m_Unregister != null)
            {
                this.m_Unregister(this.m_Handler);
                this.m_Unregister = null;
            }
        }

        public static implicit operator EventHandler<E>(AnotherWeakEventHandler<T, E> weh) => 
            weh.m_Handler;

        public EventHandler<E> Handler =>
            this.m_Handler;

        private delegate void OpenEventHandler(T @this, object sender, E e);
    }
}

