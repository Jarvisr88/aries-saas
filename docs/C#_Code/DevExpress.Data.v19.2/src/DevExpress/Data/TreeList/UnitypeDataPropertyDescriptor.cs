namespace DevExpress.Data.TreeList
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class UnitypeDataPropertyDescriptor : PropertyDescriptor
    {
        private Dictionary<Type, PropertyDescriptor> descriptorCache;
        private bool isReadOnly;
        private Type propertyType;
        private bool usePropertyType;
        private PropertyDescriptor rootPropertyDescriptor;

        public UnitypeDataPropertyDescriptor(PropertyDescriptor propertyDescriptor, bool usePropertyType);
        public UnitypeDataPropertyDescriptor(string propName, bool isReadOnly);
        public override bool CanResetValue(object component);
        protected virtual object ConvertValue(object val, PropertyDescriptor descriptor);
        protected override AttributeCollection CreateAttributeCollection();
        protected Type GetDataType(PropertyDescriptor descriptor);
        public virtual PropertyDescriptor GetPropertyDescriptor(object component);
        public override object GetValue(object component);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        public override Type PropertyType { get; }

        public override bool IsReadOnly { get; }

        public override string Category { get; }

        public override Type ComponentType { get; }
    }
}

