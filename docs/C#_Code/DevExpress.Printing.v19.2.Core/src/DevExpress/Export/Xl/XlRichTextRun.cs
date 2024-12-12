namespace DevExpress.Export.Xl
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class XlRichTextRun
    {
        private string text;

        public XlRichTextRun(string text, XlFont font)
        {
            this.Text = text;
            this.Font = font;
        }

        public override bool Equals(object obj)
        {
            XlRichTextRun run = obj as XlRichTextRun;
            return ((run != null) ? ((this.Text == run.Text) ? ((this.Font == null) ? ReferenceEquals(run.Font, null) : this.Font.Equals(run.Font)) : false) : false);
        }

        public override int GetHashCode()
        {
            int hashCode = this.Text.GetHashCode();
            if (this.Font != null)
            {
                hashCode ^= this.Font.GetHashCode();
            }
            return hashCode;
        }

        public string Text
        {
            get => 
                this.text;
            set
            {
                Guard.ArgumentIsNotNullOrEmpty(value, "Text");
                this.text = value;
            }
        }

        public XlFont Font { get; set; }

        internal int FontIndex { get; set; }
    }
}

