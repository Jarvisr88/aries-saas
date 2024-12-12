namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.IO;

    internal interface IStreamingXmlSerializer
    {
        void BeginWrite(object rootObject, Stream stream, string appName, OptionsLayoutBase options);
        void EndWrite();
        XtraPropertyInfo ReadNode(IStreamingXmlDeserializerContext context);
        IStreamingPropertyCollection SerializeInDepthToLevel(IList objects, object levelValue);
        void SerializePart(IXtraPropertyCollection props);
    }
}

