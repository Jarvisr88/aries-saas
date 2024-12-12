namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;

    public class DXAdvPropertyDescriptor : DXPropertyDescriptor
    {
        private readonly PropertyDescriptor originalDescriptor;

        public DXAdvPropertyDescriptor(PropertyDescriptor sourceDescriptor) : base(sourceDescriptor)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(base.SourceDescriptor.ComponentType);
            if (properties != null)
            {
                this.originalDescriptor = properties[base.SourceDescriptor.Name];
            }
        }

        public override bool ShouldSerializeValue(object component) => 
            (this.OriginalDescriptor == null) ? base.ShouldSerializeValue(component) : this.OriginalDescriptor.ShouldSerializeValue(component);

        protected virtual PropertyDescriptor OriginalDescriptor =>
            this.originalDescriptor;
    }
}

