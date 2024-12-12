namespace DevExpress.Utils
{
    using System;

    public class CollectionChangingEventArgs<T> : CollectionChangedEventArgs<T>
    {
        private bool cancel;
        private T newValue;
        private T oldValue;
        private string propertyName;

        public CollectionChangingEventArgs(CollectionChangedAction action, T element) : base(action, element)
        {
            this.propertyName = string.Empty;
        }

        public bool Cancel
        {
            get => 
                this.cancel;
            set => 
                this.cancel = value;
        }

        public T NewValue
        {
            get => 
                this.newValue;
            set => 
                this.newValue = value;
        }

        public T OldValue
        {
            get => 
                this.oldValue;
            set => 
                this.oldValue = value;
        }

        public string PropertyName
        {
            get => 
                this.propertyName;
            set => 
                this.propertyName = value;
        }
    }
}

