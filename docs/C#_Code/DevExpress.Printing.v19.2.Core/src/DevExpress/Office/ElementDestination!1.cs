namespace DevExpress.Office
{
    using System;
    using System.Xml;

    public abstract class ElementDestination<T> : Destination where T: IDestinationImporter
    {
        protected ElementDestination(T importer) : base(importer)
        {
        }

        protected override Destination ProcessCurrentElement(XmlReader reader)
        {
            ElementHandler<T> handler;
            string localName = reader.LocalName;
            return (!this.ElementHandlerTable.TryGetValue(localName, out handler) ? null : handler(this.Importer, reader));
        }

        public override void ProcessElementClose(XmlReader reader)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
        }

        public override bool ProcessText(XmlReader reader) => 
            true;

        protected internal virtual T Importer =>
            (T) base.Importer;

        protected internal abstract ElementHandlerTable<T> ElementHandlerTable { get; }
    }
}

