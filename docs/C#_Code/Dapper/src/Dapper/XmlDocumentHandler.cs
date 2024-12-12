namespace Dapper
{
    using System;
    using System.Xml;

    internal sealed class XmlDocumentHandler : XmlTypeHandler<XmlDocument>
    {
        protected override string Format(XmlDocument xml) => 
            xml.OuterXml;

        protected override XmlDocument Parse(string xml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(xml);
            return document;
        }
    }
}

