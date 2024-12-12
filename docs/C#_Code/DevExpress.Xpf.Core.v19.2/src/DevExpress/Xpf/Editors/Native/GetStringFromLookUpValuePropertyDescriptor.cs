namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class GetStringFromLookUpValuePropertyDescriptor : LookUpPropertyDescriptorBase
    {
        public GetStringFromLookUpValuePropertyDescriptor(System.ComponentModel.PropertyDescriptor propertyDescriptor) : base(LookUpPropertyDescriptorType.Display, propertyDescriptor.Name, propertyDescriptor.Name)
        {
            Guard.ArgumentNotNull(propertyDescriptor, "PropertyDescriptor");
            this.PropertyDescriptor = propertyDescriptor;
        }

        protected override object GetValueImpl(object component)
        {
            object obj2 = this.PropertyDescriptor.GetValue(component);
            if (IsUnsetValue(obj2))
            {
                obj2 = null;
            }
            return obj2?.ToString();
        }

        protected override void SetValueImpl(object component, object value)
        {
        }

        public System.ComponentModel.PropertyDescriptor PropertyDescriptor { get; private set; }
    }
}

