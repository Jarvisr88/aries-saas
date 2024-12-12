namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;

    public class SizeFTypeConverter : SizeFConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (!(destinationType == typeof(string)) || !(value is SizeF))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            SizeF ef = (SizeF) value;
            culture ??= CultureInfo.CurrentCulture;
            string[] strArray = new string[] { SingleTypeConverter.ToString(context, culture, ef.Width), SingleTypeConverter.ToString(context, culture, ef.Height) };
            return string.Join(culture.GetListSeparator().ToString() + " ", strArray);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) => 
            PathProperties(typeof(SizeF), attributes);

        private static PropertyDescriptorCollection PathProperties(Type type, Attribute[] attributes)
        {
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(type, attributes))
            {
                string resourceName = $"{descriptor.ComponentType}.{descriptor.Name}";
                Attribute[] attributeArray1 = new Attribute[] { new TypeConverterAttribute(typeof(SingleTypeConverter)), new DXDisplayNameAttribute(typeof(ResFinder), resourceName) };
                list.Add(TypeDescriptorHelper.CreateProperty(type, descriptor, attributeArray1));
            }
            string[] names = new string[] { "Width", "Height" };
            return new PropertyDescriptorCollection(list.ToArray()).Sort(names);
        }
    }
}

