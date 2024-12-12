namespace DevExpress.Xpf.Editors.Native
{
    using DevExpress.Xpf.Editors.EditStrategy;
    using System;
    using System.Windows.Controls;

    public class LookUpListBoxItemPropertyDescriptor : LookUpPropertyDescriptorBase
    {
        public LookUpListBoxItemPropertyDescriptor(LookUpPropertyDescriptorType descriptorType, string path) : base(descriptorType, path, null)
        {
        }

        protected override object GetValueImpl(object component) => 
            ((ListBoxItem) component).Content ?? LookUpEditStrategyBase.SpecialNull;

        public override bool IsRelevant(string internalPath) => 
            true;

        protected override void SetValueImpl(object component, object value)
        {
            ((ListBoxItem) component).Content = value;
        }
    }
}

