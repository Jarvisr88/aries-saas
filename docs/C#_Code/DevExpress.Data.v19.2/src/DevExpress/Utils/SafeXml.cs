namespace DevExpress.Utils
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Xml;

    public static class SafeXml
    {
        private const DtdProcessing XxeSafeDtdProcessing = DtdProcessing.Prohibit;
        private const XmlResolver SsrfSafeXmlResolver = null;

        public static XmlDocument CreateDocument(XmlResolver xmlResolver = null) => 
            new XmlDocument { XmlResolver = xmlResolver };

        public static XmlDocument CreateDocument(string xml, XmlResolver xmlResolver = null) => 
            LoadXml(CreateDocument(xmlResolver), xml, xmlResolver);

        public static XmlDocument CreateDocument(Stream stream, XmlResolver xmlResolver = null, Action<XmlReaderSettings> settings = null)
        {
            XmlDocument document = CreateDocument(xmlResolver);
            document.Load(XmlReader.Create(stream, CreateReaderSettings(DtdProcessing.Prohibit, xmlResolver, settings)));
            return document;
        }

        public static XmlReader CreateReader(Stream stream, Action<XmlReaderSettings> settings) => 
            XmlReader.Create(stream, CreateReaderSettings(DtdProcessing.Prohibit, null, settings));

        public static XmlReader CreateReader(TextReader textReader, Action<XmlReaderSettings> settings) => 
            XmlReader.Create(textReader, CreateReaderSettings(DtdProcessing.Prohibit, null, settings));

        public static XmlReader CreateReader(Stream stream, DtdProcessing dtdProcessing = 0, XmlResolver xmlResolver = null) => 
            XmlReader.Create(stream, CreateReaderSettings(dtdProcessing, xmlResolver, null));

        public static XmlReader CreateReader(TextReader textReader, DtdProcessing dtdProcessing = 0, XmlResolver xmlResolver = null) => 
            XmlReader.Create(textReader, CreateReaderSettings(dtdProcessing, xmlResolver, null));

        public static XmlReaderSettings CreateReaderSettings(DtdProcessing dtdProcessing = 0, XmlResolver xmlResolver = null, Action<XmlReaderSettings> settings = null)
        {
            XmlReaderSettings settings2 = new XmlReaderSettings {
                DtdProcessing = dtdProcessing,
                XmlResolver = xmlResolver
            };
            if (settings != null)
            {
                settings(settings2);
            }
            return settings2;
        }

        public static XmlTextReader CreateTextReader(Stream stream, DtdProcessing dtdProcessing = 0, XmlResolver xmlResolver = null) => 
            new XmlTextReader(stream).EnsureTextReader(dtdProcessing, xmlResolver);

        public static XmlTextReader CreateTextReader(TextReader textReader, DtdProcessing dtdProcessing = 0, XmlResolver xmlResolver = null) => 
            new XmlTextReader(textReader).EnsureTextReader(dtdProcessing, xmlResolver);

        public static XmlTextReader EnsureTextReader(this XmlTextReader reader, DtdProcessing dtdProcessing, XmlResolver xmlResolver)
        {
            reader.DtdProcessing = dtdProcessing;
            reader.XmlResolver = xmlResolver;
            return reader;
        }

        private static XmlDocument LoadXml(XmlDocument document, string xml, XmlResolver xmlResolver)
        {
            using (XmlTextReader reader = new XmlTextReader(new StringReader(xml), document.NameTable).EnsureTextReader(DtdProcessing.Prohibit, xmlResolver))
            {
                document.Load(XmlDocument_SetupReader(reader));
                return document;
            }
        }

        private static XmlTextReader XmlDocument_SetupReader(XmlTextReader xmlTextReader)
        {
            XmlTextReaderExtension.SetValidatingReaderCompatibilityMode(xmlTextReader, true);
            xmlTextReader.EntityHandling = EntityHandling.ExpandCharEntities;
            return xmlTextReader;
        }

        private static class XmlTextReaderExtension
        {
            private static readonly Action<XmlTextReader, bool> mutator = EmitMutator();

            private static Action<XmlTextReader, bool> EmitMutator()
            {
                MethodInfo meth = typeof(XmlTextReader).GetMethod("set_XmlValidatingReaderCompatibilityMode", BindingFlags.NonPublic | BindingFlags.Instance);
                Type[] parameterTypes = new Type[] { typeof(XmlTextReader), typeof(bool) };
                DynamicMethod method = new DynamicMethod("__set_XmlValidatingReaderCompatibilityMode", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(void), parameterTypes, typeof(SafeXml.XmlTextReaderExtension).Module, true);
                ILGenerator iLGenerator = method.GetILGenerator();
                iLGenerator.Emit(OpCodes.Ldarg_0);
                iLGenerator.Emit(OpCodes.Ldarg_1);
                iLGenerator.Emit(OpCodes.Call, meth);
                iLGenerator.Emit(OpCodes.Ret);
                return (method.CreateDelegate(typeof(Action<XmlTextReader, bool>)) as Action<XmlTextReader, bool>);
            }

            internal static void SetValidatingReaderCompatibilityMode(XmlTextReader xmlTextReader, bool value)
            {
                mutator(xmlTextReader, value);
            }
        }
    }
}

