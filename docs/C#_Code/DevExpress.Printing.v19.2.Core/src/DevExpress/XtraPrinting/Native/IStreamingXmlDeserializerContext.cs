namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    internal interface IStreamingXmlDeserializerContext
    {
        IStreamingXmlSerializer Serializer { get; }

        XmlReader Reader { get; }

        DeserializeHelper Helper { get; }

        ObjectConverterImplementation Converter { get; }

        Stack<XtraPropertyInfo> DeserializationStack { get; }

        string DeserializationPath { get; }
    }
}

