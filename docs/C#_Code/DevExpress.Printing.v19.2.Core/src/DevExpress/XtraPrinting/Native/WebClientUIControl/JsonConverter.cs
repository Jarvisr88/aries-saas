namespace DevExpress.XtraPrinting.Native.WebClientUIControl
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Xml.Linq;

    public static class JsonConverter
    {
        private const string ElementTextPlaceholder = "#text";

        private static string InnerXmlNodesToJson(Stream stream);
        public static XDocument JsonToXDocument(string json, bool firstChildAsRoot = true);
        public static byte[] JsonToXml(string json, bool firstChildAsRoot = true);
        private static void ModifyRecursively(IEnumerable<XElement> elements);
        private static void OutputNode(string childName, object children, StringBuilder builder, bool showNodeName);
        private static void StoreChildNode(List<JsonConverter.NodeElement> childNodeNames, string nodeName, object nodeValue);
        private static bool TryProcessJsonAttribute(XElement element);
        private static bool TryProcessJsonElementText(XElement element);
        public static string XmlToJson(XElement element);
        public static string XmlToJson(Stream stream, bool includeRootTag = true);
        public static void XmlToJsonNode(StringBuilder builder, XElement node, bool showNodeName);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly JsonConverter.<>c <>9;
            public static Action<XElement> <>9__10_0;

            static <>c();
            internal void <ModifyRecursively>b__10_0(XElement x);
        }

        private class NodeElement
        {
            public NodeElement(string name);

            public string Name { get; private set; }

            public List<object> Nodes { get; private set; }
        }
    }
}

