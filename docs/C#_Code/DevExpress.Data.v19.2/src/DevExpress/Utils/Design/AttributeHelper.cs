namespace DevExpress.Utils.Design
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;

    public class AttributeHelper
    {
        private Type resourceFinder;

        public AttributeHelper(Type resourceFinder)
        {
            this.resourceFinder = resourceFinder;
        }

        public static Attribute[] GetPropertyAttributes(Type resourceFinder, PropertyDescriptor propertyDescriptor)
        {
            List<Attribute> list = new List<Attribute>();
            string resourceName = $"{propertyDescriptor.ComponentType}.{propertyDescriptor.Name}";
            list.Add(new DXDisplayNameAttribute(resourceFinder, resourceName));
            if (propertyDescriptor.PropertyType.Equals(typeof(bool)))
            {
                list.Add(new TypeConverterAttribute(typeof(BooleanTypeConverter)));
            }
            else if (propertyDescriptor.PropertyType.Equals(typeof(Size)))
            {
                list.Add(new TypeConverterAttribute(typeof(SizeTypeConverter)));
            }
            else if (propertyDescriptor.PropertyType.Equals(typeof(SizeF)))
            {
                list.Add(new TypeConverterAttribute(typeof(SizeFTypeConverter)));
            }
            return list.ToArray();
        }

        public virtual PropertyDescriptorCollection LocalizeProperties(PropertyDescriptorCollection properties)
        {
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor descriptor in properties)
            {
                list.Add(TypeDescriptorHelper.CreateProperty(descriptor.ComponentType, descriptor, GetPropertyAttributes(this.resourceFinder, descriptor)));
            }
            return new PropertyDescriptorCollection(list.ToArray());
        }
    }
}

