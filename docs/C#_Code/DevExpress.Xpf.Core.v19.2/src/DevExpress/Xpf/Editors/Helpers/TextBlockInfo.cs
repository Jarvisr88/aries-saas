namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Runtime.CompilerServices;

    public class TextBlockInfo
    {
        public override bool Equals(object obj)
        {
            TextBlockInfo objA = obj as TextBlockInfo;
            return (!Equals(objA, null) ? (Equals(this.Text, objA.Text) && (Equals(this.HighlightedText, objA.HighlightedText) && (Equals(this.HighlightedTextCriteria, objA.HighlightedTextCriteria) && Equals(this.Appearance, objA.Appearance)))) : false);
        }

        public override int GetHashCode() => 
            ((this.GetHashCode(this.Text) ^ this.GetHashCode(this.HighlightedText)) ^ this.HighlightedTextCriteria.GetHashCode()) ^ this.GetHashCode(this.Appearance);

        private int GetHashCode(object value) => 
            (value == null) ? 0 : value.GetHashCode();

        public static bool operator ==(TextBlockInfo info1, TextBlockInfo info2) => 
            Equals(info1, info2);

        public static bool operator !=(TextBlockInfo info1, TextBlockInfo info2) => 
            !Equals(info1, info2);

        public string Text { get; set; }

        public string HighlightedText { get; set; }

        public DevExpress.Xpf.Editors.HighlightedTextCriteria HighlightedTextCriteria { get; set; }

        public IHighlighterAppearance Appearance { get; set; }
    }
}

