namespace DevExpress.Utils.Design
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;

    public class BooleanTypeConverter : BooleanConverter
    {
        private static Dictionary<object, DXDisplayNameAttribute> names = new Dictionary<object, DXDisplayNameAttribute>();

        static BooleanTypeConverter()
        {
            names.Add(true, GetDisplayName(true));
            names.Add(false, GetDisplayName(false));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                object booleanValueFromDiplayName = GetBooleanValueFromDiplayName((string) value);
                value = (booleanValueFromDiplayName != null) ? booleanValueFromDiplayName.ToString() : value;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                DXDisplayNameAttribute attribute;
                if (value is string)
                {
                    return value;
                }
                if ((value != null) && names.TryGetValue(value, out attribute))
                {
                    return this.GetDisplayName(attribute);
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        private static object GetBooleanValueFromDiplayName(string displayName)
        {
            object key;
            using (Dictionary<object, DXDisplayNameAttribute>.Enumerator enumerator = names.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<object, DXDisplayNameAttribute> current = enumerator.Current;
                        if (StringExtensions.CompareInvariantCultureIgnoreCase(current.Value.DisplayName, displayName) != 0)
                        {
                            continue;
                        }
                        key = current.Key;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return key;
        }

        private static DXDisplayNameAttribute GetDisplayName(bool value) => 
            new DXDisplayNameAttribute(typeof(ResFinder), "PropertyNamesRes", $"{value.GetType().FullName}.{value.ToString()}");

        protected virtual string GetDisplayName(DXDisplayNameAttribute attr) => 
            attr.DisplayName;
    }
}

