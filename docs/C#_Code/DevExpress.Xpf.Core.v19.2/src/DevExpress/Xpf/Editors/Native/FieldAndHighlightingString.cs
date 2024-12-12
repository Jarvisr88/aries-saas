namespace DevExpress.Xpf.Editors.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class FieldAndHighlightingString
    {
        public FieldAndHighlightingString(string field, string highlightingString = "")
        {
            this.Field = field;
            this.HighlightingString = highlightingString;
        }

        public void AddHighlightingString(string stringToAdd)
        {
            this.HighlightingString = this.HighlightingString + (string.IsNullOrEmpty(this.HighlightingString) ? stringToAdd : ("\n" + stringToAdd));
        }

        public string Field { get; private set; }

        public string HighlightingString { get; private set; }
    }
}

