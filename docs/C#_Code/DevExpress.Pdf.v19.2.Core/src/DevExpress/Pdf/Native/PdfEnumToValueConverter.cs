namespace DevExpress.Pdf.Native
{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;

    internal class PdfEnumToValueConverter
    {
        private static PdfEnumToValueConverter converter = new PdfEnumToValueConverter();

        protected PdfEnumToValueConverter()
        {
        }

        public static int Convert<T>(T value) where T: struct => 
            converter.PerformConvert<T>(value);

        public static T Parse<T>(int? value, T? defaultValue = new T?()) where T: struct => 
            converter.PerformParse<T>(value, defaultValue);

        protected int PerformConvert<T>(T value) where T: struct
        {
            Type enumType = typeof(T);
            FieldInfo field = enumType.GetField(Enum.GetName(enumType, value));
            if (field == null)
            {
                this.ThrowIncorrectDataException();
            }
            object[] customAttributes = field.GetCustomAttributes(typeof(PdfFieldValueAttribute), false);
            int index = 0;
            if (index < customAttributes.Length)
            {
                return ((PdfFieldValueAttribute) customAttributes[index]).Value;
            }
            this.ThrowIncorrectDataException();
            return 0;
        }

        protected T PerformParse<T>(int? value, T? defaultValue = new T?()) where T: struct
        {
            if (value != null)
            {
                Type enumType = typeof(T);
                FieldInfo[] fields = enumType.GetFields();
                int index = 0;
                while (index < fields.Length)
                {
                    FieldInfo info = fields[index];
                    object[] customAttributes = info.GetCustomAttributes(typeof(PdfFieldValueAttribute), false);
                    int num2 = 0;
                    while (true)
                    {
                        if (num2 >= customAttributes.Length)
                        {
                            index++;
                            break;
                        }
                        PdfFieldValueAttribute attribute = (PdfFieldValueAttribute) customAttributes[num2];
                        int? nullable = value;
                        int num3 = attribute.Value;
                        if ((nullable.GetValueOrDefault() == num3) ? (nullable != null) : false)
                        {
                            return (T) Enum.Parse(enumType, info.Name);
                        }
                        num2++;
                    }
                }
            }
            if (defaultValue == null)
            {
                this.ThrowIncorrectDataException();
            }
            return defaultValue.Value;
        }

        protected virtual void ThrowIncorrectDataException()
        {
            PdfDocumentStructureReader.ThrowIncorrectDataException();
        }
    }
}

