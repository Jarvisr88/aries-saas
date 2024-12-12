namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils;
    using System;
    using System.IO;
    using System.Xml;

    public abstract class XmlPersistenceHelper
    {
        protected XmlPersistenceHelper()
        {
        }

        protected internal virtual void ContextToStream(IXmlContext context, Stream stream)
        {
            XmlWriterSettings settings = new XmlWriterSettings {
                OmitXmlDeclaration = true
            };
            XmlWriter writer = XmlWriter.Create(stream, settings);
            this.SaveXml(context, writer);
        }

        protected internal virtual string ContextToString(IXmlContext context)
        {
            StringWriter output = new StringWriter();
            if (output != null)
            {
                XmlWriterSettings settings = new XmlWriterSettings {
                    OmitXmlDeclaration = true
                };
                XmlWriter writer = XmlWriter.Create(output, settings);
                this.SaveXml(context, writer);
            }
            return output.ToString();
        }

        public abstract ObjectXmlLoader CreateObjectXmlLoader(System.Xml.XmlNode root);
        protected virtual XmlContextWriter CreateXmlContextWriter(IXmlContext context) => 
            new XmlContextWriter(context);

        public virtual object FromXml(string xml) => 
            this.FromXmlNode(GetRootElement(xml));

        public virtual object FromXmlNode(System.Xml.XmlNode root) => 
            this.ParseXmlDocument(root);

        public static System.Xml.XmlNode GetRootElement(string xml) => 
            !string.IsNullOrEmpty(xml) ? XmlDocumentHelper.GetDocumentElement(SafeXml.CreateDocument(xml, null)) : null;

        protected abstract IXmlContext GetXmlContext();
        protected internal virtual bool IsValidContext(IXmlContext context) => 
            (context != null) ? (!string.IsNullOrEmpty(context.ElementName) ? ((context.Attributes.Count > 0) || (context.Elements.Count > 0)) : false) : false;

        public virtual object ParseXmlDocument(System.Xml.XmlNode root)
        {
            if (root == null)
            {
                return null;
            }
            return this.CreateObjectXmlLoader(root)?.ObjectFromXml();
        }

        protected internal virtual void SaveXml(IXmlContext context, XmlWriter writer)
        {
            this.CreateXmlContextWriter(context).Save(writer);
        }

        public virtual string ToXml()
        {
            IXmlContext xmlContext = this.GetXmlContext();
            return (this.IsValidContext(xmlContext) ? this.ContextToString(xmlContext) : string.Empty);
        }

        public virtual void WriteXml(Stream stream)
        {
            IXmlContext xmlContext = this.GetXmlContext();
            if (this.IsValidContext(xmlContext))
            {
                this.ContextToStream(xmlContext, stream);
            }
        }
    }
}

