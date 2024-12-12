namespace DevExpress.Data
{
    using System;
    using System.ComponentModel;

    public class RealTimePropertyDescriptor : PropertyDescriptor
    {
        private readonly int index;
        private readonly Type propertyType;
        private readonly string message;
        private readonly bool visible;
        private readonly Func<object, object> getFunc;

        public RealTimePropertyDescriptor(string name, string message);
        public RealTimePropertyDescriptor(string name, PropertyDescriptor prototype, int index, bool visible);
        public override bool CanResetValue(object component);
        public object GetSourceValue(object component);
        public override object GetValue(object component);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        public bool Visible { get; }

        public int Index { get; }

        public override Type ComponentType { get; }

        public override bool IsReadOnly { get; }

        public override Type PropertyType { get; }
    }
}

