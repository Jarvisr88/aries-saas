namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Collections;

    public class XmlContext : IXmlContext
    {
        private string elementName = string.Empty;
        private XmlContextItemCollection attributes = new XmlContextItemCollection();
        private XmlContextItemCollection elements = new XmlContextItemCollection();
        private bool xmlDocumentHeader;

        public XmlContext(string elementName)
        {
            this.ElementName = elementName;
        }

        public string ElementName
        {
            get => 
                this.elementName;
            set
            {
                value ??= string.Empty;
                this.elementName = value;
            }
        }

        ICollection IXmlContext.Attributes =>
            this.Attributes;

        ICollection IXmlContext.Elements =>
            this.Elements;

        public XmlContextItemCollection Attributes =>
            this.attributes;

        public XmlContextItemCollection Elements =>
            this.elements;

        public bool XmlDocumentHeader
        {
            get => 
                this.xmlDocumentHeader;
            set => 
                this.xmlDocumentHeader = value;
        }
    }
}

