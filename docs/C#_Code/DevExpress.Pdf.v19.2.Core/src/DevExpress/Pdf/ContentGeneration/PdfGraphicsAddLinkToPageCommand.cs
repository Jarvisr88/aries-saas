namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PdfGraphicsAddLinkToPageCommand : PdfGraphicsAddLinkCommand
    {
        private readonly int pageNumber;
        private readonly float? x;
        private readonly float? y;
        private readonly float? zoom;
        private readonly string destinationName;

        public PdfGraphicsAddLinkToPageCommand(RectangleF linkArea, string destinationName) : base(linkArea)
        {
            this.destinationName = destinationName;
        }

        public PdfGraphicsAddLinkToPageCommand(RectangleF linkArea, int pageNumber, float? x, float? y, float? zoom) : base(linkArea)
        {
            double? nullable1;
            this.pageNumber = pageNumber;
            this.x = x;
            this.y = y;
            this.zoom = zoom;
            float? nullable = zoom;
            if (nullable != null)
            {
                nullable1 = new double?((double) nullable.GetValueOrDefault());
            }
            else
            {
                nullable1 = null;
            }
            PdfXYZDestination.ValidateZoomValue(nullable1);
        }

        protected override PdfLinkAnnotation CreateLinkAnnotation(PdfRectangle rect, PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            string destinationName = this.destinationName;
            if (!string.IsNullOrEmpty(destinationName))
            {
                IDictionary<string, PdfDestination> pageDestinations = page.DocumentCatalog.Names.PageDestinations;
                if (!pageDestinations.ContainsKey(destinationName))
                {
                    pageDestinations.Add(destinationName, null);
                }
            }
            else
            {
                double? nullable4;
                double? nullable1;
                double? nullable5;
                double? nullable6;
                if (this.x != null)
                {
                    nullable1 = new double?(constructor.TransformX((double) this.x.Value));
                }
                else
                {
                    nullable4 = null;
                    nullable1 = nullable4;
                }
                double? left = nullable1;
                if (this.y != null)
                {
                    nullable5 = new double?(constructor.TransformY((double) this.y.Value));
                }
                else
                {
                    nullable4 = null;
                    nullable5 = nullable4;
                }
                PdfDocumentCatalog documentCatalog = page.DocumentCatalog;
                float? zoom = this.zoom;
                if (zoom != null)
                {
                    nullable6 = new double?((double) zoom.GetValueOrDefault());
                }
                else
                {
                    nullable4 = null;
                    nullable6 = nullable4;
                }
                destinationName = documentCatalog.Names.AddDestination(new PdfXYZDestination(documentCatalog, this.pageNumber - 1, left, nullable5, nullable6));
            }
            return new PdfLinkAnnotation(page, rect, new PdfDestinationObject(destinationName));
        }
    }
}

