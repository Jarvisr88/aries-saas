namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using System;
    using System.Collections;

    public class CollectionItemSerializationStrategySimple : CollectionItemSerializationStrategy
    {
        public CollectionItemSerializationStrategySimple(SerializeHelper helper, string name, ICollection collection, object owner) : base(helper, name, collection, owner)
        {
        }

        protected internal override bool AssignItemPropertyValue(XtraPropertyInfo itemProperty, object item)
        {
            if (ReferenceEquals(DBNull.Value, item))
            {
                item = null;
            }
            this.CheckStoreItemType(itemProperty, item);
            itemProperty.Value = item;
            return true;
        }

        private void CheckStoreItemType(XtraPropertyInfo itemProperty, object item)
        {
            if ((item != null) && (DXTypeExtensions.GetTypeCode(item.GetType()) != TypeCode.Object))
            {
                itemProperty.PropertyType = typeof(object);
            }
        }

        protected internal override XtraPropertyInfo CreateItemPropertyInfo(int index) => 
            this.CreateItemPropertyInfoCore(index, true);
    }
}

