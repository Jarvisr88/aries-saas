namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfWidgetAnnotationState<T> : PdfAnnotationState, IPdfFormFieldProvider where T: PdfInteractiveFormField
    {
        private readonly Lazy<PdfInteractiveOperation> interactiveOperation;
        private readonly PdfWidgetAnnotation widget;
        private readonly T formField;
        private PdfForm highlightedForm;
        private PdfRgbaColor formHighlightColor;

        protected PdfWidgetAnnotationState(PdfPageState pageState, PdfWidgetAnnotation annotation, T formField) : base(pageState, annotation)
        {
            this.widget = annotation;
            this.interactiveOperation = new Lazy<PdfInteractiveOperation>(() => new PdfInteractiveOperation(base.widget.Action));
            this.formField = formField;
            formField.WidgetAppearanceChanged += new EventHandler(this.OnFormFieldValueChanged);
            base.DocumentState.DocumentStateChanged += new PdfDocumentStateChangedEventHandler(this.OnDocumentStateChanged);
        }

        private bool CompareHighlightColor(PdfRgbaColor highlight) => 
            !ReferenceEquals(highlight, this.formHighlightColor) ? ((highlight != null) && ((this.formHighlightColor != null) && ((highlight.R == this.formHighlightColor.R) && ((highlight.G == this.formHighlightColor.G) && ((highlight.B == this.formHighlightColor.B) && (highlight.A == this.formHighlightColor.A)))))) : true;

        void IPdfFormFieldProvider.ResetValue()
        {
            this.ResetFormField(this.formField);
        }

        protected override PdfForm GetDrawingForm(PdfRgbaColor highlight)
        {
            if ((highlight != null) && (this.IsEditableFormField && (!this.ReadOnly && (base.AppearanceState == PdfAnnotationAppearanceState.Normal))))
            {
                if ((this.highlightedForm == null) || !this.CompareHighlightColor(highlight))
                {
                    this.RebuildHighlightedAppearance(highlight);
                }
                if (this.highlightedForm != null)
                {
                    return this.highlightedForm;
                }
            }
            return base.GetDrawingForm(highlight);
        }

        private void OnDocumentStateChanged(object sender, PdfDocumentStateChangedEventArgs e)
        {
            if (!this.CompareHighlightColor(base.HighlightColor))
            {
                this.formHighlightColor = null;
                this.highlightedForm = null;
                base.RaiseAppearanceChanged();
            }
        }

        private void OnFormFieldValueChanged(object sender, EventArgs args)
        {
            if ((this.highlightedForm != null) && (this.formHighlightColor != null))
            {
                this.RebuildHighlightedAppearance(this.formHighlightColor);
            }
            base.RaiseAppearanceChanged();
        }

        private void RebuildHighlightedAppearance(PdfRgbaColor highlight)
        {
            this.formHighlightColor = highlight;
            IPdfAnnotationAppearanceBuilder builder = new PdfHighlightedWidgetAppearanceBuilderFactory(base.DocumentState.FontSearch, highlight).Create(this.formField);
            if (builder != null)
            {
                PdfForm form = (this.highlightedForm != null) ? this.highlightedForm : base.GetDrawingForm(new PdfRgbaColor(0.0, 0.0, 0.0, 1.0));
                if (form != null)
                {
                    this.highlightedForm = new PdfForm(form);
                }
                else
                {
                    PdfRectangle rect = this.Annotation.Rect;
                    this.highlightedForm = new PdfForm(base.DocumentState.Document.DocumentCatalog, new PdfRectangle(0.0, 0.0, rect.Width, rect.Height));
                }
                builder.RebuildAppearance(this.highlightedForm);
            }
        }

        private void ResetFormField(PdfInteractiveFormField field)
        {
            field.SetValue(field.DefaultValue, base.DocumentState.FontSearch);
            if (field.Kids != null)
            {
                foreach (PdfInteractiveFormField field2 in field.Kids)
                {
                    this.ResetFormField(field2);
                }
            }
        }

        public void SetValue(object value)
        {
            this.formField.SetValue(value, base.DocumentState.FontSearch);
        }

        public T FormField =>
            this.formField;

        public PdfWidgetAnnotation Widget =>
            this.widget;

        public int RotationAngle
        {
            get
            {
                PdfWidgetAppearanceCharacteristics appearanceCharacteristics = this.widget.AppearanceCharacteristics;
                return ((appearanceCharacteristics == null) ? 0 : appearanceCharacteristics.RotationAngle);
            }
        }

        public override bool ReadOnly =>
            this.FormField.Flags.HasFlag(PdfInteractiveFormFieldFlags.ReadOnly);

        public override bool AcceptsTabStop =>
            base.Visible && !this.ReadOnly;

        public bool SummaryRotationIsZero =>
            this.SummaryRotationAngle == 0;

        public int SummaryRotationAngle =>
            PdfPageTreeObject.NormalizeRotate((base.DocumentState.RotationAngle + base.PageState.Page.Rotate) - this.RotationAngle);

        public PdfInteractiveOperation InteractiveOperation =>
            this.interactiveOperation.Value;

        public virtual bool CanOpenEditorInReadOnlyMode =>
            true;

        protected override PdfAnnotation Annotation =>
            this.widget;

        protected virtual bool IsEditableFormField =>
            true;

        PdfInteractiveFormField IPdfFormFieldProvider.FormField =>
            this.formField;
    }
}

