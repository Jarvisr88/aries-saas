namespace DevExpress.Xpf.Layout.Core
{
    using System;

    public class CollectionChangedEventArgs<T> : EventArgs where T: class
    {
        private readonly T elementCore;
        private readonly CollectionChangedType changedTypeCore;

        public CollectionChangedEventArgs(T element, CollectionChangedType changedType)
        {
            this.elementCore = element;
            this.changedTypeCore = changedType;
        }

        public T Element =>
            this.elementCore;

        public CollectionChangedType ChangedType =>
            this.changedTypeCore;
    }
}

