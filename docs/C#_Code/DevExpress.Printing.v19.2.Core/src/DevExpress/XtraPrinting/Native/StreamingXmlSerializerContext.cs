namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class StreamingXmlSerializerContext : IStreamingXmlSerializerContext
    {
        public StreamingXmlSerializerContext(System.IO.Stream stream, SerializeHelper helper, string appName, OptionsLayoutBase options)
        {
            this.Stream = stream;
            this.Helper = helper;
            this.AppName = appName;
            this.Options = options;
            this.SerializationStack = new Stack<XtraPropertyInfo>();
        }

        public bool CheckLevelValue(object value, IStreamingPropertyCollection props)
        {
            if (this.LevelValue == null)
            {
                return false;
            }
            if (this.LevelProperties != null)
            {
                return false;
            }
            if (this.LevelValue != value)
            {
                return false;
            }
            this.LevelProperties = props;
            return true;
        }

        public System.IO.Stream Stream { get; private set; }

        public SerializeHelper Helper { get; private set; }

        public string AppName { get; private set; }

        public OptionsLayoutBase Options { get; private set; }

        public Stack<XtraPropertyInfo> SerializationStack { get; private set; }

        public XmlWriter Writer { get; set; }

        public object LevelValue { get; set; }

        public IStreamingPropertyCollection LevelProperties { get; private set; }
    }
}

