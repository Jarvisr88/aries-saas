namespace DevExpress.Xpf.Core.Serialization
{
    using DevExpress.Utils.Serializing;
    using System;

    public class DXSerializableCollection : DXSerializable
    {
        private bool useCreateItem;
        private bool useFindItem;
        private bool clearCollection;

        public DXSerializableCollection(bool useCreateItem, bool useFindItem, bool clearCollection)
        {
            this.useCreateItem = useCreateItem;
            this.useFindItem = useFindItem;
            this.clearCollection = clearCollection;
        }

        protected internal override XtraSerializableProperty CreateXtraSerializableAttrubute() => 
            new XtraSerializableProperty(XtraSerializationVisibility.Collection, this.useCreateItem, this.useFindItem, this.clearCollection);
    }
}

