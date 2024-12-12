namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Reflection;

    public class LookUpGetItemPropertyDescriptor : LookUpPropertyDescriptorBase
    {
        private static readonly ReflectionHelper Helper = new ReflectionHelper();

        public LookUpGetItemPropertyDescriptor(LookUpPropertyDescriptorType descriptorType, string path, string internalPath) : base(descriptorType, path, internalPath)
        {
        }

        protected override object GetValueImpl(object component) => 
            !string.IsNullOrEmpty(base.InternalPath) ? LookUpPropertyDescriptorBase.UnsetValue : component;

        public override bool IsRelevant(string internalPath) => 
            true;

        protected override void SetValueImpl(object component, object value)
        {
            Helper.SetPropertyValue(component, base.Path, value, BindingFlags.Public | BindingFlags.Instance);
        }
    }
}

