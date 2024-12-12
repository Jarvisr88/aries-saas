namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class StreamingSerializationContext : PrintingSystemSerializationContext
    {
        private IDictionary<Type, List<SerializablePropertyDescriptorPair>> cachedProps = new ConcurrentDictionary<Type, List<SerializablePropertyDescriptorPair>>();

        public List<SerializablePropertyDescriptorPair> GetSortedProperties(object obj)
        {
            List<SerializablePropertyDescriptorPair> list2;
            StreamingSerializationContext context = this;
            lock (context)
            {
                List<SerializablePropertyDescriptorPair> list;
                Type key = obj.GetType();
                if (this.cachedProps.TryGetValue(key, out list))
                {
                    list2 = list;
                }
                else
                {
                    PropertyDescriptorCollection properties = this.GetProperties(obj);
                    list = new List<SerializablePropertyDescriptorPair>(properties.Count);
                    foreach (PropertyDescriptor descriptor in properties)
                    {
                        XtraSerializableProperty xtraSerializableProperty = this.GetXtraSerializableProperty(obj, descriptor);
                        list.Add(new SerializablePropertyDescriptorPair(descriptor, xtraSerializableProperty));
                    }
                    Comparison<SerializablePropertyDescriptorPair> comparison = <>c.<>9__2_0;
                    if (<>c.<>9__2_0 == null)
                    {
                        Comparison<SerializablePropertyDescriptorPair> local1 = <>c.<>9__2_0;
                        comparison = <>c.<>9__2_0 = delegate (SerializablePropertyDescriptorPair x, SerializablePropertyDescriptorPair y) {
                            bool flag = x.ShouldSerializeAsCollection();
                            bool flag2 = y.ShouldSerializeAsCollection();
                            return !(!flag & flag2) ? ((!flag || flag2) ? string.Compare(x.Property.Name, y.Property.Name) : 1) : -1;
                        };
                    }
                    list.Sort(comparison);
                    list2 = this.cachedProps[key] = list;
                }
            }
            return list2;
        }

        internal override XtraSerializableProperty GetXtraSerializableProperty(object obj, PropertyDescriptor property)
        {
            StreamingSerializationContext context = this;
            lock (context)
            {
                return base.GetXtraSerializableProperty(obj, property);
            }
        }

        protected internal override void ResetProperty(DeserializeHelper helper, object obj, PropertyDescriptor property, XtraSerializableProperty attr)
        {
        }

        protected internal override IXtraPropertyCollection SerializeObjectsCore(SerializeHelper helper, IList objects, OptionsLayoutBase options)
        {
            IXtraPropertyCollection propertys;
            StreamingSerializeHelper helper2 = helper as StreamingSerializeHelper;
            if (helper2 == null)
            {
                return base.SerializeObjectsCore(helper, objects, options);
            }
            SerializeHelper.CallStartSerializing(helper.RootObject);
            try
            {
                propertys = new StreamingVirtualPropertyCollection(helper2, options, objects);
            }
            finally
            {
                SerializeHelper.CallEndSerializing(helper.RootObject);
            }
            return propertys;
        }

        protected internal override IList<SerializablePropertyDescriptorPair> SortProps(object obj, List<SerializablePropertyDescriptorPair> pairsList) => 
            pairsList;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly StreamingSerializationContext.<>c <>9 = new StreamingSerializationContext.<>c();
            public static Comparison<SerializablePropertyDescriptorPair> <>9__2_0;

            internal int <GetSortedProperties>b__2_0(SerializablePropertyDescriptorPair x, SerializablePropertyDescriptorPair y)
            {
                bool flag = x.ShouldSerializeAsCollection();
                bool flag2 = y.ShouldSerializeAsCollection();
                return (!(!flag & flag2) ? ((!flag || flag2) ? string.Compare(x.Property.Name, y.Property.Name) : 1) : -1);
            }
        }
    }
}

