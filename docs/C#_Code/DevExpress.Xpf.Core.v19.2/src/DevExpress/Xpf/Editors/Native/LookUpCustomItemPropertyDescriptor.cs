namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Editors;
    using System;

    public class LookUpCustomItemPropertyDescriptor : LookUpPropertyDescriptorBase
    {
        public LookUpCustomItemPropertyDescriptor(LookUpPropertyDescriptorType descriptorType, string path) : base(descriptorType, path, null)
        {
        }

        protected override object GetValueImpl(object component)
        {
            ICustomItem item = (ICustomItem) component;
            if (base.DescriptorType == LookUpPropertyDescriptorType.Display)
            {
                return item.DisplayValue;
            }
            if (base.DescriptorType != LookUpPropertyDescriptorType.Value)
            {
                throw new ArgumentException("Path");
            }
            return item.EditValue;
        }

        public override bool IsRelevant(string internalPath) => 
            true;

        protected override void SetValueImpl(object component, object value)
        {
            ICustomItem item = (ICustomItem) component;
            if (base.DescriptorType == LookUpPropertyDescriptorType.Display)
            {
                item.DisplayValue = value;
            }
            if (base.DescriptorType == LookUpPropertyDescriptorType.Value)
            {
                item.EditValue = value;
            }
            throw new ArgumentException("Path");
        }
    }
}

