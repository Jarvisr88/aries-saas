namespace Dapper
{
    using System;
    using System.Xml.Linq;

    internal sealed class XElementHandler : XmlTypeHandler<XElement>
    {
        protected override string Format(XElement xml) => 
            xml.ToString();

        protected override XElement Parse(string xml) => 
            XElement.Parse(xml);
    }
}

