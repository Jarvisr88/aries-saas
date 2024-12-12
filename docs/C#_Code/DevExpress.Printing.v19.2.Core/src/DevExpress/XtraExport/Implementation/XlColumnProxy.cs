namespace DevExpress.XtraExport.Implementation
{
    using DevExpress.Export.Xl;
    using System;

    internal class XlColumnProxy : IXlColumn, IDisposable
    {
        private IXlExport exporter;
        private readonly IXlColumn subject;

        public XlColumnProxy(IXlExport exporter, IXlColumn subject)
        {
            this.exporter = exporter;
            this.subject = subject;
        }

        public void ApplyFormatting(XlCellFormatting formatting)
        {
            this.subject.ApplyFormatting(formatting);
        }

        public void Dispose()
        {
            if (this.exporter != null)
            {
                this.exporter.EndColumn();
                this.exporter = null;
            }
        }

        public int ColumnIndex =>
            this.subject.ColumnIndex;

        public XlCellFormatting Formatting
        {
            get => 
                this.subject.Formatting;
            set => 
                this.subject.Formatting = value;
        }

        public bool IsHidden
        {
            get => 
                this.subject.IsHidden;
            set => 
                this.subject.IsHidden = value;
        }

        public bool IsCollapsed
        {
            get => 
                this.subject.IsCollapsed;
            set => 
                this.subject.IsCollapsed = value;
        }

        public int WidthInPixels
        {
            get => 
                this.subject.WidthInPixels;
            set => 
                this.subject.WidthInPixels = value;
        }

        public float WidthInCharacters
        {
            get => 
                this.subject.WidthInCharacters;
            set => 
                this.subject.WidthInCharacters = value;
        }
    }
}

