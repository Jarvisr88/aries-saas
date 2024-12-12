namespace DevExpress.Xpf.ChunkList
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ListChangingEventArgs : EventArgs
    {
        public ListChangingEventArgs(int index, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            this.Index = index;
            this.PropertyDescriptor = propertyDescriptor;
        }

        public int Index { get; private set; }

        public System.ComponentModel.PropertyDescriptor PropertyDescriptor { get; private set; }
    }
}

