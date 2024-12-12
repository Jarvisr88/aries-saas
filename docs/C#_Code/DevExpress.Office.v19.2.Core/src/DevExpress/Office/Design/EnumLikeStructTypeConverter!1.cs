namespace DevExpress.Office.Design
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Globalization;
    using System.Reflection;
    using System.Security;

    public class EnumLikeStructTypeConverter<T> : TypeConverter
    {
        private static readonly Dictionary<T, FieldInfo> fieldsTable;
        private static readonly Dictionary<T, PropertyInfo> propertiesTable;

        static EnumLikeStructTypeConverter()
        {
            EnumLikeStructTypeConverter<T>.fieldsTable = EnumLikeStructTypeConverter<T>.CreateFieldsTable();
            EnumLikeStructTypeConverter<T>.propertiesTable = EnumLikeStructTypeConverter<T>.CreatePropertiesTable();
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => 
            !(sourceType == typeof(string)) ? base.CanConvertFrom(context, sourceType) : true;

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => 
            !(destinationType == typeof(string)) ? (!(destinationType == typeof(InstanceDescriptor)) ? base.CanConvertTo(context, destinationType) : true) : true;

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => 
            !(value.GetType() == typeof(string)) ? base.ConvertFrom(context, culture, value) : this.ConvertFromStringCore(context, culture, (string) value);

        protected internal virtual T ConvertFromStringCore(ITypeDescriptorContext context, CultureInfo culture, string value)
        {
            T local;
            using (Dictionary<T, FieldInfo>.ValueCollection.Enumerator enumerator = EnumLikeStructTypeConverter<T>.fieldsTable.Values.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        FieldInfo current = enumerator.Current;
                        if (current.Name != value)
                        {
                            continue;
                        }
                        local = (T) current.GetValue(null);
                    }
                    else
                    {
                        return default(T);
                    }
                    break;
                }
            }
            return local;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => 
            !(destinationType == typeof(string)) ? (!(destinationType == typeof(InstanceDescriptor)) ? base.ConvertTo(context, culture, value, destinationType) : this.ConvertToInstanceDescriptor(context, (T) value)) : this.ConvertToStringCore(context, culture, (T) value);

        [SecuritySafeCritical]
        protected internal virtual InstanceDescriptor ConvertToInstanceDescriptor(ITypeDescriptorContext context, T value)
        {
            FieldInfo info;
            return (!EnumLikeStructTypeConverter<T>.fieldsTable.TryGetValue(value, out info) ? null : new InstanceDescriptor(info, null));
        }

        protected internal virtual string ConvertToStringCore(ITypeDescriptorContext context, CultureInfo culture, T value)
        {
            FieldInfo info;
            return (!EnumLikeStructTypeConverter<T>.fieldsTable.TryGetValue(value, out info) ? value.ToString() : info.Name);
        }

        private static Dictionary<T, FieldInfo> CreateFieldsTable()
        {
            Dictionary<T, FieldInfo> dictionary = new Dictionary<T, FieldInfo>();
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
            int length = fields.Length;
            for (int i = 0; i < length; i++)
            {
                FieldInfo info = fields[i];
                if ((info.FieldType == typeof(T)) && info.IsInitOnly)
                {
                    object[] customAttributes = info.GetCustomAttributes(typeof(BrowsableAttribute), true);
                    if ((customAttributes == null) || ((customAttributes.Length == 0) || ((BrowsableAttribute) customAttributes[0]).Browsable))
                    {
                        dictionary.Add((T) info.GetValue(null), info);
                    }
                }
            }
            return dictionary;
        }

        private static Dictionary<T, PropertyInfo> CreatePropertiesTable()
        {
            Dictionary<T, PropertyInfo> dictionary = new Dictionary<T, PropertyInfo>();
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Static);
            int length = properties.Length;
            for (int i = 0; i < length; i++)
            {
                PropertyInfo info = properties[i];
                if ((info.PropertyType == typeof(T)) && info.CanWrite)
                {
                    dictionary.Add((T) info.GetValue(null, new object[0]), info);
                }
            }
            return dictionary;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<T> values = new List<T>();
            foreach (T local in EnumLikeStructTypeConverter<T>.fieldsTable.Keys)
            {
                values.Add(local);
            }
            foreach (T local2 in EnumLikeStructTypeConverter<T>.propertiesTable.Keys)
            {
                values.Add(local2);
            }
            return new TypeConverter.StandardValuesCollection(values);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) => 
            true;

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) => 
            true;
    }
}

