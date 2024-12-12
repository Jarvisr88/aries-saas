namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public class EditorsDataControllerWrappedDescriptor : LookUpPropertyDescriptorBase
    {
        public EditorsDataControllerWrappedDescriptor(LookUpPropertyDescriptorType descriptorType, string path, string internalPath) : base(descriptorType, path, internalPath)
        {
            this.BaseDescriptor = new LookUpPropertyDescriptor(descriptorType, path, internalPath);
        }

        protected override object GetValueImpl(object component)
        {
            object obj2 = this.BaseDescriptor.GetValue(component);
            return (IsUnsetValue(obj2) ? null : obj2);
        }

        protected override void SetValueImpl(object component, object value)
        {
            this.BaseDescriptor.SetValue(component, value);
        }

        private LookUpPropertyDescriptorBase BaseDescriptor { get; set; }
    }
}

