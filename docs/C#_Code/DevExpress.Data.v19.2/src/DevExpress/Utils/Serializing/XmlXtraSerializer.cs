namespace DevExpress.Utils.Serializing
{
    using DevExpress.Data.Internal;
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml;

    public class XmlXtraSerializer : XtraSerializer
    {
        private static readonly Dictionary<Type, MethodInfo> XmlConvertToStringMethods = new Dictionary<Type, MethodInfo>();
        private static readonly Dictionary<Type, MethodInfo> XmlConvertFromStringMethods = new Dictionary<Type, MethodInfo>();
        private readonly string alternativeAppName;
        private static ConcurrentDictionary<string, Type> types;

        static XmlXtraSerializer()
        {
            MethodInfo[] methods = typeof(XmlConvert).GetMethods(BindingFlags.Public | BindingFlags.Static);
            PopulateToStringMethods(methods);
            PopulateFromStringMethods(methods);
        }

        public XmlXtraSerializer()
        {
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public XmlXtraSerializer(string alternativeAppName)
        {
            this.alternativeAppName = alternativeAppName;
        }

        protected virtual bool CheckSerializerName(string name) => 
            name == "XtraSerializer";

        protected virtual XmlReader CreateReader(Stream stream) => 
            SafeXml.CreateTextReader(stream, DtdProcessing.Prohibit, null);

        protected XmlWriter CreateXmlTextWriter(Stream stream) => 
            XmlWriter.Create(stream, this.CreateXmlWriterSettings());

        protected virtual XmlWriterSettings CreateXmlWriterSettings() => 
            new XmlWriterSettings { 
                Indent = true,
                CheckCharacters = false,
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8
            };

        protected virtual XtraPropertyInfo CreateXtraPropertyInfo(string name, Type propType, bool isKey, Dictionary<string, string> attributes) => 
            new XmlXtraPropertyInfo(name, propType, null, isKey);

        protected override IXtraPropertyCollection Deserialize(Stream stream, string appName, IList objects) => 
            this.DeserializeCore(stream, appName, objects);

        protected override IXtraPropertyCollection Deserialize(string path, string appName, IList objects)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return this.DeserializeCore(stream, appName, objects);
            }
        }

        protected virtual IXtraPropertyCollection DeserializeCore(Stream stream, string appName, IList objects)
        {
            XmlReader reader = this.CreateReader(stream);
            IXtraPropertyCollection list = new XtraPropertyCollection();
            while (true)
            {
                if (reader.Read())
                {
                    if (!reader.IsStartElement() || !this.CheckSerializerName(reader.Name))
                    {
                        continue;
                    }
                    string attribute = reader.GetAttribute("application");
                    if ((attribute != appName) && ((attribute != this.alternativeAppName) || string.IsNullOrEmpty(this.alternativeAppName)))
                    {
                        continue;
                    }
                    this.Read(reader, list, -1);
                }
                return ((list.Count != 0) ? list : null);
            }
        }

        private static MethodInfo FindXmlFromStringMethod(Type type)
        {
            MethodInfo info;
            XmlConvertFromStringMethods.TryGetValue(type, out info);
            return info;
        }

        private static MethodInfo FindXmlToStringMethod(Type type)
        {
            MethodInfo info;
            XmlConvertToStringMethods.TryGetValue(type, out info);
            return info;
        }

        protected virtual Dictionary<string, string> GetAttributes(XtraPropertyInfo pInfo)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string> {
                { 
                    "name",
                    pInfo.Name
                }
            };
            if (pInfo.Value == null)
            {
                dictionary.Add("isnull", "true");
            }
            else if (!pInfo.IsKey)
            {
                string str;
                Func<object, string> getValueType = <>c.<>9__30_0;
                if (<>c.<>9__30_0 == null)
                {
                    Func<object, string> local1 = <>c.<>9__30_0;
                    getValueType = <>c.<>9__30_0 = v => v.GetType().FullName;
                }
                if (XtraPropertyInfo.ShouldSerializePropertyType(pInfo, out str, getValueType))
                {
                    dictionary.Add("type", str);
                }
            }
            if (pInfo.IsKey)
            {
                dictionary.Add("iskey", "true");
                if (pInfo.Value != null)
                {
                    dictionary.Add("value", XmlObjectToString(pInfo.Value));
                }
            }
            return dictionary;
        }

        private Type GetType(string typeName, ObjectConverterImplementation converter)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            Type type = Type.GetType(typeName, false);
            if (type == null)
            {
                if ((type == null) && base.HasCustomObjectConverter)
                {
                    type = base.CustomObjectConverter.GetType(typeName);
                }
                if ((type == null) && (converter != null))
                {
                    type = converter.ResolveType(typeName);
                }
                if (type != null)
                {
                    return type;
                }
                if (this.AllowCustomTypes)
                {
                    if (Types.TryGetValue(typeName, out type))
                    {
                        return type;
                    }
                    foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        Type type2 = assembly.GetType(typeName);
                        if (type2 != null)
                        {
                            SafeSerializationBinder.Ensure(assembly.GetName().Name, typeName);
                            Types[typeName] = type2;
                            return type2;
                        }
                    }
                }
            }
            return type;
        }

        private static void PopulateFromStringMethods(MethodInfo[] methods)
        {
            MethodInfo info;
            int length = methods.Length;
            for (int i = 0; i < length; i++)
            {
                MethodInfo info4 = methods[i];
                string name = info4.Name;
                int index = name.IndexOf("To");
                if (index == 0)
                {
                    string str2 = name.Substring(index + 2);
                    if (info4.ReturnType.Name == str2)
                    {
                        ParameterInfo[] parameters = info4.GetParameters();
                        if ((parameters.Length == 1) && (parameters[0].ParameterType == typeof(string)))
                        {
                            XmlConvertFromStringMethods.Add(info4.ReturnType, info4);
                        }
                    }
                }
            }
            MethodInfo method = typeof(XmlXtraPropertyInfo).GetMethod("StringToTimeSpan");
            MethodInfo info3 = typeof(XmlXtraPropertyInfo).GetMethod("StringToDateTime");
            if (XmlConvertFromStringMethods.TryGetValue(typeof(TimeSpan), out info))
            {
                XmlConvertFromStringMethods[typeof(TimeSpan)] = method;
            }
            else
            {
                XmlConvertFromStringMethods.Add(typeof(TimeSpan), method);
            }
            if (XmlConvertFromStringMethods.TryGetValue(typeof(DateTime), out info))
            {
                XmlConvertFromStringMethods[typeof(DateTime)] = info3;
            }
            else
            {
                XmlConvertFromStringMethods.Add(typeof(DateTime), info3);
            }
        }

        private static void PopulateToStringMethods(MethodInfo[] methods)
        {
            int length = methods.Length;
            for (int i = 0; i < length; i++)
            {
                MethodInfo info = methods[i];
                if ((info.Name == "ToString") && (info.ReturnType == typeof(string)))
                {
                    ParameterInfo[] parameters = info.GetParameters();
                    if (parameters.Length == 1)
                    {
                        XmlConvertToStringMethods.Add(parameters[0].ParameterType, info);
                    }
                }
            }
        }

        private void Read(XmlReader reader, IXtraPropertyCollection list, int depth)
        {
            string str = "";
            bool isKey = false;
            ILineInfoProvider provider = LineInfoProvider.Create(reader);
            while (!reader.EOF)
            {
                if ((depth != -1) && (reader.Depth <= depth))
                {
                    return;
                }
                if (!reader.IsStartElement() || (reader.Name != "property"))
                {
                    reader.Read();
                    continue;
                }
                if (reader.AttributeCount >= 1)
                {
                    Dictionary<string, string> attributes = new Dictionary<string, string>();
                    while (true)
                    {
                        if (!reader.MoveToNextAttribute())
                        {
                            string str2;
                            string str3;
                            string str4;
                            string str5;
                            reader.MoveToElement();
                            attributes.TryGetValue("name", out str);
                            attributes.TryGetValue("value", out str2);
                            attributes.TryGetValue("isnull", out str3);
                            attributes.TryGetValue("iskey", out str4);
                            attributes.TryGetValue("type", out str5);
                            bool flag2 = str3 == "true";
                            isKey = str4 == "true";
                            XtraPropertyInfo prop = this.CreateXtraPropertyInfo(str, this.GetType(str5, base.ObjectConverterImpl), isKey, attributes);
                            prop.IsNull = flag2;
                            int num = reader.Depth;
                            int lineNumber = provider.LineNumber;
                            int linePosition = provider.LinePosition;
                            if (!isKey)
                            {
                                str2 = reader.ReadString();
                            }
                            if (!flag2)
                            {
                                prop.Value = this.UpdateValue(str2);
                            }
                            if (isKey && !reader.IsEmptyElement)
                            {
                                reader.Read();
                                lineNumber = provider.LineNumber;
                                linePosition = provider.LinePosition;
                                this.Read(reader, prop.ChildProperties, num);
                            }
                            list.Add(prop);
                            if ((lineNumber == provider.LineNumber) && (linePosition == provider.LinePosition))
                            {
                                reader.Read();
                            }
                            break;
                        }
                        attributes.Add(reader.Name, reader.Value);
                    }
                }
            }
        }

        protected override bool Serialize(Stream stream, IXtraPropertyCollection props, string appName) => 
            this.SerializeCore(stream, props, appName);

        protected override bool Serialize(string path, IXtraPropertyCollection props, string appName)
        {
            bool flag = false;
            FileStream stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
            flag = this.SerializeCore(stream, props, appName);
            stream.Dispose();
            return flag;
        }

        protected virtual bool SerializeCore(Stream stream, IXtraPropertyCollection props, string appName)
        {
            using (XmlWriter writer = this.CreateXmlTextWriter(stream))
            {
                this.SerializeCore(writer, props, appName);
            }
            return true;
        }

        protected void SerializeCore(XmlWriter tw, IXtraPropertyCollection props, string appName)
        {
            this.WriteStartDocument(tw);
            this.WriteStartElement(tw, appName);
            this.SerializeLevel(tw, props);
            tw.WriteEndElement();
            tw.Flush();
        }

        protected void SerializeLevel(XmlWriter tw, IXtraPropertyCollection props)
        {
            if (props != null)
            {
                this.SerializeLevelCore(tw, props);
            }
        }

        protected virtual void SerializeLevelCore(XmlWriter tw, IXtraPropertyCollection props)
        {
            foreach (XtraPropertyInfo info in props)
            {
                this.SerializeProperty(tw, info);
            }
        }

        private void SerializeProperty(XmlWriter writer, XtraPropertyInfo pInfo)
        {
            writer.WriteStartElement("property");
            foreach (KeyValuePair<string, string> pair in this.GetAttributes(pInfo))
            {
                writer.WriteAttributeString(pair.Key, pair.Value);
            }
            try
            {
                object obj2 = pInfo.Value;
                if ((obj2 != null) && !obj2.GetType().IsPrimitive)
                {
                    obj2 = base.ObjectConverterImpl.ObjectToString(obj2);
                }
                if (!pInfo.IsKey && (pInfo.Value != null))
                {
                    string str = XmlObjectToString(obj2);
                    if (!string.IsNullOrEmpty(str))
                    {
                        writer.WriteString(str);
                    }
                }
                if (pInfo.IsKey)
                {
                    this.SerializeLevel(writer, pInfo.ChildProperties);
                }
            }
            finally
            {
                writer.WriteEndElement();
            }
        }

        protected virtual string UpdateValue(string val) => 
            val;

        protected virtual void WriteApplicationAttribute(string appName, XmlWriter tw)
        {
            tw.WriteAttributeString("application", appName);
        }

        protected virtual void WriteStartDocument(XmlWriter tw)
        {
        }

        protected virtual void WriteStartElement(XmlWriter tw, string appName)
        {
            tw.WriteStartElement(this.SerializerName);
            this.WriteVersionAttribute(tw);
            this.WriteApplicationAttribute(appName, tw);
        }

        protected virtual void WriteVersionAttribute(XmlWriter tw)
        {
            tw.WriteAttributeString("version", this.Version);
        }

        public static string XmlObjectToString(object val)
        {
            if (!(val is string))
            {
                if (val == null)
                {
                    return "~Xtra#NULL";
                }
                MethodInfo info = FindXmlToStringMethod(val.GetType());
                try
                {
                    if (info == null)
                    {
                        if (val is Enum)
                        {
                            return val.ToString();
                        }
                    }
                    else
                    {
                        object[] parameters = new object[] { val };
                        object obj2 = info.Invoke(null, parameters);
                        if (obj2 != null)
                        {
                            return obj2.ToString();
                        }
                    }
                }
                catch
                {
                }
            }
            return val.ToString();
        }

        public static object XmlStringToObject(string str, Type type)
        {
            if (str == "~Xtra#NULL")
            {
                return null;
            }
            if (!type.Equals(typeof(string)))
            {
                MethodInfo info = FindXmlFromStringMethod(type);
                try
                {
                    object obj2;
                    if (info == null)
                    {
                        if (!typeof(Enum).IsAssignableFrom(type))
                        {
                            return str;
                        }
                        else
                        {
                            obj2 = Enum.Parse(type, str);
                        }
                    }
                    else
                    {
                        object[] parameters = new object[] { str };
                        obj2 = info.Invoke(null, parameters);
                    }
                    return obj2;
                }
                catch
                {
                }
            }
            return str;
        }

        protected virtual string SerializerName =>
            "XtraSerializer";

        protected virtual string Version =>
            "1.0";

        public override bool CanUseStream =>
            true;

        private static ConcurrentDictionary<string, Type> Types
        {
            get
            {
                types ??= new ConcurrentDictionary<string, Type>();
                return types;
            }
        }

        protected bool AllowCustomTypes =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly XmlXtraSerializer.<>c <>9 = new XmlXtraSerializer.<>c();
            public static Func<object, string> <>9__30_0;

            internal string <GetAttributes>b__30_0(object v) => 
                v.GetType().FullName;
        }

        private class FakeLineInfoProvider : XmlXtraSerializer.ILineInfoProvider
        {
            public int LineNumber =>
                1;

            public int LinePosition =>
                1;
        }

        private interface ILineInfoProvider
        {
            int LineNumber { get; }

            int LinePosition { get; }
        }

        private class LineInfoProvider : XmlXtraSerializer.ILineInfoProvider
        {
            private readonly IXmlLineInfo lineInfo;

            private LineInfoProvider(IXmlLineInfo lineInfo)
            {
                this.lineInfo = lineInfo;
            }

            public static XmlXtraSerializer.ILineInfoProvider Create(XmlReader reader)
            {
                IXmlLineInfo lineInfo = reader as IXmlLineInfo;
                return ((lineInfo == null) ? ((XmlXtraSerializer.ILineInfoProvider) new XmlXtraSerializer.FakeLineInfoProvider()) : ((XmlXtraSerializer.ILineInfoProvider) new XmlXtraSerializer.LineInfoProvider(lineInfo)));
            }

            public int LineNumber =>
                this.lineInfo.LineNumber;

            public int LinePosition =>
                this.lineInfo.LinePosition;
        }

        public class XmlXtraPropertyInfo : XtraPropertyInfo
        {
            public XmlXtraPropertyInfo(string name, Type propertyType, object val, bool isKey) : base(name, propertyType, val, isKey)
            {
            }

            public static DateTime StringToDateTime(string str)
            {
                DateTime time;
                return (!DateTime.TryParse(str, CultureInfo.InvariantCulture, DateTimeStyles.None, out time) ? XmlConvert.ToDateTime(str) : time);
            }

            public static TimeSpan StringToTimeSpan(string str) => 
                TimeSpan.Parse(str);

            public override object ValueToObject(Type type)
            {
                if (Equals(typeof(object), type) && (base.PropertyType != null))
                {
                    type = base.PropertyType;
                }
                if (!(base.Value is string))
                {
                    return base.Value;
                }
                object obj2 = XmlXtraSerializer.XmlStringToObject(base.Value.ToString(), type);
                return (!(obj2 is string) ? obj2 : this.ObjectConverterImplementation.StringToObject(obj2.ToString(), type));
            }
        }
    }
}

