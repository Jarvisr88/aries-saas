namespace DevExpress.Xpf.Editors
{
    using System;

    public interface ISupportTextHighlighting
    {
        void UpdateHighlightedText(string highlightedText, DevExpress.Xpf.Editors.HighlightedTextCriteria criteria);

        string HighlightedText { get; }

        DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria { get; }
    }
}

