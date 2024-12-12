namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfMarkupAnnotationState : PdfAnnotationState
    {
        private readonly PdfMarkupAnnotation markupAnnotation;

        public PdfMarkupAnnotationState(PdfPageState pageState, PdfMarkupAnnotation markupAnnotation) : base(pageState, markupAnnotation)
        {
            this.markupAnnotation = markupAnnotation;
        }

        public override void Accept(IPdfAnnotationStateVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void NotifyAnnotationChanged()
        {
            base.DocumentState.IsDocumentModified = true;
        }

        public virtual void RebuildAppearance()
        {
        }

        protected internal override void RemoveFromPage()
        {
            base.RemoveFromPage();
            base.PageState.Page.Annotations.Remove(this.markupAnnotation.Popup);
        }

        protected override bool ShouldDrawAnnotation(bool isPrinting) => 
            ((this.markupAnnotation.InReplyTo == null) || (this.markupAnnotation.ReplyType == PdfMarkupAnnotationReplyType.Group)) ? base.ShouldDrawAnnotation(isPrinting) : false;

        public PdfToolTipSettings ToolTipSettings =>
            new PdfToolTipSettings(this.markupAnnotation.Title, this.markupAnnotation.Contents, base.InteractiveArea);

        protected override PdfAnnotation Annotation =>
            this.markupAnnotation;
    }
}

