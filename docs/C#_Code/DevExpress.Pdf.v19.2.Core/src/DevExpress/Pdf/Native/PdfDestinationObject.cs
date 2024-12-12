namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfDestinationObject
    {
        private PdfDestination destination;
        private readonly string destinationName;

        public PdfDestinationObject(PdfDestination destination)
        {
            this.destination = destination;
        }

        public PdfDestinationObject(string destinationName)
        {
            this.destinationName = destinationName;
        }

        public PdfDestination GetDestination(PdfDocumentCatalog documentCatalog, bool isInternal)
        {
            if ((this.destination == null) && (this.destinationName != null))
            {
                IDictionary<string, PdfDestination> destinations = documentCatalog.Destinations;
                if ((destinations != null) && !destinations.TryGetValue(this.destinationName, out this.destination))
                {
                    foreach (KeyValuePair<string, PdfDestination> pair in destinations)
                    {
                        if (pair.Key.Equals(this.destinationName, StringComparison.OrdinalIgnoreCase))
                        {
                            this.destination = pair.Value;
                            break;
                        }
                    }
                }
            }
            if (isInternal && (this.destination != null))
            {
                this.destination.ResolveInternalPage();
            }
            return this.destination;
        }

        public object ToWriteableObject(PdfDocumentCatalog documentCatalog, PdfObjectCollection objects, bool isInternal)
        {
            objects.AddSavedDestinationName(this.destinationName);
            string destinationName = objects.GetDestinationName(this.destinationName);
            return ((destinationName == null) ? ((object) objects.AddObject((PdfObject) this.GetDestination(documentCatalog, isInternal))) : ((object) new PdfNameTreeEncoding().GetBytes(destinationName)));
        }

        public string DestinationName =>
            this.destinationName;
    }
}

