namespace DevExpress.Utils.Serializing.Helpers
{
    using System;
    using System.Collections;

    public class CollectionItemSerializationStrategyEmpty : CollectionItemSerializationStrategy
    {
        public CollectionItemSerializationStrategyEmpty(SerializeHelper helper, string name, ICollection collection, object owner) : base(helper, name, collection, owner)
        {
        }

        protected internal override bool AssignItemPropertyValue(XtraPropertyInfo itemProperty, object item) => 
            true;
    }
}

