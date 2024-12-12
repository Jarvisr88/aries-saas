namespace Dapper
{
    using System;
    using System.Xml.Linq;

    internal sealed class XDocumentHandler : XmlTypeHandler<XDocument>
    {
        protected override string Format(XDocument xml) => 
            xml.ToString();

        protected override XDocument Parse(string xml) => 
            XDocument.Parse(xml);
    }
}

