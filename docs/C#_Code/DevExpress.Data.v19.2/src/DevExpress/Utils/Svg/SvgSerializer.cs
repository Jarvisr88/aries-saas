namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Xml;

    public class SvgSerializer
    {
        private static object lockObj = new object();
        private static Dictionary<Type, Dictionary<string, Tuple<string, Type, TypeConverter, DefaultValueParameter>>> propertyAccessors = new Dictionary<Type, Dictionary<string, Tuple<string, Type, TypeConverter, DefaultValueParameter>>>();
        private static Dictionary<Type, string> elementTypes = new Dictionary<Type, string>();

        private static Expression CreateAccessor(Type type, MemberInfo mInfo, out ParameterExpression parameter)
        {
            parameter = Expression.Parameter(typeof(object), "instance");
            return Expression.MakeMemberAccess(Expression.Convert(parameter, type), mInfo);
        }

        private static System.Xml.XmlAttribute CreateAttribute(XmlDocument document, string name, string val)
        {
            System.Xml.XmlAttribute attribute = document.CreateAttribute(name);
            attribute.Value = val;
            return attribute;
        }

        private static string GetElementName(SvgElement element)
        {
            Type key = element.GetType();
            string name = string.Empty;
            object lockObj = SvgSerializer.lockObj;
            lock (lockObj)
            {
                if (!elementTypes.TryGetValue(key, out name))
                {
                    SvgElementNameAliasAttribute customAttribute = key.GetCustomAttribute(typeof(SvgElementNameAliasAttribute)) as SvgElementNameAliasAttribute;
                    if (customAttribute != null)
                    {
                        name = customAttribute.Name;
                        elementTypes.Add(key, name);
                    }
                }
            }
            return name;
        }

        private static MemberInfo GetMemberInfo(Type type, string memberName) => 
            type.GetProperty(memberName) ?? type.GetField(memberName);

        private static IEnumerable<PropertyDescriptor> GetPropertyInfos(Type elementType)
        {
            Func<PropertyDescriptor, bool> predicate = <>c.<>9__14_0;
            if (<>c.<>9__14_0 == null)
            {
                Func<PropertyDescriptor, bool> local1 = <>c.<>9__14_0;
                predicate = <>c.<>9__14_0 = x => x.Attributes.OfType<SvgPropertyNameAliasAttribute>().Any<SvgPropertyNameAliasAttribute>();
            }
            return TypeDescriptor.GetProperties(elementType).Cast<PropertyDescriptor>().Where<PropertyDescriptor>(predicate);
        }

        private static Func<object, object> MakeAccessor(Type type, Type propertyType, string memberName)
        {
            ParameterExpression expression;
            ParameterExpression[] parameters = new ParameterExpression[] { expression };
            return Expression.Lambda<Func<object, object>>(Expression.Convert(CreateAccessor(type, GetMemberInfo(type, memberName), out expression), typeof(object)), parameters).Compile();
        }

        public static void SaveSvgImageToXML(Stream stream, SvgImage image)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            XmlTextWriter xmlWriter = new XmlTextWriter(stream, Encoding.UTF8) {
                Formatting = Formatting.Indented
            };
            xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            foreach (SvgElement element in image.Elements)
            {
                Write(element, xmlWriter);
            }
            xmlWriter.Flush();
            Thread.CurrentThread.CurrentCulture = currentCulture;
        }

        public static void SaveSvgImageToXML(string path, SvgImage image)
        {
            using (Stream stream = File.Open(path, FileMode.Create))
            {
                SaveSvgImageToXML(stream, image);
            }
        }

        public static void Write(SvgElement element, XmlWriter xmlWriter)
        {
            WriteStartElement(element, xmlWriter);
            WriteChildren(element, xmlWriter);
            WriteEndElement(element, xmlWriter);
        }

        private static void WriteChildren(SvgElement element, XmlWriter xmlWriter)
        {
            foreach (SvgElement element2 in element.Elements)
            {
                if (element2 is SvgContent)
                {
                    xmlWriter.WriteString((element2 as SvgContent).Content);
                    continue;
                }
                Write(element2, xmlWriter);
            }
        }

        public static System.Xml.XmlNode WriteElement(XmlDocument document, System.Xml.XmlNode xmlNode, SvgElement element)
        {
            if (element == null)
            {
                return null;
            }
            XmlElement newChild = document.CreateElement(GetElementName(element));
            xmlNode.AppendChild(newChild);
            WriteElementAttributes(document, newChild, element);
            foreach (SvgElement element3 in element.Elements)
            {
                WriteElement(document, newChild, element3);
            }
            return newChild;
        }

        private static void WriteElementAttributes(SvgElement element, XmlWriter xmlWriter)
        {
            Dictionary<string, Tuple<string, Type, TypeConverter, DefaultValueParameter>> dictionary;
            Type key = element.GetType();
            object lockObj = SvgSerializer.lockObj;
            lock (lockObj)
            {
                if (!propertyAccessors.TryGetValue(element.GetType(), out dictionary))
                {
                    dictionary = new Dictionary<string, Tuple<string, Type, TypeConverter, DefaultValueParameter>>();
                    foreach (PropertyDescriptor descriptor in GetPropertyInfos(element.GetType()))
                    {
                        string name = descriptor.Name;
                        SvgPropertyNameAliasAttribute attribute = descriptor.Attributes.OfType<SvgPropertyNameAliasAttribute>().First<SvgPropertyNameAliasAttribute>();
                        DefaultValueAttribute attribute2 = descriptor.Attributes.OfType<DefaultValueAttribute>().FirstOrDefault<DefaultValueAttribute>();
                        DefaultValueParameter parameter = null;
                        if (attribute2 != null)
                        {
                            parameter = new DefaultValueParameter(attribute2.Value);
                        }
                        dictionary.Add(attribute.Name, new Tuple<string, Type, TypeConverter, DefaultValueParameter>(name, descriptor.PropertyType, descriptor.Converter, parameter));
                    }
                    if (dictionary.Count > 0)
                    {
                        propertyAccessors.Add(key, dictionary);
                    }
                }
            }
            foreach (KeyValuePair<string, Tuple<string, Type, TypeConverter, DefaultValueParameter>> pair in dictionary)
            {
                object valueForSerialize = element.GetValueForSerialize(pair.Value.Item1, pair.Value.Item2);
                if ((pair.Value.Item4 == null) || !Equals(pair.Value.Item4.Value, valueForSerialize))
                {
                    string str2 = pair.Value.Item3.ConvertToInvariantString(valueForSerialize);
                    if (string.Equals(pair.Value.Item1, "Transformations") && (element.Transformations.Count > 0))
                    {
                        str2 = pair.Value.Item3.ConvertToInvariantString(element.Transformations);
                    }
                    if (string.Equals(pair.Value.Item1, "GradientTransformation") && ((element is SvgGradient) && ((element as SvgGradient).GradientTransformation.Count > 0)))
                    {
                        str2 = pair.Value.Item3.ConvertToInvariantString((element as SvgGradient).GradientTransformation);
                    }
                    if (!string.IsNullOrEmpty(str2))
                    {
                        xmlWriter.WriteAttributeString(pair.Key, str2);
                    }
                }
            }
        }

        private static void WriteElementAttributes(XmlDocument document, System.Xml.XmlNode xmlNode, SvgElement element)
        {
            Dictionary<string, Tuple<string, Type, TypeConverter, DefaultValueParameter>> dictionary;
            Type key = element.GetType();
            object lockObj = SvgSerializer.lockObj;
            lock (lockObj)
            {
                if (!propertyAccessors.TryGetValue(element.GetType(), out dictionary))
                {
                    dictionary = new Dictionary<string, Tuple<string, Type, TypeConverter, DefaultValueParameter>>();
                    foreach (PropertyDescriptor descriptor in GetPropertyInfos(element.GetType()))
                    {
                        Func<object, object> func = MakeAccessor(element.GetType(), descriptor.PropertyType, descriptor.Name);
                        SvgPropertyNameAliasAttribute attribute = descriptor.Attributes.OfType<SvgPropertyNameAliasAttribute>().First<SvgPropertyNameAliasAttribute>();
                        DefaultValueAttribute attribute2 = descriptor.Attributes.OfType<DefaultValueAttribute>().FirstOrDefault<DefaultValueAttribute>();
                        DefaultValueParameter parameter = null;
                        if (attribute2 != null)
                        {
                            parameter = new DefaultValueParameter(attribute2.Value);
                        }
                        dictionary.Add(attribute.Name, new Tuple<string, Type, TypeConverter, DefaultValueParameter>(descriptor.Name, descriptor.PropertyType, descriptor.Converter, parameter));
                    }
                    if (dictionary.Count > 0)
                    {
                        propertyAccessors.Add(key, dictionary);
                    }
                }
            }
            foreach (KeyValuePair<string, Tuple<string, Type, TypeConverter, DefaultValueParameter>> pair in dictionary)
            {
                object valueForSerialize = element.GetValueForSerialize(pair.Value.Item1, pair.Value.Item2);
                if ((pair.Value.Item4 == null) || !Equals(pair.Value.Item4.Value, valueForSerialize))
                {
                    string str = pair.Value.Item3.ConvertToInvariantString(valueForSerialize);
                    if (!string.IsNullOrEmpty(str))
                    {
                        xmlNode.Attributes.Append(CreateAttribute(document, pair.Key, str));
                    }
                }
            }
        }

        private static void WriteEndElement(SvgElement element, XmlWriter xmlWriter)
        {
            xmlWriter.WriteEndElement();
        }

        private static void WriteStartElement(SvgElement element, XmlWriter xmlWriter)
        {
            xmlWriter.WriteStartElement(GetElementName(element));
            WriteElementAttributes(element, xmlWriter);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SvgSerializer.<>c <>9 = new SvgSerializer.<>c();
            public static Func<PropertyDescriptor, bool> <>9__14_0;

            internal bool <GetPropertyInfos>b__14_0(PropertyDescriptor x) => 
                x.Attributes.OfType<SvgPropertyNameAliasAttribute>().Any<SvgPropertyNameAliasAttribute>();
        }

        private class DefaultValueParameter
        {
            public DefaultValueParameter(object value)
            {
                this.Value = value;
            }

            public object Value { get; private set; }
        }
    }
}

