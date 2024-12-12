namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.ComponentModel;

    public class EditorsDataControllerTypedListDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor descriptor;

        public EditorsDataControllerTypedListDescriptor(PropertyDescriptor descriptor) : base(descriptor.Name, null)
        {
            this.descriptor = descriptor;
        }

        public override bool CanResetValue(object component) => 
            this.descriptor.CanResetValue(component);

        public override object GetValue(object component) => 
            this.descriptor.GetValue(component);

        public override void ResetValue(object component)
        {
            this.descriptor.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            this.descriptor.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component) => 
            this.descriptor.ShouldSerializeValue(component);

        public override Type ComponentType =>
            this.descriptor.ComponentType;

        public override bool IsReadOnly =>
            this.descriptor.IsReadOnly;

        public override Type PropertyType =>
            typeof(object);
    }
}

