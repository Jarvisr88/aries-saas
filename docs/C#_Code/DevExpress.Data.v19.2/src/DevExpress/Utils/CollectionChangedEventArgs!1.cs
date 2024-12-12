namespace DevExpress.Utils
{
    using System;

    public class CollectionChangedEventArgs<T> : EventArgs
    {
        private readonly CollectionChangedAction action;
        private readonly T element;

        public CollectionChangedEventArgs(CollectionChangedAction action, T element)
        {
            this.action = action;
            this.element = element;
        }

        public CollectionChangedAction Action =>
            this.action;

        public T Element =>
            this.element;
    }
}

