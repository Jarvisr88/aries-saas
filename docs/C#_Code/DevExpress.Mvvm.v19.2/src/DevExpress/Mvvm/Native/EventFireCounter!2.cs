namespace DevExpress.Mvvm.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class EventFireCounter<TObject, TEventArgs> where TEventArgs: EventArgs
    {
        private readonly Action<EventHandler> unsubscribe;
        protected readonly EventHandler handler;

        public EventFireCounter(Action<EventHandler> subscribe, Action<EventHandler> unsubscribe)
        {
            this.unsubscribe = unsubscribe;
            this.handler = new EventHandler(this.OnEvent);
            subscribe(new EventHandler(this.OnEvent));
        }

        private void OnEvent(object source, EventArgs eventArgs)
        {
            int fireCount = this.FireCount;
            this.FireCount = fireCount + 1;
            this.LastArgs = (TEventArgs) eventArgs;
        }

        public void Unsubscribe()
        {
            this.unsubscribe(new EventHandler(this.OnEvent));
        }

        public int FireCount { get; private set; }

        public TEventArgs LastArgs { get; private set; }
    }
}

