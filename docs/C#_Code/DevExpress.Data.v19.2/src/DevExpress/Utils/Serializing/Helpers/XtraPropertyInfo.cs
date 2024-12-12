namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Data.Internal;
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class XtraPropertyInfo
    {
        private readonly bool isKey;
        private IXtraPropertyCollection childProperties;
        private DevExpress.Utils.Serializing.Helpers.ObjectConverterImplementation objectConverterImpl;
        public object Value;

        public XtraPropertyInfo() : this("", typeof(object), null)
        {
        }

        public XtraPropertyInfo(XtraObjectInfo info)
        {
            this.Name = MakeXtraObjectInfoName(info.Name);
            this.PropertyType = typeof(string);
            this.Value = info.Name;
            this.isKey = true;
            this.childProperties = new XtraPropertyCollection();
            this.SetObjectConverterImpl(ObjectConverter.Instance);
        }

        public XtraPropertyInfo(string name) : this(name, typeof(object), null)
        {
        }

        public XtraPropertyInfo(XtraObjectInfo info, IXtraPropertyCollection childProperties) : this(info)
        {
            this.childProperties = childProperties;
        }

        public XtraPropertyInfo(string name, Type propertyType, object val) : this(name, propertyType, val, false)
        {
        }

        public XtraPropertyInfo(string name, Type propertyType, object val, bool isKey)
        {
            this.Name = name;
            this.PropertyType = propertyType;
            this.Value = val;
            this.isKey = isKey;
            if (this.IsKey)
            {
                this.childProperties = this.CreateXtraPropertyCollection();
            }
            this.SetObjectConverterImpl(ObjectConverter.Instance);
        }

        protected virtual IXtraPropertyCollection CreateXtraPropertyCollection() => 
            new XtraPropertyCollection();

        protected internal XtraPropertyInfo EnsureIsPrimitive(DevExpress.Utils.Serializing.Helpers.ObjectConverterImplementation converter)
        {
            if (this.Value == null)
            {
                return this;
            }
            Type nullableType = this.Value.GetType();
            nullableType = Nullable.GetUnderlyingType(nullableType) ?? nullableType;
            XtraPropertyInfo instance = this;
            if (converter == null)
            {
                instance = (XtraPropertyInfo) ObjectConverter.Instance;
            }
            return converter.EnsurePropertyTypeOrSkipValue(nullableType, (DevExpress.Utils.Serializing.Helpers.ObjectConverterImplementation) instance);
        }

        private XtraPropertyInfo EnsurePropertyTypeOrSkipValue(Type valueType, DevExpress.Utils.Serializing.Helpers.ObjectConverterImplementation converter)
        {
            bool flag;
            bool flag1;
            if (IsPrimitiveType(valueType))
            {
                flag1 = true;
            }
            else
            {
                flag1 = this.IsConvertible = converter.CanConvertToString(valueType);
            }
            this.IsPrimitive = flag = flag1;
            if (!flag)
            {
                this.Value = null;
                this.IsNull = true;
                XtraSerializationSecurityTrace.NonPrimitiveValueForPropertyException.Mute(this.Name);
            }
            else if (Equals(typeof(object), this.PropertyType) || Equals(null, this.PropertyType))
            {
                this.PropertyType = valueType;
            }
            return this;
        }

        protected internal static bool IsPrimitiveOrTag(XtraSerializableProperty property, PropertyDescriptor descriptor) => 
            (property.Visibility != XtraSerializationVisibility.Primitive) ? (!property.IsExactVisible && ((descriptor.Name == "Tag") && (descriptor.PropertyType == typeof(object)))) : true;

        protected internal static bool IsPrimitiveType(Type type) => 
            type.IsPrimitive || ((type == typeof(string)) || ((type == typeof(decimal)) || ((type == typeof(DateTime)) || ((type == typeof(TimeSpan)) || (type == typeof(Guid))))));

        public static string MakeXtraObjectInfoName(string name) => 
            "$" + name;

        public void SetObjectConverterImpl(DevExpress.Utils.Serializing.Helpers.ObjectConverterImplementation objectConverterImpl)
        {
            this.objectConverterImpl = objectConverterImpl;
        }

        public static bool ShouldSerializePropertyType(XtraPropertyInfo pInfo, out string type, Func<object, string> getValueType)
        {
            bool flag = false;
            type = null;
            if (flag = Equals(pInfo.PropertyType, typeof(object)) || pInfo.IsPrimitive)
            {
                type = pInfo.IsPrimitive ? pInfo.PropertyType.FullName : getValueType(pInfo.Value);
            }
            return flag;
        }

        public override string ToString() => 
            (this.Name != null) ? this.Name : string.Empty;

        public virtual object ValueToObject(Type type) => 
            !(this.Value is string) ? this.Value : this.ObjectConverterImplementation.StringToObject(this.Value.ToString(), type);

        public bool IsXtraObjectInfo =>
            (this.Name.Length > 0) && (this.Name[0] == '$');

        protected internal bool IsPrimitive { get; private set; }

        protected internal bool IsConvertible { get; private set; }

        protected virtual DevExpress.Utils.Serializing.Helpers.ObjectConverterImplementation ObjectConverterImplementation =>
            this.objectConverterImpl;

        public string Name { get; set; }

        public Type PropertyType { get; set; }

        public bool IsKey =>
            this.isKey;

        public bool IsNull { get; set; }

        public IXtraPropertyCollection ChildProperties
        {
            get => 
                this.childProperties;
            protected set => 
                this.childProperties = value;
        }

        public bool HasChildren =>
            (this.ChildProperties != null) && (this.ChildProperties.Count > 0);
    }
}

