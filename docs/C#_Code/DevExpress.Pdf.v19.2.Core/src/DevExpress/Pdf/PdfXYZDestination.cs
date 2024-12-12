namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfXYZDestination : PdfDestination
    {
        internal const string Name = "XYZ";
        private readonly double? left;
        private readonly double? top;
        private readonly double? zoom;

        private PdfXYZDestination(PdfXYZDestination destination, int objectNumber) : base(destination, objectNumber)
        {
            this.left = destination.left;
            this.top = destination.top;
            this.zoom = destination.zoom;
        }

        public PdfXYZDestination(PdfPage page, double? left, double? top, double? zoom) : base(page)
        {
            this.left = left;
            this.top = top;
            this.zoom = zoom;
            ValidateZoomValue(zoom);
        }

        internal PdfXYZDestination(PdfDocumentCatalog documentCatalog, object pageObject, double? left, double? top, double? zoom) : base(documentCatalog, pageObject)
        {
            this.left = left;
            this.top = top;
            this.zoom = zoom;
        }

        protected override void AddWriteableParameters(IList<object> parameters)
        {
            parameters.Add(new PdfName("XYZ"));
            AddParameter(parameters, this.left);
            AddParameter(parameters, this.top);
            AddParameter(parameters, this.zoom);
        }

        protected override PdfDestination CreateDuplicate(int objectNumber) => 
            new PdfXYZDestination(this, objectNumber);

        protected internal override PdfTarget CreateTarget(IList<PdfPage> pages)
        {
            double? nullable;
            if (this.zoom == null)
            {
                nullable = null;
            }
            else
            {
                double? nullable1;
                double? zoom = this.zoom;
                double num = zoom.Value;
                if (num != 0.0)
                {
                    nullable1 = new double?(num);
                }
                else
                {
                    zoom = null;
                    nullable1 = zoom;
                }
                nullable = nullable1;
            }
            return new PdfTarget(base.CalculatePageIndex(pages), this.left, base.ValidateVerticalCoordinate(this.top), nullable);
        }

        internal static void ValidateZoomValue(double? zoom)
        {
            if ((zoom != null) && (zoom.Value < 0.0))
            {
                throw new ArgumentOutOfRangeException("zoom", PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectZoom));
            }
        }

        public double? Left =>
            this.left;

        public double? Top =>
            this.top;

        public double? Zoom =>
            this.zoom;
    }
}

