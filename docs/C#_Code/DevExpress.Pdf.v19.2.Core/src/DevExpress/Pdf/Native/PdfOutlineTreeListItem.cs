namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PdfOutlineTreeListItem : INotifyPropertyChanged
    {
        private readonly PdfOutline outline;
        private readonly int id;
        private readonly int parentId;
        private readonly string title;
        private readonly bool italic;
        private readonly bool bold;
        private readonly PdfColor foreColor;
        private readonly bool hasChildNodes;
        private bool useForeColor;
        private PdfInteractiveOperation interactiveOperation;

        public event PropertyChangedEventHandler PropertyChanged;

        internal PdfOutlineTreeListItem(PdfOutline outline, int id, int parentId)
        {
            this.outline = outline;
            this.id = id;
            this.parentId = parentId;
            if (outline != null)
            {
                this.title = outline.Title;
                this.italic = outline.IsItalic;
                this.bold = outline.IsBold;
                this.foreColor = outline.Color;
                this.hasChildNodes = outline.First != null;
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        internal PdfOutline Outline =>
            this.outline;

        internal PdfInteractiveOperation InteractiveOperation
        {
            get
            {
                if (this.interactiveOperation == null)
                {
                    PdfDestination destination = this.outline.Destination;
                    this.interactiveOperation = new PdfInteractiveOperation((destination == null) ? this.outline.Action : null, destination);
                }
                return this.interactiveOperation;
            }
        }

        public int Id =>
            this.id;

        public int ParentId =>
            this.parentId;

        public string Title =>
            this.title;

        public bool Italic =>
            this.italic;

        public bool Bold =>
            this.bold;

        public PdfColor ForeColor =>
            this.foreColor;

        public bool HasChildNodes =>
            this.hasChildNodes;

        public bool UseForeColor
        {
            get => 
                this.useForeColor;
            set
            {
                if (this.useForeColor != value)
                {
                    this.useForeColor = value;
                    this.RaisePropertyChanged("UseForeColor");
                }
            }
        }

        public bool Expanded
        {
            get => 
                !this.outline.Closed;
            set
            {
                value = !value;
                if (this.outline.Closed != value)
                {
                    this.outline.Closed = value;
                    this.RaisePropertyChanged("Expanded");
                }
            }
        }
    }
}

