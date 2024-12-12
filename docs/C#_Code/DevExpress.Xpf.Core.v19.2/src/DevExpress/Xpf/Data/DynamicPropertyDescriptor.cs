namespace DevExpress.Xpf.Data
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;

    public class DynamicPropertyDescriptor : PropertyDescriptor
    {
        private readonly Type propertyType;
        private readonly Func<object, object> getValue;

        public DynamicPropertyDescriptor(string name, Type propertyType, Func<object, object> getValue, Attribute[] attributes = null) : base(name, attributes)
        {
            this.propertyType = propertyType;
            this.getValue = getValue;
        }

        public override bool CanResetValue(object component) => 
            false;

        public override object GetValue(object component) => 
            this.getValue(component);

        public override void ResetValue(object component)
        {
            throw new NotImplementedException();
        }

        public override void SetValue(object component, object value)
        {
            throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component)
        {
            throw new NotImplementedException();
        }

        public override Type PropertyType =>
            this.propertyType;

        public override bool IsReadOnly =>
            true;

        public override Type ComponentType =>
            typeof(object);
    }
}

