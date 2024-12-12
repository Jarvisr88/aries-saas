namespace DevExpress.Internal
{
    using System;

    public class WeakEventHandlerStrategy<TArgs, TBaseHandler> : WeakEventHandler<object, TArgs, TBaseHandler>, IWeakEventHandlerStrategy<TArgs> where TArgs: EventArgs
    {
        public void Add(Delegate target)
        {
            base.AddEvent(target);
        }

        public void Purge()
        {
            base.PurgeEvents();
        }

        public void Raise(object sender, TArgs args)
        {
            base.Invoke(target => target(sender, args));
        }

        public void Remove(Delegate target)
        {
            base.RemoveEvent(target);
        }

        public bool IsEmpty =>
            !base.HasSubscribers;
    }
}

