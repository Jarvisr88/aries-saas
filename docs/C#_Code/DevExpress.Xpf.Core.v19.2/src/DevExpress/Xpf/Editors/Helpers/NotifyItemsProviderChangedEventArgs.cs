namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class NotifyItemsProviderChangedEventArgs : EventArgs
    {
        public NotifyItemsProviderChangedEventArgs(ListChangedType changedType, int newIndex) : this(changedType, null, newIndex)
        {
        }

        public NotifyItemsProviderChangedEventArgs(ListChangedType changedType, object item) : this(changedType, item, -1)
        {
        }

        protected NotifyItemsProviderChangedEventArgs(ListChangedType changedType, object item, int index)
        {
            this.NewIndex = index;
            this.Item = item;
            this.ChangedType = changedType;
        }

        public int NewIndex { get; private set; }

        public object Item { get; private set; }

        public ListChangedType ChangedType { get; private set; }
    }
}

