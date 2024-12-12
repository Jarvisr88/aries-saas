namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Markup;

    public class PageLayoutSettings : MarkupExtension
    {
        public PageLayoutSettings() : this(DevExpress.Xpf.DocumentViewer.PageDisplayMode.Single)
        {
        }

        public PageLayoutSettings(DevExpress.Xpf.DocumentViewer.PageDisplayMode mode) : this(mode, (mode == DevExpress.Xpf.DocumentViewer.PageDisplayMode.Columns) ? 2 : 1)
        {
        }

        public PageLayoutSettings(DevExpress.Xpf.DocumentViewer.PageDisplayMode mode, int columnCount)
        {
            this.PageDisplayMode = mode;
            this.ColumnCount = columnCount;
        }

        public override bool Equals(object obj)
        {
            PageLayoutSettings settings = obj as PageLayoutSettings;
            return ((settings != null) && ((this.PageDisplayMode == settings.PageDisplayMode) && (this.ColumnCount == settings.ColumnCount)));
        }

        public override int GetHashCode() => 
            this.PageDisplayMode.GetHashCode() ^ this.ColumnCount.GetHashCode();

        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        public DevExpress.Xpf.DocumentViewer.PageDisplayMode PageDisplayMode { get; set; }

        public int ColumnCount { get; set; }
    }
}

