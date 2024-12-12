namespace DevExpress.Internal
{
    using System;

    public interface IWeakEventHandlerStrategy<TArgs> where TArgs: EventArgs
    {
        void Add(Delegate target);
        void Purge();
        void Raise(object sender, TArgs args);
        void Remove(Delegate target);

        bool IsEmpty { get; }
    }
}

