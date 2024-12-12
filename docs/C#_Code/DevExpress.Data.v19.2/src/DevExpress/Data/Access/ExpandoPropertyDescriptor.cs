namespace DevExpress.Data.Access
{
    using DevExpress.Data;
    using System;
    using System.ComponentModel;

    public class ExpandoPropertyDescriptor : PropertyDescriptor
    {
        private Type propertyType;

        public ExpandoPropertyDescriptor(DataControllerBase controller, string name, Type propertyType);
        public override bool CanResetValue(object component);
        public static PropertyDescriptor GetProperty(string name, object obj, Type type);
        public override object GetValue(object component);
        public static bool IsDynamicType(Type type);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        public override bool IsReadOnly { get; }

        public override string Category { get; }

        public override Type PropertyType { get; }

        public override Type ComponentType { get; }

        public override string DisplayName { get; }
    }
}

