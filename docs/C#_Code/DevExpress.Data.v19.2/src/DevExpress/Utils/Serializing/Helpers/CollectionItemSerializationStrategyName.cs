namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Collections;

    public class CollectionItemSerializationStrategyName : CollectionItemSerializationStrategy
    {
        public CollectionItemSerializationStrategyName(SerializeHelper helper, string name, ICollection collection, object owner) : base(helper, name, collection, owner)
        {
        }

        protected internal override bool AssignItemPropertyValue(XtraPropertyInfo itemProperty, object item)
        {
            object obj2 = this.ExtractItemName(item);
            if (obj2 == null)
            {
                return false;
            }
            itemProperty.Value = obj2;
            return true;
        }

        protected internal override XtraPropertyInfo CreateItemPropertyInfo(int index) => 
            this.CreateItemPropertyInfoCore(index, true);

        protected internal virtual object ExtractItemName(object item)
        {
            if (item == null)
            {
                return null;
            }
            string name = "Name";
            IXtraNameSerializable serializable = item as IXtraNameSerializable;
            if ((serializable != null) && !string.IsNullOrEmpty(serializable.NameToSerialize))
            {
                name = serializable.NameToSerialize;
            }
            return item.GetType().GetProperty(name)?.GetValue(item, null);
        }
    }
}

