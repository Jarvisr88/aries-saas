namespace DevExpress.Data.Access
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;

    public class ComplexPropertyDescriptorReflection : ComplexPropertyDescriptor
    {
        private PropertyDescriptor[] descriptors;

        public ComplexPropertyDescriptorReflection(DataControllerBase controller, string path);
        public ComplexPropertyDescriptorReflection(object sourceObject, string path);
        protected override AttributeCollection CreateAttributeCollection();
        protected virtual PropertyDescriptor GetDescriptor(string name, object obj, Type type);
        public override object GetOwnerOfLast(object component);
        public override object GetValue(object component);
        protected override void Prepare();
        public override void SetValue(object component, object value);

        protected PropertyDescriptor Root { get; }

        protected PropertyDescriptor Last { get; }

        public override bool IsReadOnly { get; }

        public override Type PropertyType { get; }

        public override Type ComponentType { get; }

        public override string DisplayName { get; }
    }
}

