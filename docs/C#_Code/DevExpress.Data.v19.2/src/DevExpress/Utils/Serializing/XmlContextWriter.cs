namespace DevExpress.Utils.Serializing
{
    using System;
    using System.Collections;
    using System.Xml;

    public class XmlContextWriter
    {
        public const string XmlWhiteSpace = "  ";
        private IXmlContext context;

        public XmlContextWriter(IXmlContext context)
        {
            this.context = context;
        }

        public virtual void Save(XmlWriter writer)
        {
            if (this.context.XmlDocumentHeader)
            {
                this.WriteStartDocument(writer);
            }
            this.WriteStartElement(writer, this.context.ElementName);
            this.WriteAttributes(writer, this.context.Attributes);
            this.WriteElements(writer, this.context.Elements);
            this.WriteEndElement(writer);
            if (this.context.XmlDocumentHeader)
            {
                this.WriteEndDocument(writer);
            }
            writer.Flush();
        }

        protected virtual void WriteAttributes(XmlWriter writer, ICollection items)
        {
            foreach (IXmlContextItem item in items)
            {
                if (!Equals(item.Value, item.DefaultValue))
                {
                    writer.WriteAttributeString(item.Name, item.ValueToString());
                }
            }
        }

        protected virtual void WriteElement(XmlWriter writer, IXmlContextItem item)
        {
            string str = item.ValueToString();
            if (!string.IsNullOrEmpty(str))
            {
                writer.WriteRaw(str);
                this.WriteNewLine(writer);
            }
        }

        protected virtual void WriteElements(XmlWriter writer, ICollection items)
        {
            if (items.Count != 0)
            {
                this.WriteNewLine(writer);
                foreach (IXmlContextItem item in items)
                {
                    this.WriteElement(writer, item);
                }
            }
        }

        protected virtual void WriteEndDocument(XmlWriter writer)
        {
            writer.WriteEndDocument();
        }

        protected virtual void WriteEndElement(XmlWriter writer)
        {
            writer.WriteEndElement();
        }

        protected virtual void WriteIndent(XmlWriter writer, int count)
        {
        }

        protected virtual void WriteNewLine(XmlWriter writer)
        {
            writer.WriteString(Environment.NewLine);
        }

        protected virtual void WriteStartDocument(XmlWriter writer)
        {
            writer.WriteStartDocument();
        }

        protected virtual void WriteStartElement(XmlWriter writer, string name)
        {
            writer.WriteStartElement(name);
        }
    }
}

