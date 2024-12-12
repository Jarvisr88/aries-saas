namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Controls;

    public abstract class LookUpPropertyDescriptorBase : PropertyDescriptor
    {
        public static readonly object UnsetValue = new object();

        protected LookUpPropertyDescriptorBase(LookUpPropertyDescriptorType descriptorType, string path, string internalPath) : base(path, null)
        {
            this.Path = path;
            this.InternalPath = internalPath;
            this.DescriptorType = descriptorType;
        }

        public override bool CanResetValue(object component) => 
            false;

        public static LookUpPropertyDescriptorBase CreatePropertyDescriptor(Type componentType, LookUpPropertyDescriptorType descriptorType, string path, string internalPath = null) => 
            !typeof(ICustomItem).IsAssignableFrom(componentType) ? (!typeof(ListBoxItem).IsAssignableFrom(componentType) ? (string.IsNullOrEmpty(internalPath) ? ((LookUpPropertyDescriptorBase) new LookUpGetItemPropertyDescriptor(descriptorType, path, internalPath)) : ((LookUpPropertyDescriptorBase) new LookUpGetPropertyPropertyDescriptor(descriptorType, path, internalPath))) : ((LookUpPropertyDescriptorBase) new LookUpListBoxItemPropertyDescriptor(descriptorType, path))) : ((LookUpPropertyDescriptorBase) new LookUpCustomItemPropertyDescriptor(descriptorType, path));

        public override object GetValue(object component) => 
            this.GetValueImpl(component);

        protected abstract object GetValueImpl(object component);
        public virtual bool IsRelevant(string internalPath) => 
            (internalPath != null) && ((this.InternalPath != null) && (internalPath == this.InternalPath));

        public static bool IsUnsetValue(object value) => 
            UnsetValue == value;

        public virtual void Reset()
        {
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            this.SetValueImpl(component, value);
        }

        protected abstract void SetValueImpl(object component, object value);
        public override bool ShouldSerializeValue(object component) => 
            false;

        protected string Path { get; private set; }

        protected string InternalPath { get; private set; }

        protected LookUpPropertyDescriptorType DescriptorType { get; private set; }

        public override string DisplayName =>
            this.Path;

        public override Type ComponentType =>
            typeof(object);

        public override string Name =>
            this.Path;

        public override bool IsReadOnly =>
            false;

        public override Type PropertyType =>
            typeof(object);
    }
}

