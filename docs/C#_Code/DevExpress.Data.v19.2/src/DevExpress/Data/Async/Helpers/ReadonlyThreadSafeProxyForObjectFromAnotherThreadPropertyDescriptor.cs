namespace DevExpress.Data.Async.Helpers
{
    using System;
    using System.ComponentModel;

    public class ReadonlyThreadSafeProxyForObjectFromAnotherThreadPropertyDescriptor : PropertyDescriptor
    {
        private readonly System.Type Type;
        private readonly int Index;
        private readonly string displayName;

        public ReadonlyThreadSafeProxyForObjectFromAnotherThreadPropertyDescriptor(PropertyDescriptor proto, int index);
        public override bool CanResetValue(object component);
        public override object GetValue(object component);
        public override void ResetValue(object component);
        public override void SetValue(object component, object value);
        public override bool ShouldSerializeValue(object component);

        public override System.Type ComponentType { get; }

        public override bool IsReadOnly { get; }

        public override System.Type PropertyType { get; }

        public override string DisplayName { get; }
    }
}

