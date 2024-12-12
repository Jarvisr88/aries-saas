namespace DevExpress.Data.Access
{
    using System;
    using System.ComponentModel;

    public class SimpleListPropertyDescriptor : PropertyDescriptor
    {
        public SimpleListPropertyDescriptor();
        public override bool CanResetValue(object component);
        public override object GetValue(object component);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        public override bool IsReadOnly { get; }

        public override string Category { get; }

        public override Type PropertyType { get; }

        public override Type ComponentType { get; }
    }
}

