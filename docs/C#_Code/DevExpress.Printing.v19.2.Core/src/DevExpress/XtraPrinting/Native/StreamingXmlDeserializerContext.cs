namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class StreamingXmlDeserializerContext : IStreamingXmlDeserializerContext
    {
        public StreamingXmlDeserializerContext(XmlReader reader, IStreamingXmlSerializer serializer, ObjectConverterImplementation converter)
        {
            this.Reader = reader;
            this.Serializer = serializer;
            this.Converter = converter;
            this.DeserializationStack = new Stack<XtraPropertyInfo>();
        }

        public IStreamingXmlSerializer Serializer { get; private set; }

        public ObjectConverterImplementation Converter { get; private set; }

        public XmlReader Reader { get; private set; }

        public DeserializeHelper Helper { get; private set; }

        public Stack<XtraPropertyInfo> DeserializationStack { get; private set; }

        public string DeserializationPath =>
            string.Join<XtraPropertyInfo>(".", this.DeserializationStack.Reverse<XtraPropertyInfo>());
    }
}

