namespace DevExpress.Pdf.Native
{
    using System;
    using System.Text;
    using System.Xml;

    public class PdfXmpMetadataBuilder
    {
        private const string pdfaidNamespace = "http://www.aiim.org/pdfa/ns/id/";
        private const string pdfNamespace = "http://ns.adobe.com/pdf/1.3/";
        private const string dcNamespace = "http://purl.org/dc/elements/1.1/";
        private const string xmpNamespace = "http://ns.adobe.com/xap/1.0/";
        private const string rdfNamespace = "http://www.w3.org/1999/02/22-rdf-syntax-ns#";
        private readonly StringBuilder stringBuilder = new StringBuilder();
        private readonly XmlWriter writer;

        public PdfXmpMetadataBuilder()
        {
            XmlWriterSettings settings = new XmlWriterSettings {
                OmitXmlDeclaration = true,
                CheckCharacters = false
            };
            this.writer = XmlWriter.Create(this.stringBuilder, settings);
            this.writer.WriteProcessingInstruction("xpacket", "begin=\"\" id=\"W5M0MpCehiHzreSzNTczkc9d\"");
            this.writer.WriteStartElement("x", "xmpmeta", "adobe:ns:meta/");
            this.AppendRdfStartElement("RDF");
            this.AppendRdfStartElement("Description");
            this.writer.WriteAttributeString("", "about", "http://www.w3.org/1999/02/22-rdf-syntax-ns#", "");
            this.AppendAttribute("pdfaid", "http://www.aiim.org/pdfa/ns/id/");
            this.AppendAttribute("pdf", "http://ns.adobe.com/pdf/1.3/");
            this.AppendAttribute("dc", "http://purl.org/dc/elements/1.1/");
            this.AppendAttribute("xmp", "http://ns.adobe.com/xap/1.0/");
        }

        private void AppendAttribute(string localName, string value)
        {
            this.writer.WriteAttributeString("xmlns", localName, "", value);
        }

        public void AppendDcElement(string localName, string containerLocalName, bool writeLangAttribute, string value)
        {
            if (value != null)
            {
                this.AppendDcStartElement(localName);
                this.AppendRdfStartElement(containerLocalName);
                this.AppendRdfStartElement("li");
                if (writeLangAttribute)
                {
                    this.writer.WriteAttributeString("xml", "lang", "", "x-default");
                }
                this.writer.WriteValue(value);
                this.writer.WriteEndElement();
                this.writer.WriteEndElement();
                this.writer.WriteEndElement();
            }
        }

        public void AppendDcStartElement(string localName)
        {
            this.writer.WriteStartElement("dc", localName, "http://purl.org/dc/elements/1.1/");
        }

        private void AppendElement(string prefix, string localName, string xmlNamespace, string value)
        {
            if (value != null)
            {
                this.writer.WriteStartElement(prefix, localName, xmlNamespace);
                this.writer.WriteValue(value);
                this.writer.WriteEndElement();
            }
        }

        public void AppendEndElement()
        {
            this.writer.WriteEndElement();
        }

        public void AppendPdfACompatibilityElement(int pdfAVersion)
        {
            this.AppendElement("pdfaid", "part", "http://www.aiim.org/pdfa/ns/id/", pdfAVersion.ToString());
            this.AppendElement("pdfaid", "conformance", "http://www.aiim.org/pdfa/ns/id/", "B");
        }

        public void AppendPdfElement(string localName, string value)
        {
            this.AppendElement("pdf", localName, "http://ns.adobe.com/pdf/1.3/", value);
        }

        public void AppendRdfElement(string localName, string value)
        {
            this.AppendRdfStartElement(localName);
            this.writer.WriteValue(value);
            this.writer.WriteEndElement();
        }

        public void AppendRdfStartElement(string localName)
        {
            this.writer.WriteStartElement("rdf", localName, "http://www.w3.org/1999/02/22-rdf-syntax-ns#");
        }

        public void AppendXmpElement(string localName, string value)
        {
            this.AppendElement("xmp", localName, "http://ns.adobe.com/xap/1.0/", value);
        }

        public string GetXml(string additionalMetadata)
        {
            this.writer.WriteEndElement();
            if (!string.IsNullOrEmpty(additionalMetadata))
            {
                this.writer.WriteRaw(additionalMetadata);
            }
            this.writer.WriteEndElement();
            this.writer.WriteEndElement();
            this.writer.WriteProcessingInstruction("xpacket", "end=\"w\"");
            this.writer.Flush();
            return this.stringBuilder.ToString();
        }
    }
}

