namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class TextHighlightingProperties
    {
        public TextHighlightingProperties(string text, DevExpress.Data.Filtering.FilterCondition filterCondition)
        {
            this.Text = text;
            this.FilterCondition = filterCondition;
        }

        public string Text { get; private set; }

        public DevExpress.Data.Filtering.FilterCondition FilterCondition { get; private set; }
    }
}

