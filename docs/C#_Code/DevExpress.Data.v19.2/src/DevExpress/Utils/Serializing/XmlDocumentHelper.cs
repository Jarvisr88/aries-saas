namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Xml;

    public static class XmlDocumentHelper
    {
        public static DXXmlNodeCollection ConvertXmlNodeList(XmlNodeList list)
        {
            DXXmlNodeCollection nodes = new DXXmlNodeCollection();
            foreach (System.Xml.XmlNode node in list)
            {
                nodes.Add(node);
            }
            return nodes;
        }

        public static DXXmlNodeCollection GetChildren(System.Xml.XmlNode root) => 
            ConvertXmlNodeList(root.ChildNodes);

        public static System.Xml.XmlNode GetDocumentElement(XmlDocument doc) => 
            doc.DocumentElement;

        public static XmlDocument LoadFromStream(Stream stream) => 
            SafeXml.CreateDocument(stream, null, null);

        public static XmlDocument LoadFromXml(string xml) => 
            SafeXml.CreateDocument(xml, null);
    }
}

