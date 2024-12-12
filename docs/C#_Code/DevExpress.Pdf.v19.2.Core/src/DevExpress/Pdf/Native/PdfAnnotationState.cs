namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class PdfAnnotationState
    {
        private readonly PdfPageState pageState;
        private readonly PdfDocumentArea documentArea;
        private PdfAnnotationAppearanceState appearanceState;

        public event PdfAppearanceChangedEventHandler AppearanceChanged;

        protected PdfAnnotationState(PdfPageState pageState, PdfAnnotation annotation)
        {
            this.pageState = pageState;
            PdfRectangle cropBox = pageState.Page.CropBox;
            double left = cropBox.Left;
            double bottom = cropBox.Bottom;
            PdfRectangle rect = annotation.Rect;
            this.documentArea = new PdfDocumentArea(pageState.PageIndex + 1, new PdfRectangle(rect.Left - left, rect.Bottom - bottom, rect.Right - left, rect.Top - bottom));
        }

        public abstract void Accept(IPdfAnnotationStateVisitor visitor);
        public bool Contains(PdfPoint cropBoxPoint) => 
            this.ContainsPoint(PdfPoint.Add(this.pageState.Page.CropBox.BottomLeft, cropBoxPoint));

        protected virtual bool ContainsPoint(PdfPoint point) => 
            this.Rect.Contains(point);

        public PdfAnnotationStatePaintData CreatePaintData(bool isPrinting, PdfRgbaColor highlightColor)
        {
            if (!this.ShouldDrawAnnotation(isPrinting))
            {
                return null;
            }
            PdfDocumentStateBase documentState = this.DocumentState;
            PdfRectangle rect = this.Annotation.Rect;
            PdfForm drawingForm = this.GetDrawingForm(highlightColor);
            return (((drawingForm == null) || ((drawingForm.BBox == null) || ((drawingForm.BBox.Width == 0.0) || (drawingForm.BBox.Height == 0.0)))) ? null : new PdfAnnotationStatePaintData(drawingForm, drawingForm.GetTrasformationMatrix(rect)));
        }

        protected virtual PdfForm GetDrawingForm(PdfRgbaColor highlight) => 
            this.Annotation.EnsureAppearance(this.appearanceState, this.pageState.DocumentState);

        public PdfPoint[] GetFocusPolygon()
        {
            if (!this.ShouldPaintFocusRect)
            {
                return null;
            }
            PdfRectangle rect = this.Annotation.Rect;
            return new PdfPoint[] { rect.TopLeft, rect.TopRight, rect.BottomRight, rect.BottomLeft };
        }

        public virtual IList<PdfPoint[]> GetSelectionPolygon()
        {
            if (!this.Selected)
            {
                return null;
            }
            PdfRectangle rect = this.Annotation.Rect;
            PdfPoint[] pointArray1 = new PdfPoint[] { rect.TopLeft, rect.TopRight, rect.BottomRight, rect.BottomLeft };
            return new PdfPoint[][] { pointArray1 };
        }

        protected void RaiseAppearanceChanged()
        {
            if (this.AppearanceChanged != null)
            {
                this.AppearanceChanged(this);
            }
        }

        protected void RaiseStateChanged()
        {
            this.DocumentState.RaiseDocumentStateChanged(PdfDocumentStateChangedFlags.Annotation);
            this.RaiseAppearanceChanged();
        }

        protected internal virtual void RemoveFromPage()
        {
            this.pageState.AnnotationStates.Remove(this);
            this.pageState.Page.Annotations.Remove(this.Annotation);
        }

        protected virtual bool ShouldDrawAnnotation(bool isPrinting)
        {
            PdfAnnotationFlags flags = this.Annotation.Flags;
            return (!isPrinting ? this.Visible : (flags.HasFlag(PdfAnnotationFlags.Print) && (!flags.HasFlag(PdfAnnotationFlags.Hidden) && ((this.Rect.Width != 0.0) && !(this.Rect.Height == 0.0)))));
        }

        public PdfRectangle Rect =>
            this.Annotation.Rect;

        public bool Visible =>
            (this.Annotation.Rect.Width != 0.0) && ((this.Annotation.Rect.Height != 0.0) && (((PdfAnnotationFlags.NoView | PdfAnnotationFlags.Hidden) & this.Annotation.Flags) == PdfAnnotationFlags.None));

        public bool Focused =>
            ReferenceEquals(this.DocumentState.FocusedAnnotation, this);

        public bool Selected =>
            ReferenceEquals(this.DocumentState.SelectedAnnotation, this);

        public virtual bool ReadOnly =>
            this.Annotation.Flags.HasFlag(PdfAnnotationFlags.ReadOnly);

        public virtual bool AcceptsTabStop =>
            false;

        public PdfDocumentArea InteractiveArea =>
            this.documentArea;

        public PdfAnnotationAppearanceState AppearanceState
        {
            get => 
                this.appearanceState;
            set
            {
                if (!this.ReadOnly && (value != this.appearanceState))
                {
                    PdfAnnotationAppearances appearance = this.Annotation.Appearance;
                    if ((value == PdfAnnotationAppearanceState.Rollover) ? ((appearance != null) && (appearance.Rollover != null)) : ((value == PdfAnnotationAppearanceState.Down) ? ((appearance != null) && (appearance.Down != null)) : true))
                    {
                        this.appearanceState = value;
                        this.RaiseStateChanged();
                    }
                    else
                    {
                        this.AppearanceState = PdfAnnotationAppearanceState.Normal;
                    }
                }
            }
        }

        public string Name =>
            this.Annotation.Name;

        public int PageNumber =>
            this.pageState.PageIndex + 1;

        protected abstract PdfAnnotation Annotation { get; }

        protected PdfDocumentStateBase DocumentState =>
            this.pageState.DocumentState;

        protected IPdfExportFontProvider FontSearch =>
            this.DocumentState.FontSearch;

        protected PdfPageState PageState =>
            this.pageState;

        protected PdfRgbaColor HighlightColor =>
            this.DocumentState.HighlightFormFields ? this.DocumentState.HighlightedFormFieldColor : null;

        protected virtual bool ShouldPaintFocusRect =>
            this.Focused;
    }
}

