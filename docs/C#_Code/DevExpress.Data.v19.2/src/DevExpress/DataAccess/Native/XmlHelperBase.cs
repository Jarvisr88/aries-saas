namespace DevExpress.DataAccess.Native
{
    using DevExpress.Utils;
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;
    using System.Xml.Linq;

    public static class XmlHelperBase
    {
        public static void AddAttribute(this XElement rootEl, string value, string attributeName)
        {
            rootEl.Add(new XAttribute(attributeName, value));
        }

        public static TEnum EnumFromString<TEnum>(string str) => 
            (TEnum) Enum.Parse(typeof(TEnum), str);

        public static object FromString(Type type, string str)
        {
            object obj2;
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            if (converter == null)
            {
                throw new XmlException();
            }
            try
            {
                obj2 = !(type == typeof(TimeSpan)) ? converter.ConvertFrom(null, CultureInfo.InvariantCulture, str) : XmlConvert.ToTimeSpan(str);
            }
            catch
            {
                try
                {
                    obj2 = converter.ConvertTo(null, CultureInfo.InvariantCulture, str, type);
                }
                catch
                {
                    throw new XmlException();
                }
            }
            return obj2;
        }

        public static string GetAttributeOrElementValue(this XElement root, string name)
        {
            string attributeValue = root.GetAttributeValue(name);
            if (!string.IsNullOrEmpty(attributeValue))
            {
                return attributeValue;
            }
            XElement element = root.Element(name);
            return element?.Value;
        }

        public static string GetAttributeValue(this XElement element, string attributeName)
        {
            Guard.ArgumentNotNull(element, "element");
            Guard.ArgumentIsNotNullOrEmpty(attributeName, "attributeName");
            XAttribute attribute = element.Attribute(attributeName);
            return attribute?.Value;
        }
    }
}

