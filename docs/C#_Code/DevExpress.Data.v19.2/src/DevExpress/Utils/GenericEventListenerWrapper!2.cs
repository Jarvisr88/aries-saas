namespace DevExpress.Utils
{
    using System;

    public abstract class GenericEventListenerWrapper<T, U> where T: class where U: class
    {
        private readonly WeakReference instanceReference;
        private U eventSource;

        protected GenericEventListenerWrapper(T listenerInstance, U eventSource)
        {
            this.eventSource = eventSource;
            Guard.ArgumentNotNull(listenerInstance, "listenerInstance");
            this.instanceReference = new WeakReference(listenerInstance);
            this.SubscribeEvents();
        }

        public virtual void CleanUp()
        {
            this.UnsubscribeEvents();
            this.ResetEventSource();
            this.ResetListenerInstance();
        }

        public bool IsAlive() => 
            this.ListenerReference.IsAlive && (this.ListenerInstance != null);

        protected virtual void ResetEventSource()
        {
            this.eventSource = default(U);
        }

        protected virtual void ResetListenerInstance()
        {
            this.ListenerReference.Target = null;
        }

        protected abstract void SubscribeEvents();
        protected abstract void UnsubscribeEvents();

        protected WeakReference ListenerReference =>
            this.instanceReference;

        public T ListenerInstance =>
            this.ListenerReference.Target as T;

        public U EventSource =>
            this.eventSource;
    }
}

