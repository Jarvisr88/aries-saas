namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class ItemsProviderDataChangedEventArgs : ItemsProviderChangedEventArgs
    {
        public ItemsProviderDataChangedEventArgs() : this(System.ComponentModel.ListChangedType.Reset, -1, null)
        {
        }

        public ItemsProviderDataChangedEventArgs(System.ComponentModel.ListChangedType changedType) : this(changedType, -1, null)
        {
        }

        public ItemsProviderDataChangedEventArgs(System.ComponentModel.ListChangedType changedType, int index, PropertyDescriptor descriptor)
        {
            this.ListChangedType = changedType;
            this.NewIndex = index;
            this.Descriptor = descriptor;
        }

        public System.ComponentModel.ListChangedType ListChangedType { get; private set; }

        public PropertyDescriptor Descriptor { get; private set; }

        public int NewIndex { get; private set; }
    }
}

