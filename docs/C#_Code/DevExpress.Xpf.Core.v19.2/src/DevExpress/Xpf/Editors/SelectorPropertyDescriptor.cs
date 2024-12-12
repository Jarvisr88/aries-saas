namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Access;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class SelectorPropertyDescriptor : PropertyDescriptor
    {
        private readonly Dictionary<Type, PropertyDescriptor> descriptors;
        private readonly bool returnComponent;

        public SelectorPropertyDescriptor(string path) : this(string.IsNullOrEmpty(path) ? "empty" : path, null)
        {
            this.descriptors = new Dictionary<Type, PropertyDescriptor>();
            this.returnComponent = string.IsNullOrEmpty(path);
            this.Path = path;
        }

        public override bool CanResetValue(object component) => 
            false;

        public override object GetValue(object component)
        {
            PropertyDescriptor descriptor;
            if ((component == null) || this.returnComponent)
            {
                return component;
            }
            Type key = component.GetType();
            if (!this.descriptors.TryGetValue(key, out descriptor))
            {
                descriptor = new ComplexPropertyDescriptorReflection(component, this.Path);
                this.descriptors.Add(key, descriptor);
            }
            return descriptor.GetValue(component);
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
        }

        public override bool ShouldSerializeValue(object obj) => 
            false;

        private string Path { get; set; }

        public override Type ComponentType =>
            typeof(object);

        public override bool IsReadOnly =>
            false;

        public override Type PropertyType =>
            typeof(object);
    }
}

