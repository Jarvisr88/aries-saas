namespace DevExpress.Data.Access
{
    using System;
    using System.ComponentModel;

    public class ReferenceAsCollectionPropertyDescriptor : PropertyDescriptor
    {
        public readonly PropertyDescriptor CollectionDescriptor;

        public ReferenceAsCollectionPropertyDescriptor(PropertyDescriptor originalDescriptor);
        public override bool CanResetValue(object component);
        public override object GetValue(object component);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        public override Type ComponentType { get; }

        public override bool IsReadOnly { get; }

        public override Type PropertyType { get; }
    }
}

