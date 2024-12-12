namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml;

    public abstract class Destination
    {
        private readonly IDestinationImporter importer;

        protected Destination(IDestinationImporter importer)
        {
            Guard.ArgumentNotNull(importer, "importer");
            this.importer = importer;
        }

        public virtual Destination Peek() => 
            this;

        public bool Process(XmlReader reader)
        {
            while (!this.ProcessCore(reader))
            {
                if (reader.ReadState == System.Xml.ReadState.EndOfFile)
                {
                    return false;
                }
            }
            return true;
        }

        protected internal virtual bool ProcessCore(XmlReader reader)
        {
            if (reader.NodeType == XmlNodeType.EndElement)
            {
                Destination destination2 = this.Importer.PopDestination();
                if (!destination2.ForbidProcessElementOpenClose)
                {
                    destination2.ProcessElementClose(reader);
                }
                return true;
            }
            if (this.ShouldProcessWhitespaces(reader))
            {
                return this.Importer.PeekDestination().ProcessText(reader);
            }
            if ((reader.NodeType == XmlNodeType.Text) || ((reader.NodeType == XmlNodeType.SignificantWhitespace) || (reader.NodeType == XmlNodeType.CDATA)))
            {
                return this.Importer.PeekDestination().ProcessText(reader);
            }
            Destination destination = this.ProcessCurrentElement(reader);
            if (destination == null)
            {
                reader.Skip();
                return false;
            }
            if (reader.NodeType == XmlNodeType.Element)
            {
                if (reader.IsEmptyElement)
                {
                    destination.ProcessElementOpen(reader);
                    destination.ProcessElementClose(reader);
                }
                else
                {
                    this.Importer.PushDestination(destination);
                    if (!destination.ForbidProcessElementOpenClose)
                    {
                        destination.ProcessElementOpen(reader);
                    }
                }
            }
            return true;
        }

        protected abstract Destination ProcessCurrentElement(XmlReader reader);
        internal Destination ProcessCurrentElementInternal(XmlReader reader) => 
            this.ProcessCurrentElement(reader);

        public abstract void ProcessElementClose(XmlReader reader);
        public abstract void ProcessElementOpen(XmlReader reader);
        public abstract bool ProcessText(XmlReader reader);
        public virtual bool ShouldProcessWhitespaces(XmlReader reader) => 
            false;

        public IDestinationImporter Importer =>
            this.importer;

        internal bool ForbidProcessElementOpenClose { get; set; }
    }
}

