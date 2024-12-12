namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.ComponentModel;

    public sealed class SerializablePropertyDescriptorPair
    {
        private readonly PropertyDescriptor descriptor;
        private readonly XtraSerializableProperty attribute;

        public SerializablePropertyDescriptorPair(PropertyDescriptor descriptor, XtraSerializableProperty attribute)
        {
            this.descriptor = descriptor;
            this.attribute = attribute;
        }

        public override string ToString()
        {
            string str = string.Empty;
            if (this.attribute != null)
            {
                str = $"{this.attribute.Order} {this.attribute.Flags}";
            }
            return $"{this.descriptor.Name} ({this.descriptor.PropertyType.Name}) {str}";
        }

        public PropertyDescriptor Property =>
            this.descriptor;

        public XtraSerializableProperty Attribute =>
            this.attribute;
    }
}

