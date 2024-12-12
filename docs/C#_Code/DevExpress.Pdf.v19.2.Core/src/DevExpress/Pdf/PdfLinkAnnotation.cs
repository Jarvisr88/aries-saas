namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfLinkAnnotation : PdfAnnotation
    {
        private const string destinationDictionaryKey = "Dest";
        private const string uriActionDictionaryKey = "PA";
        internal const string Type = "Link";
        private PdfAction action;
        private PdfDestinationObject destination;
        private PdfAnnotationHighlightingMode highlightingMode;
        private PdfUriAction uriAction;
        private IList<PdfQuadrilateral> region;
        private PdfAnnotationBorderStyle borderStyle;

        private PdfLinkAnnotation(PdfPage page, IPdfLinkAnnotationBuilder linkBuilder) : base(page, linkBuilder)
        {
            this.destination = linkBuilder.Destination;
            this.action = (linkBuilder.Uri == null) ? null : new PdfUriAction(page.DocumentCatalog, linkBuilder.Uri);
        }

        internal PdfLinkAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        internal PdfLinkAnnotation(PdfPage page, PdfRectangle rect, PdfDestinationObject destination) : this(page, new PdfLinkAnnotationBuilder(rect, destination))
        {
        }

        internal PdfLinkAnnotation(PdfPage page, PdfRectangle rect, string uri) : this(page, new PdfLinkAnnotationBuilder(rect, uri))
        {
        }

        protected internal override void Accept(IPdfAnnotationVisitor visitor)
        {
            visitor.Visit(this);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("A", this.action);
            if (this.destination != null)
            {
                dictionary.Add("Dest", this.destination.ToWriteableObject(base.DocumentCatalog, objects, true));
            }
            if (this.highlightingMode != PdfAnnotationHighlightingMode.Invert)
            {
                dictionary.AddEnumName<PdfAnnotationHighlightingMode>("H", this.highlightingMode);
            }
            if (this.uriAction != null)
            {
                dictionary.Add("PA", objects.AddObject((PdfObject) this.uriAction));
            }
            if (this.borderStyle != null)
            {
                dictionary.Add("BS", objects.AddObject((PdfObject) this.borderStyle));
            }
            PdfQuadrilateral.Write(dictionary, this.region);
            return dictionary;
        }

        protected override void ResolveDictionary(PdfReaderDictionary dictionary)
        {
            base.ResolveDictionary(dictionary);
            this.action = dictionary.GetAction("A");
            this.destination = dictionary.GetDestination("Dest");
            if ((this.action != null) && (this.destination != null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.highlightingMode = dictionary.GetAnnotationHighlightingMode();
            PdfAction action = dictionary.GetAction("PA");
            if (action != null)
            {
                this.uriAction = action as PdfUriAction;
                if (this.uriAction == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            this.region = PdfQuadrilateral.ParseArray(dictionary);
            this.borderStyle = PdfAnnotationBorderStyle.Parse(dictionary);
        }

        public PdfAction Action
        {
            get
            {
                this.Ensure();
                return this.action;
            }
        }

        public PdfDestination Destination
        {
            get
            {
                this.Ensure();
                return this.destination?.GetDestination(base.DocumentCatalog, true);
            }
        }

        public PdfAnnotationHighlightingMode HighlightingMode
        {
            get
            {
                this.Ensure();
                return this.highlightingMode;
            }
        }

        public PdfUriAction UriAction
        {
            get
            {
                this.Ensure();
                return this.uriAction;
            }
        }

        public IList<PdfQuadrilateral> Region
        {
            get
            {
                this.Ensure();
                return this.region;
            }
        }

        public PdfAnnotationBorderStyle BorderStyle
        {
            get
            {
                this.Ensure();
                return this.borderStyle;
            }
        }

        protected override string AnnotationType =>
            "Link";
    }
}

