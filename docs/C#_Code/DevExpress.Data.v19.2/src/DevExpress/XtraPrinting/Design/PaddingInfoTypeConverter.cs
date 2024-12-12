namespace DevExpress.XtraPrinting.Design
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;

    public class PaddingInfoTypeConverter : ExpandableObjectConverter
    {
        private static readonly string[] SortOrderPaddingInfoTypeDescriptor = new string[] { "All", "Left", "Right", "Top", "Bottom" };

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
            {
                return base.ConvertFrom(context, culture, value);
            }
            string a = ((string) value).Trim();
            if (string.Equals(a, "Undefined", StringComparison.OrdinalIgnoreCase))
            {
                return PaddingInfo.Undefined;
            }
            culture = this.ValidateCulture(culture);
            char[] separator = new char[] { culture.GetListSeparator() };
            string[] strArray = a.Split(separator);
            if (strArray.Length == 0)
            {
                throw new ArgumentException("TextParseFailedFormat");
            }
            int[] numArray = new int[4];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
            if (strArray.Length != 1)
            {
                for (int i = 0; (i < strArray.Length) && (i < numArray.Length); i++)
                {
                    numArray[i] = (int) converter.ConvertFromString(context, culture, strArray[i]);
                }
            }
            else
            {
                int num2 = (int) converter.ConvertFromString(context, culture, strArray[0]);
                numArray = new int[] { num2, num2, num2, num2 };
            }
            return new PaddingInfo(numArray[0], numArray[1], numArray[2], numArray[3], (context != null) ? this.GetDpi(context) : 300f);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType == typeof(string)) && (value is PaddingInfo))
            {
                PaddingInfo info = (PaddingInfo) value;
                return string.Format("{1}{0} {2}{0} {3}{0} {4}", new object[] { this.GetListSeparator(culture), info.Left, info.Right, info.Top, info.Bottom });
            }
            if (!(destinationType == typeof(InstanceDescriptor)))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            PaddingInfo info2 = (PaddingInfo) value;
            Type[] types = new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(float) };
            object[] arguments = new object[] { info2.Left, info2.Right, info2.Top, info2.Bottom, info2.Dpi };
            return new InstanceDescriptor(value.GetType().GetConstructor(types), arguments);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            PaddingInfo info = (PaddingInfo) context.PropertyDescriptor.GetValue(context.Instance);
            int all = (int) propertyValues["All"];
            if (info.All != all)
            {
                return new PaddingInfo(all, info.Dpi);
            }
            object obj3 = propertyValues["Right"];
            object obj4 = propertyValues["Top"];
            object obj5 = propertyValues["Bottom"];
            return new PaddingInfo(ToInt32(propertyValues["Left"]), ToInt32(obj3), ToInt32(obj4), ToInt32(obj5), info.Dpi);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) => 
            true;

        protected float GetDpi(ITypeDescriptorContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            if ((context.Instance != null) && (context.PropertyDescriptor != null))
            {
                try
                {
                    object obj2 = context.PropertyDescriptor.GetValue(context.Instance);
                    if (obj2 is PaddingInfo)
                    {
                        return ((PaddingInfo) obj2).Dpi;
                    }
                }
                catch
                {
                }
            }
            return this.GetDpi(context.Instance);
        }

        private float GetDpi(object obj) => 
            (obj is Array) ? this.GetDpiFromArray((Array) obj) : ((obj != null) ? this.GetDpiFromObject(obj) : -1f);

        private float GetDpiFromArray(Array array)
        {
            float num2;
            using (IEnumerator enumerator = array.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        object current = enumerator.Current;
                        float dpiFromObject = this.GetDpiFromObject(current);
                        if (dpiFromObject <= 0f)
                        {
                            continue;
                        }
                        num2 = dpiFromObject;
                    }
                    else
                    {
                        return -1f;
                    }
                    break;
                }
            }
            return num2;
        }

        private float GetDpiFromObject(object obj)
        {
            float dpi;
            using (IEnumerator enumerator = TypeDescriptor.GetProperties(obj).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PropertyDescriptor current = (PropertyDescriptor) enumerator.Current;
                        if (!(current.PropertyType == typeof(PaddingInfo)))
                        {
                            continue;
                        }
                        dpi = ((PaddingInfo) current.GetValue(obj)).Dpi;
                    }
                    else
                    {
                        return -1f;
                    }
                    break;
                }
            }
            return dpi;
        }

        private string GetListSeparator(CultureInfo culture) => 
            new string(this.ValidateCulture(culture).GetListSeparator(), 1);

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) => 
            TypeDescriptor.GetProperties(typeof(PaddingInfo), attributes).Sort(SortOrderPaddingInfoTypeDescriptor);

        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => 
            true;

        private static int ToInt32(object value) => 
            (value != null) ? Math.Max(0, Convert.ToInt32(value)) : 0;

        private CultureInfo ValidateCulture(CultureInfo culture) => 
            (culture != null) ? culture : CultureInfo.CurrentCulture;
    }
}

