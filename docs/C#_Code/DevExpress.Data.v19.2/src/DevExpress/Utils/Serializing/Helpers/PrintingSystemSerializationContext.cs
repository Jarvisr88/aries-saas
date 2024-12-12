namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Data.Access;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public class PrintingSystemSerializationContext : SerializationContext
    {
        private Dictionary<Type, PropertyDescriptorCollection> propertyDescriptorCollections = new Dictionary<Type, PropertyDescriptorCollection>();

        protected internal override void DeserializeObjectsCore(DeserializeHelper helper, IList objects, IXtraPropertyCollection store, OptionsLayoutBase options)
        {
            DeserializeHelper.CallStartDeserializing(helper.RootObject, string.Empty);
            try
            {
                foreach (XtraPropertyInfo info in store)
                {
                    if (info.ChildProperties != null)
                    {
                        XtraObjectInfo info2 = FindObject(objects, info.Name);
                        helper.DeserializeObject(info2.Instance, info.ChildProperties, options);
                    }
                }
            }
            finally
            {
                DeserializeHelper.CallEndDeserializing(helper.RootObject, string.Empty);
            }
        }

        private static XtraObjectInfo FindObject(IList objects, string name)
        {
            XtraObjectInfo info2;
            using (IEnumerator enumerator = objects.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        throw new InvalidOperationException();
                    }
                    XtraObjectInfo current = (XtraObjectInfo) enumerator.Current;
                    if (current.Name == name)
                    {
                        info2 = current;
                        break;
                    }
                }
            }
            return info2;
        }

        protected internal override int GetCollectionItemsCount(XtraPropertyInfo root) => 
            root.ChildProperties.Count;

        protected internal override PropertyDescriptorCollection GetProperties(object obj, IXtraPropertyCollection store)
        {
            PropertyDescriptorCollection fastProperties;
            Type key = obj.GetType();
            if (!this.propertyDescriptorCollections.TryGetValue(key, out fastProperties))
            {
                fastProperties = DataListDescriptor.GetFastProperties(key);
                this.propertyDescriptorCollections.Add(key, fastProperties);
            }
            return fastProperties;
        }

        protected internal override MethodInfo GetShouldSerializeCollectionMethodInfo(SerializeHelper helper, string name, object owner) => 
            null;

        protected internal override void InvokeAfterDeserialize(DeserializeHelper helper, object obj, XtraPropertyInfo bp, object value)
        {
            IXtraSupportAfterDeserialize deserialize = obj as IXtraSupportAfterDeserialize;
            if (deserialize != null)
            {
                bp.Value = value;
                deserialize.AfterDeserialize(new XtraItemEventArgs(helper.RootObject, obj, null, bp));
            }
        }

        protected internal override object InvokeCreateCollectionItem(DeserializeHelper helper, string propertyName, XtraItemEventArgs e)
        {
            IXtraSupportDeserializeCollectionItem owner = e.Owner as IXtraSupportDeserializeCollectionItem;
            return owner?.CreateCollectionItem(propertyName, e);
        }

        protected internal override object InvokeCreateContentPropertyValueMethod(DeserializeHelper helper, XtraItemEventArgs e, PropertyDescriptor prop)
        {
            IXtraSupportCreateContentPropertyValue owner = e.Owner as IXtraSupportCreateContentPropertyValue;
            if (owner == null)
            {
                return null;
            }
            object obj2 = owner.Create(e);
            prop.SetValue(e.Owner, obj2);
            return obj2;
        }

        protected internal override void InvokeSetIndexCollectionItem(DeserializeHelper helper, string propertyName, XtraSetItemIndexEventArgs e)
        {
            IXtraSupportDeserializeCollectionItem owner = e.Owner as IXtraSupportDeserializeCollectionItem;
            if (owner != null)
            {
                owner.SetIndexCollectionItem(propertyName, e);
            }
        }

        protected internal override bool InvokeShouldSerialize(SerializeHelper helper, object obj, PropertyDescriptor property)
        {
            IXtraSupportShouldSerialize serialize = obj as IXtraSupportShouldSerialize;
            return ((serialize != null) ? serialize.ShouldSerialize(property.Name) : true);
        }

        protected internal override IXtraPropertyCollection SerializeObjectsCore(SerializeHelper helper, IList objects, OptionsLayoutBase options)
        {
            IXtraPropertyCollection propertys;
            SerializeHelper.CallStartSerializing(helper.RootObject);
            try
            {
                propertys = new SerializationVirtualXtraPropertyCollection(helper, options, objects);
            }
            finally
            {
                SerializeHelper.CallEndSerializing(helper.RootObject);
            }
            return propertys;
        }

        protected internal override bool ShouldSerializeCollectionItem(SerializeHelper helper, object owner, MethodInfo mi, XtraPropertyInfo itemProperty, object item, XtraItemEventArgs e)
        {
            bool flag;
            IXtraSupportShouldSerializeCollectionItem item2 = owner as IXtraSupportShouldSerializeCollectionItem;
            if (item2 == null)
            {
                return true;
            }
            itemProperty.Value = item;
            try
            {
                flag = item2.ShouldSerializeCollectionItem(e);
            }
            finally
            {
                itemProperty.Value = null;
            }
            return flag;
        }

        protected internal override bool ShouldSerializeProperty(SerializeHelper helper, object obj, PropertyDescriptor prop, XtraSerializableProperty xtraSerializableProperty)
        {
            if (xtraSerializableProperty.SupressDefaultValue)
            {
                return true;
            }
            IHasDefaultValue value2 = prop as IHasDefaultValue;
            if (value2 != null)
            {
                return (!value2.HasDefaultValue || !Equals(value2.DefaultValue, prop.GetValue(obj)));
            }
            DefaultValueAttribute attribute = prop.GetAttribute<DefaultValueAttribute>();
            return ((attribute == null) || !Equals(attribute.Value, prop.GetValue(obj)));
        }

        protected internal override IList<SerializablePropertyDescriptorPair> SortProps(object obj, List<SerializablePropertyDescriptorPair> pairsList)
        {
            IXtraSortableProperties properties = obj as IXtraSortableProperties;
            return (((properties == null) || !properties.ShouldSortProperties()) ? pairsList : this.SortPropsCore(obj, pairsList));
        }

        protected IList<SerializablePropertyDescriptorPair> SortPropsCore(object obj, List<SerializablePropertyDescriptorPair> pairsList) => 
            base.SortProps(obj, pairsList);
    }
}

