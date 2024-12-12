namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PdfSelectionState
    {
        private PdfSelection selection;
        private PdfCaret caret;

        public event EventHandler SelectionChanged;

        public bool Contains(PdfDocumentPosition position) => 
            this.HasSelection && this.selection.Contains(position);

        private void RaiseSelectionChanged()
        {
            EventHandler selectionChanged = this.SelectionChanged;
            if (selectionChanged != null)
            {
                selectionChanged(this, new EventArgs());
            }
        }

        public bool HasSelection =>
            this.selection != null;

        public bool HasCaret =>
            this.caret != null;

        public PdfSelection Selection
        {
            get => 
                this.selection;
            set
            {
                if (!PdfSelection.AreEqual(this.selection, value))
                {
                    this.selection = value;
                    this.RaiseSelectionChanged();
                }
            }
        }

        public PdfCaret Caret
        {
            get => 
                this.caret;
            set
            {
                if (!ReferenceEquals(this.caret, value))
                {
                    this.caret = value;
                    this.RaiseSelectionChanged();
                }
            }
        }
    }
}

