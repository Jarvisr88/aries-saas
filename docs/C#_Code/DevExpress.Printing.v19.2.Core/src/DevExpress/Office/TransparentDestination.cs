namespace DevExpress.Office
{
    using DevExpress.Utils;
    using System;
    using System.Xml;

    public class TransparentDestination : Destination
    {
        private readonly Destination destination;

        public TransparentDestination(IDestinationImporter importer) : base(importer)
        {
            this.destination = importer.PeekDestination();
            Guard.ArgumentNotNull(this.destination, "destination");
        }

        public override Destination Peek() => 
            this.destination.Peek();

        protected override Destination ProcessCurrentElement(XmlReader reader) => 
            this.destination.ProcessCurrentElementInternal(reader);

        public override void ProcessElementClose(XmlReader reader)
        {
        }

        public override void ProcessElementOpen(XmlReader reader)
        {
        }

        public override bool ProcessText(XmlReader reader) => 
            this.destination.ProcessText(reader);
    }
}

