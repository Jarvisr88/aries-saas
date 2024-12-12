namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Localization;
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfGoToAction : PdfJumpAction
    {
        internal const string Name = "GoTo";

        internal PdfGoToAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
        }

        public PdfGoToAction(PdfDocument document, PdfDestination destination) : base(document, destination)
        {
            if (destination == null)
            {
                throw new ArgumentNullException("destination");
            }
            if (!ReferenceEquals(document.DocumentCatalog, destination.DocumentCatalog))
            {
                throw new ArgumentException(PdfCoreLocalizer.GetString(PdfCoreStringId.MsgIncorrectDestination), "destination");
            }
        }

        protected internal override void Execute(IPdfInteractiveOperationController interactiveOperationController, IList<PdfPage> pages)
        {
            PdfDestination destination = base.Destination;
            if (destination != null)
            {
                interactiveOperationController.ShowDocumentPosition(destination.CreateTarget(pages));
            }
        }

        protected override string ActionType =>
            "GoTo";

        protected override bool IsInternal =>
            true;
    }
}

