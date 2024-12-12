namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;

    public class SizeTypeConverter : SizeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) => 
            PathProperties(typeof(Size), attributes);

        private static PropertyDescriptorCollection PathProperties(Type type, Attribute[] attributes)
        {
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(type, attributes))
            {
                string resourceName = $"{descriptor.ComponentType}.{descriptor.Name}";
                Attribute[] attributeArray1 = new Attribute[] { new DXDisplayNameAttribute(typeof(ResFinder), "PropertyNamesRes", resourceName) };
                list.Add(TypeDescriptorHelper.CreateProperty(type, descriptor, attributeArray1));
            }
            string[] names = new string[] { "Width", "Height" };
            return new PropertyDescriptorCollection(list.ToArray()).Sort(names);
        }
    }
}

