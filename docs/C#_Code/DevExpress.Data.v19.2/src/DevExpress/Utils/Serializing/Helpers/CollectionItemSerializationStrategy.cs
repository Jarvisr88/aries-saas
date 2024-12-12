namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;
    using System.Reflection;

    public abstract class CollectionItemSerializationStrategy
    {
        private SerializeHelper helper;
        private ICollection collection;
        private object owner;
        private MethodInfo mi;

        protected CollectionItemSerializationStrategy(SerializeHelper helper, string name, ICollection collection, object owner)
        {
            this.helper = helper;
            this.collection = collection;
            this.owner = owner;
            this.mi = helper.Context.GetShouldSerializeCollectionMethodInfo(helper, name, owner);
        }

        protected internal abstract bool AssignItemPropertyValue(XtraPropertyInfo itemProperty, object item);
        protected internal virtual XtraPropertyInfo CreateItemPropertyInfo(int index) => 
            this.CreateItemPropertyInfoCore(index, false);

        protected internal virtual XtraPropertyInfo CreateItemPropertyInfoCore(int index, bool isSimpleCollection) => 
            new XtraPropertyInfo("Item" + index.ToString(), null, null, !isSimpleCollection);

        public virtual XtraPropertyInfo SerializeCollectionItem(int index, object item)
        {
            XtraPropertyInfo itemProperty = this.CreateItemPropertyInfo(index);
            return ((!this.ShouldSerializeCollectionItem(itemProperty, item) || !this.AssignItemPropertyValue(itemProperty, item)) ? null : itemProperty);
        }

        protected internal virtual bool ShouldSerializeCollectionItem(XtraPropertyInfo itemProperty, object item) => 
            this.helper.Context.ShouldSerializeCollectionItem(this.helper, this.owner, this.mi, itemProperty, item, new XtraItemEventArgs(this.helper.RootObject, this.owner, this.collection, itemProperty));

        protected internal SerializeHelper Helper =>
            this.helper;

        protected internal ICollection Collection =>
            this.collection;

        protected internal object Owner =>
            this.owner;

        protected internal MethodInfo ShouldSerializeCollectionItemMethodInfo =>
            this.mi;
    }
}

