namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public abstract class SerializeHelperBase
    {
        protected internal const string LayoutPropertyName = "#LayoutVersion";
        protected internal const string LayoutScaleFactorPropertyName = "#LayoutScaleFactor";
        private SerializationContext fContext;
        protected object rootObject;

        protected SerializeHelperBase() : this(null)
        {
        }

        protected SerializeHelperBase(object rootObject) : this(rootObject, null)
        {
        }

        protected SerializeHelperBase(object rootObject, SerializationContext context)
        {
            this.rootObject = rootObject;
            this.fContext = context;
            this.fContext ??= this.CreateContextFromRoot();
            this.fContext ??= this.CreateSerializationContext();
        }

        internal bool AllowProperty(object obj, PropertyDescriptor prop, XtraSerializableProperty attribute, OptionsLayoutBase options, bool isSerializing) => 
            (!((attribute != null) & isSerializing) || !attribute.IsLoadOnly) ? this.fContext.AllowProperty(this, obj, prop, options, isSerializing) : false;

        internal static IList CheckObjects(IList objects)
        {
            if ((objects == null) || (objects.Count == 0))
            {
                return null;
            }
            if (!objects.IsReadOnly)
            {
                return objects;
            }
            List<XtraObjectInfo> list = new List<XtraObjectInfo>();
            foreach (XtraObjectInfo info in objects)
            {
                if (info.Instance != null)
                {
                    list.Add(info);
                }
            }
            return ((list.Count != 0) ? ((IList) list.ToArray()) : null);
        }

        protected virtual SerializationContext CreateContextFromRoot()
        {
            if (this.rootObject != null)
            {
                SerializationContextAttribute attribute = TypeDescriptor.GetAttributes(this.rootObject)[typeof(SerializationContextAttribute)] as SerializationContextAttribute;
                if (attribute != null)
                {
                    return attribute.CreateSerializationContext();
                }
            }
            return null;
        }

        protected virtual SerializationContext CreateSerializationContext() => 
            new SerializationContext();

        protected internal virtual MethodInfo GetMethod(object obj, string name) => 
            this.fContext.GetMethod(obj, name);

        protected internal virtual string GetMethodName(string prop, string action) => 
            "Xtra" + action + prop;

        protected internal virtual string GetMethodNameItem(string prop, string action) => 
            this.GetMethodName(prop, action) + "Item";

        protected List<SerializablePropertyDescriptorPair> GetProperties(object obj) => 
            this.GetProperties(obj, null);

        protected virtual List<SerializablePropertyDescriptorPair> GetProperties(object obj, IXtraPropertyCollection store)
        {
            PropertyDescriptorCollection properties = this.fContext.GetProperties(obj, store);
            int count = properties.Count;
            List<SerializablePropertyDescriptorPair> pairsList = new List<SerializablePropertyDescriptorPair>(count);
            for (int i = 0; i < count; i++)
            {
                PropertyDescriptor descriptor = properties[i];
                XtraSerializableProperty xtraSerializableProperty = this.GetXtraSerializableProperty(obj, descriptor);
                pairsList.Add(new SerializablePropertyDescriptorPair(descriptor, xtraSerializableProperty));
            }
            this.fContext.CustomGetSerializableProperties(obj, pairsList, properties);
            return pairsList;
        }

        protected internal int GetPropertyId(PropertyDescriptor property)
        {
            XtraSerializablePropertyId id = property.Attributes[typeof(XtraSerializablePropertyId)] as XtraSerializablePropertyId;
            return ((id != null) ? id.Id : 0);
        }

        internal XtraSerializableProperty GetXtraSerializableProperty(object obj, PropertyDescriptor property) => 
            this.fContext.GetXtraSerializableProperty(obj, property);

        protected bool ShouldNotTryCache(XtraSerializableProperty attr) => 
            !attr.IsCachedProperty || !(this.RootObject is IXtraRootSerializationObject);

        protected internal virtual IList<SerializablePropertyDescriptorPair> SortProps(object obj, List<SerializablePropertyDescriptorPair> pairsList) => 
            this.fContext.SortProps(obj, pairsList);

        protected internal SerializationContext Context
        {
            get => 
                this.fContext;
            set => 
                this.fContext = value;
        }

        internal object RootObject =>
            this.rootObject;

        internal IXtraRootSerializationObject RootSerializationObject =>
            this.RootObject as IXtraRootSerializationObject;
    }
}

