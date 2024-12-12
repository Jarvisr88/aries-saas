namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfLinkAnnotationState : PdfAnnotationState
    {
        private readonly PdfLinkAnnotation linkAnnotation;
        private Lazy<PdfInteractiveOperation> interactiveOperation;
        private readonly string hintText;

        public PdfLinkAnnotationState(PdfPageState pageState, PdfLinkAnnotation linkAnnotation) : base(pageState, linkAnnotation)
        {
            this.linkAnnotation = linkAnnotation;
            this.interactiveOperation = new Lazy<PdfInteractiveOperation>(() => new PdfInteractiveOperation(this.linkAnnotation.Action, this.linkAnnotation.Destination));
            PdfUriAction action = linkAnnotation.Action as PdfUriAction;
            if (action != null)
            {
                this.hintText = action.Uri;
            }
        }

        public override void Accept(IPdfAnnotationStateVisitor visitor)
        {
            visitor.Visit(this);
        }

        public PdfInteractiveOperation InteractiveOperation =>
            this.interactiveOperation.Value;

        public string HintText =>
            this.hintText;

        public override bool AcceptsTabStop =>
            base.Visible;

        protected override PdfAnnotation Annotation =>
            this.linkAnnotation;
    }
}

