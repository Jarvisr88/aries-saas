namespace DevExpress.Utils.Design
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;

    public class PointFTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;
            if (str == null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            string str2 = str.Trim();
            if (str2.Length == 0)
            {
                return null;
            }
            culture ??= CultureInfo.CurrentCulture;
            char listSeparator = culture.GetListSeparator();
            char[] separator = new char[] { listSeparator };
            string[] strArray = str2.Split(separator);
            float[] numArray = new float[strArray.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (float) converter.ConvertFromString(context, culture, strArray[i]);
            }
            if (numArray.Length != 2)
            {
                throw new ArgumentException("value");
            }
            return new PointF(numArray[0], numArray[1]);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (!(destinationType == typeof(string)) || !(value is PointF))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            PointF tf = (PointF) value;
            culture ??= CultureInfo.CurrentCulture;
            string[] strArray = new string[] { SingleTypeConverter.ToString(context, culture, tf.X), SingleTypeConverter.ToString(context, culture, tf.Y) };
            return string.Join(culture.GetListSeparator().ToString() + " ", strArray);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues) => 
            new PointF((float) propertyValues["X"], (float) propertyValues["Y"]);

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => 
            true;

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            string[] names = new string[] { "X", "Y" };
            return PathProperties(typeof(PointF), attributes).Sort(names);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => 
            true;

        private static PropertyDescriptorCollection PathProperties(Type type, Attribute[] attributes)
        {
            List<PropertyDescriptor> list = new List<PropertyDescriptor>();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(type, attributes))
            {
                Attribute[] attributeArray1 = new Attribute[] { new TypeConverterAttribute(typeof(SingleTypeConverter)) };
                list.Add(TypeDescriptorHelper.CreateProperty(type, descriptor, attributeArray1));
            }
            return new PropertyDescriptorCollection(list.ToArray());
        }
    }
}

