namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class RenderRealTextBlockContext : RenderControlBaseContext
    {
        private string text;
        private string highlightedText;
        private DevExpress.Xpf.Editors.HighlightedTextCriteria criteria;

        public RenderRealTextBlockContext(RenderRealTextBlock factory);

        private System.Windows.Controls.TextBlock TextBlock { get; }

        public string Text { get; set; }

        public string HighlightedText { get; set; }

        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria { get; set; }

        public System.Windows.TextWrapping? TextWrapping { get; set; }

        public System.Windows.TextTrimming? TextTrimming { get; set; }

        public System.Windows.TextAlignment? TextAlignment { get; set; }

        public TextDecorationCollection TextDecorations { get; set; }
    }
}

