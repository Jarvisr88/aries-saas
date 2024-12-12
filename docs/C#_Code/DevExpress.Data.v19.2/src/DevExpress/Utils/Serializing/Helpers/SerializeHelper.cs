namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class SerializeHelper : SerializeHelperBase
    {
        public const int UndefinedObjectIndex = -1;
        public const string IndexPropertyName = "Index";

        public SerializeHelper()
        {
        }

        public SerializeHelper(object rootObject) : this(rootObject, null)
        {
        }

        public SerializeHelper(object rootObject, SerializationContext context) : base(rootObject, context)
        {
        }

        internal static void AddIndexPropertyInfo(XtraPropertyInfo propInfo, int index)
        {
            if (index != -1)
            {
                propInfo.ChildProperties.Add(new XtraPropertyInfo("Index", typeof(int), index));
            }
        }

        internal static void CallEndSerializing(object obj)
        {
            IXtraSerializable serializable = obj as IXtraSerializable;
            if (serializable != null)
            {
                serializable.OnEndSerializing();
            }
        }

        internal static void CallStartSerializing(object obj)
        {
            IXtraSerializable serializable = obj as IXtraSerializable;
            if (serializable != null)
            {
                serializable.OnStartSerializing();
            }
        }

        protected internal virtual bool CheckNeedSerialize(object obj, PropertyDescriptor prop, XtraSerializableProperty attr, XtraSerializationFlags parentFlags) => 
            (attr != null) && (attr.Serialize && (base.Context.ShouldSerializeProperty(this, obj, prop, attr) && (((((parentFlags | attr.Flags) & XtraSerializationFlags.DefaultValue) == XtraSerializationFlags.None) || (!base.Context.IsDesignerSerializationVisible(prop) || (prop.ShouldSerializeValue(obj) || ((prop.GetType().Name == "InheritedPropertyDescriptor") || attr.SupressDefaultValue)))) ? base.Context.InvokeShouldSerialize(this, obj, prop) : false)));

        protected internal virtual CollectionItemSerializationStrategy CreateCollectionItemSerializationStrategy(XtraSerializableProperty attr, string name, ICollection collection, object owner, XtraSerializationFlags parentFlags, OptionsLayoutBase options)
        {
            switch (attr.Visibility)
            {
                case XtraSerializationVisibility.Collection:
                    return new CollectionItemSerializationStrategyCollection(this, name, collection, owner, parentFlags, options, attr);

                case XtraSerializationVisibility.SimpleCollection:
                    return new CollectionItemSerializationStrategySimple(this, name, collection, owner);

                case XtraSerializationVisibility.NameCollection:
                    return new CollectionItemSerializationStrategyName(this, name, collection, owner);
            }
            return new CollectionItemSerializationStrategyEmpty(this, name, collection, owner);
        }

        protected virtual XtraPropertyInfo CreateXtraPropertyInfo(PropertyDescriptor prop, object value, bool isKey) => 
            new XtraPropertyInfo(prop.Name, prop.PropertyType, value, isKey);

        protected internal XtraPropertyInfo GetSerializedPropertyAsCollection(XtraPropertyInfo root, XtraSerializableProperty attr, object owner, XtraSerializationFlags parentFlags, OptionsLayoutBase options, ICollection list)
        {
            if (list.Count > 0)
            {
                CollectionItemSerializationStrategy strategy = this.CreateCollectionItemSerializationStrategy(attr, root.Name, list, owner, parentFlags, options);
                int index = 1;
                foreach (object obj2 in list)
                {
                    XtraPropertyInfo prop = strategy.SerializeCollectionItem(index, obj2);
                    if (prop != null)
                    {
                        root.ChildProperties.Add(prop);
                        index++;
                    }
                }
            }
            root.Value = root.ChildProperties.Count;
            if (attr.IsCollectionContent)
            {
                this.SerializeCollectionContent(root, list, options);
            }
            return root;
        }

        protected internal virtual XtraPropertyInfo GetSerializedPropertyAsContent(object obj, PropertyDescriptor prop, OptionsLayoutBase options, XtraSerializableProperty attr)
        {
            object obj2 = prop.GetValue(obj);
            if (obj2 == null)
            {
                return null;
            }
            IXtraPropertyCollection props = this.SerializeObject(obj2, attr.Flags, options);
            if (((attr.Flags & XtraSerializationFlags.DefaultValue) != XtraSerializationFlags.None) && (props.Count == 0))
            {
                return null;
            }
            XtraPropertyInfo info = this.CreateXtraPropertyInfo(prop, null, true);
            info.ChildProperties.AddRange(props);
            return info;
        }

        protected internal virtual XtraPropertyInfo[] PerformManualSerialization(object obj)
        {
            IXtraSerializable2 serializable = obj as IXtraSerializable2;
            return ((serializable == null) ? new XtraPropertyInfo[0] : serializable.Serialize());
        }

        protected virtual void RaiseEndSerializing(object obj)
        {
            CallEndSerializing(obj);
        }

        protected virtual void RaiseStartSerializing(object obj)
        {
            CallStartSerializing(obj);
        }

        protected internal virtual void SerializeCollection(XtraSerializableProperty attr, string name, XtraPropertyInfoCollection props, object owner, XtraSerializationFlags parentFlags, OptionsLayoutBase options, ICollection list)
        {
            XtraPropertyInfo root = new XtraPropertyInfo(name, null, 0, true);
            root = this.GetSerializedPropertyAsCollection(root, attr, owner, parentFlags, options, list);
            props.Add(root);
        }

        private void SerializeCollectionContent(XtraPropertyInfo root, ICollection list, OptionsLayoutBase options)
        {
            IXtraPropertyCollection props = this.SerializeObject(list, options);
            if ((props != null) && (props.Count != 0))
            {
                XtraPropertyInfo prop = new XtraPropertyInfo("Content", typeof(string), null, true);
                prop.ChildProperties.AddRange(props);
                root.ChildProperties.Add(prop);
            }
        }

        protected internal virtual XtraPropertyInfoCollection SerializeLayoutVersion(OptionsLayoutBase options, object obj)
        {
            XtraPropertyInfoCollection infos = new XtraPropertyInfoCollection();
            IXtraSerializableLayout layout = obj as IXtraSerializableLayout;
            IXtraSerializableLayout2 layout2 = obj as IXtraSerializableLayout2;
            if (layout != null)
            {
                string layoutVersion = layout.LayoutVersion;
                if (!ReferenceEquals(options, OptionsLayoutBase.FullLayout))
                {
                    layoutVersion = options.LayoutVersion;
                }
                infos.Add(new XtraPropertyInfo("#LayoutVersion", typeof(string), layoutVersion));
            }
            if (layout2 != null)
            {
                infos.Add(new XtraPropertyInfo("#LayoutScaleFactor", typeof(SizeF), layout2.LayoutScaleFactor));
            }
            return infos;
        }

        public virtual IXtraPropertyCollection SerializeObject(object obj, OptionsLayoutBase options) => 
            this.SerializeObject(obj, XtraSerializationFlags.None, options);

        public virtual IXtraPropertyCollection SerializeObject(object obj, XtraSerializationFlags parentFlags, OptionsLayoutBase options)
        {
            IXtraPropertyCollection propertys;
            this.RaiseStartSerializing(obj);
            try
            {
                propertys = this.SerializeObjectCore(obj, parentFlags, options);
            }
            finally
            {
                this.RaiseEndSerializing(obj);
            }
            return propertys;
        }

        protected internal virtual IXtraPropertyCollection SerializeObjectCore(object obj, XtraSerializationFlags parentFlags, OptionsLayoutBase options)
        {
            XtraPropertyInfoCollection store = new XtraPropertyInfoCollection();
            store.AddRange(this.SerializeLayoutVersion(options, obj));
            store.AddRange(this.PerformManualSerialization(obj));
            List<SerializablePropertyDescriptorPair> properties = base.GetProperties(obj);
            if ((properties != null) && (properties.Count > 0))
            {
                foreach (SerializablePropertyDescriptorPair pair in this.SortProps(obj, properties))
                {
                    PropertyDescriptor property = pair.Property;
                    if (base.AllowProperty(obj, property, pair.Attribute, options, true))
                    {
                        this.SerializeProperty(store, obj, pair, parentFlags, options);
                    }
                }
            }
            return store;
        }

        public IXtraPropertyCollection SerializeObjects(IList objects, OptionsLayoutBase options) => 
            base.Context.SerializeObjectsCore(this, objects, options);

        protected internal virtual void SerializeProperty(XtraPropertyInfoCollection store, object obj, SerializablePropertyDescriptorPair pair, XtraSerializationFlags parentFlags, OptionsLayoutBase options)
        {
            PropertyDescriptor prop = pair.Property;
            XtraSerializableProperty attribute = pair.Attribute;
            if (this.CheckNeedSerialize(obj, prop, attribute, parentFlags))
            {
                if (attribute.SerializeCollection)
                {
                    this.SerializePropertyAsCollection(store, obj, prop, options, attribute);
                }
                else
                {
                    int index = -1;
                    if (!this.TrySerializePropertyValueCacheIndex(attribute, prop, store, obj, ref index))
                    {
                        if (attribute.Visibility == XtraSerializationVisibility.Content)
                        {
                            this.SerializePropertyAsContent(store, obj, prop, options, attribute, index);
                        }
                        else if (XtraPropertyInfo.IsPrimitiveOrTag(attribute, prop))
                        {
                            this.SerializePropertyAsPrimitive(store, obj, prop);
                        }
                        else
                        {
                            this.SerializePropertyAsSimple(store, obj, prop);
                        }
                    }
                }
            }
        }

        protected internal virtual void SerializePropertyAsCollection(XtraPropertyInfoCollection store, object obj, PropertyDescriptor prop, OptionsLayoutBase options, XtraSerializableProperty attr)
        {
            ICollection list = prop.GetValue(obj) as ICollection;
            if (list != null)
            {
                this.SerializeCollection(attr, prop.Name, store, obj, attr.Flags, options, list);
            }
        }

        protected internal virtual void SerializePropertyAsContent(XtraPropertyInfoCollection store, object obj, PropertyDescriptor prop, OptionsLayoutBase options, XtraSerializableProperty attr, int index)
        {
            XtraPropertyInfo propInfo = this.GetSerializedPropertyAsContent(obj, prop, options, attr);
            if (propInfo != null)
            {
                AddIndexPropertyInfo(propInfo, index);
                store.Add(propInfo);
            }
        }

        protected internal virtual void SerializePropertyAsPrimitive(XtraPropertyInfoCollection store, object obj, PropertyDescriptor prop)
        {
            object obj2 = prop.GetValue(obj);
            XtraPropertyInfo info = this.CreateXtraPropertyInfo(prop, obj2, false);
            store.Add(info.EnsureIsPrimitive(this.ObjectConverterImpl));
        }

        protected internal virtual void SerializePropertyAsSimple(XtraPropertyInfoCollection store, object obj, PropertyDescriptor prop)
        {
            object obj2 = prop.GetValue(obj);
            XtraPropertyInfo item = this.CreateXtraPropertyInfo(prop, obj2, false);
            store.Add(item);
        }

        private bool TrySerializeCacheIndex(string name, XtraSerializableProperty attr, IXtraPropertyCollection store, object value, ref int index)
        {
            SerializationInfo indexByObject = base.RootSerializationObject.GetIndexByObject(name, value);
            index = indexByObject.Index;
            if (!indexByObject.Serialized)
            {
                indexByObject.Serialized = true;
                return false;
            }
            store.Add(new XtraPropertyInfo(name, typeof(int), indexByObject.Index));
            return true;
        }

        internal bool TrySerializeCollectionItemCacheIndex(string name, XtraSerializableProperty attr, IXtraPropertyCollection store, object value, ref int index) => 
            !base.ShouldNotTryCache(attr) ? this.TrySerializeCacheIndex(name, attr, store, value, ref index) : false;

        private bool TrySerializePropertyValueCacheIndex(XtraSerializableProperty attr, PropertyDescriptor prop, IXtraPropertyCollection store, object obj, ref int index)
        {
            if (base.ShouldNotTryCache(attr))
            {
                return false;
            }
            object obj2 = prop.GetValue(obj);
            return ((obj2 != null) ? this.TrySerializeCacheIndex(prop.Name, attr, store, obj2, ref index) : false);
        }

        public ObjectConverterImplementation ObjectConverterImpl { get; set; }
    }
}

