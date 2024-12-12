namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfInteractiveOperation
    {
        private readonly PdfAction action;
        private readonly PdfDestination destination;

        public PdfInteractiveOperation(PdfAction action) : this(action, null)
        {
        }

        public PdfInteractiveOperation(PdfAction action, PdfDestination destination)
        {
            this.action = action;
            this.destination = destination;
        }

        public PdfAction Action =>
            this.action;

        public PdfDestination Destination =>
            this.destination;
    }
}

