namespace DevExpress.Pdf.Native
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal static class PdfEnumToStringConverter
    {
        public static string Convert<T>(T value, bool useDefault = true) where T: struct
        {
            if (useDefault)
            {
                PdfDefaultFieldAttribute attribute = GetAttribute<T, PdfDefaultFieldAttribute>();
                if ((attribute != null) && value.Equals(attribute.Value))
                {
                    return null;
                }
            }
            Type enumType = typeof(T);
            string name = Enum.GetName(enumType, value);
            if (name == null)
            {
                return null;
            }
            FieldInfo field = enumType.GetField(name);
            if (field == null)
            {
                return null;
            }
            object[] customAttributes = field.GetCustomAttributes(typeof(PdfFieldNameAttribute), false);
            int index = 0;
            return ((index < customAttributes.Length) ? ((PdfFieldNameAttribute) customAttributes[index]).Text : name);
        }

        private static A GetAttribute<T, A>() where T: struct where A: Attribute
        {
            object[] customAttributes = typeof(T).GetCustomAttributes(typeof(A), false);
            int index = 0;
            if (index < customAttributes.Length)
            {
                return (A) customAttributes[index];
            }
            return default(A);
        }

        public static T Parse<T>(string str, bool useDefault = true) where T: struct => 
            Parse<T>(str, useDefault, false).Value;

        public static T? Parse<T>(string str, bool useDefault, bool supressException) where T: struct
        {
            T local;
            if (!TryParse<T>(str, out local, useDefault))
            {
                if (useDefault || (GetAttribute<T, PdfSupportUndefinedValueAttribute>() != null))
                {
                    PdfDefaultFieldAttribute attribute = GetAttribute<T, PdfDefaultFieldAttribute>();
                    if (attribute != null)
                    {
                        return new T?((T) attribute.Value);
                    }
                }
                if (supressException)
                {
                    return null;
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return new T?(local);
        }

        public static bool TryParse<T>(string str, out T value, bool useDefault = true) where T: struct
        {
            PdfDefaultFieldAttribute attribute = GetAttribute<T, PdfDefaultFieldAttribute>();
            if (string.IsNullOrEmpty(str))
            {
                if (useDefault && (attribute != null))
                {
                    value = (T) attribute.Value;
                    return true;
                }
                value = default(T);
                return false;
            }
            Type enumType = typeof(T);
            FieldInfo[] fields = enumType.GetFields();
            int index = 0;
            while (index < fields.Length)
            {
                FieldInfo info = fields[index];
                object[] customAttributes = info.GetCustomAttributes(typeof(PdfFieldNameAttribute), false);
                int num2 = 0;
                while (true)
                {
                    if (num2 >= customAttributes.Length)
                    {
                        index++;
                        break;
                    }
                    PdfFieldNameAttribute attribute2 = (PdfFieldNameAttribute) customAttributes[num2];
                    string alternateText = attribute2.AlternateText;
                    if ((str == attribute2.Text) || ((alternateText != null) && (str == alternateText)))
                    {
                        value = (T) Enum.Parse(enumType, info.Name);
                        return true;
                    }
                    num2++;
                }
            }
            return (Enum.TryParse<T>(str, out value) && ((attribute == null) || (attribute.CanUsed || !value.Equals(attribute.Value))));
        }
    }
}

