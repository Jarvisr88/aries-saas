namespace DevExpress.Utils
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Drawing;
    using System.Globalization;
    using System.Reflection;

    public class PointFloatConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            (sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType);

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            (destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;
            if (str == null)
            {
                return base.ConvertFrom(context, culture, value);
            }
            str = str.Trim();
            if (str.Length == 0)
            {
                return null;
            }
            culture ??= CultureInfo.CurrentCulture;
            char listSeparator = culture.GetListSeparator();
            char[] separator = new char[] { listSeparator };
            string[] strArray = str.Split(separator);
            float[] numArray = new float[strArray.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(float));
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (float) converter.ConvertFromString(context, culture, strArray[i]);
            }
            if (numArray.Length == 2)
            {
                return new PointFloat(numArray[0], numArray[1]);
            }
            object[] args = new object[] { str, "x" + listSeparator.ToString() + " y" };
            throw new ArgumentException(SRGetString("TextParseFailedFormat", args));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if ((destinationType == typeof(string)) && (value is PointFloat))
            {
                PointFloat num = (PointFloat) value;
                culture ??= CultureInfo.CurrentCulture;
                TypeConverter singleConverter = this.GetSingleConverter();
                string[] textArray1 = new string[] { singleConverter.ConvertToString(context, culture, num.X), singleConverter.ConvertToString(context, culture, num.Y) };
                return string.Join(culture.GetListSeparator().ToString() + " ", textArray1);
            }
            if ((destinationType == typeof(InstanceDescriptor)) && (value is PointFloat))
            {
                PointFloat num2 = (PointFloat) value;
                Type[] types = new Type[] { typeof(float), typeof(float) };
                ConstructorInfo constructor = typeof(PointFloat).GetConstructor(types);
                if (constructor != null)
                {
                    object[] arguments = new object[] { num2.X, num2.Y };
                    return new InstanceDescriptor(constructor, arguments);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            object obj2 = propertyValues["X"];
            object obj3 = propertyValues["Y"];
            if ((obj2 == null) || ((obj3 == null) || (!(obj2 is float) || !(obj3 is float))))
            {
                throw new ArgumentException(SRGetString("PropertyValueInvalidEntry"));
            }
            return new PointFloat((float) obj2, (float) obj3);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            string[] names = new string[] { "X", "Y" };
            return TypeDescriptor.GetProperties(typeof(PointFloat), attributes).Sort(names);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => 
            true;

        protected virtual TypeConverter GetSingleConverter() => 
            TypeDescriptor.GetConverter(typeof(float));

        private static string SRGetString(string str)
        {
            Type[] types = new Type[] { typeof(string) };
            object[] parameters = new object[] { str };
            return (string) SR.GetMethod("GetString", types).Invoke(null, parameters);
        }

        private static string SRGetString(string str, params object[] args)
        {
            Type[] types = new Type[] { typeof(string), typeof(object[]) };
            object[] parameters = new object[] { str, args };
            return (string) SR.GetMethod("GetString", types).Invoke(null, parameters);
        }

        private static Type SR =>
            typeof(PointF).Assembly.GetType("System.Drawing.SR");
    }
}

