namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Data.Access;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Dynamic;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Windows.Interop;

    public class LookUpGetPropertyPropertyDescriptor : LookUpPropertyDescriptorBase
    {
        public LookUpGetPropertyPropertyDescriptor(LookUpPropertyDescriptorType descriptorType, string path, string internalPath) : base(descriptorType, path, internalPath)
        {
        }

        private PropertyDescriptor CreateBaseDescriptor(object component) => 
            !IsComplexColumn(base.InternalPath) ? ((PropertyDescriptor) (this.CreatePropertyAccessDescriptor(component) ?? new LookUpGetItemPropertyDescriptor(base.DescriptorType, base.Path, base.InternalPath))) : ((PropertyDescriptor) new ComplexPropertyDescriptorReflection(component, base.InternalPath));

        protected virtual PropertyDescriptor CreateFastPropertyDescriptor(PropertyDescriptor descriptor) => 
            DataListDescriptor.GetFastProperty(descriptor);

        private PropertyDescriptor CreatePropertyAccessDescriptor(object component)
        {
            if (component is DynamicObject)
            {
                return new DynamicObjectPropertyDescriptor(base.InternalPath);
            }
            if (component is IDictionary<string, object>)
            {
                return new ExpandoPropertyDescriptor(null, base.InternalPath, null);
            }
            PropertyDescriptorCollection listItemProperties = ListBindingHelper.GetListItemProperties(component);
            PropertyDescriptor descriptor = listItemProperties?[base.InternalPath];
            descriptor ??= TypeDescriptor.GetProperties(component)[base.InternalPath];
            return ((descriptor == null) ? null : (this.ShouldCreateFastPropertyDescriptor(component) ? this.CreateFastPropertyDescriptor(descriptor) : descriptor));
        }

        protected override object GetValueImpl(object component)
        {
            this.BaseDescriptor ??= this.CreateBaseDescriptor(component);
            return this.BaseDescriptor.GetValue(component);
        }

        private static bool IsComplexColumn(string member) => 
            !string.IsNullOrEmpty(member) && member.Contains(".");

        public override bool IsRelevant(string internalPath) => 
            ((this.BaseDescriptor == null) || !(this.BaseDescriptor is LookUpPropertyDescriptorBase)) ? base.IsRelevant(internalPath) : ((LookUpPropertyDescriptorBase) this.BaseDescriptor).IsRelevant(base.InternalPath);

        protected override void SetValueImpl(object component, object value)
        {
            this.BaseDescriptor ??= this.CreateBaseDescriptor(component);
            this.BaseDescriptor.SetValue(component, value);
        }

        private bool ShouldCreateFastPropertyDescriptor(object component) => 
            !(component is ICustomTypeDescriptor) ? !BrowserInteropHelper.IsBrowserHosted : false;

        private PropertyDescriptor BaseDescriptor { get; set; }
    }
}

