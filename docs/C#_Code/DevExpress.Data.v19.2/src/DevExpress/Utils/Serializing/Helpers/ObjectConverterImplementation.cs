namespace DevExpress.Utils.Serializing.Helpers
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ObjectConverterImplementation
    {
        private ObjectConverters converters = new ObjectConverters();
        private static Type[] structTypes = new Type[] { typeof(Point), typeof(PointF), typeof(Size), typeof(SizeF), typeof(Rectangle), typeof(RectangleF) };

        private string ArrayElementToString(object obj)
        {
            if (obj == null)
            {
                return "null";
            }
            Type type = obj.GetType();
            if (type.IsPrimitive || ((obj is string) || ((obj is DateTime) || (obj is decimal))))
            {
                return (DXTypeExtensions.GetTypeCode(type).ToString() + ", " + this.PrimitiveToString(obj).Replace(",", ",,"));
            }
            string str = this.ObjectToString(obj);
            if (str == null)
            {
                throw new Exception($"Class {obj.GetType()} should either have a TypeConverter that can convert to/from a string, or implement the ISerializable interface to solve the problem.");
            }
            string[] textArray1 = new string[] { 1.ToString(), ", ", type.AssemblyQualifiedName.Replace(",", ",,"), ", ", str.Replace(",", ",,") };
            return string.Concat(textArray1);
        }

        private string ArrayToString(object obj)
        {
            Array array = (Array) obj;
            StringBuilder builder = new StringBuilder();
            builder.Append("~Xtra#Array").Append(array.Length).Append(", ");
            for (int i = 0; i < array.Length; i++)
            {
                builder.Append(this.ArrayElementToString(array.GetValue(i)).Replace(",", ",,")).Append(", ");
            }
            return builder.ToString();
        }

        public bool CanConvertFromString(Type type, string s)
        {
            IOneTypeObjectConverter converter = this.Converters.GetConverter(type);
            return ((converter is IOneTypeObjectConverter2) ? ((IOneTypeObjectConverter2) converter).CanConvertFromString(s) : (converter != null));
        }

        public bool CanConvertToString(Type type) => 
            this.Converters.IsConverterExists(type);

        public object ConvertFromString(Type type, string str) => 
            this.Converters.ConvertFromString(type, str);

        public string ConvertToString(object obj) => 
            this.Converters.ConvertToString(obj);

        public void CopyConvertersTo(ObjectConverterImplementation ocImplTo)
        {
            this.Converters.CopyTo(ocImplTo.Converters);
        }

        protected virtual object DeserialzeWithBinaryFormatter(string str) => 
            SafeBinaryFormatter.Deserialize(str.Substring("~Xtra#Base64".Length));

        private static object EnumToObject(string str, Type type) => 
            Enum.Parse(type, str, false);

        internal int GetIndexOfDelimiter(string str, int index)
        {
            for (int i = index; i < (str.Length - 1); i++)
            {
                if (str[i] == ',')
                {
                    char ch = str[i + 1];
                    if (ch == ' ')
                    {
                        return i;
                    }
                    if (ch == ',')
                    {
                        i++;
                    }
                }
            }
            return -1;
        }

        internal string GetNextPart(string str, ref int index)
        {
            string str2;
            int startIndex = index;
            int indexOfDelimiter = this.GetIndexOfDelimiter(str, index);
            if (indexOfDelimiter < 0)
            {
                index = str.Length;
                str2 = str.Substring(startIndex);
            }
            else
            {
                index = indexOfDelimiter + 2;
                str2 = str.Substring(startIndex, indexOfDelimiter - startIndex);
            }
            return str2.Replace(",,", ",");
        }

        private TypeConverter GetToStringConverter(Type type)
        {
            TypeConverter converter = null;
            converter ??= TypeDescriptor.GetConverter(type);
            return (((converter == null) || (!converter.CanConvertTo(typeof(string)) || !converter.CanConvertFrom(typeof(string)))) ? null : converter);
        }

        public object IntStringToStructure(string str, Type type)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            object component = Activator.CreateInstance(type);
            char[] separator = new char[] { ',', '=' };
            string[] strArray = str.Split(separator);
            for (int i = 0; i < strArray.Length; i += 2)
            {
                PropertyDescriptor descriptor = properties[strArray[i]];
                object obj3 = this.StringToObject(strArray[i + 1], descriptor.PropertyType);
                descriptor.SetValue(component, obj3);
            }
            return component;
        }

        public string IntStructureToString(object obj)
        {
            StringBuilder builder = new StringBuilder();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                if (!descriptor.IsReadOnly && descriptor.PropertyType.IsPrimitive)
                {
                    string str = this.ObjectToString(descriptor.GetValue(obj));
                    str ??= string.Empty;
                    builder.AppendFormat("{2}{0}={1}", descriptor.Name, str, (builder.Length > 0) ? "," : string.Empty);
                }
            }
            return builder.ToString();
        }

        private static bool IsKnownStructure(Type t) => 
            Array.IndexOf<Type>(structTypes, t) != -1;

        private static bool IsNullable(Type t) => 
            Nullable.GetUnderlyingType(t) != null;

        private bool IsSimpleType(Type type) => 
            (type != null) ? (!type.IsPrimitive ? (!type.Equals(typeof(string)) ? (!type.Equals(typeof(DateTime)) ? (!type.Equals(typeof(decimal)) ? (!type.IsEnum ? (!IsKnownStructure(type) ? type.IsValueType : true) : true) : true) : true) : true) : true) : false;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public object IStringToStructure(string str, Type type) => 
            this.StringToStructure(str, type, true);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string IStructureToString(object obj) => 
            this.StructureToString(obj, true, false);

        protected internal virtual bool IsTypeSerializable(Type type) => 
            type.IsSerializable;

        public virtual string ObjectToString(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if (obj is string)
            {
                return (string) obj;
            }
            Type type = obj.GetType();
            if (this.CanConvertToString(type))
            {
                return this.ConvertToString(obj);
            }
            if (IsKnownStructure(type))
            {
                return this.StructureToString(obj, false, true);
            }
            if (type.IsArray)
            {
                return this.ArrayToString(obj);
            }
            TypeConverter toStringConverter = this.GetToStringConverter(type);
            return ((toStringConverter == null) ? (this.IsTypeSerializable(type) ? this.SerialzeWithBinaryFormatter(obj) : null) : ((string) toStringConverter.ConvertTo(this.Context, CultureInfo.InvariantCulture, obj, typeof(string))));
        }

        private string PrimitiveToString(object obj) => 
            !(obj is double) ? Convert.ToString(obj, CultureInfo.InvariantCulture) : ((double) obj).ToString("R", CultureInfo.InvariantCulture);

        public void RegisterConverter(IOneTypeObjectConverter converter)
        {
            this.Converters.RegisterConverter(converter);
        }

        public Type ResolveType(string typeName) => 
            this.Converters.ResolveType(typeName);

        protected virtual string SerialzeWithBinaryFormatter(object obj) => 
            "~Xtra#Base64" + Convert.ToBase64String(SafeBinaryFormatter.Serialize(obj));

        private object StringToArray(string str, Type type)
        {
            str = str.Substring("~Xtra#Array".Length);
            int result = 0;
            int index = 0;
            int num3 = 0;
            if (!int.TryParse(this.GetNextPart(str, ref index), out result))
            {
                return null;
            }
            Array array = !(type == typeof(object)) ? Array.CreateInstance(type.GetElementType(), result) : ((Array) new object[result]);
            while ((index < str.Length) && (num3 < result))
            {
                string nextPart = this.GetNextPart(str, ref index);
                array.SetValue(this.StringToArrayElement(nextPart), num3);
                num3++;
            }
            return array;
        }

        private object StringToArrayElement(string part)
        {
            int index = 0;
            string nextPart = this.GetNextPart(part, ref index);
            if (nextPart == "null")
            {
                return null;
            }
            TypeCode typeCode = (TypeCode) Enum.Parse(typeof(TypeCode), nextPart, false);
            if (typeCode != TypeCode.Object)
            {
                return Convert.ChangeType(this.GetNextPart(part, ref index), typeCode, CultureInfo.InvariantCulture);
            }
            string typeName = this.GetNextPart(part, ref index);
            return this.StringToObject(this.GetNextPart(part, ref index), Type.GetType(typeName));
        }

        public virtual object StringToObject(string str, Type type)
        {
            if (type.Equals(typeof(string)))
            {
                return str;
            }
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            if (this.CanConvertFromString(type, str))
            {
                return this.ConvertFromString(type, str);
            }
            if (IsKnownStructure(type))
            {
                return this.StringToStructure(str, type, false);
            }
            if (type.IsEnum)
            {
                return EnumToObject(str, type);
            }
            if (str.StartsWith("~Xtra#Base64"))
            {
                return this.DeserialzeWithBinaryFormatter(str);
            }
            if (str.StartsWith("~Xtra#Array"))
            {
                return this.StringToArray(str, type);
            }
            TypeConverter toStringConverter = this.GetToStringConverter(type);
            if (toStringConverter == null)
            {
                return Convert.ChangeType(str, type, CultureInfo.InvariantCulture);
            }
            object obj2 = toStringConverter.ConvertFrom(this.Context, CultureInfo.InvariantCulture, str);
            return (!IsNullable(type) ? (((obj2 == null) || !type.IsAssignableFrom(obj2.GetType())) ? Convert.ChangeType(obj2, type, CultureInfo.InvariantCulture) : obj2) : obj2);
        }

        private object StringToStructure(string str, Type type, bool allowConvertableTypes = false)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(type);
            object component = Activator.CreateInstance(type);
            int startIndex = 0;
            while (true)
            {
                if (startIndex < str.Length)
                {
                    int index = str.IndexOf("@", startIndex);
                    if (index != -1)
                    {
                        startIndex = index + 1;
                        char[] separator = new char[] { ',', '=' };
                        string[] strArray = str.Substring(startIndex).Split(separator);
                        if ((strArray != null) && (strArray.Length >= 2))
                        {
                            int length = Convert.ToInt32(strArray[0]);
                            index = (startIndex + str.Substring(startIndex).IndexOf("=")) + 1;
                            PropertyDescriptor descriptor = properties[strArray[1]];
                            if (descriptor == null)
                            {
                                continue;
                            }
                            string str3 = str.Substring(index, length);
                            startIndex = index + length;
                            descriptor.SetValue(component, this.StringToObject(str3, descriptor.PropertyType));
                            continue;
                        }
                    }
                }
                return component;
            }
        }

        private string StructureToString(object obj, bool allowConvertableTypes = false, bool onlyPrimitiveValues = false)
        {
            StringBuilder builder = new StringBuilder();
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                if (((!descriptor.IsReadOnly && (!onlyPrimitiveValues || descriptor.PropertyType.IsPrimitive)) && (this.IsSimpleType(descriptor.PropertyType) || (allowConvertableTypes && this.CanConvertToString(descriptor.PropertyType)))) && descriptor.ShouldSerializeValue(obj))
                {
                    string str = this.ObjectToString(descriptor.GetValue(obj));
                    str ??= string.Empty;
                    builder.AppendFormat("@{2},{0}={1}", descriptor.Name, str, str.Length);
                }
            }
            return builder.ToString();
        }

        public void UnregisterConverter(Type type)
        {
            this.Converters.UnregisterConverter(type);
        }

        protected virtual ITypeDescriptorContext Context =>
            null;

        protected virtual ObjectConverters Converters =>
            this.converters;
    }
}

