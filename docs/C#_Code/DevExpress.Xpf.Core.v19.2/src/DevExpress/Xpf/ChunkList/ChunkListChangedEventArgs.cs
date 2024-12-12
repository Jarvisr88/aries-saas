namespace DevExpress.Xpf.ChunkList
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ChunkListChangedEventArgs : ListChangedEventArgs
    {
        public ChunkListChangedEventArgs(ListChangedType listChangedType, int index, object oldItem) : base(listChangedType, index)
        {
            this.OldItem = oldItem;
        }

        public object OldItem { get; private set; }
    }
}

